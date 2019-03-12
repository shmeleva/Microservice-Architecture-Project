## Service Architecture

### Services

| Service                                 | Description |
| --------------------------------------- | -------------|
| [Identity](./src/Services/Identity)     | Issues JWT tokens. Stores users in an SQL database. |
| [Carsharing](./src/Services/Carsharing) | Provides methods for searching, booking and returning cars. Stores cars in an SQL database. Requires JWT authentication. *Does not provide methods for creating or removing cars from the database, i.e., cars simply exist. Does not have any payment mechanism, i.e., cars are free.* |
| [Geocoding](./src/Services/Geocoding)   | Provides methods for converting an address to coordinates and vice versa. |

#### Identity Service

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>POST&nbsp;api/{v1&#124;v2}/identity/create</code>     | Creates a user with the specified username and password. Accepts JSON. |
| <code>GET&nbsp;api/{v1&#124;v2}/identity/jwt</code> <br><br> **Query parameters:** <br> `username`: `string`, required <br> `password`: `string`, required | Issues a JWT for the specified user. |

#### References

* [Make secure .NET Microservices and Web Applications](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/secure-net-microservices-web-applications/)

#### Carsharing

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>GET&nbsp;api/{v1&#124;v2}/carsharing/cars</code> <br><br> **Query parameters:** <br> `latitude`: `double`, required <br> `longitude`: `double`, required <br> `radius`: `double`, required    | Searches for available cars in the specified area. Caches search results. |
| <code>POST&nbsp;api/{v1&#124;v2}/carsharing/book</code> | Books a car with the specified `Id`. Accepts JSON. |
| <code>POST&nbsp;api/{v1&#124;v2}/carsharing/return</code> | Marks a car with the specified `Id` as available and updates its location. Accepts JSON. |

#### Geocoding

Stateless.

*More to be added.*

### Big Picture

*More to be added.*

#### References
* [Microsoft – Designing a microservice-oriented application](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/microservice-application-design)
* [Microsoft – Implement the microservice application layer using the Web API](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)
* [.NET Application Architecture: Reference Apps – eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
* [Microsoft – Data considerations for microservices](https://docs.microsoft.com/en-us/azure/architecture/microservices/design/data-considerations)
* [Microsoft – The API gateway pattern versus the Direct client-to-microservice communication](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern)
* [Pitstop – Garage Management System](https://github.com/EdwinVW/pitstop)
* [Building Microservices with ASP.NET Core by Kevin Hoffman – Chapter 8. Service Discovery](https://www.oreilly.com/library/view/building-microservices-with/9781491961728/ch08.html)\*
* [GoogleCloudPlatform – Hipster Shop: Cloud-Native Microservices Demo Application](https://github.com/GoogleCloudPlatform/microservices-demo)

## Aspects

### Service Discovery

![NGINX – Service Discovery in a Microservices Architecture](https://www.nginx.com/wp-content/uploads/2016/04/Richardson-microservices-part4-3_server-side-pattern.png)
[Image Credit](https://www.nginx.com/blog/service-discovery-in-a-microservices-architecture/)

*More to be added.*

#### References
* [NGINX – Service Discovery in a Microservices Architecture](https://www.nginx.com/blog/service-discovery-in-a-microservices-architecture/)
* [Microservices: From Design to Deployment, a Free Ebook from NGINX](https://www.nginx.com/blog/microservices-from-design-to-deployment-ebook-nginx/)
* [Steeltoe – Service Discovery](https://steeltoe.io/docs/steeltoe-discovery/)
* [Consul](https://www.consul.io/)
* [Auth0 – An Introduction to Microservices, Part 3: The Service Registry](https://auth0.com/blog/an-introduction-to-microservices-part-3-the-service-registry/)
* [Airbnb – Synapse](https://github.com/airbnb/synapse)


### Versioning

*To be added.*

#### References
* [Microsoft – Implementing versioning in ASP.NET Web APIs](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/data-driven-crud-microservice#implementing-versioning-in-aspnet-web-apis)

### Caching

*To be added.*

#### References
* ["Caching at Netflix: The Hidden Microservice" by Scott Mansfield](https://www.youtube.com/watch?v=Rzdxgx3RC0Q)
* [Distributed caching in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-2.2)

### A/B Testing

#### References
* [Testing Microservices, the sane way](https://medium.com/@copyconstruct/testing-microservices-the-sane-way-9bb31d158c16)
* [It’s All A/Bout Testing: The Netflix Experimentation Platform](https://medium.com/netflix-techblog/its-all-a-bout-testing-the-netflix-experimentation-platform-4e1ca458c15)


#### References
*
