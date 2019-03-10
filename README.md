## Service Architecture

| Service                                 | Description |
| --------------------------------------- | -------------|
| [Carsharing](./src/Services/Carsharing) | Provides methods for searching, booking and returning cars. Stores cars in an SQL database. Requires JWT authentication. |
| [Identity](./src/Services/Identity)     | Issues JWT tokens. Stores users in an SQL database. |
| [Geocoding](./src/Services/Geocoding)   | Provides methods for converting an address to coordinates and vice versa. |
