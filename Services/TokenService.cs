using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        //Iniciando um manipulador de tokens Jwt
        var tokenHandler = new JwtSecurityTokenHandler();
        //Passando a chave em formato de array de bytes
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        //Ira conter todas as informacoes do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.Sha512)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}