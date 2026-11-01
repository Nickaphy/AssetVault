CLIENT REQUEST
↓
Endpoint translates HTTP input into C# values
↓
Application performs the operation through its ports
↓
Infrastructure talks to SQL Server
↓
Outcome travels back up
↓
Endpoint translates the outcome into an HTTP response
↓
CLIENT RESPONSE


| Request                   | Expected response                     |
| ------------------------- | ------------------------------------- |
| GET assets                | `200` + array                         |
| POST valid asset          | `201` + created asset                 |
| POST blank name           | `400` + explanation                   |
| POST future purchase date | `400` + explanation                   |
| DELETE existing ID        | `204`                                 |
| DELETE same ID again      | `404`                                 |
| Unexpected server failure | Safe error response handled centrally |