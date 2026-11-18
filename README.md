# Trilhas

Repositório de trilhas de aprendizado e projetos desenvolvidos ao longo dos estudos de programação.

## Informações

- **Nome do repositório:** Trilhas
- **Autor:** Rogerio-filho80 (rogerio.filho80@gmail.com)
- **Licença:** MIT
- **Linguagem principal:** C# (.NET 8.0)
- **Plataforma:** Windows / .NET

## Sobre

Este repositório reúne os projetos e exercícios realizados nas trilhas de estudo, incluindo o projeto **AppBanco**, um aplicativo de console em C# para gerenciamento de bancos, agências, contas e movimentações, com persistência em arquivos XML.

## Estrutura do Repositório

```
Trilhas/
├── README.md
├── LICENSE
├── .gitignore
├── .gitattributes
└── ligtecprog/
    └── Segundo_Peeriodo/
        └── Projeto Banco/
            └── Rogerio_Filho_Daiana_Myllena/
                └── AppBanco/
                    ├── AppBanco.csproj
                    ├── Program.cs
                    ├── Tela.cs
                    ├── Dados/            # Arquivos XML dos bancos
                    ├── Entities/         # Banco, Agencia, Conta, Correntista, Movimento
                    └── Models/           # Enums (TipoConta, TipoMovimento)
```

## Projeto: AppBanco

Aplicativo de console (.NET 8) que permite:

- Listar bancos existentes a partir de arquivos XML na pasta `Dados/`;
- Carregar e exibir as informações de um banco (número, nome, agências, contas e movimentações);
- Criar um novo banco (opção de menu).

### Entidades

- **Banco**: número, nome e lista de agências.
- **Agencia**: número, nome, endereço, bairro, cidade, estado e lista de contas.
- **Conta**: tipo (Corrente/Poupança), número, saldo e movimentações.
- **Correntista**: nome, CPF, endereço, número, estado, cidade e telefone.
- **Movimento**: data, tipo (Crédito/Débito), descrição e valor.

### Tecnologias

- C#
- .NET 8.0
- System.Text.RegularExpressions
- Persistência em XML

## Como Executar

1. Instale o [.NET SDK 8.0](https://dotnet.microsoft.com/download).
2. Navegue até a pasta do projeto:

   ```bash
   cd "ligtecprog/Segundo_Peeriodo/Projeto Banco/Rogerio_Filho_Daiana_Myllena/AppBanco"
   ```

3. Execute:

   ```bash
   dotnet run
   ```

> Observação: o aplicativo procura a pasta `Dados/` no diretório atual de execução.

## Autores

- Rogerio Filho
- Daiana Myllena

## Licença

Distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
