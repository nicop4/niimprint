FROM python:3.11-slim

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

COPY . /app

WORKDIR /app

RUN pip install --no-cache-dir -r requirements.txt
RUN poetry install
ENV PYTHONPATH=/app

CMD ["python", "niimprint/test_env.py"]
