# Docker Deployment Guide

This guide explains how to deploy the PrepareToInterview API with PostgreSQL using Docker.

## Prerequisites

- Docker installed on your remote server
- Docker Compose installed on your remote server
- Git (to clone the repository)

## Quick Start

1. **Clone the repository to your remote server:**
   ```bash
   git clone <your-repository-url>
   cd PrepareToInterview
   ```

2. **Build and start the containers:**
   ```bash
   docker-compose up -d
   ```

3. **Check if containers are running:**
   ```bash
   docker-compose ps
   ```

4. **View logs:**
   ```bash
   # View all logs
   docker-compose logs
   
   # View specific service logs
   docker-compose logs api
   docker-compose logs postgres
   ```

## Access Points

- **API**: http://your-server-ip:5000
- **Swagger UI**: http://your-server-ip:5000
- **PostgreSQL**: localhost:5432 (or your-server-ip:5432)

## Database Migration

The application will automatically run migrations when it starts. If you need to run migrations manually:

```bash
# Run migrations
docker-compose exec api dotnet ef database update
```

## Environment Variables

You can customize the deployment by setting environment variables in the `docker-compose.yml` file:

- `POSTGRES_PASSWORD`: PostgreSQL password
- `ASPNETCORE_ENVIRONMENT`: .NET environment (Production/Development)
- `ConnectionStrings__PostgreSQL`: Database connection string

## Data Persistence

- PostgreSQL data is persisted in a Docker volume named `postgres_data`
- User uploaded images are persisted in `./wwwroot/images` directory

## Security Considerations

1. **Change default passwords** in production
2. **Use environment variables** for sensitive data
3. **Configure firewall** to only expose necessary ports
4. **Use HTTPS** in production (configure reverse proxy)

## Production Deployment

For production deployment, consider:

1. **Using a reverse proxy** (nginx/traefik) for SSL termination
2. **Setting up proper logging** and monitoring
3. **Configuring backup strategies** for the database
4. **Using Docker secrets** for sensitive data

## Troubleshooting

### Container won't start
```bash
# Check logs
docker-compose logs api

# Check if database is accessible
docker-compose exec postgres psql -U postgres -d PrepareToInterviewAPI
```

### Database connection issues
- Ensure PostgreSQL container is running: `docker-compose ps`
- Check connection string in environment variables
- Verify network connectivity between containers

### Port conflicts
- Change ports in `docker-compose.yml` if needed
- Ensure ports are not used by other services

## Commands Reference

```bash
# Start services
docker-compose up -d

# Stop services
docker-compose down

# Rebuild and start
docker-compose up -d --build

# View logs
docker-compose logs -f

# Access container shell
docker-compose exec api sh
docker-compose exec postgres psql -U postgres

# Remove everything (including volumes)
docker-compose down -v
``` 