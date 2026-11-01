# Project workflow
Build one feature end to end at a time.

## 1. Requirements and business rules
Define what the application does, its data, and what makes an operation valid.

## 2. Architecture and OpenAPI contract
Separate responsibilities and describe the API's routes, inputs, outputs, and status codes.

## 3. Core, Application, and ports
Implement entities and business rules, then use cases and interfaces for external dependencies.

## 4. Database, EF Core, and migrations
Run the database with persistent storage. Configure the DbContext, generate migrations, and apply them.

## 5. Infrastructure and DI
Implement the ports using EF Core. Register implementations and application services at startup.

## 6. Endpoints, responses, and errors
Translate HTTP requests into application calls and outcomes into HTTP responses. Handle unexpected failures centrally.

## 7. Aspire startup
Declare applications and dependencies in AppHost so they start together with the required configuration.

## 8. Verification
Test success, invalid input, missing resources, and persistence after restarting.
