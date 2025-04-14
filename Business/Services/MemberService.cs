
using Domain.Extensions;
using Business.Models;
using Data.Repositories;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Data.Entities;
using Domain.Models;
using System.Diagnostics;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager, RoleManager<IdentityRole> roleManager) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager; 

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();

        return result.MapTo<MemberResult>();
    }

   
    public async Task<MemberResult> AddMemberToRole(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new MemberResult { Success = false, StatusCode = 404, Error = "Role doesn't exist." };

        var member = await _userManager.FindByIdAsync(userId);
        if (member == null)
            return new MemberResult { Success = false, StatusCode = 404, Error = "Member doesn't exist." };

        var result = await _userManager.AddToRoleAsync(member, roleName);
            return result.Succeeded
            ? new MemberResult { Success = true, StatusCode = 200 }
            : new MemberResult { Success = false, StatusCode = 500, Error = "Unable to add member to role." };
        
    }

    public async Task<MemberResult> CreateMemberAsync(MemberSignUpModel form, string roleName = "Member")
    {
        if (form == null)
            return new MemberResult { Success = false, StatusCode = 400, Error = "Form data can't be null." };

        var existsResult = await _memberRepository.ExistAsync(x => x.Email == form.Email);
        if (existsResult.Success )
            return new MemberResult { Success = false, StatusCode = 409, Error = "Member already exists." };

        try
        {
            var memberEntity = form.MapTo<MemberEntity>();

            var result = await _userManager.CreateAsync(memberEntity, form.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await AddMemberToRole(memberEntity.Id, roleName);
                return result.Succeeded
                   ? new MemberResult { Success = true, StatusCode = 201 }
                   : new MemberResult { Success = false, StatusCode = 201, Error = "Member created but not added to role." };
            }

           return new MemberResult { Success = false, StatusCode = 500, Error = "Unable to create member." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new MemberResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
