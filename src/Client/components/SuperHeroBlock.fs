module Client.Components.SuperHeroBlock

open Fable.React
open Fable.React.Props
open Fulma

let safeComponents =
    let components =
        span [ ]
           [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
               [ str "SAFE  "
                 str ReleaseNotes.template ]
             str ", "
             a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
             str ", "
             a [ Href "http://fable.io" ] [ str "Fable" ]
             str ", "
             a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
             str ", "
             a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]
             str ", "
             a [ Href "https://bulmatemplates.github.io/bulma-templates/" ] [ str "Bulma\u00A0Templates" ]
           ]

    span [ ]
        [ str "Version "
          strong [ ] [ str ReleaseNotes.app ]
          str " powered by: "
          components ]

let show = function
    | { Counter.Counter = Some counter } -> string counter.Value
    | { Counter = None   } -> "Loading..."

let containerBox (model : Counter.Model) (dispatch : Counter.Msg -> unit) =
    Box.box' [ ]
        [ Field.div [ Field.IsGrouped ]
            [ Control.p [ Control.IsExpanded ]
                [ Input.text
                    [ Input.Disabled true
                      Input.Value (show model) ] ]
              br []
              Control.p [ ]
                [ Button.a
                    [ Button.Color IsPrimary
                      Button.OnClick (fun _ -> dispatch Counter.Increment) ]
                    [ str "+" ] ]
              Control.p [ ]
                [ Button.a
                    [ Button.Color IsPrimary
                      Button.OnClick (fun _ -> dispatch Counter.Decrement) ]
                    [ str "-" ] ] ] ]

let root model dispatch =
                       [ Heading.h1 [ Heading.Is2 ]
                           [ str "Superhero Scaffolding" ]
                         Heading.h2
                           [ Heading.IsSubtitle
                             Heading.Is4 ]
                           [ safeComponents ]
                         containerBox model dispatch ]