# Carglass.DivisorPrime.CLI

## ?? Sobre o Projeto

O **Carglass.DivisorPrime.CLI** � um frontend do tipo console (CLI - Command Line Interface) desenvolvido para consumir a API do projeto **Carglass.DivisorPrime.Api**. Este programa permite calcular divisores e divisores primos de n�meros informados diretamente pelo terminal, apresentando os resultados de forma simples e eficiente.

O projeto foi desenvolvido com foco em modularidade, boas pr�ticas de desenvolvimento e padr�es de design que garantem facilidade de manuten��o, testabilidade e escalabilidade.

---

## ??? Tecnologias Utilizadas

- **.NET 8**
- **HttpClient** para comunica��o com a API
- **Microsoft.Extensions.DependencyInjection** para inje��o de depend�ncias
- **Moq** para testes unit�rios

---

## ??? Estrutura do Projeto

Este projeto foi estruturado utilizando boas pr�ticas de desenvolvimento e diversos padr�es de design. Abaixo est�o os principais conceitos aplicados:

### ??? Frontend Console
- O projeto foi desenvolvido como uma aplica��o **CLI (Command Line Interface)**, permitindo intera��o direta com o usu�rio pelo terminal.

### ?? Camadas e Padr�es de Design

#### **1. Inje��o de Depend�ncia (Dependency Injection)**
- Configurada no arquivo `DependencyInjection.cs`, utilizando o container de servi�os do **Microsoft.Extensions.DependencyInjection**.
- Exemplo: O `HttpClient` para chamadas � API � configurado dinamicamente com a URL base a partir do arquivo `appsettings.json`.

#### **2. Builder Pattern**
- Implementado na classe `ResponseBuilder`, que permite construir objetos de resposta de forma fluente e flex�vel (ex.: `WithMessage`, `AsError`, `AsSuccess`).

#### **3. Service Layer Pattern**
- A l�gica principal est� centralizada no servi�o `DivisorService`, que conecta a interface do usu�rio � camada de comunica��o com a API (`DivisorApi`).

#### **4. Integra��o com APIs**
- Utiliza��o do `HttpClient` na classe `DivisorApi` para gerenciar chamadas HTTP � API de divisores.

#### **5. DTOs (Data Transfer Objects)**
- Classes como `ApiResponseDto` e `DivisorsDto` s�o utilizadas para transferir dados entre as camadas.

#### **6. Tratamento de Erros Centralizado**
- Mensagens de erro e sucesso s�o padronizadas pelo `ResponseBuilder`, garantindo consist�ncia.

### ?? Princ�pios de Clean Code
- **Nomina��o Significativa:** Classes e m�todos possuem nomes que refletem seu prop�sito (ex.: `ExecuteAsync`, `WithMessage`, `AsSuccess`).
- **Separa��o de Responsabilidades:** Cada classe � respons�vel por uma tarefa espec�fica.
- **Simplicidade:** C�digo organizado, com fun��es pequenas e claras.

### ? Testes Unit�rios
- Testes est�o implementados para garantir a funcionalidade das classes (`DivisorServiceTests`, `DivisorApiTests`).
- Utiliza��o de **Moq** para simular depend�ncias e isolar o comportamento dos componentes.

---

## ?? Configura��o e Execu��o

### Pr�-requisitos
- **.NET 8 SDK** instalado em sua m�quina.

### Configura��o
1. Clone este reposit�rio:
   ```bash
   git clone https://github.com/seu-usuario/Carglass.DivisorPrime.CLI.git

### Execu��o
1. Exemplo de execu��o do compilado:
   ```bash
   Carglass.DivisorPrime.CLI 45

