rm -rf bin obj && dotnet restore InvalidProgram-net9.csproj && dotnet run --project InvalidProgram-net9.csproj -f net9.0-ios -c Release -p:ArchiveOnBuild=true -r:ios-arm64 /p:_DeviceName=$1 -v:n
