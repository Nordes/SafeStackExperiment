module Client.Examples

open Elmish
open Fable.React
open Fable.React.Props

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

let view = Utils.elmishView "Examples" (fun { Model = model; Dispatch = dispatch } ->
    div [] [
        h4 [] [str "Examples stuff..."]
    ]
)