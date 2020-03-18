module Client.Documentation

open Fable.React
open Elmish

type Msg =
    | Loaded

type Model = {
    WishList : string option
}
    with
        static member Empty : Model = {
            WishList = None
        }

let init () = Model.Empty, Cmd.ofMsg Loaded

type Props = {
    Model: Model
    Dispatch: Msg -> unit
}

let view = Utils.elmishView "Documentation" (fun { Model = model; Dispatch = dispatch } ->
    div [] [
        h4 [] [str "Documentation stuff..."]
    ]
)