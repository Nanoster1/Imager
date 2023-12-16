$envoyFile = '/etc/envoy.yaml'
$envoyTmplFile = '/tmpl/envoy.yaml.tmpl'

Write-Host 'Generating envoy.yaml config file...'
Get-Content -Path $envoyTmplFile | envsubst '' > $envoyFile

Write-Host "Starting Envoy..."
/usr/local/bin/envoy -c $envoyFile
