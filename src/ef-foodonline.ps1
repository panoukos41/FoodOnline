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
            -o .\Persistence\Migrations\FoodOnline\ `
            -p .\Infrastructure\Infrastructure.csproj `
            -s .\WebApi\WebApi.csproj `
            -c FoodOnlineDbContext
    
    }
    remove {
        dotnet ef migrations `
            remove `
            -p .\Infrastructure\Infrastructure.csproj `
            -s .\WebApi\WebApi.csproj `
            -c FoodOnlineDbContext
    }
    update {
        dotnet ef database `
            update `
            -p .\Infrastructure\Infrastructure.csproj `
            -s .\WebApi\WebApi.csproj `
            --context FoodOnlineDbContext
    }

}
Read-Host "Press Any Key to exit!"