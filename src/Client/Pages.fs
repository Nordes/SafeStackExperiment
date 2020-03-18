module Client.Pages

open Elmish.UrlParser

// This Page file act as a router. We could also rename it at some point "Router" like what we have for the API's

type PageModel =
    | HomePageModel of Home.Model
    | ExamplesModel of Examples.Model
    | DocumentationModel of Documentation.Model
    | NotFoundModel

/// The different pages of the application. If you add a new page, then add an entry here.
[<RequireQualifiedAccess>]
type Page =
    | Home
    | Examples
    | Documentation
    | NotFound

let toPath =
    function
    | Page.Home -> "/"
    | Page.Examples -> "/examples"
    | Page.Documentation -> "/documentation"
    | Page.NotFound -> "/notfound"

/// The URL is turned into a Result.
let pageParser : Parser<Page -> Page,_> =
    oneOf
        [ map Page.Home (s "")
          map Page.Examples (s "examples")
          map Page.Documentation (s "documentation")
          map Page.NotFound (s "notfound")]

let urlParser location = parsePath pageParser location
