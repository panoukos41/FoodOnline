[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [ValidateSet("add", "remove", "update")]
    [string] $Command,

    $MigrationName = $null
)

switch -Exact ($Command) {
    add { 
        $MigrationName ??= Read-Host "Please provide a migration name"
        if (!$MigrationName) { Write-Error "Please provide a migration name!"; exit }
        dotnet ef migrations `
            add `
            $MigrationName `
            -o .\Persistence\Migrations\App\ `
            -s .\WebApi\WebApi.csproj `
            -p .\Infrastructure\Infrastructure.csproj `
            -c ApplicationDbContext
    
    }
    remove {
        dotnet ef migrations `
            remove `
            -s .\WebApi\WebApi.csproj `
            -p .\Infrastructure\Infrastructure.csproj `
            -c ApplicationDbContext
    }
    update {
        dotnet ef database `
            update `
            -s .\WebApi\WebApi.csproj `
            -p .\Infrastructure\Infrastructure.csproj `
            --context ApplicationDbContext
    }

}
Read-Host "Press Any Key to exit!"