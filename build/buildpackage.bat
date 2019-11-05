@echo off

@cd
set msbuilddir=%PROGRAMFILES(X86)%
if not "%msbuilddir%"=="" goto x64
set msbuilddir=%PROGRAMFILES%
:x64

rem rebuild solution
"%msbuilddir%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" ..\TgaLib.sln /t:Rebuild /p:Configuration=Release
