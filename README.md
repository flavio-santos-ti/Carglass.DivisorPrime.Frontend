# ![Logo](assets/images/logo.png) Carglass.DivisorPrime.CLI

## 📖 Sobre o Projeto

O **Carglass.DivisorPrime.CLI** é um frontend do tipo console (CLI - Command Line Interface) desenvolvido para consumir a API do projeto **Carglass.DivisorPrime.Api**. Este programa permite calcular divisores e divisores primos de números informados diretamente pelo terminal, apresentando os resultados de forma simples e eficiente.

O projeto foi desenvolvido com foco em modularidade, boas práticas de desenvolvimento e padrões de design que garantem facilidade de manutenção, testabilidade e escalabilidade.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8**
- **HttpClient** para comunicação com a API
- **Microsoft.Extensions.DependencyInjection** para injeção de dependências
- **Moq** para testes unitários

---

## 🏗️ Estrutura do Projeto

Este projeto foi estruturado utilizando boas práticas de desenvolvimento e diversos padrões de design. Abaixo estão os principais conceitos aplicados:

### 🖥️ Frontend Console
- O projeto foi desenvolvido como uma aplicação **CLI (Command Line Interface)**, permitindo interação direta com o usuário pelo terminal.

### 🔗 Camadas e Padrões de Design

#### **1. Injeção de Dependência (Dependency Injection)**
- Configurada no arquivo `DependencyInjection.cs`, utilizando o container de serviços do **Microsoft.Extensions.DependencyInjection**.
- Exemplo: O `HttpClient` para chamadas à API é configurado dinamicamente com a URL base a partir do arquivo `appsettings.json`.

#### **2. Builder Pattern**
- Implementado na classe `ResponseBuilder`, que permite construir objetos de resposta de forma fluente e flexível (ex.: `WithMessage`, `AsError`, `AsSuccess`).

#### **3. Service Layer Pattern**
- A lógica principal está centralizada no serviço `DivisorService`, que conecta a interface do usuário à camada de comunicação com a API (`DivisorApi`).

#### **4. Integração com APIs**
- Utilização do `HttpClient` na classe `DivisorApi` para gerenciar chamadas HTTP à API de divisores.

#### **5. DTOs (Data Transfer Objects)**
- Classes como `ApiResponseDto` e `DivisorsDto` são utilizadas para transferir dados entre as camadas.

#### **6. Tratamento de Erros Centralizado**
- Mensagens de erro e sucesso são padronizadas pelo `ResponseBuilder`, garantindo consistência.

#### **7. Métodos Pequenos e Coesos**
- Mensagens de erro e sucesso são padronizadas pelo `ResponseBuilder`, garantindo consistência.

Os métodos no projeto seguem o princípio de fazer apenas uma tarefa específica, o que facilita a leitura, manutenção e reutilização. Cada método tem uma responsabilidade clara, contribuindo para um código mais modular e legível.

- **Exemplo no `ResponseBuilder`**:
  - `WithMessage`: Adiciona uma mensagem à resposta.
  - `AsError`: Define o estado como erro.
  - `AsSuccess`: Define o estado como sucesso.
  - `Build`: Constrói e retorna o objeto configurado.
  - `Print`: Exibe a mensagem construída no console.
  - `WaitForExit`: Pausa a execução aguardando interação do usuário.

Essa abordagem evita métodos longos e complexos, reduzindo a possibilidade de erros e tornando o código mais fácil de compreender.

#### **8. Separação de Responsabilidades**

- O projeto segue o **Princípio da Responsabilidade Única** (SRP do SOLID):
  - Cada classe é responsável por uma única tarefa.
  - **Exemplo**:
    - `DivisorApi`: Gerencia chamadas HTTP.
    - `DivisorService`: Lógica de negócios.
    - `ResponseBuilder`: Construção de respostas.
  - Isso facilita a compreensão e manutenção do código.

#### **9. Evitando Código Duplicado**

- Mensagens de erro e sucesso são centralizadas no `ResponseBuilder`, evitando repetições em diferentes partes do código.

### 🧼 Princípios de Clean Code
- **Nominação Significativa:** Classes e métodos possuem nomes que refletem seu propósito (ex.: `ExecuteAsync`, `WithMessage`, `AsSuccess`).
- **Separação de Responsabilidades:** Cada classe é responsável por uma tarefa específica.
- **Simplicidade:** Código organizado, com funções pequenas e claras.

### ✅ Testes Unitários
- Testes estão implementados para garantir a funcionalidade das classes (`DivisorServiceTests`, `DivisorApiTests`).
- Utilização de **Moq** para simular dependências e isolar o comportamento dos componentes.

---

## ⚙️ Configuração e Execução

### Pré-requisitos
- **.NET 8 SDK** instalado em sua máquina.

### Configuração
1. Clone este repositório:

   ```bash
   git clone https://github.com/seu-usuario/Carglass.DivisorPrime.CLI.git

2. O arquivo appsettings.json deve estar no mesmo diretório da aplicação com o seguinte conteúdo:

   ```json
   {
      "ApiSettings": {
      "BaseUrl": "http://localhost:5180"
      }
   }

### Execução
1. Após compilado, executar o comando:

   ```bash
   Carglass.DivisorPrime.CLI 45


   
