Per generare le entità dal database LeMA aprire la Package Manager Console, 
impostare come Default Project Repower.LeMA.DAL e inserire il seguente comando (ovviamente specificando la Connection String adeguata):

Scaffold-DbContext "Server=khors;Database=LeMA;Trusted_Connection=True;Application Name=Repower.LeMA" Microsoft.EntityFrameworkCore.SqlServer
-OutputDir Models -Context "LeMAContext" -StartupProject "Repower.LeMA.API"

I packages NuGet necessari sono i seguenti:
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.1" />  
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.0.0" />   
</ItemGroup>

Se l'operazione va a buon fine nella folder Models del progetto Repower.LeMA.DAL verranno create le classi entità e il file LeMAContext.
N.B.
1) In caso di modifiche alla base dati, per rigenerare le classi e sovrascriverle, appendere il flag "-f" al comando Scaffold-DbContext precedente
2) Il flag -StartupProject è necessario perchè bisogna specificare un file eseguibile (LeMA.DAL è una class library)
3) La classe LeMAContext generata configura una proprietà dell'entità Activations come colonna calcolata andando in errore. Per il momento ho commentato quella riga in quanto non necessaria (ci pensa SQL Server a valorizzare il contenuto della colonna).
// entity.Property(e => e.IsActivationVerified)
//    .IsRequired()
//    .HasComputedColumnSql("((([IsEmailPecVerified]&[IsOTPVerified])&[IsOTSDLVerified])&[IsValid])");