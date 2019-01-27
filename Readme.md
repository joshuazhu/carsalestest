# Carsales Test
#### Run Docker
$ docker pull joshuazhu1989/carsalestest

$ docker run --rm -p 8080:80 joshuazhu1989/carsalestest

visit http://localhost:8080/swagger/index.html to see swagger page

#### API access restriction

CORS policy is used to restrict API could only be access from 3 application.

```
services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationA.com"));
    options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationB.com"));
    options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://applicationC.com"));
});
```
