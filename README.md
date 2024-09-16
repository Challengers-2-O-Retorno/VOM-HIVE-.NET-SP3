# VOM-HIVE

# Contextualização do problema
No cenário empresarial atual, a capacidade de compreender e antecipar as tendências
do mercado é crucial para o sucesso de qualquer empresa. Especificamente no campo do
marketing, onde a concorrência é intensa e as preferências dos consumidores mudam
rapidamente, a falta de insights atualizados pode resultar em estratégias desatualizadas e
perda de oportunidades de negócios.
As empresas estão constantemente buscando maneiras de se destacar em um mercado
saturado, e uma das maneiras mais eficazes de fazer isso é por meio da compreensão das
tendências e preferências do público-alvo. Isso inclui identificar as hashtags mais relevantes,
compreender os padrões de comportamento nas redes sociais, e entender as necessidades e
desejos dos consumidores.

Portanto, o desafio enfrentado pelas empresas é como aproveitar ao máximo a
tecnologia para impulsionar suas estratégias de marketing e, consequentemente, seu
crescimento empresarial. É essencial desenvolver ferramentas e plataformas que possam
facilitar a análise de dados e fornecer insights acionáveis de forma acessível e compreensível
para os profissionais de marketing.

Assim, o problema a ser resolvido é como criar uma solução que permita às empresas
explorar o potencial dos dados gerados nas interações entre empresa e cliente, compra e
atendimento para impulsionar suas estratégias de marketing e, por consequência, seu
crescimento no mercado. Isso envolve não apenas coletar e analisar dados, mas também
traduzir essas informações em ações tangíveis que melhorem o desempenho e a eficácia das
campanhas de marketing.

# Escopo
Nosso principal objetivo é desenvolver uma plataforma que simplifique o processo
de planejamento estratégico de marketing para empresas de todos os setores. Reconhecemos
a grande importância de compreender as tendências do mercado e as preferências dos
consumidores para impulsionar o crescimento empresarial.

Para alcançar esse objetivo, buscamos integrar tecnologias de inteligência artificial e
deep analytics para analisar grandes volumes de dados gerados nas interações da empresa e
cliente, compra e atendimento. Esses dados são uma fonte rica de insights que podem
orientar as estratégias de marketing das empresas.

Nossa solução visa oferecer uma experiência intuitiva e abrangente, permitindo que
as empresas cadastrem suas campanhas de marketing de forma eficiente e definam o públicoalvo com precisão. Além disso, forneceremos ferramentas para identificar hashtags
relevantes e compreender os padrões de comportamento dos consumidores nas redes sociais
e na internet em geral.

Acreditamos que a chave para o sucesso no marketing moderno está em fornecer
insights acionáveis que ajudem as empresas a tomar decisões informadas. Portanto, nosso
foco é não apenas coletar e analisar dados, mas também traduzir essas informações em
relatórios detalhados, gráficos e dashboards que sejam compreensíveis e úteis para os
profissionais de marketing.

Ao aprimorar o engajamento com o público-alvo e melhorar as taxas de conversão,
nossa solução visa impulsionar o crescimento das empresas. Acreditamos que ao fornecer às
empresas as ferramentas e os insights necessários para otimizar suas estratégias de
marketing, podemos ajudá-las a expandir sua base de clientes, aumentar suas vendas e
fortalecer sua posição no mercado competitivo de hoje.

# Público-alvo
O público-alvo principal que comprará a solução são empresas de diversos setores e
portes. Isso engloba desde startups e empresas de médio porte até grandes corporações. Essas
empresas estão interessadas em maximizar o impacto de suas campanhas de marketing,
aumentar seu alcance e engajamento nas redes sociais e, consequentemente, impulsionar seu
crescimento empresarial. Profissionais de marketing, gerentes de produto, executivos de
vendas e outros stakeholders envolvidos na estratégia de marketing das empresas seriam os
responsáveis por adquirir a solução.

Os usuários da solução seriam os profissionais de marketing e equipes de publicidade
das empresas que adquiriram a plataforma. Isso inclui gerentes de marketing digital,
analistas de mídia social, especialistas em SEO (Search Engine Optimization) e outros
profissionais responsáveis pela concepção, execução e análise das campanhas de marketing.

