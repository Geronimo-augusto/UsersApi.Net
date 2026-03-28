using Microsoft.AspNetCore.Identity;

namespace UsersApi.Model;

public class User : IdentityUser
{
    // usando IdentityUser, ja funcionaria para muita coisa. Mas se tivermos uma classe nossa propria
    // Podemos colocar propriendades exclusivas nossas. como:
    public DateTime DataNascimento { get; set; }

    public User() : base() { }
}
