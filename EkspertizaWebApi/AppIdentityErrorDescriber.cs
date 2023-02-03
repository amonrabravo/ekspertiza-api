using Microsoft.AspNetCore.Identity;

namespace EkspertizaWebApi;

public class AppIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = "DuplicateEmail", Description = $"{email} adresi zaten kayıtlıdır!" };
    }
}