# FraudSys

## Descrição

FraudSys é um sistema para monitoramento e prevenção de fraudes em transações Pix.  
Desenvolvido em .NET 8 com Clean Code, SOLID e testes unitários, utiliza AWS DynamoDB para armazenamento escalável e seguro.  

---

## Configuração

### Substituindo o arquivo de configuração - Credenciais AWS DynamoDB

O arquivo `appsettings.json` incluído no repositório contém apenas valores de exemplo para as credenciais AWS. Para rodar o projeto com acesso ao banco de dados, você deve substituir esse arquivo pelo arquivo `appsettings.Development.json` que contém as credenciais reais e que foi enviado separadamente por email.

Para isso:

1. Na pasta raiz do projeto, localize o arquivo `appsettings.json`.
2. Faça um backup desse arquivo, caso queira preservar os valores de exemplo.
3. Copie o arquivo `appsettings.json` recebido por email para a pasta raiz do projeto.
4. Renomeie o arquivo copiado para `appsettings.json`, substituindo o arquivo de exemplo.

Após essa substituição, ao executar o projeto, ele utilizará as credenciais corretas para acessar o DynamoDB.

Exemplo de configuração do arquivo `appsettings.json`:

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