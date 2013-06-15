@echo off
%systemroot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe AgentMulder.proj %*

call powershell -Command "[System.Reflection.Assembly]::LoadFrom('..\output\Release\8.0\AgentMulder.ReSharper.Plugin.dll').GetName().Version.ToString()" > temp
set /p version=<temp
del temp

set nuget=
if "%nuget%" == "" (
	set nuget=.nuget\nuget.exe
)

%nuget% pack ".nuget\AgentMulder.nuspec" -NoPackageAnalysis -verbosity detailed -o . -Version %version%-EAP1553 -p Configuration="Release"