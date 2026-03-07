destination="./tmp"
filter="[0-9A-Za-z]_"

mkdir -p "$destination"
find .. -maxdepth 1 -type f | grep -E "$filter" | xargs -I {} cp {} "$destination"
cp ../utils.glsl "$destination"
# if you don't have zip installed and can't install it just zip the folder manually
zip -r submission.zip "$destination" && rm -rf "$destination"