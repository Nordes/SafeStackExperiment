module Client.App

open Elmish
open Elmish.React
open Elmish.HMR
open Fable.React
open Fable.Core
open Fulma
open Client.Pages
open Fable.Core.JsInterop
open Fable.React.Props
open Fable.Import
open Elmish.Navigation
open Client.Components
open Thoth.Json
open Fulma
open Fable.FontAwesome
// Other way: https://github.com/MangelMaxime/fulma-demo/blob/master/src/App.fs
type Model = {
    MenuModel : NavBar.Model
    PageModel : PageModel
}
type Msg =
    | HomePageMsg of Home.Msg
    | ExamplesMsg of Examples.Msg
    | DocumentationMsg of Documentation.Msg

let handleNotFound (model: Model) =
    JS.console.error("Error parsing url: " + Browser.Dom.window.location.href)
    ( model, Navigation.modifyUrl (toPath Page.NotFound) )


let goToUrl (e: Browser.Types.MouseEvent) =
    e.preventDefault()
    let href = !!e.target?href
    Navigation.newUrl href |> List.map (fun f -> f ignore) |> ignore

let viewLink page description =
    Navbar.Item.a [ // Style [ Padding "0 20px" ]
        Navbar.Item.Props [
        Href (Pages.toPath page)
        OnClick goToUrl ]]
        [ str description ]

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
             [
              viewLink Page.Home "Home"
              viewLink Page.Examples "Examples"
              viewLink Page.Documentation "Docs"

              Navbar.Item.div [ ]
                [ Button.a
                    [ Button.Size IsSmall
                      Button.Props [ Href "https://github.com/SAFE-Stack/SAFE-template" ] ]
                    [ Icon.icon [ ]
                        [ Fa.i [Fa.Brand.Github; Fa.FixedWidth] [] ]
                      span [ ] [ str "View Source" ] ] ]
                      ] ]


let view model dispatch =
    Hero.hero
        [ Hero.IsFullHeight
          Hero.IsBold ]
        [ Hero.head [ ]
            [ Navbar.navbar [  ]
                [ Container.container [ ]
                    [ navBrand
                      navMenu ] ] ]
          Hero.body [ ]
            [ Container.container
                [ Container.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                [ Columns.columns [ Columns.IsVCentered ]
                    [
                        match model.PageModel with
                        | HomePageModel model ->
                            yield Home.view model
                        | NotFoundModel ->
                            yield div [] [ str "The page is not available." ]
                        | ExamplesModel m ->
                            yield Examples.view { Model = m; Dispatch = (ExamplesMsg >> dispatch) }
                        | DocumentationModel m ->
                            yield Documentation.view { Model = m; Dispatch = (DocumentationMsg >> dispatch) }
                        //Column.column
                    //     [ Column.Width (Screen.All, Column.Is5) ]
                    //     [ Image.image [ Image.Is4by3 ]
                    //         [ img [ Src "http://placehold.it/800x600" ] ] ]
                    //   Column.column
                    //    [ Column.Width (Screen.All, Column.Is5)
                    //      Column.Offset (Screen.All, Column.Is1) ]
                    //      (SuperHeroBlock.root model dispatch)
                    //   Column.column
                    //    [ Column.Width (Screen.All, Column.Is5)
                    //      Column.Offset (Screen.All, Column.Is1) ]
                    //      (SuperHeroBlock.root model dispatch)
                    //       ]
                          ] ] ]
          Hero.foot [ ]
            [ Container.container [ ]
                [ Tabs.tabs [ Tabs.IsCentered ]
                    [ ul [ ]
                        [ li [ ]
                            [ a [ ]
                                [ str "And this at the bottom" ] ] ] ] ] ] ]

/// The navigation logic of the application given a page identity parsed from the .../#info
/// information in the URL.
let urlUpdate (result:Page option) (model:Model) =
    match result with
    | None ->
        handleNotFound model

    | Some Page.NotFound ->
        { model with PageModel = NotFoundModel }, Cmd.none

    | Some Page.Examples ->
        let m, cmd = Examples.init()
        { model with PageModel = ExamplesModel m }, Cmd.map ExamplesMsg cmd

    | Some Page.Documentation ->
        let m, cmd = Documentation.init()
        { model with PageModel = DocumentationModel m }, Cmd.map DocumentationMsg cmd

    | Some Page.Home ->
        let subModel, cmd = Home.init()
        { model with PageModel = HomePageModel subModel }, Cmd.map HomePageMsg cmd



let update msg model =
    match msg, model.PageModel with
    // | StorageFailure e, _ ->
    //     printfn "Unable to access local storage: %A" e
    //     model, Cmd.none
    | HomePageMsg msg, HomePageModel m ->
        let m, cmd = Home.update msg m

        { model with
            PageModel = HomePageModel m }, Cmd.map HomePageMsg cmd

    | HomePageMsg _, _ -> model, Cmd.none

    | ExamplesMsg _, _ -> model, Cmd.none

    | DocumentationMsg _, _ -> model, Cmd.none

let hydrateModel (json:string) (page: Page) =
    // The page was rendered server-side and now react client-side kicks in.
    // If needed, the model could be fixed up here.
    // In this case we just deserialize the model from the json and don't need to to anything special.
    let model: Model = Decode.Auto.unsafeFromString(json)
    match page, model.PageModel with
    | Page.Home, HomePageModel subModel when subModel.WishList <> None ->
        Some model
    | Page.Examples, ExamplesModel _ ->
        Some model
    | Page.Documentation, DocumentationModel _ ->
        Some model
    | _ ->
        None

let init page =
    let defaultModel () =
        // no SSR
        let model =
            { MenuModel = { CurrentPage = "stuff"; }
              PageModel = HomePageModel Home.Model.Empty }

        urlUpdate page model

    // was the page rendered server-side?
    let stateJson: string option = !!Browser.Dom.window?__INIT_MODEL__

    match stateJson, page with
    | Some json, Some page ->
        // SSR -> hydrate the model
        match hydrateModel json page with
        // | Some model ->
        //     { model with MenuModel = { model.MenuModel with User = loadUser() } }, Cmd.ofMsg AppHydrated
        | _ ->
            defaultModel()
    | _ ->
        defaultModel()

#if DEBUG
open Elmish.Debug
#endif

Program.mkProgram init update view
|> Program.toNavigable Pages.urlParser urlUpdate
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
