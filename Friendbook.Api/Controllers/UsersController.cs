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
    private readonly IUserService _userService;
    private readonly IFollowersService _followersService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IFollowersService followersService, IMapper mapper)
    {
        _userService = userService;
        _followersService = followersService;
        _mapper = mapper;
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

    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetList(LimitsDto dto)
    {
        IEnumerable<UserProfile> result = _userService.GetList(dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);
        
        return mappedResult;
    }

    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFollowers(GetRelationsDto dto)
    {
        UserProfile userProfile = _userService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFollowers(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFollowings(GetRelationsDto dto)
    {
        UserProfile userProfile = _userService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFollowings(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFriends(GetRelationsDto dto)
    {
        UserProfile userProfile = _userService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFriends(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
}
