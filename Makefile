.PHONY: build run-local

build:
	cd ./GeolocationAPI && docker build . -f ./GeolocationAPI/Dockerfile -t piolas/geolocation-api

run-local: build
	docker run piolas/geolocation-api

