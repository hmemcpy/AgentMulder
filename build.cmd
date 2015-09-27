@echo off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.2.92.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=src\.nuget\nuget.exe
)

%nuget% restore src\AgentMulder.sln
"%ProgramFiles(x86)%\MSBuild\14.0\Bin\msbuild" src\AgentMulder.sln /t:Rebuild /p:Configuration="%config%" /m /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false 

set package_id="ReSharper.AgentMulder"

%nuget% pack "src\AgentMulder.nuspec" -NoPackageAnalysis -Version %version% -Properties "Configuration=%config%;ReSharperDep=Wave;ReSharperVer=[3.0];PackageId=%package_id%"