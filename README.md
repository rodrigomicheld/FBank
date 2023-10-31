![image](https://github.com/rodrigomicheld/FBank/assets/45425275/08ca4fd9-d119-40cc-ab0d-36da99909ca1)
# Passo a passo para executar A API
## **Subir banco de dados**
Para subir o banco de dados Sql Server do FBank, você poderá executar o arquivo docker no diretório **FBank/devops/docker/docker-compose.yaml**
## **Cadastrar Conta**
## **Logar na conta para poder realizar consultas e transações**
Acessar o endpoint Login passando: 
Agencia = 1
conta
senha
Se não inserir token válido o cliente não terá permissão para efetuar cunsultas e transações
![CriandoOrdemInicial](https://www.loom.com/share/4beb27c586fb4cfc81ef04c92eb5edfc?sid=c2f8b14b-354a-4710-8123-9bd473841494)

## **Realizar transferencia**
## **Realizar saque**
## **Realizar depósito**
## **Exibir extrato das movimentações**
### Filtros
* **LISTAR POR DATA** - Data inicial e Data Final, onde Data inicial não poderá ser maior que a data final e data final não poderá ser menor que a data incial. Caso não seja preenchido irá retornar todas as transações.
* **LISTAR POR FLUXO** - 0: para movimentações de entrada e 1: para movimentações de saída. caso não seja preenchido irá retornar todas as transações.	
	
 OBS: A paginação dessas transações está por padrão trazer 10 transações por página podendo ser modificada.
