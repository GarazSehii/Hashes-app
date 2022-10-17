dotnet restore
dotnet build
start /d ".\HashHandler" dotnet run args
start /d ".\HashHandler.Processor" dotnet run args