Eles utilizarão a plataforma para cadastrar campanhas, analisar dados, extrair insights e
monitorar o desempenho das campanhas em tempo real. Além disso, outras equipes dentro
da empresa, como vendas e atendimento ao cliente, podem se beneficiar dos insights gerados
pela solução para alinhar suas estratégias e atividades com as campanhas de marketing em
curso.

#
# Arquitetura da API 

A escolha pela arquitetura monolítica para o desenvolvimento da nossa API foi baseada em fatores estratégicos relacionados ao time, ao prazo do projeto e à familiaridade com a tecnologia. Como estamos lidando com uma equipe reduzida e dispomos de um tempo limitado para a entrega, optar por uma solução monolítica nos permite concentrar esforços em uma única aplicação, facilitando o desenvolvimento e a manutenção.

Além disso, o conhecimento prévio da equipe com a tecnologia envolvida favoreceu essa abordagem, proporcionando mais agilidade no desenvolvimento. A arquitetura monolítica também simplifica a integração entre os componentes do sistema, o que é crucial em projetos com prazos apertados. Essa decisão nos permite entregar um produto funcional de maneira eficiente, sem a complexidade adicional que outras arquiteturas, como microsserviços, poderiam introduzir neste momento.

# Estrutura de pastas da API
<pre>
.
└── VOM-HIVE.API /
    ├── Properties/
    │   └── launchSettings.json
    ├── Controllers/
    │   ├── CampaignController.cs
    │   ├── CompanyController.cs
    │   ├── ProductController.cs
    │   └── ProfileUserController.cs
    ├── Data/
    │   └── AppDbContext.cs
    ├── DTO/
    │   ├── Campaign/
    │   │   ├── Vinculo/
    │   │   │   ├── CompanyVinculoDto.cs
    │   │   │   └── ProductVinculoDto.cs
    │   │   ├── CampaignCreateDto.cs
    │   │   └── CampaignEditDto.cs
    │   ├── Company/
    │   │   ├── CompanyCreateDto.cs
    │   │   └── CompanyEditDto.cs 
    │   ├── Product/
    │   │   ├── ProductCreateDto.cs
    │   │   └── ProductEditDto.cs
    │   ├── ProfileUser/
    │   │   ├── Vinculo
    │   │   └── CompanyVinculoDto.cs
    │   ├── ProfileUserCreateDto.cs
    │   └── ProfileUserEditDto.cs
    ├── Migrations/
    │   ├── 20240912231203_InitialMigration.cs
    │   └── AppDbContextModelSnapshot.cs
    ├── Models/
    │   ├── Campaign.cs
    │   ├── Company.cs
    │   ├── ErrorViewModel.cs
    │   ├── Product.cs
    │   ├── Profileuser.cs
    │   └── ResponseModel.cs
    ├── Services/
    │   ├── Campaign/
    │   │   ├── CampaignService.cs
    │   │   └── ICampaignInterface.cs
    │   ├── Company/
    │   │   ├── CompanyService.cs
    │   │   └── ICompanyInterface.cs
    │   ├── Product/
    │   │   ├── ProductService.cs
    │   │   └── IProductInterface.cs
    │   └── ProfileUser/
    │       ├── ProfileUserService.cs
    │       └── IProfileUserInterface.cs
    ├── appsettings.json
    └── Program.cs
</pre>

# Design patterns
## Repository
O Repository Pattern é um padrão que isola a lógica de acesso a dados da lógica de negócios. Os serviços são responsáveis por interagir com o banco de dados através do AppDbContext e fornecer métodos de alto nível para manipulação de dados. Neste caso, embora não haja uma pasta explicitamente chamada "Repositories", os serviços estão desempenhando o papel de repositórios.

## Dependency Injection
A injeção de dependência é um padrão que facilita o gerenciamento de dependências na aplicação, tornando-as mais fáceis de testar e mantendo o código desacoplado. Aqui, estamos injetando as dependências dos serviços nas controllers.

