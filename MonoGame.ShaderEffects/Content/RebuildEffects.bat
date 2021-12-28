setlocal

rem install mgcb globally: https://www.nuget.org/packages/dotnet-mgcb/
rem eg "dotnet tool install --global dotnet-mgcb --version 3.8.0.1641"
SET TWOMGFX="mgcb"

set expanded_list=
for /f "tokens=*" %%F in ('dir /b *.fx') do call set expanded_list=%%expanded_list%% "%%F"

call %TWOMGFX% %expanded_list% /Platform:WindowsStoreApp /outputDir:DirectX
call %TWOMGFX% %expanded_list% /Platform:Android /outputDir:OpenGL

endlocal

pause