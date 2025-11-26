# 🖥️ Projeto StreamLine

Este projeto tem o objetivo de facilitar a comunicação entre alunos e técnicos, de maneira que os técnicos possam visualizar todas solicitações de atenção em computadores de salas específicas, melhorando a eficiência de aprendizado dos alunos da instituição.

---

## ⌨️ Funcionalidades

- Cadastro de computadores por sala <- ADM
- Cadastro de salas no banco de dados <- ADM
- Listagem de todas solicitações em um lugar <- ADM
- Edição da lista com a opção de excluir solicitações <- ADM
- Dashboard com estastísticas gerais das salas como total <- ADM
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

3. Clone o repositório com o link:
https://github.com/Ryan-Marcel/Projeto_Streamline

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
    └──ComputadorController.cs
    └──PrincipalController.cs
    └──SalaController.cs
├── /Data
    └── BancoContext.cs
├── Migrations
├── /Models
        └── /Models
            └── /ViewModel
                └── CriarMensagem.cs
                └── CriarPc_Sala.cs
    └── ComputadorModel.cs
    └── ErrorViewModel.cs
    └── MensagemModels.cs
    └── SalaModels.cs
├── /obj
├── /Properties
├── /Repository
├── /Views
        Computador
            └── CriarPC.cshtml
        Principal
            └── Criar.cshtml
            └── Dashboard.cshtml
            └── Deletar.cshtml
            └── Editar.cshtml
            └── Index.cshtml
            └── IndexADM.cshtml
            └── Listar.cshtml
            └── Login.cshtml
            └── Solicitacao.cshtml
├── Shared
├── /wwwroot (arquivos estáticos como CSS e JS)
├── appsettings.json
├── Program.cs```


## 🤝 Contribuidores

- Leonardo Araujo Oliveira
- Gabriel Gregorio Silva
- Ryan Marcel Sousa Santos 

---

## 📬 Contato

Para dúvidas ou sugestões:
- 📧 ryan.m.santos@ba.estudante.senai.br
- 📧 leonardo.a.oliveira6@ba.estudante.senai.br
- 📧 gabriel.g.silva6@ba.estudante.senai.br
- 🌐 https://github.com/Ryan-Marcel
- 🌐 https://github.com/Trustingtag
- 🌐 https://github.com/GregDev71
