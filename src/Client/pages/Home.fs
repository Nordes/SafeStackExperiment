module Client.Home

open Elmish
open Fable.React
open Fable.React.Props

type Msg =
    | Loaded
    | Error of exn

type Model = {
    WishList : string option
}
    with
        static member Empty : Model = {
            WishList = None
        }

let init () = Model.Empty, Cmd.ofMsg Loaded

let update (msg:Msg) model : Model*Cmd<Msg> =
    match msg with
    | Loaded ->
        // model, Cmd.OfPromise.either getWishList "test" WishListLoaded Error
        { model with WishList = Some "wishList" }, Cmd.none

    | Error e ->
        printfn "Error: %s" e.Message
        model, Cmd.none


let view = Client.Utils.elmishView "Home" (fun (model:Model) ->
    div [] [
        h4 [] [str "Home stuff..."]
    ]
)