# Gestão Biblioteca API

API RESTful para gerenciamento de empréstimos de livros em uma biblioteca.

## Funcionalidades

- Cadastro de livros
- Empréstimo de livros
- Devolução de livros
- Listagem de livros e empréstimos

## Requisitos

- .NET 8.0 ou superior
- SQL Server
- Entity Framework Core

## Instalação

1. Clone o repositório:
```bash
git clone [url-do-repositorio]
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Crie a migração inicial:
```bash
dotnet ef migrations add InitialCreate
```

4. Atualize o banco de dados:
```bash
dotnet ef database update
```

## Configuração do Banco de Dados

O arquivo `appsettings.json` deve conter a string de conexão para o SQL Server:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GestaoBibliotecaDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Endpoints

### Livros

- POST `/api/livros` - Criar um novo livro
- GET `/api/livros/{id}` - Obter livro por ID
- GET `/api/livros` - Listar todos os livros

### Empréstimos

- POST `/api/emprestimos/{livroId}` - Solicitar empréstimo
- PUT `/api/emprestimos/devolver/{id}` - Devolver empréstimo
- GET `/api/emprestimos` - Listar todos os empréstimos

## Testes

Para executar os testes:
```bash
dotnet test
```

## Documentação

A API possui documentação Swagger disponível em:
`http://localhost:5000/swagger`
