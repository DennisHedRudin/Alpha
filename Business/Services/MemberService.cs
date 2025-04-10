using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

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
