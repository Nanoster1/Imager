Invoke-Expression $PSScriptRoot/minio-init.ps1 || $null
Set-Location "$PSScriptRoot/../docker"
docker-compose -f "./docker-compose.yaml" up --build
