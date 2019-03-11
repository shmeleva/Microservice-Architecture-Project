## Service Architecture

### Services

| Service                                 | Description |
| --------------------------------------- | -------------|
| [Identity](./src/Services/Identity)     | Issues JWT tokens. Stores users in an SQL database. |
| [Carsharing](./src/Services/Carsharing) | Provides methods for searching, booking and returning cars. Stores cars in an SQL database. Requires JWT authentication. *Does not provide methods for creating or removing cars from the database, i.e., cars simply exist. Does not have any payment mechanism, i.e., cars are free.* |
| [Geocoding](./src/Services/Geocoding)   | Provides methods for converting an address to coordinates and vice versa. |

#### Identity

##### Technologies
*ASP.NET Core 2.1. Microsoft SQL Server.*

#### API

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>POST api/{v1&#124;v2}/identity/create</code>     | Creates user with the specified username and password. Accepts JSON. |
| <code>GET api/{v1&#124;v2}/identity/jwt</code> <br><br> **Query parameters:** <br> `username`: `string`, required <br> `password`: `string`, required | Issues a JWT for the specified user. |

#### Carsharing

##### Technologies
*ASP.NET Core 2.1. Microsoft SQL Server.*

#### API

| Method                                 | Description |
| --------------------------------------- | -------------|
| <code>GET api/{v1&#124;v2}/carsharing/cars</code> <br><br> **Query parameters:** <br> `latitude`: `double`, required <br> `longitude`: `double`, required <br> `radius`: `double`, required    | Searches for available cars in the specified area. Caches search results. |
| <code>POST api/{v1&#124;v2}/carsharing/book</code> | Books a car with the specified `Id`. Accepts JSON. |
| <code>POST api/{v1&#124;v2}/carsharing/return</code> | Marks a car with the specified `Id` as available and updates its location. Accepts JSON. |

#### Geocoding

Stateless.

##### Technologies
*ASP.NET Core 2.1.*

...

### Big Picture

*More to be added.*

## Aspects

### Service Discovery

*To be added.*

**References:**
[1]

### Versioning

*To be added.*

**References:**
[1]

### Caching

*To be added.*

**References:**
[1]
