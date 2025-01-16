FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["api.csproj", "."]
RUN dotnet restore "./api.csproj"

# Install the dotnet-ef tool
RUN dotnet tool install --global dotnet-ef

# Ensure the dotnet tools are on the PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy the remaining source files and build/publish the application
COPY . .
RUN dotnet build "./api.csproj" -c Release -o /app/build
RUN dotnet publish "./api.csproj" -c Release -o /app/publish

# Apply migrations during the build stage
# RUN dotnet ef database update --project ./api.csproj

# Use the ASP.NET Core runtime to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
