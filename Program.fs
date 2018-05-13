open TodoApi.Controllers.TodoController
open TodoApi.Models
open TodoApi.Models.TodoRepositoryDb
open Suave

[<EntryPoint>]
let main argv = 
    let item = {
        Key = ""
        Name = "My Item 1 GB"
        IsComplete = false
    }
    let result = todoRepositoryDb.Add item

    let item2 = {
        Key = ""
        Name = "My Item 2 IH"
        IsComplete = false
    }
    let result2 = todoRepositoryDb.Add item2

    startWebServer defaultConfig (todoController todoRepositoryDb)
    0 // return an integer exit code