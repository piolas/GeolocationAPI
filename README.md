# GeolocationAPI
Recruitment task to create and integrate API with external service that provides geolocation data based on URL or IP address
[![Build Status](https://piotr-laskawski.visualstudio.com/GeolocationAPI/_apis/build/status/piolas.GeolocationAPI?branchName=master)](https://piotr-laskawski.visualstudio.com/GeolocationAPI/_build/latest?definitionId=2&branchName=master)

Goal was to implement simple API that would be capable based on URL or IP parameter fetch set of data using external service - IPStack.com . Using GET method API first checks its own databse for record and in case there is none, make request to IPStack for it and then store it internaly for future use.

Internally API was stuctured in CQRS-kinda way so separate read and write operations. I'm aware it needs to be improved(repository interface should be moved to Domain and Commands/Queries to API project).

## Here is list of tools/libraries I've used:

- Dotnet Core 2.1
- EF Core
- FluentAPI
- FluentValidation
- MediatR
- Polly
- Azure KeyVault
- Docker

## How to run this project?
> Clone code and run API project

> Build docker image using *Dockerfile* and run it as container

If you like it or learnt something please leave a star or comment.
