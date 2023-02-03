using Microsoft.AspNetCore.Identity;

namespace EkspertizaWebApi.Models;

public class TokenResult 
{
    public required SignInResult SignInResult { get; set; }
    public string? Token { get; set; }
}