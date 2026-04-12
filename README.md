# 🌐 Translate App API

## 📖 Descrição do Projeto

O **Translate App API** é uma API backend robusta e escalável projetada para facilitar a tradução de textos e arquivos. Construída com base nos princípios de *Clean Architecture* e no padrão *CQRS* (Command Query Responsibility Segregation), a aplicação permite que usuários autenticados realizem traduções, além de salvar, listar e gerenciar o próprio histórico de tradução.

## ✨ Funcionalidades do Projeto

- **Autenticação e Autorização**: Sistema de login baseado em JWT (JSON Web Tokens).
- **Tradução Direta de Texto**: Tradução de pequenos ou grandes fragmentos de texto via API.
- **Extração e Tradução de Arquivos**: Suporte para envio de arquivos de texto, onde o conteúdo é extraído e traduzido automaticamente para o idioma de destino.
- **Gerenciamento de Histórico**:
    - Salvar traduções vinculadas à conta do usuário.
    - Listar traduções salvas.
    - Excluir traduções do histórico (Soft/Hard delete).

## 💻 Tecnologias e Linguagens

- C#
- .NET (Core / 8+)
- SQL (Banco de dados relacional via Entity Framework)

## 🛠 Bibliotecas e Frameworks

- **ASP.NET Core / Web API**: Base do roteamento e hospedagem da aplicação.
- **MediatR**: Implementação do padrão Mediator para suporte estrutural ao CQRS, promovendo o baixo acoplamento.
- **Entity Framework Core**: ORM principal para interações e migrações de banco de dados.
- **System.IdentityModel.Tokens.Jwt**: Gerenciamento e autorização via JWT.

## ⚙️ Pré-requisitos e Instalação

As seguintes ferramentas são necessárias para executar o projeto:
- [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o projeto, recomendada versão 8.0+)
- Um banco de dados suportado pelo EF Core (ex: SQL Server, PostgreSQL ou SQLite) configurado no arquivo `appsettings.json`.
- IDE recomendada: Visual Studio, JetBrains Rider ou VS Code.

**Passo a passo da instalação:**

1. Clone o repositório:
   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd translate-app-api
   ```
2. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   ```
3. Aplique as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update --project translate-app.Infrastructure --startup-project translate-app.Api
   ```

## 🚀 Instruções de Uso

Para executar a aplicação em ambiente de desenvolvimento:
```bash
dotnet run --project translate-app.Api
```

- A API estará disponível normalmente em `https://localhost:5001` ou `http://localhost:5000`.
- O ambiente expõe o **Swagger** para testes das rotas facilmente pelo navegador: `https://localhost:<port>/swagger`.
- **Dica**: Utilize o endpoint de Autenticação/Login para receber o *Bearer Token*, e insira-o no campo de autorização do Swagger para testar as rotas protegidas em `TranslationController`.

## 📚 Documentação

- [Documentação Oficial do ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core)
- [Padrão CQRS e MediatR](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)

## 📄 Licença

Este projeto é destinado a uso educacional e não comercial, sendo distribuído sob a licença **MIT**.