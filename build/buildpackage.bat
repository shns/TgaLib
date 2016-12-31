@echo off

@cd
set msbuilddir=%PROGRAMFILES(X86)%
if not "%msbuilddir%"=="" goto x64
set msbuilddir=%PROGRAMFILES%
:x64

"%msbuilddir%\MSBuild\14.0\Bin\MSBuild.exe" ..\TgaLib.sln /t:Rebuild /p:Configuration=Release
..\packages\NuGet.CommandLine.3.4.4-rtm-final\tools\NuGet.exe pack ..\TgaLib\TgaLib.csproj -Prop Configuration=Release
pause
