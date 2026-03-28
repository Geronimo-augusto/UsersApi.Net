using Microsoft.AspNetCore.Authorization;

namespace UsersApi.Authorization
{
    public struct IdadeMinima : IAuthorizationRequirement
    {
        public IdadeMinima(int idade)
        {
            Idade = idade;
        }
        public int Idade { get; set; }


    }
}
