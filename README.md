# 📦 Pack Solver API

A **Pack Solver API** é uma aplicação RESTful desenvolvida em ASP.NET Core que recebe um pedido com uma lista de produtos e suas dimensões e retorna a caixa mais adequada para acomodá-los, otimizando o espaço disponível.

---

## 🚀 Tecnologias

- ASP.NET Core 8
- Entity Framework Core
- SQL Server (via Docker)
- Docker / Docker Compose
- Swashbuckle (Swagger UI)

---

## 🧰 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Opcional) Visual Studio 2022 com suporte a .NET

---

## 🛠️ Como executar

### 1. Clone o projeto:

```bash
git clone https://github.com/seu-usuario/pack-solver-api.git
cd pack-solver-api
```

### 2. Execute os containers com Docker:

```bash
docker-compose up --build
```
> Caso o container da API não funcione de primeira, é necessário reinicia-lo.

Isso irá:
- Criar e iniciar o banco SQL Server
- Criar a imagem da API e iniciá-la
- Mapear as portas da API para o host (`http://localhost:8081`)

> Verifique se a porta `8081` não está ocupada.

Após o container subir, acesse a documentação Swagger em:

```
http://localhost:8081/swagger
```

---

## 🧪 Exemplo de uso

### POST `/api/order/pack`

Requisição
```json
  "products": [
    {
      "productId": "PS5",
      "height": 40,
      "width": 10,
      "length": 25
    },
    {
      "productId": "VOLANTE",
      "height": 40,
      "width": 30,
      "length": 30
    }
  ]
}
```

Resposta:

```json
[
  {
    "boxId": "CAIXA_1",
    "height": 30,
    "width": 40,
    "length": 80
  }
]
```