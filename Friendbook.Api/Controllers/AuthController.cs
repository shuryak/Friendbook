using AutoMapper;
using Friendbook.Api.Helpers;
using Friendbook.Api.Models;
using Friendbook.Api.Models.Users;
using Friendbook.Domain.Models;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("auth/[action]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IUserSessionService _userSessionService;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<User> _passwordHasher;
        
    public AuthController(IConfiguration configuration, IUserService userService, IUserSessionService userSessionService, IMapper mapper)
    {
        _configuration = configuration;
        _userService = userService;
        _userSessionService = userSessionService;
        _mapper = mapper;
        _passwordHasher = new PasswordHasher<User>();
    }
        
    [HttpPost]
    public ActionResult<bool> Register(CreateUserDto dto)
    {
        User? userProfile = _mapper.Map<User>(dto);
        userProfile.PasswordHash = _passwordHasher.HashPassword(userProfile, dto.Password);
        
        return _userService.Create(userProfile);
    }
    
    [HttpPost]
    public ActionResult<AuthenticateUserResponseDto> Login(AuthenticateUserRequestDto dto)
    {
        User? user = _userService.GetByNickname(dto.Nickname);

        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) 
            != PasswordVerificationResult.Success)
        {
            return BadRequest("Incorrect nickname or password");
        }

        UserSession userSession = _userSessionService.Create(user, TimeSpan.FromDays(7));
        
        string accessToken = _configuration.GenerateJwtToken(user);

        return new AuthenticateUserResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = userSession.RefreshToken,
            RefreshTokenExpiresAt = userSession.ExpiresAt
        };
    }

    [HttpPost]
    public ActionResult<AuthenticateUserResponseDto> RefreshTokenPair(RefreshTokenPairRequestDto dto)
    {
        UserSession? userSession = _userSessionService.Renew(dto.RefreshToken, TimeSpan.FromDays(7));

        if (userSession == null)
        {
            return BadRequest("Refresh token expired or invalid");
        }
        
        User? user = _userService.GetById(userSession.UserId);

        if (user == null)
        {
            return BadRequest("User does not exists");
        }
        
        string accessToken = _configuration.GenerateJwtToken(user);
        
        return new AuthenticateUserResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = userSession.RefreshToken,
            RefreshTokenExpiresAt = userSession.ExpiresAt
        };
    }
}
