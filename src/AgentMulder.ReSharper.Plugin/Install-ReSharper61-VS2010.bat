mkdir "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder" 2> NUL
mkdir "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder\Containers" 2> NUL
copy /y AgentMulder.ReSharper.Domain.dll "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder" > NUL
copy /y AgentMulder.ReSharper.Domain.pdb "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder" > NUL
copy /y AgentMulder.ReSharper.Plugin.dll "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder" > NUL
copy /y AgentMulder.ReSharper.Plugin.pdb "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder" > NUL

copy /y Containers\AgentMulder.Containers.CastleWindsor.dll "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder\Containers" > NUL
copy /y Containers\AgentMulder.Containers.CastleWindsor.pdb "%APPDATA%\JetBrains\ReSharper\v6.1\vs10.0\Plugins\AgentMulder\Containers" > NUL

