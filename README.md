# PlanetoR
Back-end API for retrieving and saving informationa about celestial bodies and satellites in our universe written in ASP.NET/C#


### Available endpoints:
## Auth:
## Register new user:
POST @ https://planetor-production.up.railway.app/api/auth/register

"username" select your username

"password" select your password

"email" enter your email-address
``` Json
{
  "username": "username",
  "password": "password",
  "email": "email"
}
```


## Sign in user:
POST @ https://planetor-production.up.railway.app/api/auth/login

"username" enter your username or email-address used when signed up

"password" enter your password
``` Json
{
  "username": "username",
  "password": "password",
}
```
Returns JWT as string


## Update password:
PUT @ https://planetor-production.up.railway.app/api/auth/update-password

"username" enter your username

"password" enter your password

"newPassword" enter your new password
``` Json
{
  "username": "jensis",
  "password": "qwerty",
  "newPassword": "password"
}
```


## Celestial Bodies:
## Get all celestial bodies (Must be signed in on user- or admin account):
GET @ https://planetor-production.up.railway.app/api/celestialbodies/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER


## Get celestial body by id (Must be signed in on user- or admin account):
GET @ https://planetor-production.up.railway.app/api/celestialbodies/{id}

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER


## Add celestial body (Must be signed in on admin account):
POST @ https://planetor-production.up.railway.app/api/celestialbodies/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER
``` Json
{
  "name": "NameOfCelestialBody",
  "mass": 123,
  "density": 123,
  "diameter": 123,
  "gravity": 123,
  "dayInEarthHours": 123,
  "yearInEarthDays": 123,
  "averageTemperature": 123,
  "numberOfMoons": 123
}
```


## Update celestial body (Must be signed in on admin account):
PUT @ https://planetor-production.up.railway.app/api/celestialbodies/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER
``` Json
{
  "id": 123,
  "name": "NameOfCelestialBody",
  "mass": 123,
  "density": 123,
  "diameter": 123,
  "gravity": 123,
  "dayInEarthHours": 123,
  "yearInEarthDays": 123,
  "averageTemperature": 123,
  "numberOfMoons": 123
}
```


## Delete celestial body (Must be signed in on admin account):
DELETE @ https://planetor-production.up.railway.app/api/celestialbodies/{id}

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER


## Satellites:
## Get all satellites (Must be signed in on user- or admin account):
GET @ https://planetor-production.up.railway.app/api/satellites/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER


## Get satellite by id (Must be signed in on user- or admin account):
GET @ https://planetor-production.up.railway.app/api/satellites/{id}

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER


## Add satellite (Must be signed in on admin account):
POST @ https://planetor-production.up.railway.app/api/satellites/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER
``` Json
{
	"name": "satelliteName",
	"description": "descriptionOfSatelite",
	"longitude": "longitudeOfSatellite",
	"latitude": "latitudeOfSatellite",
	"country": "countryOfCoordinates"
}
```


## Update satellite (Must be signed in on admin account):
PUT @ https://planetor-production.up.railway.app/api/satellites/

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER

``` Json
{
  "id": 123,
	"name": "satelliteName",
	"description": "descriptionOfSatelite",
	"longitude": "longitudeOfSatellite",
	"latitude": "latitudeOfSatellite",
	"country": "countryOfCoordinates"
}
```


## Delete satellite (Must be signed in on admin account):
DELETE @ https://planetor-production.up.railway.app/api/satellites/{id}

SEND JWT AS BEARER-TOKEN IN REQUEST HEADER
