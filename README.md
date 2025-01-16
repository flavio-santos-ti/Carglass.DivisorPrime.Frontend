# Carglass.DivisorPrime.CLI

## ?? Sobre o Projeto

O **Carglass.DivisorPrime.CLI** é um frontend do tipo console (CLI - Command Line Interface) desenvolvido para consumir a API do projeto **Carglass.DivisorPrime.Api**. Este programa permite calcular divisores e divisores primos de números informados diretamente pelo terminal, apresentando os resultados de forma simples e eficiente.

O projeto foi desenvolvido com foco em modularidade, boas práticas de desenvolvimento e padrões de design que garantem facilidade de manutenção, testabilidade e escalabilidade.

---

## ??? Tecnologias Utilizadas

- **.NET 8**
- **HttpClient** para comunicação com a API
- **Microsoft.Extensions.DependencyInjection** para injeção de dependências
- **Moq** para testes unitários

---

## ??? Estrutura do Projeto

Este projeto foi estruturado utilizando boas práticas de desenvolvimento e diversos padrões de design. Abaixo estão os principais conceitos aplicados:

### ??? Frontend Console
- O projeto foi desenvolvido como uma aplicação **CLI (Command Line Interface)**, permitindo interação direta com o usuário pelo terminal.

### ?? Camadas e Padrões de Design

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

### ?? Princípios de Clean Code
- **Nominação Significativa:** Classes e métodos possuem nomes que refletem seu propósito (ex.: `ExecuteAsync`, `WithMessage`, `AsSuccess`).
- **Separação de Responsabilidades:** Cada classe é responsável por uma tarefa específica.
- **Simplicidade:** Código organizado, com funções pequenas e claras.

### ? Testes Unitários
- Testes estão implementados para garantir a funcionalidade das classes (`DivisorServiceTests`, `DivisorApiTests`).
- Utilização de **Moq** para simular dependências e isolar o comportamento dos componentes.

---

## ?? Configuração e Execução

### Pré-requisitos
- **.NET 8 SDK** instalado em sua máquina.

### Configuração
1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/Carglass.DivisorPrime.CLI.git

### Execução
1. Exemplo de execução do compilado:
   ```bash
   Carglass.DivisorPrime.CLI 45

