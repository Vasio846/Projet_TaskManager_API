ERREURS : ========================================================================
   - charactères ASCII uniquement, (é, è, ç, ...) pas acceptés 


Push : DockerCompose Test (10/10/26) ==============================================
   docker compose up -d --build api
   docker compose ps
   docker compose logs api --tail 50

   $env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5433;Database=taskmanager;Username=postgres;Password=YOUR_PASSWORD"

   dotnet ef migrations list
   dotnet ef database update

   docker compose up -d --build api
   docker compose logs api --tail 50

   $body = @{
      title       = "Apprendre Docker"
      description = "Tester Compose"
      status      = $false
      priority    = 1
   } | ConvertTo-Json -Compress

   Invoke-RestMethod -Method Post -Uri "http://localhost:8080/tasks" `
      -ContentType "application/json" -Body $body

===================================================================================

