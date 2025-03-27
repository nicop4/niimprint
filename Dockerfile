# Stage 1: Python environment setup
FROM python:3.11-slim AS python-base

# System dependencies
RUN apt-get update && apt-get install -y \
    bluez \
    libbluetooth-dev \
    libjpeg62-turbo-dev \
    zlib1g-dev \
    libpng-dev \
    curl \
    && rm -rf /var/lib/apt/lists/*

# Poetry install
RUN curl -sSL https://install.python-poetry.org | python3 -

# Add Poetry to PATH
ENV PATH="/root/.local/bin:$PATH"

# Python application setup
COPY . /app
WORKDIR /app

RUN pip install --no-cache-dir -r requirements.txt
RUN poetry install
ENV PYTHONPATH=/app

# Stage 2: .NET application setup
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-base

# Copy .NET source code
WORKDIR /src
COPY ./api/NiimprintApi ./api/NiimprintApi

# Restore and build .NET project
WORKDIR /src/api/NiimprintApi
RUN dotnet restore
RUN dotnet publish -c Release -o /app/dotnet

# Stage 3: Final image combining Python and .NET
FROM python:3.11-slim AS final

# Install .NET runtime
RUN apt-get update && apt-get install -y \
    wget \
    && wget https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x dotnet-install.sh \
    && ./dotnet-install.sh --channel 8.0 --runtime aspnetcore \
    && rm dotnet-install.sh \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

# Copy Python environment
COPY --from=python-base /root/.local /root/.local
COPY --from=python-base /app /app

# Copy .NET application
COPY --from=dotnet-base /app/dotnet /app/dotnet

# Set environment variables
ENV PATH="/root/.local/bin:/root/.dotnet:$PATH"
ENV PYTHONPATH=/app
ENV NIIMPRINT_B1_USB_ADDRESS=/dev/ttyACM1

# Expose port (if needed for .NET or Python APIs)
EXPOSE 8080

# Default command
CMD ["dotnet", "/app/dotnet/NiimprintApi.dll"]