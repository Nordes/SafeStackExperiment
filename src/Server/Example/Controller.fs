module Example.Controller
open Saturn
open Giraffe
open FSharp.Control.Tasks
open Shared
open Microsoft.AspNetCore.Http

let __get next ctx = task {
        let exampleData = {Data = "Hello world"}

        return! json exampleData next ctx
    }

let __post next (ctx:HttpContext) = task {
        let! stuff = ctx.BindModelAsync<ExampleData>()

        return! json { stuff with Data = sprintf "%s%s" stuff.Data "wello" } next ctx
    }

let apiRouter = router {
        get "" __get
        get "/" __get
        post "/tryme" __post
    }

