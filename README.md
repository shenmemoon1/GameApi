# Game store api

## Starting Sql Server

```powershell
$sa_password = "[SA PASWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)$sa_password" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name sqlpreview --hostname sqlpreview -v sqlvolume:/var/opt/mssql -d --rm mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
```
