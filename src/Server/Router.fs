module Router

open Saturn
open Giraffe.Core
open Giraffe.ResponseWriters
open FSharp.Control.Tasks
open Microsoft.AspNetCore.Http
open Shared

type Stuff = { Postcode: string }

let getDistanceFromLondon next (ctx:HttpContext) = task {
        return! json { Postcode = "Abc" } next ctx
    }

let apiRouter = router {
    pipe_through (pipeline { set_header "x-pipeline-type" "Api" })

    forward "/example" Example.Controller.apiRouter
    forward "/init" Init.Controller.apiRouter
    }


// let browserRouter = router {
//     not_found_handler (htmlView NotFound.layout) //Use the default 404 webpage
//     pipe_through browser //Use the default browser pipeline

//     forward "" defaultView //Use the default view
// }
