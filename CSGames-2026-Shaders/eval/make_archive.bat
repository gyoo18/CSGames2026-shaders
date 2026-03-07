git clone .. tmp
: zip -P "DSD2026" -r csgames_2026_shaders.zip ../
"C:\Program Files\7-Zip\7z.exe" a -p"DSD2026" -bsp1 -mem=AES256 csgames_2026_shaders.zip tmp
del /f /q /s tmp

: To extract the archive, use 7z > extract all to be prompted for password
: or '7z x -p"DSD2026" csgames_2026_shaders.zip'
: on linux, 'unzip -P "DSD2026" csgames_2026_shaders.zip'