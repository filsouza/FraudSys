# FraudSys

## Descrição

**FraudSys** é um sistema desenvolvido em **.NET 8** com foco em **monitoramento e prevenção de fraudes** em transações via Pix. O projeto aplica os princípios de **Clean Code**, **SOLID** e **testes unitários** para garantir qualidade de código, escalabilidade e manutenibilidade.

O armazenamento é feito de forma segura e escalável com uso do **AWS DynamoDB**.

---

## Tecnologias utilizadas

- .NET 8
- AWS DynamoDB
- xUnit & Moq (testes unitários)
- Clean Code / SOLID

---

## Configuração do projeto

O projeto inclui um arquivo `appsettings.json` com **valores de exemplo**. Para executar o sistema com conexão real ao banco de dados, será necessário substituir esse arquivo pelas **credenciais reais**, fornecidas separadamente.

### Passo a passo:

1. Acesse o diretório de credenciais enviado:
2. Copie o arquivo `appsettings.json` fornecido.
3. Na **pasta raiz** do projeto, localize o arquivo `appsettings.json` original.
4. Faça um backup, se desejar preservar os valores de exemplo.
5. Substitua o arquivo original pelo novo `appsettings.json`.

Após essa substituição, o sistema já estará apto a utilizar as credenciais corretas para acesso ao DynamoDB.

---

### Exemplo de estrutura do `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AWS": {
    "Region": "us-east-2",
    "TableName": "Clientes",
    "AccessKey": "ACCESS_KEY_AQUI",
    "SecretKey": "SECRET_KEY_AQUI"
  }
}

Testes

---

Se preferir algo ainda mais didático:

```md
## Testes

Este projeto possui testes unitários utilizando **xUnit** e **Moq**, localizados na solução `FraudSys.Tests`.

### Como executar os testes:

1. Abra o terminal na raiz da solução.
2. Execute o comando abaixo:

```bash
dotnet test

## Contato

Caso tenha dúvidas, sugestões ou deseje conversar sobre o projeto, fico à disposição.

[Meu LinkedIn](https://www.linkedin.com/in/felipe-alves-de-souza-santos-a65584142/)

