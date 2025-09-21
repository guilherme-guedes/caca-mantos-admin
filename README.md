# Admin - Caça Mantos

Projeto para gerenciamento das informações do website Caça Mantos (cacamantos.com.br);

### Monorepo

Neste repositório estão versionadas a aplicação frontend e a backend que dão suporte ao painel admin.

## Tecnologias

- Frontend : Anguar 19
- Backend : ASP.NET Core 9
 Há no backend análise de código e testes.\
 No commit são executadas verificações de convenção de código;\
 No push são executadas além das verificações anteriores os testes de unidade;

## Setup

### Restaure as ferramentas locais

#### Backend
   ```sh
   dotnet tool restore
   dotnet husky install
   ```
Isso instalará o husky e o format que serão utilizados para revisão dos commit's e push's.\
  
Caso queira rodar a formatação segundo os analisadores:
   ```sh
   dotnet format analyzers
   ```

### ROADMAP

- Worker / Agent (Fila) para replicar os dados alterados do admin nos bancos utilizado pela aplicação principal