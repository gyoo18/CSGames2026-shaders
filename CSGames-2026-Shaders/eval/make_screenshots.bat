set "dst=eval\screenshots"
set "java=extern\jdk-27-windows\bin\java.exe"
cd ..

rmdir "%dst%" /s /q
mkdir "%dst%"
%java% -jar "extern/dsd.jar" screenshot "1_mazeart.fs" -o "%dst%\1.png" -w 500 -h 500
%java% -jar "extern/dsd.jar" screenshot "2_ascii.fs" -o "%dst%\2.png" -w 500 -h 500
%java% -jar "extern/dsd.jar" screenshot "3_bayer.fs" -o "%dst%\3.png" -w 888 -h 500
%java% -jar "extern/dsd.jar" screenshot "4_sun.fs" -o "%dst%\4.png" -w 500 -h 888
%java% -jar "extern/dsd.jar" screenshot "5_mandlebrot.fs" -o "%dst%\5.png" -w 500 -h 500
%java% -jar "extern/dsd.jar" screenshot "6_stars.fs" -o "%dst%\6.png" -w 888 -h 500
%java% -jar "extern/dsd.jar" screenshot "7_fractal.fs" -o "%dst%\7.png" -w 500 -h 500
%java% -jar "extern/dsd.jar" video "a_golfing.fs" -d 5 -o "%dst%\a.mp4" -w 888 -h 500
%java% -jar "extern/dsd.jar" video "b_gameoflife.json" -d 5 -o "%dst%\b.mp4" -w 500 -h 500
%java% -jar "extern/dsd.jar" screenshot "d_pathtracing.fs" -o "%dst%\d.png" -w 888 -h 500
%java% -jar "extern/dsd.jar" screenshot "e_retro.json" -o "%dst%\e.png" -w 500 -h 500
: c_snake must be played
cd eval
