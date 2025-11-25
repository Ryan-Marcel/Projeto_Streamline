# 📝 CHANGES - v1.1

## Detalhamento de Mudanças

### Controllers - Namespace Updates (3 mudanças)

#### Controllers/Admin/SalaController.cs
```
Status: ATUALIZADO
Mudanças:
  - Removido: using Projeto_Dotnet8.Repository;
  + Adicionado: using Projeto_Dotnet8.Data.Contexts;
  + Adicionado: using Projeto_Dotnet8.Models.Entities;
  - Removido: namespace Projeto_Dotnet8.Controllers;
  + Adicionado: namespace Projeto_Dotnet8.Controllers.Admin;
```

#### Controllers/ComputadorController.cs
```
Status: ATUALIZADO
Mudanças:
  - Removido: using Projeto_Dotnet8.Repository;
  + Adicionado: using Projeto_Dotnet8.Models.Entities;
  + Adicionado: using Projeto_Dotnet8.Models.ViewModels;
```

#### Controllers/Dashboard/PrincipalController.cs
```
Status: ATUALIZADO
Mudanças:
  - Removido: using Projeto_Dotnet8.Data;
  - Removido: using Projeto_Dotnet8.Models.ViewModel;
  - Removido: using Projeto_Dotnet8.Repository;
  + Adicionado: using Projeto_Dotnet8.Data.Contexts;
  + Adicionado: using Projeto_Dotnet8.Models.Entities;
  + Adicionado: using Projeto_Dotnet8.Models.ViewModels;
  - Removido: namespace Reserva.Controllers;
  + Adicionado: namespace Projeto_Dotnet8.Controllers.Dashboard;
```

---

### ViewModels - Nullable Properties (2 mudanças)

#### Models/ViewModels/CriarPC_Sala.cs
```
Status: ATUALIZADO
Mudanças:
  public string NovaSalaNum { get; set; }
  ➜ public string? NovaSalaNum { get; set; }
  
  public ComputadorModels Computador { get; set; }
  ➜ public ComputadorModels? Computador { get; set; }
  
  public List<SalaModels> Salas { get; set; }
  ➜ public List<SalaModels>? Salas { get; set; }
```

#### Models/ViewModels/CriarMensagem.cs
```
Status: ATUALIZADO
Mudanças:
  public List<SalaModels> Salas { get; set; }
  ➜ public List<SalaModels>? Salas { get; set; }
  
  public List<ComputadorModels> Computadores { get; set; }
  ➜ public List<ComputadorModels>? Computadores { get; set; }
  
  public string Mensagem { get; set; }
  ➜ public string? Mensagem { get; set; }
```

---

### Views - Reference Updates (2 mudanças)

#### Views/Admin/Computadores/CriarPC.cshtml
```
Status: ATUALIZADO
Mudanças:
  @model Projeto_Dotnet8.Models.CriarPC_Sala;
  ➜ @model Projeto_Dotnet8.Models.ViewModels.CriarPC_Sala;
  
  @foreach (var sala in Model.Salas)
  ➜ @if (Model?.Salas != null)
     {
       @foreach (var sala in Model.Salas)
       {
         ...
       }
     }
```

---

### Configuration - Program.cs (1 mudança)

#### Program.cs
```
Status: ATUALIZADO
Mudanças:
  - Removido: using Projeto_Dotnet8.Data;
  - Removido: using Projeto_Dotnet8.Repository;
  + Adicionado: using Projeto_Dotnet8.Data.Contexts;
  - Removido: builder.Services.AddControllers();
  - Removido: builder.Services.AddScoped<IcomputadorRepository, ComputadorRepository>();
  - Removido: builder.Services.AddScoped<ISalaRepository, SalaRepository>();
```

---

### Views - Global Imports (1 mudança)

#### Views/_ViewImports.cshtml
```
Status: ATUALIZADO
Mudanças:
  @using Projeto_Dotnet8
  @using Projeto_Dotnet8.Models
  @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
  ➜ @using Projeto_Dotnet8
     @using Projeto_Dotnet8.Models
     @using Projeto_Dotnet8.Models.Entities
     @using Projeto_Dotnet8.Models.ViewModels
     @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

## 📊 Resumo Estatístico

| Tipo | ATUALIZADO | REFEITO | ADICIONADO | REMOVIDO |
|------|-----------|---------|-----------|----------|
| Controllers | 3 | 0 | 6 imports | 3 imports |
| ViewModels | 2 | 0 | 6 ? | 0 |
| Views | 2 | 0 | 1 null-check | 0 |
| Program.cs | 1 | 0 | 1 import | 3 lines |
| _ViewImports | 1 | 0 | 2 usings | 0 |
| **TOTAL** | **9** | **0** | **16** | **6** |

---

**Total de Mudanças**: 9 arquivos

**Data**: 25 de Novembro de 2025
