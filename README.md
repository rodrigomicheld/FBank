![image](https://github.com/rodrigomicheld/FBank/assets/45425275/08ca4fd9-d119-40cc-ab0d-36da99909ca1)
# **Passo a passo para executar a API**
### **Subir banco de dados**
Para subir o banco de dados Sql Server do FBank, você poderá executar o arquivo docker no diretório:
* **FBank/devops/docker/docker-compose.yaml**
## **Operações disponibilizadas:**
### **Cadastrar Conta**
Acessar o endpoint POST Client passando:
* Nome;
* Documento (Documento tem que ser um cpf válido);
* Senha
![client](https://github.com/rodrigomicheld/FBank/assets/45425275/18b49cf2-c009-42b0-8863-0299729d9d1b)
### **Logar na conta**
Acessar o endpoint GET Login passando: 
* Agencia = 1 (A agência sempre será essa);
* Conta;
* Senha;
![login](https://github.com/rodrigomicheld/FBank/assets/45425275/f532751b-2d70-43e3-a8b0-9f3d19b6cdf8)
Para realizar transações e consultas é necessário está logado em alguma conta com token válido, caso contrário o cliente não terá autorização de acesso.
### **Realizar depósito**
Acessar o endpoint POST DepositAccount passando:
* Valor
![deposit](https://github.com/rodrigomicheld/FBank/assets/45425275/f2853ea7-ffcb-4ccd-9218-1edea8061106)
Nesse endpoint só poderá colocar saldo na conta logada nesse exemplo estou logado na conta 3 (Maria dos Santos). 
### **Realizar transferência**
Acessar o endpoint POST Transfer passando:
* Conta de destino
* Valor
![transfer](https://github.com/rodrigomicheld/FBank/assets/45425275/69669ad6-33b6-4643-9862-11f31906023c)
Como a transferência só poderá ser feita de contas da mesma instituição e a agência padrão é igual a código 1 não é necessário informar a agência.
### **Realizar saque**
Acessar o endpoint POST WithDraw passando:
* Valor
![withDraw](https://github.com/rodrigomicheld/FBank/assets/45425275/2ef88e96-a67b-4c91-bcab-c79f7b8900bd) 
Saque será realizado na conta logada.
### **Listar informações do cliente**
Acessar o endpoint GET Client passando:
* Documento
![getClient](https://github.com/rodrigomicheld/FBank/assets/45425275/01eacebf-cb86-42a6-b4a7-f72aa34f383b)
Nesse endpoint é possível visualizar dados do cliente e da conta.
### **Exibir extrato das movimentações**
#### Filtros
**1. LISTAR SEM FILTRO**
* Não precisa preencher nenhum campo só executar, por padrão ele vai retornar a primeira página com 10 movimentações, caso haja mais de 10 transações terá que informar a próxima página para exibir os dados.
  
**2. LISTAR POR DATA**
* Data inicial e Data Final tem ser no formato [AAAA-mm-DD], onde Data inicial não poderá ser maior que a data final e data final não poderá ser menor que a data incial. Caso não seja preenchido irá retornar todas as transações do cliente logado.
  
**3. LISTAR POR FLUXO** 
* 1 para movimentações de entrada;
* 2 para movimentações de saída;

Caso não seja preenchido irá retornar todas as transações.
![extract](https://github.com/rodrigomicheld/FBank/assets/45425275/59ecaade-a88d-4220-9344-b6b36f418d63)
#### Ordenação
É possível ordenar o resultado do extrato pelos seguintes campos:
* dateTransaction
* amount

 
	
 
