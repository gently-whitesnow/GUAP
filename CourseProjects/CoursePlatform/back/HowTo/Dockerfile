FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src

COPY ./*.sln ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore
COPY ./ ./
RUN dotnet publish ./HowTo/ -o /publish -c Release --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build-env /publish .

# change timezone from ETC to local
RUN apt update && apt-get install -y --no-install-recommends apt-utils && apt install tzdata -y
ENV TZ="Europe/Moscow"

ENTRYPOINT ["dotnet","HowTo.dll"]
