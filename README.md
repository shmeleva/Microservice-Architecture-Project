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
| <code>POST&nbsp;api/{v1&#124;v2}/identity/create</code>     | Creates a user with the specified username and password. Accepts JSON. |
| <code>GET&nbsp;api/{v1&#124;v2}/identity/jwt</code> <br><br> **Query parameters:** <br> `username`: `string`, required <br> `password`: `string`, required | Issues a JWT for the specified user. |

#### References

* [Make secure .NET Microservices and Web Applications](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/secure-net-microservices-web-applications/)

#### 2. Carsharing

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>GET&nbsp;api/{v1&#124;v2}/carsharing/cars</code> <br><br> **Query parameters:** <br> `latitude`: `double`, required <br> `longitude`: `double`, required <br> `radius`: `double`, required    | Searches for available cars in the specified area. Caches search results. |
| <code>POST&nbsp;api/{v1&#124;v2}/carsharing/book</code> | Books a car with the specified `Id`. Accepts JSON. |
| <code>POST&nbsp;api/{v1&#124;v2}/carsharing/return</code> | Marks a car with the specified `Id` as available and updates its location. Accepts JSON. |

#### 3. Geocoding

Stateless.

*More to be added.*

## II. Aspects

### 1. Service Discovery

Consul.

[Consul](https://www.consul.io/)

### 2. Dynamic Configuration

Consul.

### 3. Caching

A sidecar between

#### References
* [Microsoft – Designing a microservice-oriented application](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/microservice-application-design)
* [Microsoft – Implement the microservice application layer using the Web API](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)
* [.NET Application Architecture: Reference Apps – eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
* [Microsoft – Data considerations for microservices](https://docs.microsoft.com/en-us/azure/architecture/microservices/design/data-considerations)
* [Microsoft – The API gateway pattern versus the Direct client-to-microservice communication](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern)
* [Pitstop – Garage Management System](https://github.com/EdwinVW/pitstop)
* [Building Microservices with ASP.NET Core by Kevin Hoffman – Chapter 8. Service Discovery](https://www.oreilly.com/library/view/building-microservices-with/9781491961728/ch08.html)\*
* [GoogleCloudPlatform – Hipster Shop: Cloud-Native Microservices Demo Application](https://github.com/GoogleCloudPlatform/microservices-demo)
