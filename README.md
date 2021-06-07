# Assert-Variation
API construída para processar as variações do ativo PETR4.SA nos últimos 30 dias.

## Tecnologias:
### Net5;
### EntityFrameworkCore;
### SqlServer.

## Informações:
#### Setar conexão no arquivo.appsettings.json(*SqlConnectionString*)
#### Gerar Banco de Dados via Migration(*Add-Migration InitialCreate,Update-Database*)
#### Executar api via Swagger
##### EndPoint => /api/v1/Asset/processAsset (*para processar os dados*)
##### EndPoint => /api/v1/Asset/variation (*para exibir a variação*)



