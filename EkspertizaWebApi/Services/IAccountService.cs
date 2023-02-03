using EkspertizaWebApi.Models;
using EkspertizaWebApiData;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EkspertizaWebApi.Services;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    Task<TokenResult> TokenAsync(TokenViewModel model);

}

public class AccountService : IAccountService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IConfiguration configuration;

    public AccountService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration
        )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            UserName = model.UserName,
            Email = model.UserName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, model.Password!);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Members");
        }
        return result;

    }

    public async Task<TokenResult> TokenAsync(TokenViewModel model)
    {
        var result = new TokenResult { SignInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, true) };

        if (result.SignInResult.Succeeded)
        {
            var user = (await userManager.FindByNameAsync(model.UserName))!;
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);
        }
        return result;
    }
}