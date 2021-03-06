# Scaffold-DbContext #
Per generare le classi .cs di entità da un database aprire la *Package Manager Console*, 
impostare come Default Project il progetto al quale si vuole aggiungere le classi e 
inserire il seguente comando (ovviamente specificando la Connection String adeguata):

``
Scaffold-DbContext "Server=MyServer;Database=myDB;Trusted_Connection=True;Application Name=A" Microsoft.EntityFrameworkCore.SqlServer
-OutputDir Models -Context "LeMAContext" -StartupProject "Repower.LeMA.API"
```

**I packages NuGet necessari sono i seguenti:**
```xml
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.1" />  
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.0.0" />   
</ItemGroup>
```

Se l'operazione va a buon fine nella folder **Models** del progetto verranno create le classi entità e il file LeMAContext.cs.

**N.B.**

1) In caso di modifiche alla base dati, per rigenerare le classi e sovrascriverle, appendere il flag "-f" al comando Scaffold-DbContext precedente
2) Il flag -StartupProject è necessario se il progetto target è una *class library* (assembly .dll).
