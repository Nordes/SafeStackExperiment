module Init.Controller
open Saturn
open Giraffe.Core
open Giraffe.ResponseWriters
open FSharp.Control.Tasks
open Shared

let __get next ctx = task {
        let counter = { Value = 45 }
        return! json counter next ctx
    }

let apiRouter = router {
        get "" __get
        get "/" __get
    }

