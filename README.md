## 0. Deployment

For the sake of simplicity, everything exists inside Docker Compose. Run to build and start the system:

```
docker-compose up --build
```
All methods are available through this API gateway.

#### Public IP addresses

##### API Gateway
`http://localhost:4000`

##### Consul
`http://localhost:48500`

##### Redis
`http://localhost:46379`

## I. System Architecture

| Service                                 | Description |
| --------------------------------------- | -------------|
| [Identity](./src/Services/Identity)     | Issues JWT tokens. Stores users in an MongoDB database. |
| [Carsharing](./src/Services/Carsharing) | Provides methods for searching, booking and returning cars. Stores cars in a MongoDB database. Requires JWT authentication. *Does not provide methods for creating or removing cars from the database, i.e., cars simply exist. Does not have any payment mechanism, i.e., cars are free.* |
| [Geocoding](./src/Services/Geocoding)   | Provides methods for converting an address to coordinates and vice versa. |

![System Architecture](/docs/images/diagram.png)

#### 1. Identity Service

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>POST&nbsp;api/v1/identity/create</code> <br><br> **Query parameters:** <br> `username`: `string`, required, unique <br> `password`: `string`, required     | Creates a user with the specified username and password. |
| <code>GET&nbsp;api/v1/identity/jwt</code> <br><br> **Query parameters:** <br> `username`: `string`, required <br> `password`: `string`, required | Issues a JWT for the specified user. |

#### 2. Carsharing

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>GET&nbsp;api/v1/cars/available</code> <br><br> **Query parameters:** <br> `latitude`: `double`, required <br> `longitude`: `double`, required <br> `radius`: `double` *(meters)*, 500 meters by default    | Searches for available cars in the specified area. *Only offers mock functionality, i.e., ignores coordinates and returns all available cars.* |
| <code>POST&nbsp;api/v1/cars/book</code> <br><br> `{ CarId: string }` | Books a car with the specified `Id`. Accepts JSON. <br> *Not thread-safe.* |
| <code>POST&nbsp;api/v1/cars/return</code> <br><br> `{ CarId: string, Latitude: double, Longitude: double }` | Marks a car with the specified `Id` as available and updates its location. Accepts JSON. |

*Requires JWT authentication:*
```
Authorization: Bearer <token>
```
*Tokens are issued by* `api/v1/identity/jwt`.

#### 3. Geocoding

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>GET&nbsp;api/v1/geocode/forward</code> <br><br> **Query parameters:** <br> `address`: `string`, required | Returns coordinates of the specified location. Only works for Uusimaa. |
| <code>GET&nbsp;api/v1/geocode/reverse</code> <br><br> **Query parameters:** <br> `latitude`: `double`, required <br> `longitude`: `double`, required | Returns an address of the specified location. |

Source: [HERE Geocoder API](https://developer.here.com/documentation/geocoder/topics/what-is.html).

#### 4. API Gateway

API gateway [exposes](/src/ApiGateway/ocelot.json) with their original routes, e.g., `api/v1/cars/available` is mapped to `api/v1/cars/available`.

## II. Aspects

### 1. Service Discovery

(1) Services [register](https://learn.hashicorp.com/consul/getting-started/services) themselves in [Consul](https://www.consul.io/) on startup:
```
var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
var port = 80;
...
consulClient.Agent.ServiceRegister(serviceRegistration).Wait();
```
(2) Services also [register](https://learn.hashicorp.com/consul/getting-started/checks) their `api/v1/health` methods so that Consul can run health checks.

(3) API Gateway is implemented with [Ocelot](https://ocelot.readthedocs.io/en/latest/) that offers a support for [Consul service discovery](https://ocelot.readthedocs.io/en/latest/features/servicediscovery.html):

```
"ServiceDiscoveryProvider": {
  "Type": "Consul",
  "Host": "consul",
  "Port": 8500
}
```

Consul is only used to discover service IPs. MongoDB, Redis and Consul itself are accessed through their container names.

### 2. Dynamic Configuration

[`consul-template`](https://github.com/hashicorp/consul-template) daemon is used for dynamic configuration. At this point, only `Carsharing` mircoservice includes support for dynamic configuration.

In order to change a configuration, a file with `Carsharing/appsettings.json` key must be created in `http://localhost:48500/ui/dc1/kv`. This will replace [original service configuration file](/src/Services/Carsharing/appsettings.json).

`consul-template` is started in a Dockerfile and runs alongside with `Carsharing.dll`:

```
./consul-template -consul-addr "consul:8500" -template "appsettings.tpl:appsettings.json"
dotnet Carsharing.dll
```

### 3. Caching

`Geocoding` mircoservice caches the data retrieved from [HERE Geocoder API](https://developer.here.com/documentation/geocoder/topics/what-is.html) in a Redis distributed cache:

```
services.AddDistributedRedisCache(options =>
{
    options.Configuration = "redis:6379";
});
```

### 4. Authentication

`Identity` mircoservice stores users in a MongoDB:
```
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Username")]
    public string Username { get; set; }

    [BsonElement("Salt")]
    public string Salt { get; set; }

    [BsonElement("Hash")]
    public string Hash { get; set; }
}
```

It validates `username`+`password` combinations and issues JWT tokens. `Identity` and `Carsharing` mircoservices share the symmetric security key.

```
services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        ...
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ...
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret")),
        };
    });
```

#### References
* [Microsoft – Designing a microservice-oriented application](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/microservice-application-design)
* [Microsoft – Implement the microservice application layer using the Web API](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)
* [.NET Application Architecture: Reference Apps – eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
* [Microsoft – Data considerations for microservices](https://docs.microsoft.com/en-us/azure/architecture/microservices/design/data-considerations)
* [Microsoft – The API gateway pattern versus the Direct client-to-microservice communication](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern)
* [Pitstop – Garage Management System](https://github.com/EdwinVW/pitstop)
* [Building Microservices with ASP.NET Core by Kevin Hoffman – Chapter 8. Service Discovery](https://www.oreilly.com/library/view/building-microservices-with/9781491961728/ch08.html)\*
* [GoogleCloudPlatform – Hipster Shop: Cloud-Native Microservices Demo Application](https://github.com/GoogleCloudPlatform/microservices-demo)
* [Make secure .NET Microservices and Web Applications](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/secure-net-microservices-web-applications/)
