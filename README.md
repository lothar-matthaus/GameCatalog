# GameCatalog
API básica desenvolvida em ASP.NET com o intuito de estudar e praticar CORS e JWT e seu uso em API's.

## Restrições
Esta API foi desenvolvida pra a disciplina **Desenvolvimento de Software Para Web**. Por mais que a mesma ainda possua validações básicas, pode ocorrer inconsistência dos dados caso ocorra algum problema decorrido pela falta de validações.
- Não possui persistência de dados em Base de Dados (SQLServer, PostgreSQL, MongoDB, etc).
- Não possui **criptografia** em *senhas de usuários*, ou seja, não mantém a ***confidencialidade*** dos dados do usuário cadastrado.
- Não possui políticas de restrição para o CORS, ou seja, qualquer header, origin ou credential pode se utilizar dos recursos da API.

## Features
- A API possui as funcionalidades básicas de **criação, leitura, atualização e remoção** de dados (**CRUD**).
- Autenticação por **BearerToken** (JWT).
- **Persistência** de dados por arquivos de leitura (arquivos de texto).
- CORS com políticas abertas para qualquer **Origem, Header ou Credencial**.

# Documentação

## Entidades

### Game (Jogo)
Propriedades:
| Nome da Propriedade | Tipo     | Descrição                        |
|---------------------|----------|----------------------------------|
| Id                  | int      | Identificador único              |
| Name                | string   | Título do Jogo                   |
| Description         | string   | Descrição do título              |
| ReleaseDate         | DateTime | Data de Lançamento do Jogo       |
| CreationDate        | DateTime | Data que o título foi cadastrado |
---
Json de geração:
~~~ json
{
  "name": "string",
  "description": "string",
  "categories": [
    "string"
  ],
  "releaseDate": "2021-12-12T18:12:45.465Z",
  "creationDate": "2021-12-12T18:12:45.465Z"
}
~~~
---
Json de atualização:
~~~json
{
  "id": 0,
  "name": "string",
  "description": "string",
  "categories": [
    "string"
  ],
  "releaseDate": "2021-12-12T18:12:45.465Z",
  "creationDate": "2021-12-12T18:12:45.465Z"
}
~~~
---
### User (Usuário)
Propriedades:
| Nome da Propriedade | Tipo   | Descrição                          |
|---------------------|--------|------------------------------------|
| Id                  | int    | Identificador único                |
| Name                | string | Nome do usuário                    |
| Email               | string | E-mail de identificação do usuário |
| Password            | string | Senha de acesso do usuário         |
---
Json de geração:
~~~json
{
  "name": "string",
  "email": "user@example.com",
  "password": "string"
}
~~~
---

# Funcionamento
O funcionamento da API é simples e direto. Há uma necessidade de se estar cadastrado no sistema para poder se utilizar dos métodos **POST, PATCH e DELETE**.
> Nota: Para fazer o cadastro de um usuário, deve-se utilizar o **Json de Geração** acima na rota *api/User* via método **POST**.

## Geração de Token
Para gerar um Token de acesso, você deve realizar o Login na rota *api/User/Login* via método **POST**. Se o login for realizado com sucesso, será retornado o token de acesso.
> Nota: Os método GET são todos públicos, em suma, não há necessidade de se estar logado para poder visualizar um título ou todos os títulos.

## Create, Update e Delete
Com o Token de acesso, você poderá criar, deletar e atualizar os jogos persistidos no sistema.

