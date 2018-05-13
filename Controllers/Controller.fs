namespace TodoApi.Controllers

module Controller = 

    open Newtonsoft.Json
    open Newtonsoft.Json.Serialization
    open Suave
    open Suave.Operators

    let fromJson<'a> json =
        let obj = JsonConvert.DeserializeObject(json, typeof<'a>) 
        if isNull obj then
            None
        else
            Some(obj :?> 'a)

    let getResourceFromReq<'a> (req : HttpRequest) =
        let getString rawForm =
            System.Text.Encoding.UTF8.GetString(rawForm)
        req.rawForm |> getString |> fromJson<'a>

    let JSON value =
        let settings = new JsonSerializerSettings()
        settings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

        JsonConvert.SerializeObject(value, settings)
        |> Successful.OK
        >=> Writers.setMimeType "application/json; charset=utf-8"
    
    let handleResource f requestError = function
        | Some r -> r |> f
        | _ -> requestError

    let handleResourceBADREQUEST = 
        (fun f -> handleResource f (RequestErrors.BAD_REQUEST "No Resource from request"))

    let handleResourceNOTFOUND = 
        (fun f -> handleResource f (RequestErrors.NOT_FOUND "Resource not found"))

    let handleResourceCONFLICT = 
        (fun f -> handleResource f (RequestErrors.CONFLICT "Resource already exists"))
  


  
