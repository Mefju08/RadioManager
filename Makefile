SOLUTION_FILE = RadioManager.sln
API_PROJECT = src/RadioManager.Api/RadioManager.Api.csproj
TEST_PROJECT = tests/RadioManager.Tests/RadioManager.Tests.csproj
DOCKER_IMAGE_NAME = radiomanager-api
DOCKER_CONTAINER_NAME = radiomanager-api-container

.PHONY: build test run up down logs docker-build docker-run help
default: help

down:
	@echo "Zatrzymywanie srodowiska deweloperskiego..."
	docker-compose down

build:
	@echo "Kompilowanie projektu..."
	dotnet build $(SOLUTION_FILE) -c Release

run:
	docker-compose up --build -d
	@echo "Aplikacja dostepna pod adresem http://localhost:8080/swagger/index.html"

test:
	@echo "Uruchamianie testów..."
	dotnet test $(SOLUTION_FILE)

docker-build:
	@echo "Budowanie obrazu Docker: $(DOCKER_IMAGE_NAME)..."
	docker build -t $(DOCKER_IMAGE_NAME) .

docker-run:
	docker-compose up --build -d
	@echo "Kontener uruchomiony. Aplikacja dostepna pod adresem http://localhost:8080/swagger/index.html"