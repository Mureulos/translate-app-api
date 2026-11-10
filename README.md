# 🌐 Translate App API

API backend para tradução de textos e arquivos, construída com **ASP.NET Core (.NET 9)**, *Clean Architecture* e *CQRS*. As traduções são geradas localmente por um modelo de IA servido pelo **Ollama**, sem depender de serviços de tradução externos.

## 📑 Sumário

- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Executando com Docker Compose](#-executando-com-docker-compose-recomendado)
- [Executando localmente](#-executando-localmente-sem-docker)
- [Configuração](#-configuração)
- [Uso da API](#-uso-da-api)
- [Documentação](#-documentação)
- [Licença](#-licença)

## ✨ Funcionalidades

- **Autenticação e autorização** com JWT (JSON Web Tokens) e senhas protegidas com BCrypt.
- **Tradução de texto** via API, usando um modelo de IA local (Ollama).
- **Tradução de arquivos**: o texto é extraído (incluindo OCR com Tesseract) e traduzido para o idioma de destino.
- **Cache de traduções** com Redis (TTL configurável).
- **Histórico do usuário**: salvar, listar e excluir traduções.
- **Idiomas suportados** consultáveis pela API e populados via script SQL inicial.

## 💻 Tecnologias

| Categoria | Tecnologia |
|---|---|
| Linguagem / Runtime | C# · .NET 9 |
| Framework web | ASP.NET Core Web API |
| Padrões | Clean Architecture · CQRS · Mediator ([MediatR](https://github.com/jbogard/MediatR)) |
| Banco de dados | PostgreSQL 16 · Entity Framework Core 9 (Npgsql) |
| Cache | Redis (`Microsoft.Extensions.Caching.StackExchangeRedis`) |
| IA / Tradução | [Ollama](https://ollama.com) via [OllamaSharp](https://github.com/awaescher/OllamaSharp) (modelo padrão: `translategemma:4b`) |
| OCR | Tesseract |
| Segurança | JWT Bearer · BCrypt.Net-Next |
| Infraestrutura | Docker · Docker Compose |

## 🏗 Arquitetura

```
translate-app-api/             → Camada de apresentação (Controllers, configuração, Program.cs)
translate-app.application/     → Casos de uso (Commands/Queries, handlers MediatR)
translate-app.domain/          → Entidades e regras de domínio
translate-app.Infrastructure/  → EF Core, repositórios, migrations, serviços (IA, cache)
init-db/                       → Scripts SQL de carga inicial (idiomas)
```

## 🐳 Executando com Docker Compose (recomendado)

### Pré-requisitos

- [Docker](https://docs.docker.com/get-docker/) e [Docker Compose](https://docs.docker.com/compose/install/) (v2)
- Cerca de 5 GB livres em disco para as imagens e o modelo de IA

### Passo a passo

1. Clone o repositório:
   ```bash
   git clone https://github.com/Mureulos/translate-app-api.git
   cd translate-app-api
   ```
2. Suba todos os serviços (API, PostgreSQL, Redis e Ollama):
   ```bash
   docker compose up -d --build
   ```
3. Baixe o modelo de tradução no container do Ollama (necessário apenas na primeira vez; fica salvo no volume `ollama_data`):
   ```bash
   docker exec translate-app-ollama ollama pull translategemma:4b
   ```
4. Acompanhe os logs, se desejar:
   ```bash
   docker compose logs -f translate-app.api
   ```

### Serviços e portas

| Serviço | Container | Porta no host |
|---|---|---|
| API | `translate-app.api` | `8080` |
| PostgreSQL | `translate-app-postgres` | `5433` |
| Redis | `translate-app-redis` | `6379` |
| Ollama | `translate-app-ollama` | `11434` |

A API ficará disponível em `http://localhost:8080`.

### Comandos úteis

```bash
docker compose ps              # status dos serviços
docker compose down            # para os serviços (mantém os dados)
docker compose down -v         # para os serviços e apaga os volumes (banco, cache e modelos)
docker compose up -d --build   # reconstrói a imagem da API após alterações no código
```

> ⚠️ **Atenção:** as credenciais e a chave JWT definidas no `docker-compose.yaml` são apenas para desenvolvimento. Troque-as em qualquer outro ambiente.

## 🖥 Executando localmente (sem Docker)

### Pré-requisitos

- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- PostgreSQL, Redis e Ollama em execução (você pode subir só as dependências com `docker compose up -d postgres redis ollama`)
- Modelo baixado: `ollama pull translategemma:4b`

### Passo a passo

1. Configure `translate-app-api/appsettings.Development.json` (ou *user secrets*) com a connection string, o Redis, o Ollama e os dados do JWT. Veja [Configuração](#-configuração).
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Aplique as migrações:
   ```bash
   dotnet ef database update --project translate-app.Infrastructure --startup-project translate-app-api
   ```
4. Execute a API:
   ```bash
   dotnet run --project translate-app-api
   ```

## ⚙️ Configuração

As opções podem ser definidas no `appsettings.json` ou por variáveis de ambiente (use `__` como separador, ex.: `Jwt__Key`).

| Chave | Descrição | Exemplo |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | Conexão com o PostgreSQL | `Host=postgres;Port=5432;Database=translate_app_db;Username=admin;Password=admin` |
| `ConnectionStrings:Redis` | Endereço do Redis | `redis:6379` |
| `Ollama:Url` | URL do servidor Ollama | `http://ollama:11434` |
| `Ollama:Model` | Modelo usado nas traduções | `translategemma:4b` |
| `Jwt:Key` | Chave secreta (mín. 32 caracteres) | — |
| `Jwt:Issuer` / `Jwt:Audience` | Emissor e audiência do token | — |
| `TranslationCache:TtlSeconds` | Tempo de vida do cache, em segundos | `3600` |

## 🚀 Uso da API

- Com Docker Compose, a API responde em `http://localhost:8080`.
- Os endpoints estão organizados nos controllers `Auth`, `User`, `Language` e `Translation`.
- O arquivo `translate-app-api/translate-app.Api.http` contém exemplos de requisições.
- Fluxo básico: crie um usuário, faça login para obter o *Bearer Token* e envie-o no header `Authorization: Bearer <token>` para acessar as rotas protegidas.

## 📚 Documentação

- [ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core)
- [CQRS e MediatR](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Ollama](https://github.com/ollama/ollama)

## 📄 Licença

Projeto destinado a uso educacional e não comercial, distribuído sob a licença **MIT**.
