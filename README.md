# XPE - 2025-6A - Arq. Software

## API Produtos

### Estrutura de arquivos e diretórios:   
   
└── Diretório Raiz do projeto   
&nbsp;&nbsp;&nbsp;&nbsp; └── ProdutosApi   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └── Controllers # Controladores que gerenciam as requisições HTTP   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├── Models # Entidades de domínio (Produto)   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├── Repository # Repositórios para interação com o banco de dados   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├── Service # Serviços que implementam a lógica de negócios   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └── Program # Classe principal para iniciar a aplicação   


### Papel de cada estrutura:

#### Controllers
Função: Camada responsável por expor os endpoints da API.   
Responsabilidade:   
&nbsp; &nbsp; Receber requisições HTTP (GET, POST, PUT, DELETE).   
&nbsp; &nbsp; Validar dados de entrada.   
&nbsp; &nbsp; Acionar os serviços da camada Service.   
&nbsp; &nbsp; Retornar respostas HTTP adequadas (200, 201, 204, 400, 404, etc.).   

#### Models
Função: Representar as entidades de domínio ou estruturas de dados utilizadas pela aplicação.   
Responsabilidade:   
&nbsp; &nbsp; Definir classes que mapeiam objetos da vida real (ex: Produto, Cliente, Pedido).   
&nbsp; &nbsp; Servir como contrato de dados para entrada e saída de informações da API.   

#### Repository
Função: Camada de acesso a dados (Data Access Layer – DAL).   
Responsabilidade:   
&nbsp; &nbsp; Realizar a comunicação direta com o banco de dados.   
&nbsp; &nbsp; Isolar consultas SQL, LINQ ou operações de persistência.   
&nbsp;  &nbsp; Evitar que Services ou Controllers precisem conhecer detalhes de banco.   

#### Services
Função: Camada de lógica de negócio.    
Responsabilidade:   
&nbsp; &nbsp; Orquestrar as regras de negócio da aplicação.   
&nbsp; &nbsp; Usar os Repositories para persistência, mas sem expor detalhes de banco ao Controller.   
&nbsp; &nbsp; Aplicar validações, cálculos, regras de atualização e consistência de dados.   

### Relação entre as camadas
- Controller → recebe requisição HTTP e chama o Service.
- Service → aplica regras de negócio e delega persistência ao Repository.
- Repository → acessa o banco e retorna dados.
- Model → estrutura usada em todas as camadas para trafegar dados.

