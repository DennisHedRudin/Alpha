document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150

    initDarkModeToggle();
    initializeDropdowns();
    getAntiForgeryToken();

    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal)
                modal.style.display = 'flex';
        })
    })

    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal)
                modal.style.display = 'none'

            modal.querySelectorAll('form').forEach(form => {
                form.reset()

                const imagePreview = form.querySelector('.image-preview')
                if (imagePreview)
                    imagePreview.src = ''

                const imagePreviewer = form.querySelector('.image-previewer')
                if (imagePreviewer)
                    imagePreviewer.classList.remove('selected')
            })

        })
    })


    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0]
            if (file)
                processImage(file, imagePreview, previewer, previewSize)

        })
    })

    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            if (!form.classList.contains('ajax-form')) return;

            e.preventDefault()


            clearErrorMessages(form)

            const formData = new FormData(form)
            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData
                })

                if (res.ok) {
                    const modal = form.closest('.modal')
                    if (modal)
                        modal.style.display = 'none';

                    window.location.reload()
                }
                else if (res.status === 400) {
                    const data = await res.json()

                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {

                            let input = form.querySelector(`[name=${key}]`)
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
                            if (span) {
                                span.innerText = data.errors[key].join('\n');
                                span.classList.add('field-validation-error')

                            }


                        });
                    }
                }
            }
            catch {
                console.log('error submitting the form')
            }

        })
    })

    document.querySelectorAll('.delete-project-btn').forEach(button => {
        button.addEventListener('click', async (e) => {
            e.preventDefault();
            e.stopPropagation();

            const projectId = button.getAttribute('data-project-id');
            if (!projectId) return;

            const confirmed = confirm("Are you sure you want to delete this project?");
            if (!confirmed) return;

            try {
                const res = await fetch(`/Projects/DeleteProject`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': getAntiForgeryToken()
                    },
                    body: JSON.stringify({ id: projectId })
                });

                if (res.ok) {
                    // reload or remove element from DOM
                    window.location.reload();
                } else {
                    const error = await res.text();
                    alert("Failed to delete project: " + error);
                }
            } catch (err) {
                console.error("Error deleting project", err);
                alert("Something went wrong.");
            }
        });


    });

    document.querySelectorAll('.edit-project-btn').forEach(button => {
        button.addEventListener('click', async (e) => {
            e.preventDefault();
            e.stopPropagation();

            const projectId = button.getAttribute('data-project-id');
            if (!projectId) return;

            try {
                const res = await fetch(`/Projects/Edit?id=${projectId}`);
                if (!res.ok) throw new Error("Failed to load edit form");

                const html = await res.text();

                const modal = document.querySelector('#editProjectModal');
                const modalContent = document.querySelector('#editProjectModalContent');

                modalContent.innerHTML = html;
                modal.style.display = 'flex';

                // rebind close buttons inside loaded content
                modal.querySelectorAll('[data-close="true"]').forEach(btn => {
                    btn.addEventListener('click', () => {
                        modal.style.display = 'none';
                    });
                });

            } catch (err) {
                console.error("Error loading edit form:", err);
                alert("Could not load edit form.");
            }
        });
    })

    document.querySelectorAll('.edit-project-btn').forEach(btn => {
        btn.addEventListener('click', e => {
            e.preventDefault();
            const target = btn.getAttribute('data-target');
            const modal = document.querySelector(target);
            if (modal) modal.style.display = 'flex';
        });
    });

});


    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }

    function closeAllDropdowns(exceptDropdown, dropdownElements) {
        dropdownElements.forEach(dropdown => {
            if (dropdown != exceptDropdown) {
                dropdown.classList.remove('show')
            }
        })
    }

    function initializeDropdowns() {
        const dropdownTriggers = document.querySelectorAll('[data-type="dropdown"]')
        const dropdownElements = new Set()

        dropdownTriggers.forEach(trigger => {
            const targetSelector = trigger.getAttribute('data-target')
            if (targetSelector) {
                const dropdown = document.getElementById(targetSelector.replace('#', ''))
                if (dropdown) {
                    dropdownElements.add(dropdown)
                }
            }
        })

        dropdownTriggers.forEach(trigger => {
            trigger.addEventListener('click', (e) => {
                e.stopPropagation()
                const targetSelector = trigger.getAttribute('data-target')
                if (!targetSelector) return
                const dropdown = document.getElementById(targetSelector.replace('#', ''))
                if (!dropdown) return

                closeAllDropdowns(dropdown, dropdownElements)
                dropdown.classList.toggle('show')
            })

        })

        dropdownElements.forEach(dropdown => {
            dropdown.addEventListener('click', (e) => {
                e.stopPropagation()
            })
        })

        document.addEventListener('click', () => {
            closeAllDropdowns(null, dropdownElements)
        })
    }

    function initDarkModeToggle() {
        const darkModeSwitch = document.querySelector('#darkModeSwitch');
        if (!darkModeSwitch) return;


        if (localStorage.getItem('theme') === 'dark') {
            document.documentElement.setAttribute('data-theme', 'dark');
            darkModeSwitch.checked = true;
        }

        darkModeSwitch.addEventListener('change', () => {
            if (darkModeSwitch.checked) {
                document.documentElement.setAttribute('data-theme', 'dark');
                localStorage.setItem('theme', 'dark');
            } else {
                document.documentElement.setAttribute('data-theme', 'light');
                localStorage.setItem('theme', 'light');
            }
        });
    }

    function clearErrorMessages(form) {
        form.querySelectorAll('[data-val="true"]').forEach(input => {
            input.classList.remove('input-validation-error')
        })

        form.querySelectorAll('[data-valmsg-for]').forEach(span => {
            span.innerText = ''
            span.classList.remove('field-validation-error')
        })
    }

    function addErrorMessage(key, errorMessage) {
        let input = form.querySelector(`[name=${key}]`)
        if (input) {
            input.classList.add('input-validation-error')
        }

        let span = form.querySelector(`[data-valmsg-for="${key}"]`)
        if (span) {
            span.innerText = errorMessage
            span.classList.add('field-validation-error')

        }
    }


    async function loadImage(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader()

            reader.onerror = () => reject(new Error("Failed to load file."))
            reader.onload = (e) => {
                const img = new Image()
                img.onerror = () => reject(new Error("Failed to load image."))
                img.onload = () => resolve(img)
                img.src = e.target.result
            }

            reader.readAsDataURL(file)
        })
    }

    async function processImage(file, imagePreview, previewer, previewSize = 150) {
        try {
            const img = await loadImage(file)
            const canvas = document.createElement('canvas')
            canvas.width = previewSize
            canvas.height = previewSize

            const ctx = canvas.getContext('2d')
            ctx.drawImage(img, 0, 0, previewSize, previewSize)
            imagePreview.src = canvas.toDataURL('image/jpeg')
            previewer.classList.add('selected')
        }
        catch (error) {
            console.error('Failed on image-processing', error)
        }
    } 