using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Business.Models.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        var entities = await _userManager.Users            
            .ToListAsync();

        var members = entities.Select(MemberFactory.Create);

        return members;
    }
}
