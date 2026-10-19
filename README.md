# ERREURS : =================================================================
   - charactères ASCII uniquement, (é, è, ç, ...) pas acceptés 


# Push : DockerCompose Test (10/10/26) ======================================

docker compose up -d --build api        # Check wsl version if error
docker compose ps
docker compose logs api --tail 50

$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5433;Database=taskmanager;Username=postgres;Password=YOUR_PASSWORD"

dotnet ef migrations list
dotnet ef database update

docker compose up -d --build api
docker compose logs api --tail 50

### Post 
$body = @{
   title       = "Apprendre Docker"
   description = "Tester Compose"
   status      = $false
   priority    = 1
} | ConvertTo-Json -Compress

Invoke-RestMethod -Method Post -Uri "http://localhost:8080/tasks" `
   -ContentType "application/json" -Body $body

### Get 
Invoke-RestMethod -Uri "http://localhost:8080/tasks" |
   Format-Table Id, Title, Description, Priority, Status, CreatedAt

### Put 
$body = @{
   title       = "New Title"
   description = "Test Update"
   status      = $false
   priority    = 3
} | ConvertTo-Json -Compress

*Replace 1 with task id :* 
Invoke-RestMethod -Method Put -Uri "http://localhost:8080/tasks/1" `
   -ContentType "application/json" -Body $body

### Delete 
*Replace 1 with task id :*
Invoke-RestMethod -Method Delete -Uri "http://localhost:8080/tasks/1"

### To close:
   docker compose down
### To close and delete database data: 
   docker compose down -v

============================================================================

