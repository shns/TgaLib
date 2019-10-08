@echo off

@cd
set msbuilddir=%PROGRAMFILES(X86)%
if not "%msbuilddir%"=="" goto x64
set msbuilddir=%PROGRAMFILES%
:x64

rem rebuild solution
"%msbuilddir%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" ..\TgaLib.sln /t:Rebuild /p:Configuration=Release

setlocal enabledelayedexpansion

set runps=powershell -NoProfile -ExecutionPolicy Unrestricted -Command
set tgalib=..\TgaLib\bin\Release\TgaLib.dll

set idpsscript=(Get-Item %tgalib%).BaseName
set id=""
for /f "usebackq tokens=*" %%i in (`%runps% "%idpsscript%"`) do (
	set id=%%i
)

set verpsscript=(Get-Item %tgalib%).VersionInfo.ProductVersion
set version=""
for /f "usebackq tokens=*" %%i in (`%runps% "%verpsscript%"`) do (
	set version=%%i
)

set authorpsscript=(Get-Item %tgalib%).VersionInfo.CompanyName
set author=""
for /f "usebackq tokens=*" %%i in (`%runps% "%authorpsscript%"`) do (
	set author=%%i
)

set descriptionpsscript=(Get-Item %tgalib%).VersionInfo.Comments
set description=""
for /f "usebackq tokens=*" %%i in (`%runps% "%descriptionpsscript%"`) do (
	set description=%%i
)

set nuget=..\packages\NuGet.CommandLine.3.4.4-rtm-final\tools\NuGet.exe
%nuget% pack .\TgaLib.nuspec ^
-properties id=!id!;version="!version!";title=!id!;author=!author!;description="!description!" ^
-Prop Configuration=Release

endlocal

pause
