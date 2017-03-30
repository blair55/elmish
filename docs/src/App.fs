namespace Docs

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser

module App =

  [<Emit("require('../sass/main.sass');")>]
  let requireStyle () = ()

  requireStyle()

  type Page =
    | Home
    //| Docs
    //| Sample
    | About

  type SubModels =
    {
      Tmp: int
    }

    static member Initial =
      {
        Tmp = 0
      }

  type Model =
    {
      CurrentPage: Page
      SubModels: SubModels
    }

    static member Initial =
      {
        CurrentPage = Home
        SubModels = SubModels.Initial
      }

  let toHash =
    function
    | Home -> "#home"
    | About -> "#about"

  let pageParser: Parser<Page->_,_> =
    oneOf [
      map Home (s "home")
      map About (s "about")
    ]

  let urlUpdate (result: Option<Page>) model =
    match result with
    | None ->
      Browser.console.error("Error parsing url")
      ( model, Navigation.modifyUrl (toHash model.CurrentPage) )
    | Some page ->
        { model with CurrentPage = page }, []

  type Msg =
    | NoOp

  let init result =
    urlUpdate result Model.Initial

  let update msg model =
    match msg with
    | NoOp ->
        model, Cmd.Empty

  open Fable.Helpers.Snabbdom
  open Fable.Helpers.Snabbdom.Props

  let view model dispatch =
    // let navbarHtml =
    //   Html.map NavbarActions (Navbar.view model.SubModels.Navbar)

    // let headerHtml =
    //   Html.map HeaderActions (Header.view model.SubModels.Header)

    div
      []
      [ div
          [ props [
              ClassName "navbar-bg"
            ]
          ]
          [ div
              [ props [
                  ClassName "container"
                ]
              ]
              [ Navbar.view
              ]
          ]
        // headerHtml
        // pageHtml
      ]

  open Elmish.Snabbdom

  // App
  Program.mkProgram init update view
  |> Program.toNavigable (parseHash pageParser) urlUpdate
  |> Program.withSnabbdom "elmish-app"
  |> Program.run
