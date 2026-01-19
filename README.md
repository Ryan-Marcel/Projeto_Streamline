# 🖥️ Projeto StreamLine

Este projeto tem o objetivo de facilitar a comunicação entre alunos e técnicos, de maneira que os técnicos possam visualizar todas solicitações de atenção em computadores de salas específicas, melhorando a eficiência de aprendizado dos alunos da instituição.

---

## ⌨️ Funcionalidades

- Cadastro de computadores por sala <- ADM
- Cadastro de salas no banco de dados <- ADM
- Listagem de todas solicitações em um lugar <- ADM
- Edição da lista com a opção de 3 status de solicitações <- ADM
- Dashboard com estastísticas gerais das salas como total <- ADM
- Visualização de todas salas e seus computadores com listas colapsadas <- ADM
- Gerador de solicitações para notificar problemas com uma máquina <- Aluno
- Visualizar histórico e status de visualizações <- Aluno
- Editar notificações criadas <- Aluno

---

## 💻 Tecnologias Utilizadas

- C# (lógica de programação e backend)
- HTML5, CSS3 e JavaScript (frontend)
- MySQL (banco de dados)
- Padrão de arquitetura MVC
- Visual Studio Code (ambiente de desenvolvimento)

---

## 🚀 Como executar o projeto localmente

### Pré-requisitos ###

- Ter o [Visual Studio Code](https://code.visualstudio.com/) instalado
- Ter o [MySQL Server](https://dev.mysql.com/downloads/mysql/) instalado e rodando
- Ter o .NET SDK instalado (caso o projeto utilize .NET Core ou similar)

### Passos ###

1. Crie ou acesse uma pasta (de preferência vazia.)

2. Acesse o git bash no diretório escolhido

3. Clone o repositório com o comando:
" git clone https://github.com/Ryan-Marcel/Projeto_Streamline "

4. Abra o diretório no VScode

### Rodando o programa ###

1. No MySQL crie a database "projeto_ds"
2. No terminal digite: "dotnet ef migrations add Teste1"
3. Após digite: "dotnet ef database update"
4. Inicie o programa utilizando o "dotnet watch"

---

## 📁 Estrutura do Projeto (MVC) ##

```
/Projeto_Dotnet8
├── /bin
├── /Controllers
    └──AccountControllers.cs
    └──ComputadorController.cs
    └──DashboardController.cs
    └──PrincipalController.cs
    └──SalaController.cs
├── /cypress
├── /Data
    └── BancoContext.cs
    └── SeedData.cs
├── /Migrations
├── /Models
        └── /ViewModel
            └── CriarMensagem.cs
            └── CriarPc_Sala.cs
            └── LoginViewModel.cs
            └── RegisterViewModel
        └── ApplicationUser.cs    
        └── ComputadorModels.cs
        └── ErrorViewModel.cs
        └── MensagemModels.cs
        └── SalaModels.cs
├── /obj
├── /Properties
├── /Repository
├── /Views
        ├── /Account
            └── Register.cshtml
        ├──/Computador
            └── CriarPC.cshtml
        ├──/Principal
            └── _ComputadoresGrid.cshtml
            └── AcessoNegado.cshtml
            └── Criar.cshtml
            └── Dashboard.cshtml
            └── Deletar.cshtml
            └── DetalhesComputador.cshtml
            └── Editar.cshtml
            └── HistoricoSolicitacoes.cshtml
            └── Index.cshtml
            └── IndexADM.cshtml
            └── ListarADM.cshtml
            └── Login.cshtml
            └── Salas.cshtml
            └── Solicitacao.cshtml
├── /Shared
├── /wwwroot (arquivos estáticos como CSS e JS)
├── appsettings.json
├── Program.cs

```

## 🤝 Contribuidores

- Leonardo Araujo Oliveira
- Gabriel Gregorio Silva
- Ryan Marcel Sousa Santos 

---

## 📬 Contato

Para dúvidas ou sugestões:
- 📧 ryanmarcel604@gmail.com
- 📧 leonardo.a.oliveira6@ba.estudante.senai.br
- 📧 gabriel.g.silva6@ba.estudante.senai.br
- 🌐 https://github.com/Ryan-Marcel
- 🌐 https://github.com/Trustingtag
- 🌐 https://github.com/GregDev71
