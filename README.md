<h1 align="center">
    API Catálogo em .NET
</h1>

<img src="https://img.shields.io/static/v1?label=Tipo&message=Estudo&color=00ff08&labelColor=000000" alt="Estudo" />
</p>

Pequeno projeto onde estudo um pouco dos conceitos elementares e fundamentais das construções de Web Api's usando arquitetura REST com .NET, além do ORM EntityFramework (EF).

## Tecnologias
 
- [.NET](https://dotnet.microsoft.com/pt-br/)
- [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
- [MySQL](https://www.mysql.com/)

## Práticas adotadas

- API REST;
- Injeção de Dependências;
- Tratamento de respostas de erro;
- Geração automática do Swagger com a OpenAPI;

## Como Executar

- Clonar repositório git;
- Configurar o banco de dados que você está utilizando no arquivo appsettings.json (recomendo utilizar o MySQL, pois a configuração do AppDbContext nos services do builder da classe Programs foi realizada em cima do contexto do MySQL);
- Aplicar os migrations;
- Utilizar o Swagger ou algum outro recurso que ajude-o a fazer as consultas das rotas;

## API Endpoints

Para fazer as requisições HTTP, foi utilizada a ferramenta [Postman](https://www.postman.com/).
