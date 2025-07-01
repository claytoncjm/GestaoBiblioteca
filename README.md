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

## Testes Unitários

Os testes unitários estão localizados na pasta `Tests` e são executados usando o xUnit. Eles garantem que:
- As regras de negócio estão sendo aplicadas corretamente
- Os métodos dos serviços estão funcionando conforme esperado
- As validações de domínio estão sendo respeitadas

Para executar os testes:
```bash
dotnet test
```

## Camada Services

A camada Services é responsável por:
- Implementar as regras de negócio da aplicação
- Orquestrar as operações entre os repositórios
- Validar as operações antes de persistir os dados
- Gerenciar transações e consistência dos dados

Os serviços funcionam como uma camada intermediária entre os controllers e os repositórios, garantindo que as operações sejam realizadas de forma segura e consistente.

## Documentação

A API possui documentação Swagger disponível em:
`http://localhost:5000/swagger`

A documentação inclui:
- Descrição detalhada de cada endpoint
- Parâmetros necessários
- Exemplos de requisições e respostas
- Códigos de status HTTP esperados

Os summaries foram adicionados nos controllers e métodos para melhorar a documentação.
