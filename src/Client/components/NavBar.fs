module Client.Components.NavBar

open Fulma
open Fable.FontAwesome
open Fable.React
open Fable.React.Props

type Model = {
    CurrentPage: string
    }

let navBrand =
    Navbar.Brand.div [ ]
        [ Navbar.Item.a
            [ Navbar.Item.Props
                [ Href "https://safe-stack.github.io/"
                  Style [ BackgroundColor "#00d1b2" ] ] ]
            [ img [ Src "https://safe-stack.github.io/images/safe_top.png"
                    Alt "Logo" ] ] ]

let navMenu =
    Navbar.menu [ ]
        [ Navbar.End.div [ ]
            [ Navbar.Item.a [ ]
                [ str "Home" ]
              Navbar.Item.a [ ]
                [ str "Examples" ]
              Navbar.Item.a [ ]
                [ str "Documentation" ]
              Navbar.Item.div [ ]
                [ Button.a
                    [ Button.Size IsSmall
                      Button.Props [ Href "https://github.com/SAFE-Stack/SAFE-template" ] ]
                    [ Icon.icon [ ]
                        [ Fa.i [Fa.Brand.Github; Fa.FixedWidth] [] ]
                      span [ ] [ str "View Source" ] ] ] ] ]