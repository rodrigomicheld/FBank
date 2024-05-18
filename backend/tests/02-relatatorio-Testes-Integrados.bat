dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

reportgenerator -reports:"D:\00-Codify\FBank\backend\tests\IntegrationTests\coverage.cobertura.xml" -targetdir:"D:\00-Codify\FBank\backend\tests\CoverageResults\IntegrationTests" -reporttypes:Html