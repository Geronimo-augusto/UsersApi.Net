# UsersApi.Net

API de cadastro e login com ASP.NET Identity, JWT e uma policy de idade mínima lida das claims do token.

Exercício de autenticação/autorização em .NET — não é um produto.

## O que a API faz

- Cadastro e login de usuário (Identity)
- JWT com id, nome, data de nascimento e instante de emissão
- Handler de autorização que bloqueia menor de 18 anos
- Swagger para exercitar as rotas localmente

## Stack

ASP.NET Core · Identity · EF Core · MySQL · JWT · AutoMapper · Swagger

## Como rodar

```bash
git clone https://github.com/Geronimo-augusto/UsersApi.Net.git
cd UsersApi.Net
dotnet restore
# ajuste a connection string do MySQL
dotnet ef database update
dotnet run
```

Swagger em `https://localhost:5001/swagger`.

## Rotas

| Método | Rota | Auth |
|---|---|---|
| POST | `/User/cadastro` | público |
| POST | `/User/login` | público (devolve JWT, expira em 10 min) |
| GET | `/User/all` | protegido |
| DELETE | `/User/delete` | protegido |
| GET | `/Acess` | policy `IdadeMinima` |

```
Authorization: Bearer <token>
```

## Estrutura

```
Controllers/     UserController, AcessController
Services/        UserService, TokenService
Authorization/   IdadeAuthorization, IdadeMinima
Data/            UserDbContext, DTOs
Model/           User
Profiles/        AutoMapper
```

## Próximos passos (já listados no repo — bons para entrevista)

- Refresh token
- Paginação em `/User/all`
- Roles (Admin / User)
- Docker
- Serilog
