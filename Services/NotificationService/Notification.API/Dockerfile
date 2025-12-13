# Слой для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Слой для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем ВСЕ необходимые папки для работы Shared и текущего сервиса:
# 1. Общие модули
COPY ./EducationPortal/Shared/ ./EducationPortal/Shared/

# 2. Все Domain слои, от которых зависит Shared
COPY ./EducationPortal/Services/AuthService/Auth.Domain/ ./EducationPortal/Services/AuthService/Auth.Domain/
COPY ./EducationPortal/Services/CourseService/Course.Domain/ ./EducationPortal/Services/CourseService/Course.Domain/
COPY ./EducationPortal/Services/EmailService/Email.Domain/ ./EducationPortal/Services/EmailService/Email.Domain/
COPY ./EducationPortal/Services/EnrollmentService/Enrollment.Domain/ ./EducationPortal/Services/EnrollmentService/Enrollment.Domain/
COPY ./EducationPortal/Services/NotificationService/Notification.Domain/ ./EducationPortal/Services/NotificationService/Notification.Domain/
COPY ./EducationPortal/Services/OrderService/Order.Domain/ ./EducationPortal/Services/OrderService/Order.Domain/
COPY ./EducationPortal/Services/ResultService/Result.Domain/ ./EducationPortal/Services/ResultService/Result.Domain/

# 3. Текущий сервис целиком
COPY ./EducationPortal/Services/NotificationService/ ./EducationPortal/Services/NotificationService/

# Восстанавливаем и публикуем проект
RUN dotnet restore "EducationPortal/Services/NotificationService/Notification.API/Notification.API.csproj"
WORKDIR "/src/EducationPortal/Services/NotificationService/Notification.API"
RUN dotnet publish -c Release -o /app/publish --no-restore -nowarn:NU1605,NU1608

# Финальный слой
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Notification.API.dll"]