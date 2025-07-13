# 📻 Radio Manager API

---

## 🚀 Uruchomienie docker

Do poprawnego uruchomienia projektu wymagany jest zainstalowany Docker.

1.  **Sklonuj repozytorium:**
    ```bash
    git clone https://github.com/Mefju08/RadioManager.git
    ```

2.  **Uruchom kontenery Dockera:**
    W głównym folderze projektu wykonaj polecenie, aby zbudować i uruchomić aplikację oraz bazę danych.
    ```bash
    make docker-run
    ```
✅ Po pomyślnym uruchomieniu, API będzie dostępne pod adresem: `http://localhost:8080/swagger`

--- 

## 🚀 Uruchomienie lokalne

Aby uruchomić aplikację za pośrednictwem IDE, należy utworzyć kontener z bazą danych za pomocą polecenia
 ```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 --name radiomanager-sql-server -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## 📡 Przykładowe Zapytania do API

Poniżej znajdują się przykładowe zapytania `curl` do interakcji z API.

#### Dodawanie nowej audycji

```bash
curl -X POST http://localhost:8080/api/shows -H "Content-Type: application/json" -d "{\"presenter\": \"Paweł Nowak\", \"title\": \"Druga audycja\", \"startTime\": \"2030-12-17T15:00:00Z\", \"durationMinutes\": 20}"
```
#### Pobieranie listy audycji z danego dnia  
```bash
curl -X GET http://localhost:8080/api/shows?date=2030-12-17
```
---

## 📡 Przykładowe Zapytania do API
```bash
.
├── src/
│   ├── RadioManager.Api/           # Warstwa Prezentacji (Kontrolery API)
│   ├── RadioManager.Application/   # Warstwa Aplikacji (Logika, Commandy, Query)
│   ├── RadioManager.Domain/        # Warstwa Domeny (Encje, Reguły biznesowe)
│   └── RadioManager.Infrastructure/  # Warstwa Infrastruktury (Baza danych, obsługa wyjątków)
│
├── tests/
│   ├── RadioManager.Unit.Tests/      # Testy jednostkowe
│   └── RadioManager.Integration.Tests/ # Testy integracyjne
│
├── docker-compose.yml                # Definicja kontenerów Docker
├── Dockerfile                        # Definicja obrazu Docker dla aplikacji
└── Makefile                          # Skrypty pomocnicze
```

---
