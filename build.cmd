@echo off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.2.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=src\.nuget\nuget.exe
)

%nuget% restore src\AgentMulder.sln
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\AgentMulder.sln /t:Rebuild /p:Configuration="%config%" /m /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false 

set package_id="AgentMulder"
set package_id_90="ReSharper.AgentMulder"

%nuget% pack "src\AgentMulder.nuspec" -NoPackageAnalysis -Version %version% -Properties "Configuration=%config%;ReSharperDep=ReSharper;ReSharperVer=[8.1,8.3);PackageId=%package_id%"
%nuget% pack "src\AgentMulder.9.0.nuspec" -NoPackageAnalysis -Version %version% -Properties "Configuration=%config%;ReSharperDep=Wave;ReSharperVer=[2.0];PackageId=%package_id_90%"