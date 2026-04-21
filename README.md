# Training Center API

Prosta aplikacja ASP.NET Core Web API do zarządzania salami i rezerwacjami w centrum szkoleniowym.
Dane przechowywane są w pamięci aplikacji (bez bazy danych).

## Funkcjonalności

* zarządzanie salami (CRUD)
* zarządzanie rezerwacjami (CRUD)
* filtrowanie danych przez query string
* walidacja danych wejściowych
* obsługa statusów HTTP (200, 201, 204, 404, 409)

## Technologie

* .NET 8
* ASP.NET Core Web API
* Swagger

## Jak uruchomić

1. Otwórz projekt w Riderze / Visual Studio
2. Uruchom aplikację
3. Wejdź na:

```
https://localhost:{port}/swagger
```

## Przykładowe endpointy

* GET `/api/rooms`
* GET `/api/rooms/{id}`
* POST `/api/rooms`
* GET `/api/reservations`
* POST `/api/reservations`

## Uwagi

* dane są zapisane w pamięci (AppData)
* brak bazy danych i Entity Framework
* rezerwacje nie mogą się nakładać czasowo
