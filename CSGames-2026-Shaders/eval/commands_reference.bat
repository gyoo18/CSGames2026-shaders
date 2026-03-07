set "dst=eval\screenshots"
set "java=extern\jdk-27-windows\bin\java.exe"

%java% -jar "extern/dsd.jar" run "1_mazeart.fs"
%java% -jar "extern/dsd.jar" run "2_ascii.fs"
%java% -jar "extern/dsd.jar" run "3_bayer.fs" -w 888 -h 500
%java% -jar "extern/dsd.jar" run "4_sun.fs" -w 500 -h 888
%java% -jar "extern/dsd.jar" run "5_mandlebrot.fs" -w 888 -h 500
%java% -jar "extern/dsd.jar" run "6_stars.fs" -w 888 -h 500
%java% -jar "extern/dsd.jar" run "7_fractal.fs
%java% -jar "extern/dsd.jar" run "a_golfing.fs" -w 888 -h 500
%java% -jar "extern/dsd.jar" run "b_gameoflife.json" -rte -w 500 -h 500
%java% -jar "extern/dsd.jar" run "c_snake.json" -rte
%java% -jar "extern/dsd.jar" run "d_pathtracing.fs" -w 888 -h 500
%java% -jar "extern/dsd.jar" run "e_retro.json" -w 500 -h 500