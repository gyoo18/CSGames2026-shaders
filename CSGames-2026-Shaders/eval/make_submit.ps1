$destination = ".\tmp"
$filter = "^[0-9A-Za-z]_"
New-Item -ItemType Directory -Path $destination -Force
((Get-ChildItem -Path "..\") -match $filter) | Copy-Item -Destination $destination
Copy-item -Path "..\utils.glsl" -Destination $destination
Compress-Archive -Path .\tmp\* -DestinationPath .\submission.zip -Force
Remove-Item -Path $destination -Recurse -Force
