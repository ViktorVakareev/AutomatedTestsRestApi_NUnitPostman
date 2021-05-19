# AutomatedTestsRestApi_NUnitPostman
C# Automated tests on ShortUrl web RESTful API - https://shorturl.nakov.repl.co/api using Posman and NUnit

## Test the following functios
•	GET /api – list all API endpoints.
•	GET /api/urls – list all shout URLs.
•	GET /api/urls/:shortCode – finds short URL by given shortCode.
•	POST /api/urls – create a new URL short code.
o	Post a JSON objects in the request body, e. g.
{"url":"https://cnn.com", "shortCode":"cnn"}
•	DELETE /api/urls/:shortCode – delete short URL by given shortCode.
•	POST /api/urls/visit/:shortCode – visit short URL by given shortCode (increases the visits count).

### Screenshots