## DTO
O DTO Pattern é utilizado para transferir dados entre camadas da aplicação. Os DTOs são usados para transferir dados entre o frontend e o backend, ajudando a controlar os dados que entram e saem da API.

## Service Layer
A Camada de Serviço abstrai a lógica de negócios da aplicação, centralizando essa lógica em classes específicas. Isso mantém as controllers mais enxutas, delegando tarefas complexas aos serviços. Cada serviço implementa sua respectiva interface (ICampaignInterface, ICompanyInterface), o que facilita o desacoplamento e os testes.

## Controller
Seguindo o padrão MVC (Model-View-Controller), as controllers atuam como intermediárias entre a camada de visualização (ou requisições HTTP) e as camadas de lógica de negócios. Elas recebem requisições, chamam os serviços necessários e retornam respostas ao cliente.

## Models
Seguindo o padrão MVC, os Models representam os dados e a lógica de negócios no sistema. Essas classes são as que interagem diretamente com o banco de dados via AppDbContext

# Tabelas

## Campaign
| Campo        | Tipo        | Restrição     |
|--------------|-------------|---------------|
| id_campaign  | INTEGER     | Primary Key   |
| nm_campaign  | VARCHAR2(50)| Not Null      |
| target       | VARCHAR2    | Not Null      |
| dt_register  | DATE        | Not Null      |
| details      | CLOB        |               |
| status       | VARCHAR2(50)|               |
| id_company   | INTEGER     | Foreign Key   |
| id_product   | INTEGER     | Foreign Key   |

### Constraints:
- Primary Key: `Campaign_PK(id_campaign)`
- Foreign Key: `Campaign_Company_FK(id_company)` references `Company`
- Foreign Key: `Campaign_Product_FK(id_product)` references `Product`

---

## Company
| Campo        | Tipo          | Restrição     |
|--------------|---------------|---------------|
| id_company   | INTEGER       | Primary Key   |
| nm_company   | VARCHAR2(50)  | Not Null      |
| cnpj         | CHAR(18)      | Not Null      |
| email        | VARCHAR2(255) | Not Null      |
| dt_register  | DATE          | Not Null      |

### Constraints:
- Primary Key: `Company_PK(id_company)`

---

## Product
| Campo           | Tipo          | Restrição     |
|-----------------|---------------|---------------|
| id_product      | INTEGER       | Primary Key   |
| nm_product      | VARCHAR2      | Not Null      |
| category_product| VARCHAR2      |               |

### Constraints:
- Primary Key: `Product_PK(id_product)`

---

## Profile_user
| Campo           | Tipo          | Restrição     |
|-----------------|---------------|---------------|
| id_user         | INTEGER       | Primary Key   |
| nm_user         | VARCHAR2(50)  | Not Null      |
| pass_user       | VARCHAR2(15)  | Not Null      |
| dt_register     | DATE          | Not Null      |
| permission_user | VARCHAR2      | Not Null      |
| status          | VARCHAR2      |               |
| id_company      | INTEGER       | Foreign Key   |

### Constraints:
- Primary Key: `Profile_user_PK(id_user)`
- Foreign Key: `Profile_user_Company_FK(id_company)` references `Company`

# Instruções
Para executar a aplicação, siga os passos abaixo:  

1. Copie o link do repositório
2. Cole o link: `https://github.com/Challengers-2-O-Retorno/VOM-HIVE-.NET-SP3.git`
3. Clone o repositório para sua IDE (ex: Visual Studio 2022, Rider)
4. No arquivo "appsettings.json" troque o campo "Id" e o "Password"
5. Instale as dependências necessárias com o comando `dotnet restore`.
6. Localize seu Package Manager Console digite "update database" e pressione enter.
7. Dê run na API.
8. De acordo com nossas regras de negócio, é necessário começar a operação de Create na seguinte ordem: Company, ProfileUser, Product e Campaign
9. Fique à vontade para testar os endpoints

# Integrantes

Gabriel Siqueira Rodrigues RM:98626

Gustavo de Oliveira Azevedo RM:550548

Isabella Jorge Ferreira RM:552329

Mateus Mantovani Araújo RM:98524

Juan de Godoy RM:551408

### Todos 2TDSPF