
# run: docker build --network=host --no-cache .
# reference: https://blog.setapp.pl/how-to-use-sonarscanner

FROM guilhermelionzo/sonar-scanner-dotnet5:latest AS sonarqube_scan

WORKDIR /app

COPY . .

RUN dotnet restore

# Running tests with following parameters generate code coverage report
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
RUN ls -list
# sonar.cs.opencover.reportsPaths - a similar case like for javascript code coverage parameter
# we need to pass paths to coverage reports.
RUN dotnet sonarscanner begin \
/k:"MY_PROJECT_KEY" \
# /n:"SONAR_NET_PROJECT_NAME" \
# /v:"SONAR_NET_PROJECT_VERSION" \
/d:sonar.login="MY_PROJECT_KEY" \
/d:sonar.exclusions="**/wwwroot/**, **/obj/**, **/bin/**" \
/d:sonar.cs.opencover.reportsPaths="tests/FunctionalTests/coverage.opencover.xml,tests/IntegrationTests/coverage.opencover.xml,tests/UnitTests/coverage.opencover.xml"
# --no-incremental option marks the build as unsafe for incremental build. 
# This flag turns off incremental compilation and forces a clean rebuild of the project's dependency graph.
RUN dotnet build --no-incremental
RUN dotnet sonarscanner end /d:sonar.login="MY_PROJECT_KEY"