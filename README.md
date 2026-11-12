# EasyShop - Persistência Poliglota (SQL + NoSQL) com .NET MAUI

Protótipo de um mini e-commerce que usa **dois bancos de dados locais ao mesmo tempo** no mesmo aplicativo.

## Integrantes

- Mateus Coimbra de Oliveira
- 

## Vídeo de demonstração

- Link: COLE_AQUI_O_LINK_DO_VIDEO (YouTube ou Google Drive)

## Objetivo

Demonstrar uma arquitetura de persistência poliglota, dividindo as responsabilidades entre dois paradigmas:

| Dado | Banco | Biblioteca | Motivo |
|------|-------|-----------|--------|
| Catálogo de Produtos | NoSQL (documentos) | LiteDB | Produtos têm atributos dinâmicos (cor, tamanho, voltagem, processador...), sem schema fixo |
| Cadastro de Clientes | SQL (relacional) | SQLite (sqlite-net-pcl) | Dados estruturados que exigem consistência (campos obrigatórios e e-mail único) |

## Funcionalidades

**Aba Produtos (NoSQL / LiteDB)**
- Cadastro com Nome, Preço, Categoria (opcional) e Atributos dinâmicos no formato `chave=valor; chave=valor`
- Listagem dos produtos consultando o banco de documentos

**Aba Clientes (SQL / SQLite)**
- Cadastro com Nome Completo e E-mail (e-mail validado e único)
- Listagem dos clientes consultando o banco relacional

## Arquitetura em camadas

```
EasyShop/
├── Models/       Entidades (Customer, Product)
├── Data/         CAMADA DE ACESSO A DADOS: CustomerRepository (SQLite) e ProductRepository (LiteDB)
├── Services/     CAMADA DE NEGÓCIO: validações e regras (CustomerService, ProductService)
├── ViewModels/   CAMADA DE APRESENTAÇÃO: estado da tela e comandos
└── Views/        CAMADA DE APRESENTAÇÃO: telas XAML
```

- **Apresentação:** exibe a interface e captura as ações do usuário. Não acessa banco de dados.
- **Negócio:** valida os dados (nome, e-mail, preço) e orquestra as operações.
- **Dados:** única camada que conhece SQLite e LiteDB e executa o CRUD.

Fluxo: `View -> ViewModel -> Service -> Repository -> Banco`

## Tecnologias

- .NET 8 / .NET MAUI
- sqlite-net-pcl + SQLitePCLRaw.bundle_green
- LiteDB
- CommunityToolkit.Mvvm

## Como executar

**Pré-requisitos**
- Visual Studio 2022 (ou mais recente) com a carga de trabalho **Desenvolvimento de interface do usuário multiplataforma .NET (MAUI)**
- Android SDK e um emulador Android criado no **Device Manager** (ex.: Pixel 5, API 34)

**Passos**
1. Clone o repositório:
   ```bash
   git clone URL_DO_REPOSITORIO
   ```
2. Abra o arquivo `EasyShop.sln` (ou `EasyShop.csproj`) no Visual Studio.
3. Aguarde a restauração dos pacotes NuGet.
4. Na barra superior, selecione o framework **net8.0-android** e o emulador criado.
5. Pressione **F5** para executar.

## Como testar

1. Aba **Produtos**: cadastre produtos com atributos diferentes, por exemplo:
   - `Notebook Gamer | 4599,90 | Informática | processador=Ryzen 7; ram=16GB; ssd=512GB`
   - `Camisa Polo | 89,90 | Roupas | tamanho=M; cor=Azul`
   - `Liquidificador | 249 | Eletro | voltagem=220V; garantia=12 meses`
2. Aba **Clientes**: cadastre um cliente válido, depois tente repetir o mesmo e-mail e informar um e-mail inválido (o sistema exibe a mensagem de erro).
3. Feche o app e abra novamente: os dados continuam nas duas abas.

## Onde ficam os dados

Dois arquivos separados em `FileSystem.AppDataDirectory`:
- `customers.db3` (SQLite)
- `products.db` (LiteDB)