# ProfanityCheckerService
- Service is currently deployed at AWS. The Swagger UI for the service can be viewed [here](http://profanitychecker-env.eba-7yhnkdes.us-east-2.elasticbeanstalk.com)
- Service also has exposed metrics which can be viewed [here](http://profanitychecker-env.eba-7yhnkdes.us-east-2.elasticbeanstalk.com/metrics). This should be viewed with `Prometheus` for better experience.

## Steps to run
- Add an environment variable with key `PostgreSQLConnectionString` and value set to the connection string to a PostgreSQL server.
- Add a table to the PostgreSQL server with following command
```sql
create table Profanities(Word text unique, Frequency integer Default 0)
```
- Run the service with `dotnet run` command

## Possible improvements
- Service currently does not cache frequently used words. Using a cache will improve the performance significantly. 
- It is also possible to update the frequency count after every few intervals instead of every time the service is hit.
- JWTs or any other form of tokens should be used to secure the service.
- Currently, CORS is enabled for all origins. This should be limited.