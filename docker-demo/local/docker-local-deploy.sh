#!/bin/bash

# perform dotnet compilation
dotnet clean ../**/*.csproj
dotnet build ../**/*.csproj -c Release
dotnet publish ../**/*.csproj -c Release -o publish --no-build --no-self-contained

# run docker builds
docker build -f Dockerfile-local -t sample:local .
docker-compose -f docker-compose-local.yml up -d

# clean up the publish folder
rm -rf publish