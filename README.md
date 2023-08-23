# API de Controle de Produtos

Esta API permite gerenciar informações de produtos.

## Pré-requisitos

- [Visual Studio Code](https://code.visualstudio.com/) instalado na máquina.
- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado na máquina.

## Dependências

Antes de executar a API, certifique-se de ter as seguintes dependências instaladas:

- **Dapper:** Micro-ORM para acesso a dados. Versão 2.0.151.
- **Microsoft.Data.Sqlite.Core:** Cliente SQLite para o SQLite. Versão 7.0.10. Pode ser útil para desenvolvimento local.
- **Swashbuckle.AspNetCore:** Biblioteca para geração de documentação Swagger. Versão 6.5.0.
- **System.Data.SqlClient:** Cliente SQL para o SQL Server. Versão 4.8.5.

## Configuração do Banco de Dados

Antes de executar a API, você precisará configurar o banco de dados. Siga as etapas abaixo para criar o banco de dados usando o Microsoft SQL Server:

1. **Instale o SQL Server Express:** Se você ainda não tem o SQL Server instalado, você pode baixar e instalar o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

2. **Abra o SQL Server Management Studio (SSMS):** Inicie o SQL Server Management Studio, que é a ferramenta de gerenciamento de banco de dados para o SQL Server.

3. **Conecte-se ao Servidor:** No SSMS, conecte-se ao servidor SQL Server onde deseja criar o banco de dados. Insira suas credenciais de autenticação quando solicitado.

4. **Crie um Banco de Dados:** Clique com o botão direito em "Databases" na janela "Object Explorer" e selecione "New Database...".

5. **Defina um Nome para o Banco de Dados:** Insira um nome para o banco de dados no campo "Database name". Escolha "Estoque" como nome

6. **Crie uma Tabela:** Com o banco de dados criado, clique com o botão direito na pasta "Tables" e selecione "New Table...".

7. **Defina as Colunas:** Na janela de criação da tabela, defina as colunas necessárias para a tabela de produtos. Por exemplo:

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

1. **Clonar ou Baixar:** Clone o repositório ou faça o download dos arquivos do projeto.

2. **Abrir no Visual Studio Code:** Abra o diretório do projeto no Visual Studio Code.

3. **Abrir Terminal:** No menu do VS Code, vá para "Terminal" > "Novo Terminal" para abrir um terminal integrado.

4. **Compilar o Projeto:** Execute o seguinte comando para compilar o projeto:

   ```bash
   dotnet build

## Iniciar a API

Execute o seguinte comando para iniciar a API:

```bash
dotnet run
```

## Documentação Swagger

Para acessar a documentação Swagger e testar os endpoints da sua API, siga estas etapas:

1. Certifique-se de que a API está em execução. Use o seguinte comando para iniciar a API:

    ```bash
    dotnet run
    ```

2. Abra um navegador da web.

3. Acesse o seguinte URL:

    ```
    https://localhost:{PORTA}/swagger/index.html
    ```

   Lembre-se de substituir `{PORTA}` pela porta configurada na execução da API.

4. Você será direcionado para a interface da documentação Swagger. Nela, você encontrará uma lista completa de endpoints disponíveis na sua API.

5. Expanda um endpoint para ver detalhes como os parâmetros que ele aceita e exemplos de requisições.

6. Utilize a própria interface para preencher os parâmetros e fazer chamadas de teste diretamente do navegador.

A documentação Swagger é uma ferramenta valiosa para explorar e testar sua API de maneira interativa. Aproveite essa funcionalidade para entender melhor o funcionamento da sua API e garantir que ela está atendendo às suas necessidades.

## Endpoints da API

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
