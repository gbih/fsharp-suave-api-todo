namespace TodoApi.Controllers

module TodoController = 

    open Suave
    open Suave.Filters
    open Suave.Operators
    open TodoApi.Models

    let getAll db =
        warbler (fun _ -> db.GetAll() |> Controller.JSON)
    
    let find db =
        db.Find 
        >> (Controller.handleResourceNOTFOUND Controller.JSON)

    let add db =
        let addDb =
            db.Add 
            >> (Controller.handleResourceCONFLICT Controller.JSON)
        request (Controller.getResourceFromReq >> (Controller.handleResourceBADREQUEST addDb))
    
    let update db key =
        let updateDb =
            db.Update
            >> (Controller.handleResourceNOTFOUND Controller.JSON)
        request (Controller.getResourceFromReq >> (Controller.handleResourceBADREQUEST updateDb))

    let remove db =
        db.Remove 
        >> (Controller.handleResourceNOTFOUND Controller.JSON)

    let todoController (db:TodoRepository) = 
        pathStarts "/api/todo" >=> choose [
            POST >=> path "/api/todo" >=> (add db)
            GET >=> path "/api/todo" >=> (getAll db)
            GET >=> pathScan "/api/todo/%s" (find db)
            DELETE >=> pathScan "/api/todo/%s" (remove db)
            PUT >=> pathScan "/api/todo/%s" (update db)  
        ]