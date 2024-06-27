using AutoMapper;
using HepsiAPI.Application.Features.Auths.Rules;
using HepsiAPI.Application.Services.TokenHelper;
using HepsiAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HepsiAPI.Application.Features.Auths.Commands.Register
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AuthsRules _authsRules;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(ITokenHelper tokenHelper, UserManager<AppUser> userManager, IMapper mapper, AuthsRules authsRules)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
            _mapper = mapper;
            _authsRules = authsRules;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {

            await _authsRules.UserShouldNotBeExistsAsync(request.Email);

            AppUser registeredUser = _mapper.Map<AppUser>(request);

            IdentityResult isUserCreated = await _userManager.CreateAsync(registeredUser, request.Password);

            await _authsRules.CheckIfUserCreatedSuccessfullyAsync(isUserCreated);

            await _authsRules.EnsureRoleExistsAsync("user");

            IdentityResult isRoleAssigned = await _userManager.AddToRoleAsync(registeredUser, "user");

            await _authsRules.CheckIfAssignRoleToUserAsync(isRoleAssigned);

            var roles = await _userManager.GetRolesAsync(registeredUser);

            var token = _tokenHelper.CreateToken(registeredUser, roles);
            var refreshToken = _tokenHelper.CreateRefreshToken();


            registeredUser.RefreshToken = refreshToken.Token;
            registeredUser.RefreshTokenExpiration = refreshToken.Expiration;
            await _userManager.UpdateAsync(registeredUser);

            var response = new RegisterCommandResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };


            return response;
        }
    }

    public class RegisterCommandResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}
