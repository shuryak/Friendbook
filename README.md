# Friendbook Social Network

## Migrations

```bash
cd Friendbook.DataAccess.PostgreSql
```

```bash
dotnet ef --startup-project ../Friendbook.Api/ migrations add <name_of_migration>
```

```bash
dotnet ef --startup-project ../Friendbook.Api/ database update
```

## Messages ERD

![Messages ERD](images/messages-erd.png)

## WebSockets (via SignalR)

[SignalR Web Client](https://gourav-d.github.io/SignalR-Web-Client/dist/)
