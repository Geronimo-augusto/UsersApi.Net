# 🚀 Users API (.NET)

API REST desenvolvida em **.NET** para gerenciamento de usuários com autenticação via **JWT**, utilizando **ASP.NET Identity**, **Entity Framework Core** e **MySQL**.

O projeto implementa um sistema completo de:

* Cadastro e autenticação de usuários
* Geração de tokens JWT
* Controle de acesso baseado em políticas
* Validação de idade mínima via Authorization Handler

---

## 📌 Tecnologias Utilizadas

* .NET 6/7
* ASP.NET Core
* Entity Framework Core
* MySQL
* ASP.NET Identity
* JWT (JSON Web Token)
* AutoMapper
* Swagger

---

## 📂 Estrutura do Projeto

```
UsersApi/
│
├── Controllers/
│   ├── UserController.cs
│   └── AcessController.cs
│
├── Services/
│   ├── UserService.cs
│   └── TokenService.cs
│
├── Authorization/
│   ├── IdadeAuthorization.cs
│   └── IdadeMinima.cs
│
├── Data/
│   ├── UserDbContext.cs
│   └── Dtos/
│       ├── CreateUsuarioDto.cs
│       └── LoginUserDto.cs
│
├── Model/
│   └── User.cs
│
├── Profiles/
│   └── UserProfile.cs
│
└── Program.cs
```

---

## ⚙️ Configuração do Projeto

### 🔑 appsettings.json

Adicione as seguintes configurações:

```json
{
  "ConnectionStrings": {
    "UserConnection": "server=localhost;database=usersdb;user=root;password=senha"
  },
  "SymmetricSecurityKey": "SUA_CHAVE_SECRETA_AQUI"
}
```

---

## ▶️ Como Executar o Projeto

```bash
# Restaurar dependências
dotnet restore

# Aplicar migrations (caso necessário)
dotnet ef database update

# Rodar aplicação
dotnet run
```

A API estará disponível em:

```
https://localhost:5001
```

Swagger:

```
https://localhost:5001/swagger
```

---

## 🔐 Autenticação com JWT

Após login, a API retorna um **token JWT** que deve ser enviado nas requisições protegidas:

```
Authorization: Bearer SEU_TOKEN
```

O token contém:

* Username
* ID do usuário
* Data de nascimento
* Timestamp de login

---

## 👤 Endpoints

### 📌 Usuário

#### ➕ Cadastro

```
POST /User/cadastro
```

Body:

```json
{
  "userName": "usuario",
  "dataNascimento": "2000-01-01",
  "password": "123456",
  "rePassword": "123456"
}
```

---

#### 🔑 Login

```
POST /User/login
```

Body:

```json
{
  "userName": "usuario",
  "password": "123456"
}
```

Retorno:

```json
"token_jwt"
```

---

#### 📄 Listar usuários

```
GET /User/all
```

---

#### ❌ Deletar todos usuários

```
DELETE /User/delete
```

---

### 🔒 Acesso protegido

#### ✔️ Endpoint com restrição de idade

```
GET /Acess
```

Requisitos:

* Token JWT válido
* Usuário com **18 anos ou mais**

---

## 🧠 Regra de Negócio: Idade Mínima

O sistema utiliza uma **Policy personalizada**:

```csharp
options.AddPolicy("IdadeMinima", policy => 
    policy.AddRequirements(new IdadeMinima(18)));
```

### 🔍 Como funciona

* A data de nascimento é armazenada no token JWT
* Um `AuthorizationHandler` calcula a idade do usuário
* O acesso só é permitido se:

```
idade >= 18
```

---

## 🔄 Fluxo de Autenticação

1. Usuário se cadastra
2. Usuário faz login
3. API gera token JWT
4. Cliente envia token nas requisições
5. Middleware valida token
6. Policy verifica idade (quando necessário)

---

## 📌 Boas práticas aplicadas

* Separação por camadas (Controller, Service, Data)
* Injeção de dependência
* Uso de DTOs
* Uso de AutoMapper
* Segurança com JWT
* Authorization baseada em Policy
* Uso do Identity para gestão de usuários

---

## ⚠️ Observações

* O endpoint `/User/delete` remove **todos os usuários** (usar com cuidado)
* O token expira em **10 minutos**
* A chave JWT deve ser armazenada de forma segura (ex: variáveis de ambiente)

---

## 📈 Possíveis melhorias

* Refresh Token
* Paginação de usuários
* Logs estruturados (Serilog)
* Deploy com Docker
* Controle de roles (Admin/User)
* Validações mais robustas

---

## 👨‍💻 Autor
Nanachi
Projeto desenvolvido para fins de estudo e prática com autenticação e autorização em .NET.

---
