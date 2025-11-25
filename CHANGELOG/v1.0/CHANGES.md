# 📝 CHANGES - v1.0

## Detalhamento de Mudanças

### Controllers (3 mudanças)

#### Controllers/Dashboard/PrincipalController.cs
```
Status: MOVIDO
De: Controllers/PrincipalController.cs
Para: Controllers/Dashboard/PrincipalController.cs
Namespace: Projeto_Dotnet8.Controllers → Projeto_Dotnet8.Controllers.Dashboard
```

#### Controllers/Admin/SalaController.cs
```
Status: MOVIDO
De: Controllers/SalaController.cs
Para: Controllers/Admin/SalaController.cs
Namespace: Projeto_Dotnet8.Controllers → Projeto_Dotnet8.Controllers.Admin
```

#### Controllers/ComputadorController.cs
```
Status: MANTIDO
Local: Controllers/ComputadorController.cs
Namespace: Projeto_Dotnet8.Controllers
```

---

### Models - Entities (3 mudanças)

#### Models/Entities/ComputadorModels.cs
```
Status: MOVIDO
De: Models/ComputadorModels.cs
Para: Models/Entities/ComputadorModels.cs
Namespace: Projeto_Dotnet8.Models (mantido)
```

#### Models/Entities/SalaModels.cs
```
Status: MOVIDO
De: Models/SalaModels.cs
Para: Models/Entities/SalaModels.cs
Namespace: Projeto_Dotnet8.Models (mantido)
```

#### Models/Entities/MensagemModels.cs
```
Status: MOVIDO
De: Models/MensagemModels.cs
Para: Models/Entities/MensagemModels.cs
Namespace: Projeto_Dotnet8.Models (mantido)
```

---

### Models - ViewModels (3 mudanças)

#### Models/ViewModels/CriarPC_Sala.cs
```
Status: MOVIDO E REORGANIZADO
De: Models/ViewModel/CriarPC_Sala.cs
Para: Models/ViewModels/CriarPC_Sala.cs
Namespace: Projeto_Dotnet8.Models.ViewModels
Mudanças: Consolidado de pasta "ViewModel" para "ViewModels"
```

#### Models/ViewModels/CriarMensagem.cs
```
Status: MOVIDO E REORGANIZADO
De: Models/ViewModel/CriarMensagem.cs
Para: Models/ViewModels/CriarMensagem.cs
Namespace: Projeto_Dotnet8.Models.ViewModels
Mudanças: Consolidado de pasta "ViewModel" para "ViewModels"
```

#### Models/ViewModels/ErrorViewModel.cs
```
Status: MOVIDO
De: Models/ErrorViewModel.cs
Para: Models/ViewModels/ErrorViewModel.cs
Namespace: Projeto_Dotnet8.Models.ViewModels
```

---

### Views - Auth (1 mudança)

#### Views/Auth/Login.cshtml
```
Status: MOVIDO
De: Views/Principal/Login.cshtml
Para: Views/Auth/Login.cshtml
Propósito: Página de login público
```

---

### Views - User (7 mudanças)

#### Views/User/Index.cshtml
```
Status: MOVIDO
De: Views/Principal/Index.cshtml
Para: Views/User/Index.cshtml
Propósito: Home do usuário comum
```

#### Views/User/Dashboard.cshtml
```
Status: MOVIDO
De: Views/Principal/Dashboard.cshtml
Para: Views/User/Dashboard.cshtml
Propósito: Dashboard de usuário
```

#### Views/User/Listar.cshtml
```
Status: MOVIDO
De: Views/Principal/Listar.cshtml
Para: Views/User/Listar.cshtml
Propósito: Listar computadores
```

#### Views/User/Criar.cshtml
```
Status: MOVIDO
De: Views/Principal/Criar.cshtml
Para: Views/User/Criar.cshtml
Propósito: Criar solicitação
```

#### Views/User/Editar.cshtml
```
Status: MOVIDO
De: Views/Principal/Editar.cshtml
Para: Views/User/Editar.cshtml
Propósito: Editar computador
```

#### Views/User/Deletar.cshtml
```
Status: MOVIDO
De: Views/Principal/Deletar.cshtml
Para: Views/User/Deletar.cshtml
Propósito: Deletar computador
```

#### Views/User/Solicitacao.cshtml
```
Status: MOVIDO
De: Views/Principal/Solicitacao.cshtml
Para: Views/User/Solicitacao.cshtml
Propósito: Formulário de solicitação
```

---

### Views - Admin (2 mudanças)

#### Views/Admin/Auth/IndexADM.cshtml
```
Status: MOVIDO
De: Views/Principal/IndexADM.cshtml
Para: Views/Admin/Auth/IndexADM.cshtml
Propósito: Home do administrador
```

#### Views/Admin/Computadores/CriarPC.cshtml
```
Status: MOVIDO
De: Views/Computador/CriarPC.cshtml
Para: Views/Admin/Computadores/CriarPC.cshtml
Propósito: Criar novo computador
```

---

### Data (1 mudança)

#### Data/Contexts/BancoContext.cs
```
Status: MOVIDO
De: Data/BancoContext.cs
Para: Data/Contexts/BancoContext.cs
Namespace: Projeto_Dotnet8.Data.Contexts
Propósito: Centralizar contextos em subpasta
```

---

### Outros Arquivos

#### Program.cs
```
Status: ATUALIZADO
Mudanças:
  - Removidas referências a namespaces antigos
  - Importações de Repository removidas
```

#### Views/_ViewImports.cshtml
```
Status: MANTIDO
Local: Views/_ViewImports.cshtml
```

#### Views/Shared/* (6 arquivos)
```
Status: MANTIDO
Local: Views/Shared/
Arquivos:
  - _Layout.cshtml
  - _Header.cshtml
  - _HeaderADM.cshtml
  - _ValidationScriptsPartial.cshtml
  - _ViewStart.cshtml
```

---

## 📊 Resumo Estatístico

| Tipo | MOVIDO | CRIADO | DELETADO | MANTIDO |
|------|--------|--------|----------|---------|
| Controllers | 2 | 0 | 0 | 1 |
| Models (Entities) | 3 | 0 | 0 | 0 |
| Models (ViewModels) | 3 | 0 | 0 | 0 |
| Views | 10 | 0 | 0 | 0 |
| Data | 1 | 0 | 0 | 0 |
| Configuração | 0 | 0 | 0 | 1 |
| Shared | 0 | 0 | 0 | 6 |
| **TOTAL** | **19** | **0** | **0** | **8** |

---

**Total de Mudanças**: 27 arquivos

**Data**: 25 de Novembro de 2025
