# .net Core 3.1, Docker, postgres, swagger

## Required
-  Docker

This demo app will run completly in docker.  Simply build the container:

```
docker-compose build
```
Then start
```
docker-compose up
```
Build will compile the application.

Up will start the containers.

2 images are used, postgres and a .netcore 3.1 sdk image. 

The database will start up a database "posts" will be created on postgres init.  The applicaiton itself will run the DB migrations before startign the app you can see this in:
```
entrypoint.sh
```

Once the app starts you can interact at:  http://localhost:5000/swagger

You can connect to the database aswell by connecting to:  localhost port 5432 with you favoriute tool.

If you wish to run the app locally add a lost host entry

```
127.0.0.1     database
````

Or chaneg the connection string in appsettings.Development.json to localhost ( from database ).