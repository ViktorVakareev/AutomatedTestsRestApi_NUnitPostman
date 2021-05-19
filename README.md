# AutomatedTestsRestApi_NUnitPostman
C# Automated tests on ShortUrl web RESTful API - https://shorturl.nakov.repl.co/api using Postman and NUnit

## Test the following functions
•	GET /api – list all API endpoints.
•	GET /api/urls – list all shout URLs.
•	GET /api/urls/:shortCode – finds short URL by given shortCode.
•	POST /api/urls – create a new URL short code.
o	Post a JSON objects in the request body, e. g.
{"url":"https://cnn.com", "shortCode":"cnn"}
•	DELETE /api/urls/:shortCode – delete short URL by given shortCode.
•	POST /api/urls/visit/:shortCode – visit short URL by given shortCode (increases the visits count).

### Screenshots
![ShortUrl0](https://user-images.githubusercontent.com/79919124/118830444-9d2e6c00-b8c7-11eb-8242-e636d02f9b38.jpg)
![ShortUrl1](https://user-images.githubusercontent.com/79919124/118830477-a61f3d80-b8c7-11eb-88d4-f775c3aa41dd.jpg)
![ShortUrl2](https://user-images.githubusercontent.com/79919124/118830489-a8819780-b8c7-11eb-9d4c-e1a1a1b65949.jpg)
![ShortUrl3](https://user-images.githubusercontent.com/79919124/118830505-ab7c8800-b8c7-11eb-8d0c-7a718dc78297.jpg)
![ShortUrl4](https://user-images.githubusercontent.com/79919124/118830517-ae777880-b8c7-11eb-9275-d3d91e9ef7a8.jpg)
