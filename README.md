# EasyShop - Persistência Poliglota (SQL + NoSQL) com .NET MAUI

Protótipo de um mini e-commerce que usa **dois bancos de dados locais ao mesmo tempo** no mesmo aplicativo.

## Integrantes

- Mateus Coimbra de Oliveira
- Maria Fernanda Candido Dionisio

## Vídeo de demonstração
- Link: [Assistir no Google Drive](https://drive.google.com/file/d/1T23He9JI88TR35AoyRfgqpXfqZwf8bdY/view?usp=sharing)

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

- .NET 10 / .NET MAUI
- sqlite-net-pcl + SQLitePCLRaw.bundle_green
- LiteDB
- CommunityToolkit.Mvvm

## Como executar

**Pré-requisitos**
- Visual Studio com suporte ao .NET 10 e a carga de trabalho **Desenvolvimento de interface do usuário multiplataforma .NET (MAUI)**
- .NET 10 SDK

**Passos**
1. Clone o repositório:
   ```bash
   git clone https://github.com/MateusCoimbraa/EasyShop.git
   ```
2. Abra o arquivo `EasyShop.csproj` no Visual Studio (Arquivo > Abrir > Projeto/Solução).
3. Aguarde a restauração dos pacotes NuGet.
4. Na barra superior, selecione o framework **net10.0-android** e o emulador criado.
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