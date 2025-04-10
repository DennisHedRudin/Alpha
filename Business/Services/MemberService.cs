
using Domain.Extensions;
using Business.Models;
using Data.Repositories;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Data.Entities;
using Domain.Models;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager, RoleManager<IdentityRole> roleManager) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager; 

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAll();

        return result.MapTo<MemberResult>();
    }

    public async Task<MemberResult> AddMemberAsync()
    {
        var result = await _userManager.CreateAsync(Member);
    }
    public async Task<MemberResult> AddMemberToRole(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new MemberResult { Success = false, StatusCode = 404, Error = "Role doesn't exist." };

        var member = await _roleManager.FindByIdAsync(userId);
        if (member == null)
            return new MemberResult { Success = false, StatusCode = 404, Error = "Member doesn't exist." };

        var result = await _userManager.AddToRoleAsync(member, roleName)
    }
}
