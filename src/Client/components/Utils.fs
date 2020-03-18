module Client.Utils

open Fable.React
open Fable.React.Props
open Elmish.Navigation
open Fable.Core.JsInterop

let inline elmishView name render = FunctionComponent.Of(render, name, equalsButFunctions)
