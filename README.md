# .net Core 3.1, Docker, PostgreSQL, Swagger, C#

## Required
- [Docker](https://www.docker.com/) 

## Recommended
- Local install of [dotnet core 6.1](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [DBBeaver](https://dbeaver.io/) 

## Over view and intro.

Have you been tempted by .net core but not sure where to start?  Well you’re not the only one, in fact it's actually difficult to find a comprehensive story on "Your first .net core API" let alone connect it to Postgres.  This quick start aim to provide you a template that can literally be run with 2 commands and provides the opportunity to continue to build out a local application.

The QuickStart  provides an API that includes:

- GETs
- POSTs
- Connectivity to PostgreSQL
- Swagger integration

This demo app will run completely in docker.  Simply build the container:

```
docker-compose build
```
Build will compile the application and move out entrypoint shell script ready for execution.  This step is important, we want the DB migrations to run when the container starts but before the applications tries to connect.  The migrations will create the tables in the database.  I have added some data to the database for initial viewing as seen in: 20191231091325_initial.cs

Docker file:
```
FROM mccr.microsoft.com/dotnet/sdk:6.0
COPY . /app
WORKDIR /app
RUN dotnet tool install --global dotnet-ef
RUN dotnet restore
RUN dotnet build
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
```

entrypoint.sh
```
#!/bin/bash

set -e
run_cmd="dotnet run --no-build --urls http://0.0.0.0:5000 -v d"

export PATH="$PATH:/root/.dotnet/tools"

until dotnet ef database update; do
    >&2 echo "Migrations executing"
    sleep 1
done

>&2 echo "DB Migrations complete, starting app."
>&2 echo "Running': $run_cmd"
exec $run_cmd
```

Then start
```
docker-compose up
```
This will start the 2 containers.  PostgreSQL is completely standard we simply pass in an enviroment value which is the password for the database ( postgres ), the small piece to add here is that init.sql script is also mounted between the host ( local machine ) and the container.  This script is copied to a loclation that is executed on the start-up of the database container.  If is very simply.  It will drop the "Posts" database if it exists and then create it again, so we start fresh.  This configuration is located in the "docker-compose.yml" file

```
docker-compose.yml:
version: '3'
services:
  web:
    container_name: dotnetCore60
    build: .
    ports:
        - "5005:5000"
    depends_on:
        - database
  database:
    container_name: database
    image: postgres:latest
    ports: 
      - "5432:5432"
    environment:
      - POSTGRES_PASSWORD=password
    volumes:
      - ./init.sql:/docker-entrypoint-initdb.d/init.sql
```

Once the app starts you can interact at:  http://localhost:5005/swagger to test out the API.

You can connect to the database as well by connecting to:  localhost port 5432 with you favourite tool.  I recommend and use DBBeaver: settings are:

```
host:  localhost
port: 5432
database: postgres
user: postgres
password: postgres
```

![Overview](https://raw.githubusercontent.com/kukielp/dotnetcore60quickstart/master/pg-1.png "Overview")

![Overview](https://raw.githubusercontent.com/kukielp/dotnetcore60quickstart/master/pg-2.png "Overview")

If you wish to run the app locally add a lost host entry.  The containers internal DNS knows to resolve databse to the postgres ( name dfined in docker-compse.yml file line 10 )container so adding this alias to your hosts file will allow you project in VSCode or Visual Studio to execute and connect to the database.

Or change the connection string in appsettings.Development.json to localhost ( from database ).

```
#windows:  c:\windows\system32\drivers\etc\hosts
Linux/MacOS: /etc/hosts

127.0.0.1     database
````

I prefer to use Visual Studio, to run the application locally simply double click the pgapp.sln file and click run.  Your app will compile and run locally and be alliable on port 5000

Local url:  http://localhost:5000/swagger 

Local url with posts:  http://localhost:5005/api/posts

Local url for post 1:  http://localhost:5005/api/posts/1

Local url with comments: http://localhost:5005/api/comments

Local url for comment 1: http://localhost:5005/api/comments/1


![Overview](https://raw.githubusercontent.com/kukielp/dotnetcore60quickstart/master/overview.png "Overview")