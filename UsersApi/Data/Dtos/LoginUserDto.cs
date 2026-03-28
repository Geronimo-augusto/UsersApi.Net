using System.ComponentModel.DataAnnotations;

namespace UsersApi.Data.Dtos;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required] 
    public string Password { get; set; }

}
