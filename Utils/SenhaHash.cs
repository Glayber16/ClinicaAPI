using BCrypt.Net;
namespace SenhaHash.Utils;
public static class SenhaHash
{
    
    public static string HashPassword(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    
    public static bool VerificarSenha(string senha, string SenhaHash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
    }
}
