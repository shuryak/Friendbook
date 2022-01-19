using AutoMapper;
using Friendbook.Api.Helpers;
using Friendbook.Api.Models;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("api/[controller].[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly IFollowersService _followersService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<UserProfile> _passwordHasher;
        
    public UsersController(IUserProfileService userProfileService, IFollowersService followersService, IConfiguration configuration, IMapper mapper)
    {
        _userProfileService = userProfileService;
        _followersService = followersService;
        _configuration = configuration;
        _mapper = mapper;
        _passwordHasher = new PasswordHasher<UserProfile>();
    }

    [HttpPost]
    public ActionResult<AuthenticateUserResponseDto> Login(AuthenticateUserRequestDto dto)
    {
        UserProfile? userProfile = _userProfileService.GetByNickname(dto.Nickname);

        if (_passwordHasher.VerifyHashedPassword(userProfile, userProfile.PasswordHash, dto.Password) 
            != PasswordVerificationResult.Success)
        {
            return BadRequest("Incorrect nickname or password");
        }

        string token = _configuration.GenerateJwtToken(userProfile);

        AuthenticateUserResponseDto responseDto = _mapper.Map<AuthenticateUserResponseDto>(userProfile);
        responseDto.Token = token;

        return responseDto;
    }
    
    [HttpPost]
    public ActionResult<bool> Register(CreateUserProfileDto dto)
    {
        UserProfile? userProfile = _mapper.Map<UserProfile>(dto);
        userProfile.PasswordHash = _passwordHasher.HashPassword(userProfile, dto.Password);
        
        return _userProfileService.Create(userProfile);
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
        IEnumerable<UserProfile> result = _userProfileService.GetList(dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);
        
        return mappedResult;
    }

    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFollowers(GetRelationsDto dto)
    {
        UserProfile userProfile = _userProfileService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFollowers(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFollowings(GetRelationsDto dto)
    {
        UserProfile userProfile = _userProfileService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFollowings(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
    
    [HttpPost]
    public ActionResult<List<ShowUserProfileDto>> GetFriends(GetRelationsDto dto)
    {
        UserProfile userProfile = _userProfileService.GetByNickname(dto.Nickname);

        IEnumerable<UserProfile> result = _followersService.GetFriends(userProfile.Id, dto.Offset, dto.Limit);

        List<ShowUserProfileDto> mappedResult = _mapper.Map<List<ShowUserProfileDto>>(result);

        return mappedResult;
    }
}
