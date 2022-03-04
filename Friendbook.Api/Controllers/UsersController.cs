using AutoMapper;
using Friendbook.Api.Helpers;
using Friendbook.Api.Models;
using Friendbook.Api.Models.Users;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Identity;
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
    public ActionResult<List<ShowUserDto>> GetList(LimitsDto dto)
    {
        IEnumerable<User> result = _userService.GetList(dto.Offset, dto.Limit);

        List<ShowUserDto> mappedResult = _mapper.Map<List<ShowUserDto>>(result);
        
        return mappedResult;
    }

    [HttpPost]
    public ActionResult<List<ShowUserDto>> GetFollowers(GetRelationsDto dto)
    {
        User user = _userService.GetByNickname(dto.Nickname);

        IEnumerable<User> result = _followersService.GetFollowers(user.Id, dto.Offset, dto.Limit);

        List<ShowUserDto> mappedResult = _mapper.Map<List<ShowUserDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserDto>> GetFollowings(GetRelationsDto dto)
    {
        User user = _userService.GetByNickname(dto.Nickname);

        IEnumerable<User> result = _followersService.GetFollowings(user.Id, dto.Offset, dto.Limit);

        List<ShowUserDto> mappedResult = _mapper.Map<List<ShowUserDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserDto>> GetFriends(GetRelationsDto dto)
    {
        User user = _userService.GetByNickname(dto.Nickname);

        IEnumerable<User> result = _followersService.GetFriends(user.Id, dto.Offset, dto.Limit);

        List<ShowUserDto> mappedResult = _mapper.Map<List<ShowUserDto>>(result);

        return mappedResult;
    }
}
