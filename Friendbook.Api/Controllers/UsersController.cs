using AutoMapper;
using Friendbook.Api.Models;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("api/[controller].[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly IFollowersService _followersService;
    private readonly IMapper _mapper;
        
    public UsersController(IUserProfileService userProfileService, IFollowersService followersService, IMapper mapper)
    {
        _userProfileService = userProfileService;
        _followersService = followersService;
        _mapper = mapper;
    }
        
    [HttpPost]
    public ActionResult<bool> Register(CreateUserProfileDto dto)
    {
        return _userProfileService.Create(_mapper.Map<UserProfile>(dto));
    }

    [HttpPost]
    public ActionResult<bool> Follow(FollowDto dto)
    {
        return _followersService.Follow(dto.FollowerId, dto.FollowingId);
    }
    
    [HttpPost]
    public ActionResult<bool> Unfollow(FollowDto dto)
    {
        return _followersService.Unfollow(dto.FollowerId, dto.FollowingId);
    }
}
