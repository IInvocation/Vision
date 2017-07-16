dotnet publish ..\FluiTec.Vision.Client.AspNetCoreEndpoint\FluiTec.Vision.Client.AspNetCoreEndpoint.csproj
rmdir \bin\Debug\AspNetCoreEndpoint /S /Q
rmdir \bin\Release\AspNetCoreEndpoint /S /Q
robocopy ..\FluiTec.Vision.Client.AspNetCoreEndpoint\bin\Release\PublishOutput\ .\bin\Debug\AspNetCoreEndpoint /E
robocopy ..\FluiTec.Vision.Client.AspNetCoreEndpoint\bin\Release\PublishOutput\ .\bin\Release\AspNetCoreEndpoint /E