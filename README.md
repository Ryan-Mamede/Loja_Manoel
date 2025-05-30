
# 📦 CaixasAPI - Empacotamento de Pedidos para a Loja do Seu Manoel

Esta API realiza o empacotamento automático de pedidos de uma loja online, selecionando as caixas ideais para os produtos informados. A aplicação foi desenvolvida em .NET 9 e utiliza SQL Server como banco de dados, ambos executados via Docker.

---

## ✅ Funcionalidades

- Receber múltiplos pedidos via JSON
- Processar cada pedido com base nas dimensões dos produtos
- Alocar os produtos em uma ou mais caixas otimizando o espaço
- Retornar a lista de caixas utilizadas e os produtos alocados em cada uma
- Swagger UI para testes diretos via navegador

---

## 📦 Caixas disponíveis

| Nome     | Altura (cm) | Largura (cm) | Comprimento (cm) |
|----------|-------------|--------------|-------------------|
| Caixa 1  | 30          | 40           | 80                |
| Caixa 2  | 80          | 50           | 40                |
| Caixa 3  | 50          | 80           | 60                |

---

## 🚀 Como executar

### ⚙ Pré-requisitos

- Docker instalado
- Docker Compose

### ▶ Comandos para subir o ambiente

Na raiz do projeto:

```bash
docker-compose up --build
```

Isso irá:

- Criar a imagem da API (`caixasapi-api`)
- Subir o container da API e do SQL Server (`caixasapi-db`)

### 🌐 Acessar Swagger

Após o build, acesse: [http://localhost:5218/swagger](http://localhost:5218/swagger)

🌐 ou via Docker:

http://localhost:8080

---

## 📥 Exemplo de Entrada

```json
{
  "pedidos": [
    {
      "numeroPedido": "0001",
      "produtos": [
        {
          "nome": "Notebook",
          "dimensoes": {
            "altura": 5,
            "largura": 8,
            "comprimento": 12
          }
        }
      ]
    }
  ]
}
```

## 📤 Exemplo de Saída

```json
{
  "pedidos": [
    {
      "numeroPedido": "0001",
      "caixas": [
        {
          "nome": "Caixa 1",
          "dimensoes": {
            "altura": 30,
            "largura": 40,
            "comprimento": 80
          },
          "produtos": [
            {
              "nome": "Notebook",
              "dimensoes": {
                "altura": 5,
                "largura": 8,
                "comprimento": 12
              }
            }
          ]
        }
      ]
    }
  ]
}
```

---

## 🛠 Tecnologias Utilizadas

- .NET 9
- ASP.NET Core Web API
- SQL Server 2022
- AutoMapper
- Docker + Docker Compose
- Swagger / Swashbuckle

---

## 📁 Organização do Projeto

- `CaixasAPI.API/` - Projeto principal da API
- `CaixasAPI.Infrastructure/` - Repositórios, contexto e entidades
- `CaixasAPI.Domain/` - Modelos de domínio e DTOs

---

## 🔐 Autenticação
Este projeto atualmente não utiliza autenticação. Futuramente poderá ser integrado com JWT ou OAuth.

---

⚙️ Configuração do AutoMapper

O projeto utiliza AutoMapper para mapear automaticamente entre DTOs e entidades de domínio.

---

