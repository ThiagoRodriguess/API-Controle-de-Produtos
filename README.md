# API de Controle de Produtos

Esta API permite gerenciar informações de produtos.

## Pré-requisitos

- [Visual Studio](https://visualstudio.microsoft.com/) instalado na máquina.
- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado na máquina.

## Dependências do Projeto

A API faz uso das seguintes dependências para seu funcionamento:

- [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next/4.0.3): Biblioteca para criptografia de senhas.
- [Dapper](https://www.nuget.org/packages/Dapper/2.0.151): Micro ORM que facilita o acesso ao banco de dados.
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/6.0.21): Biblioteca para autenticação JWT.
- [Microsoft.Data.Sqlite.Core](https://www.nuget.org/packages/Microsoft.Data.Sqlite.Core/7.0.10): Pacote SQLite para .NET Core.
- [Microsoft.IdentityModel.JsonWebTokens](https://www.nuget.org/packages/Microsoft.IdentityModel.JsonWebTokens/6.32.2): Biblioteca para manipulação de tokens JWT.
- [Microsoft.IdentityModel.Tokens](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/6.32.2): Biblioteca para manipulação de tokens de autenticação.
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.5.0): Biblioteca para geração de documentação Swagger.
- [Swashbuckle.AspNetCore.Filters](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Filters/7.0.8): Adiciona filtros Swagger para melhorar a documentação.
- [System.Data.SqlClient](https://www.nuget.org/packages/System.Data.SqlClient/4.8.5): Driver SQL Server para .NET.
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/6.32.2): Biblioteca para manipulação de tokens JWT.

Certifique-se de verificar a versão específica de cada pacote conforme mencionado nas dependências acima.

## Configuração do Banco de Dados

Antes de executar a API, você precisará configurar o banco de dados. Siga as etapas abaixo para criar o banco de dados usando o Microsoft SQL Server:

1. **Instale o SQL Server Express:** Se você ainda não tem o SQL Server instalado, você pode baixar e instalar o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

2. **Abra o SQL Server Management Studio (SSMS):** Inicie o SQL Server Management Studio, que é a ferramenta de gerenciamento de banco de dados para o SQL Server.

3. **Conecte-se ao Servidor:** No SSMS, conecte-se ao servidor SQL Server onde deseja criar o banco de dados. Insira suas credenciais de autenticação quando solicitado.

4. **Crie um Banco de Dados:** Clique com o botão direito em "Databases" na janela "Object Explorer" e selecione "New Database...".

5. **Defina um Nome para o Banco de Dados:** Insira um nome para o banco de dados no campo "Database name". Escolha "Estoque" como nome

6. **Crie uma Tabela:** Com o banco de dados criado, clique com o botão direito na pasta "Tables" e selecione "New Table...".

7. **Defina as Colunas:** Na janela de criação da tabela, defina as colunas necessárias para a tabela de produtos da seguinte maneira:

   ```sql
   CREATE TABLE Produto (
       Id INT IDENTITY(1,1) PRIMARY KEY,
       Name NVARCHAR(MAX) NOT NULL,
       Preco DOUBLE NOT NULL,
       Quantidade INT NOT NULL
   );
   
Agora, você pode usar a string de conexão no arquivo `appsettings.json` ou `appsettings.Development.json` para que a API possa se conectar a esse banco de dados:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS; Database=Estoque; Trusted_Connection=True;"
  }
}
```

## Como Executar o Projeto

1. Clone o repositório ou faça o download dos arquivos do projeto.

2. Abra o projeto no Visual Studio.

3. Verifique se o projeto de inicialização está configurado corretamente.

4. Compile o projeto para garantir que não há erros.

5. Pressione F5 ou clique no botão "Iniciar" na barra de ferramentas para executar o aplicativo.

6. A aplicação será iniciada e estará disponível no endereço especificado.

7. Para acessar a documentação Swagger e testar os endpoints, abra um navegador e acesse a URL fornecida.

## Documentação Swagger

Para acessar a documentação Swagger e testar os endpoints da sua API, siga estas etapas:

1. Abra um navegador da web.

2. Acesse o seguinte URL:

    ```
    https://localhost:{PORTA}/swagger/index.html
    ```

   Lembre-se de substituir `{PORTA}` pela porta configurada na execução da API.

3. Você será direcionado para a interface da documentação Swagger. Nela, você encontrará uma lista completa de endpoints disponíveis na sua API.

4. Expanda um endpoint para ver detalhes como os parâmetros que ele aceita e exemplos de requisições.

5. Utilize a própria interface para preencher os parâmetros e fazer chamadas de teste diretamente do navegador.

A documentação Swagger é uma ferramenta valiosa para explorar e testar sua API de maneira interativa. Aproveite essa funcionalidade para entender melhor o funcionamento da sua API e garantir que ela está atendendo às suas necessidades.

## Autenticação e Autorização

A API oferece funcionalidades de autenticação e autorização, permitindo que os usuários se registrem e façam login para acessar recursos protegidos. Abaixo está uma visão geral da `AuthController`, que gerencia esses processos:

### Registrar Novo Usuário

- **Método:** POST
- **URL:** `/api/auth/register`
- **Descrição:** Registra um novo usuário na aplicação.
- **Corpo da Requisição:**
  ```json
  {
    "username": "novousuario",
    "password": "senhadonovousuario"
  }
   ```
- **Resposta de Exemplo:**
  ```json
  {
  "username": "usuarioregistrado",
  "passwordHash": "$2a$11$Rz6jJYqeDQLmf44KcEDRzux4smJmOHyWKkALzkFWqwzD3PuUVi4JO"
  }
  ```
  
### Login de Usuário

- **Método:** POST
- **URL:** `/api/auth/login`
- **Descrição:** Permite que um usuário registrado faça login na aplicação.
- **Corpo da Requisição:**
  ```json
  {
    "username": "usuarioregistrado",
    "password": "senhadousuarioregistrado"
  }
  ```
- **Resposta de Exemplo:**
  ```json
   eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c3VhcmlvcmVnaXN0YWRvIiwianRpIjoiNWY1NTNlYjUtOWE2Yi00ZmYxLTg5N2UtOGQxOWZlMmRiYjA1IiwiaWF0IjoxNjI5MDA3NTA3LCJleHAiOjE2MjkwOTM1MDd9.TLXsyWeug5a8lsflv15NXq0yomM0MvWmZQp3BE24vHs

  ```

## Uso da API de Controle de Estoque

Antes de utilizar a API de Controle de Estoque, é necessário autenticar-se na API de Login para obter um token JWT válido. O token JWT é usado como forma de autenticação para acessar os recursos protegidos da API de Controle de Estoque. Siga as etapas abaixo para começar:

1. Inicie fazendo uma requisição de login à API de Login usando o endpoint `/api/auth/login`. Forneça um nome de usuário e senha válidos no corpo da requisição. A API de Login retornará um token JWT se as credenciais forem corretas.

2. Com o token JWT obtido, você pode incluí-lo no cabeçalho `Authorization` de todas as suas requisições para a API de Controle de Estoque. O token será usado para verificar sua identidade e conceder acesso aos recursos protegidos.

   **Importante:** Copie o token JWT obtido.

3. Acesse a documentação Swagger da API de Controle de Estoque em um navegador. A URL deve ser `https://localhost:{PORTA}/swagger/index.html`, substituindo `{PORTA}` pela porta configurada na execução da API.

4. No canto superior direito da página Swagger, clique no botão "Authorize".

5. Na janela de autorização, cole o token JWT que você copiou na etapa 2 no campo "Value". Certifique-se de incluir a palavra "bearer" seguida por um espaço antes de colar o token. Por exemplo, se o seu token JWT for "seu_token_aqui", você deve colar "bearer seu_token_aqui".

6. Clique em "Authorize" para aplicar o token.

7. Agora você pode usar os endpoints da API de Controle de Estoque através da interface Swagger, e o Swagger automaticamente incluirá o token JWT no cabeçalho das requisições, autenticando você para acessar os recursos protegidos.

**Nota:** Mantenha seu token JWT em segredo e nunca o compartilhe publicamente. O token é uma forma de autenticação que concede acesso aos recursos da API em seu nome.


## Controle de estoque

A API possui os seguintes endpoints que podem ser acessados para realizar diversas operações:

### Listar todos os produtos

- **Método:** GET
- **URL:** `/api/produto`
- **Descrição:** Retorna uma lista de todos os produtos cadastrados.
- **Resposta de Exemplo:**
  ```json
  [
    {
      "id": 1,
      "name": "Produto A",
      "preco": 10.99,
      "quantidade": 50
    },
    {
      "id": 2,
      "name": "Produto B",
      "preco": 15.49,
      "quantidade": 30
    }
  ]
  ```
  
### Buscar Produto por ID

- **Método:** GET
- **URL:** `/api/produto/{ProdutoId}`
- **Descrição:** Retorna os detalhes de um produto específico com base no seu ID.
- **Parâmetros:**
  - `{ProdutoId}`: O ID do produto desejado.
- **Resposta de Exemplo:**
  ```json
  {
    "id": 1,
    "name": "Produto A",
    "preco": 10.99,
    "quantidade": 50
  }
  ```
  
### Pesquisar Produtos por Nome

- **Método:** GET
- **URL:** `/api/produto/searchByName/{ProdutoName}`
- **Descrição:** Retorna uma lista de produtos que correspondem ao nome de pesquisa fornecido.
- **Parâmetros:**
  - `{ProdutoName}`: O nome de pesquisa dos produtos desejado.
- **Resposta de Exemplo:**
  ```json
  [
    {
      "id": 6,
      "nome": "Produto Pesquisado A",
      "preco": 9.99,
      "quantidade": 15
    }
  ]
  
### Buscar Produtos por Preço

- **Método:** GET
- **URL:** `/api/produto/searchByPrice/{Price}`
- **Descrição:** Retorna uma lista de produtos com o preço especificado.
- **Parâmetros:**
  - `{Price}`: O preço dos produtos a serem buscados.
- **Resposta de Exemplo:**
  ```json
  [
    {
      "id": 1,
      "nome": "Produto A",
      "preco": 10.99,
      "quantidade": 50
    },
    {
      "id": 3,
      "nome": "Produto C",
      "preco": 10.99,
      "quantidade": 30
    }
  ]

### Buscar Produtos por Quantidade

- **Método:** GET
- **URL:** `/api/produto/searchByQuantity/{Quantity}`
- **Descrição:** Retorna uma lista de produtos com a quantidade especificada.
- **Parâmetros:**
  - `{Quantity}`: A quantidade dos produtos a serem buscados.
- **Resposta de Exemplo:**
  ```json
  [
    {
      "id": 2,
      "nome": "Produto B",
      "preco": 15.99,
      "quantidade": 40
    },
    {
      "id": 4,
      "nome": "Produto D",
      "preco": 8.99,
      "quantidade": 40
    }
  ]

### Adicionar Novo Produto

- **Método:** POST
- **URL:** `/api/produto`
- **Descrição:** Adiciona um novo produto à lista.
- **Corpo da Requisição:**
  ```json
  {
    "name": "Novo Produto",
    "preco": 25.99,
    "quantidade": 20
  }
   ```
- **Resposta de Exemplo:**
  ```json
  {
    "id": 3,
    "name": "Novo Produto",
    "preco": 25.99,
    "quantidade": 20
  }
  ```
  
 ### Atualizar Produto Existente

- **Método:** PUT
- **URL:** `/api/produto/{ProdutoId}`
- **Descrição:** Atualiza os detalhes de um produto existente.
- **Parâmetros:**
  - `{ProdutoId}`: O ID do produto a ser atualizado.
- **Corpo da Requisição:**
  ```json
  {
    "name": "Produto Atualizado",
    "preco": 19.99,
    "quantidade": 40
  }
  ```
  - **Resposta de Exemplo:**
  ```json
  {
    "id" : 3
    "name": "Produto Atualizado",
    "preco": 19.99,
    "quantidade": 40
  }
  ```

 ### Excluir Produto

- **Método:** DELETE
- **URL:** `/api/produto/{ProdutoId}`
- **Descrição:** Exclui um produto específico com base no seu ID.
- **Parâmetros:**
  - `{ProdutoId}`: O ID do produto a ser excluído.
- **Resposta de Exemplo:**
  ```json
  {
    "id": 1,
    "name": "Produto A que sobrou",
    "preco": 10.99,
    "quantidade": 50
  }
  ```
