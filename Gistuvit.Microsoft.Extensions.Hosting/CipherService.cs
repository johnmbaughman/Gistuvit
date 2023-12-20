using Microsoft.AspNetCore.DataProtection;

namespace Gistuvit.Microsoft.Extensions.Hosting;

public class CipherService(IDataProtectionProvider dataProtectionProvider)
{
    private const string Key = "dkC%UuLs1BK~)H#qz.-ha*fvz>GEDw8F_2n#Gaku:wHs8r:R!XJWX0LQC.]C+Y6Q#a585UzKceUb@ifo1^wh)tm?yb+GYy)r_~An";

    public string Encrypt(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var protector = dataProtectionProvider.CreateProtector(Key);
        return protector.Protect(input);
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return string.Empty;
        var protector = dataProtectionProvider.CreateProtector(Key);
        return protector.Unprotect(cipherText);
    }
}