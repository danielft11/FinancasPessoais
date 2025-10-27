📊 # Finanças Pessoais API

**Introdução**: Finanças Pessoais API é uma API RESTful desenvolvida em .NET 7, seguindo os princípios do Clean Architecture, para o gerenciamento de finanças pessoais como lançamento de receitas e despesas por categorias
e subcategorias, gerenciamento de contas à pagar, lembrete de contas à pagar, etc.

Além disso, uso este projeto para praticar meus conhecimentos em back-end e transformá-los em solução para uma necessidade que eu e outras pessoas possam ter. 

🏛 **Arquitetura**

O projeto segue o padrão Clean Architecture, que garante:

Separação de responsabilidades por camada:

- API: camada de apresentação e endpoints REST

- Application: serviços, casos de uso e lógica de aplicação

- Domain: entidades, regras de negócio e interfaces

- Infra.Data: implementação de persistência, repositórios e integração externa

- Infra.IoC: camada de Injeção de Dependência e inversão de controle

- Tests: testes unitários e de integração. Serão desenvolvidos futuramente.

⚡ **Funcionalidades**

- CRUD de contas bancárias

- CRUD de categorias e subcategorias de receitas e de despesas

- Lançamento de Receitas e Despesas por categorias e subcategorias

- Cadastro de Contas a Pagar com lembrete

- Cadastro de usuários e recuperação de senha (fluxo em desenvolvimento)

🛠 **Pré-requisitos**

- .NET 7 SDK

- SQL Server ou container Docker (opcional, para rodar via docker-compose)

💻 **Como executar**

1. Clonar o repositório:
   git clone https://github.com/danielft11/FinancasPessoais.git
   cd FinancasPessoais

2. Rodar via .NET CLI:
   cd src/FinancasPessoais.API
   dotnet restore
   dotnet build
   dotnet run

O projeto será iniciado em https://localhost:5001 por padrão.

3. Rodar via Docker Compose (opcional)
   docker-compose up -d

🧰 **Estrutura do projeto**

FinancasPessoais/
│
├─ src/
│   ├─ FinancasPessoais.API            # Camada de apresentação (endpoints)
│   ├─ FinancasPessoais.Application    # Serviços e casos de uso
│   ├─ FinancasPessoais.Domain         # Entidades e regras de negócio
│   ├─ FinancasPessoais.Infra.Data     # Persistência
│   └─ FinancasPessoais.Infra.IoC      # Injeção de Dependência e inversão de controle
│
├─ docker-compose.yml                  # Configuração de containers
└─ README.md

📌 **Contato**

- GitHub: danielft11
- E-mail: danielft11@hotmail.com


