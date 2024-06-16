# Agenda

Este repositório contém uma aplicação completa para gerenciar uma agenda de contatos e tarefas, composta por três principais projetos:

- **Agenda-UI**: Interface do usuário desenvolvida em Vue.js.
- **WebAPI**: API RESTful desenvolvida em C# .NET 7.
- **TestAgenda**: Projeto de testes unitários utilizando XUnit.

## Requisitos

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas em seu ambiente:

- [Node.js](https://nodejs.org/) (versão 14 ou superior)
- [Vue CLI](https://cli.vuejs.org/)
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recomendado) ou qualquer IDE de sua escolha para desenvolvimento C#
- [XUnit](https://xunit.net/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Projetos da Solução

Agenda-UI: Contém o projeto Vue.js para a interface do usuário.

WebAPI: Contém o projeto da API RESTful desenvolvida em C# .NET 7.

TestAgenda: Contém o projeto de testes unitários utilizando XUnit.

Infrastructure: Contém classes de acesso ao banco de dados, migrations e validators.

## Principais Features e Tecnologias
- Entity Framevork;
- Entity Framevork Migrations;
- Banco de dados SQL Server;
- Autenticação JWT;
- Swagger;
- Testes unitários e integração com XUnit;
- FluentValidation;
- Dependency Injection;
- Componentes Vue.js;
- RESTFul web API.
  
## Instalação

## Clonando o Repositório

Clone este repositório em sua máquina local usando o comando:

```
git clone https://github.com/G10van1/Agenda.git
```
## Configuração do Backend (WebAPI)

Navegue até o diretório do projeto WebAPI:

```
cd ../WebAPI
```
Restaure as dependências do projeto:

```
dotnet restore
```
## Configuração do Frontend (Agenda-UI)

Navegue até o diretório do projeto Agenda-UI:

```
cd Agenda-UI
```
Instale as dependências do projeto:

```
npm install
```
## Configuraçâo do Banco de Dados SQL Server

Navegue até o diretório do projeto Infrastructure:

```
cd ../Infrastructure
```
Certifique-se de que o serviço do SQL Server está ativo, pode ser usado o Microsoft SQL Server Management Studio.
Monte o banco de dados através do Entity Framework Migrations:

```
dotnet-ef database update
```
Se preferir pode importar o script do banco localizado na pasta Database.

## Execução

## Executando a API (WebAPI)

Navegue até o diretório do projeto WebAPI:

```
cd ../WebAPI
```

Inicie a API:

```
dotnet run
```

Outra opção é executar através do Visual Studio.

A API estará disponível no endereço http://localhost:5223 ou https://localhost:7266.

É possível usar a interface do Swagger para testar os endpoints através do link:
http://localhost:5223/swagger/index.html ou
https://localhost:7266/swagger/index.html

## Executando a Aplicação Web (Agenda-UI)

Navegue até o diretório do projeto Agenda-UI:

```
cd Agenda-UI
```

Inicie o servidor de desenvolvimento:

```
npm run serve
```

Acesse a aplicação no seu navegador através do endereço http://localhost:8080.

Quando clicar qualquer um dos ítens se ainda não está logado, deverá aparecer a tela de login. Para logar usar as seguintes credenciais:

Usuário: admin
Senha: admin

## Executando os Testes (TestAgenda)

Navegue até o diretório do projeto TestAgenda:

```
cd ../TestAgenda
```
Execute os testes:
```
dotnet test
```
Se preferir pode usar o gerenciador de testes do Visual Studio.

O projeto de testes está configurado para URL base "https://localhost:7266".

Se o projeto WebAPI estiver rodando em outra URL, será necessário alterar a configuração da URL base no construtor da classe de teste (UnitTestAgenda), conforme mostrado no código abaixo:

```
_httpClient.BaseAddress = new Uri("https://localhost:7266"); // Configurar para a URL do projeto WebAPI
```


