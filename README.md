# Quick Setup

Clone this repo, then:
```sh
$ dotnet build
$ dotnet run
```

Use a tool like Postman to test the API endpoints:
```sh
GET  http://127.0.0.1:8080/api/todo
POST http://127.0.0.1:8080/api/todo

```

or curl:
```sh
$  curl -v http://127.0.0.1:8080/api/todo
$  curl -v -d '{ "name": "Test1", "isComplete": true }' -H "Content-Type: application/json" -X POST http://127.0.0.1:8080/api/todo
```