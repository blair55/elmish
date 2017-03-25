namespace Docs

open Elmish
open Elmish.Browser.Navigation
open Elmish.UrlParser
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
      format Home (s "home")
      format About (s "about")
    ]

  let hashParser (location: Location) =
    UrlParser.parse id pageParser (location.hash.Substring 1)

  let urlUpdate (result: Elmish.Result<Page, string>) model =
    match result with
    | Error e ->
      Browser.console.error("Error parsing url:", e)
      ( model, Navigation.modifyUrl (toHash model.CurrentPage) )
    | Ok page ->
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
    div
      []
      [ unbox "coucou" ]

  open Elmish.Snabbdom

  // App
  Program.mkProgram init update view
  |> Program.toNavigable hashParser urlUpdate
  |> Program.withSnabbdom "elmish-app"
  |> Program.run
