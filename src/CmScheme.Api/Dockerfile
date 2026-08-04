FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy solution and project files
COPY CmScheme.slnx .
COPY src/CmScheme.Api/CmScheme.Api.csproj src/CmScheme.Api/
COPY src/CmScheme.Common.Core/CmScheme.Common.Core.csproj src/CmScheme.Common.Core/
COPY src/CmScheme.Common.Infrastructure/CmScheme.Common.Infrastructure.csproj src/CmScheme.Common.Infrastructure/
COPY src/CmScheme.Endpoints.Abstractions/CmScheme.Endpoints.Abstractions.csproj src/CmScheme.Endpoints.Abstractions/
COPY src/CmScheme.Masters.Core/CmScheme.Masters.Core.csproj src/CmScheme.Masters.Core/
COPY src/CmScheme.Masters.Application/CmScheme.Masters.Application.csproj src/CmScheme.Masters.Application/
COPY src/CmScheme.Masters.Infrastructure/CmScheme.Masters.Infrastructure.csproj src/CmScheme.Masters.Infrastructure/
COPY src/CmScheme.Masters.Endpoints/CmScheme.Masters.Endpoints.csproj src/CmScheme.Masters.Endpoints/
COPY src/CmScheme.Registration.Core/CmScheme.Registration.Core.csproj src/CmScheme.Registration.Core/
COPY src/CmScheme.Registration.Application/CmScheme.Registration.Application.csproj src/CmScheme.Registration.Application/
COPY src/CmScheme.Registration.Infrastructure/CmScheme.Registration.Infrastructure.csproj src/CmScheme.Registration.Infrastructure/
COPY src/CmScheme.Registration.Endpoints/CmScheme.Registration.Endpoints.csproj src/CmScheme.Registration.Endpoints/
COPY src/CmScheme.Training.Core/CmScheme.Training.Core.csproj src/CmScheme.Training.Core/
COPY src/CmScheme.Training.Application/CmScheme.Training.Application.csproj src/CmScheme.Training.Application/
COPY src/CmScheme.Training.Infrastructure/CmScheme.Training.Infrastructure.csproj src/CmScheme.Training.Infrastructure/
COPY src/CmScheme.Training.Endpoints/CmScheme.Training.Endpoints.csproj src/CmScheme.Training.Endpoints/
COPY src/CmScheme.WorkAllocation.Core/CmScheme.WorkAllocation.Core.csproj src/CmScheme.WorkAllocation.Core/
COPY src/CmScheme.WorkAllocation.Application/CmScheme.WorkAllocation.Application.csproj src/CmScheme.WorkAllocation.Application/
COPY src/CmScheme.WorkAllocation.Infrastructure/CmScheme.WorkAllocation.Infrastructure.csproj src/CmScheme.WorkAllocation.Infrastructure/
COPY src/CmScheme.WorkAllocation.Endpoints/CmScheme.WorkAllocation.Endpoints.csproj src/CmScheme.WorkAllocation.Endpoints/
COPY src/CmScheme.AttendanceLeave.Core/CmScheme.AttendanceLeave.Core.csproj src/CmScheme.AttendanceLeave.Core/
COPY src/CmScheme.AttendanceLeave.Application/CmScheme.AttendanceLeave.Application.csproj src/CmScheme.AttendanceLeave.Application/
COPY src/CmScheme.AttendanceLeave.Infrastructure/CmScheme.AttendanceLeave.Infrastructure.csproj src/CmScheme.AttendanceLeave.Infrastructure/
COPY src/CmScheme.AttendanceLeave.Endpoints/CmScheme.AttendanceLeave.Endpoints.csproj src/CmScheme.AttendanceLeave.Endpoints/
COPY src/CmScheme.Performance.Core/CmScheme.Performance.Core.csproj src/CmScheme.Performance.Core/
COPY src/CmScheme.Performance.Application/CmScheme.Performance.Application.csproj src/CmScheme.Performance.Application/
COPY src/CmScheme.Performance.Infrastructure/CmScheme.Performance.Infrastructure.csproj src/CmScheme.Performance.Infrastructure/
COPY src/CmScheme.Performance.Endpoints/CmScheme.Performance.Endpoints.csproj src/CmScheme.Performance.Endpoints/
COPY src/CmScheme.Certificate.Core/CmScheme.Certificate.Core.csproj src/CmScheme.Certificate.Core/
COPY src/CmScheme.Certificate.Application/CmScheme.Certificate.Application.csproj src/CmScheme.Certificate.Application/
COPY src/CmScheme.Certificate.Infrastructure/CmScheme.Certificate.Infrastructure.csproj src/CmScheme.Certificate.Infrastructure/
COPY src/CmScheme.Certificate.Endpoints/CmScheme.Certificate.Endpoints.csproj src/CmScheme.Certificate.Endpoints/
COPY src/CmScheme.HelpDesk.Core/CmScheme.HelpDesk.Core.csproj src/CmScheme.HelpDesk.Core/
COPY src/CmScheme.HelpDesk.Application/CmScheme.HelpDesk.Application.csproj src/CmScheme.HelpDesk.Application/
COPY src/CmScheme.HelpDesk.Infrastructure/CmScheme.HelpDesk.Infrastructure.csproj src/CmScheme.HelpDesk.Infrastructure/
COPY src/CmScheme.HelpDesk.Endpoints/CmScheme.HelpDesk.Endpoints.csproj src/CmScheme.HelpDesk.Endpoints/
COPY src/CmScheme.Dashboard.Core/CmScheme.Dashboard.Core.csproj src/CmScheme.Dashboard.Core/
COPY src/CmScheme.Dashboard.Application/CmScheme.Dashboard.Application.csproj src/CmScheme.Dashboard.Application/
COPY src/CmScheme.Dashboard.Infrastructure/CmScheme.Dashboard.Infrastructure.csproj src/CmScheme.Dashboard.Infrastructure/
COPY src/CmScheme.Dashboard.Endpoints/CmScheme.Dashboard.Endpoints.csproj src/CmScheme.Dashboard.Endpoints/
COPY src/CmScheme.Administration.Endpoints/CmScheme.Administration.Endpoints.csproj src/CmScheme.Administration.Endpoints/

RUN dotnet restore CmScheme.slnx

# Copy full source and publish
COPY src/ src/
RUN dotnet publish src/CmScheme.Api/CmScheme.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CmScheme.Api.dll"]
