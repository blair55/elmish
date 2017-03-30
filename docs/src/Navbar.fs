namespace Docs

module Navbar =

  open Fable.Helpers.Snabbdom
  open Fable.Helpers.Snabbdom.Props

  let navButton classy href faClass txt =
    a
      [ props [
          ClassName (sprintf "button %s" classy)
          Href href
        ]
      ]
      [ span
          [ props [
              ClassName "icon"
            ]
          ]
          [ i
              [ props [
                  ClassName (sprintf "fa %s" faClass)
                ]
              ]
              [ ]
          ]
        span
          [ ]
          [ unbox txt ]
      ]

  let navButtons =
    span
      [ props [
          ClassName "nav-item"
        ]
      ]
      [ navButton "twitter" "https://twitter.com/FableCompiler" "fa-twitter" "Twitter"
        navButton "github" "https://github.com/fable-compiler/fable-arch" "fa-github" "Fork me"
        navButton "github" "https://gitter.im/fable-compiler/Fable" "fa-comments" "Gitter"
      ]

  let view =
    nav
      [ props [
          ClassName "nav"
        ]
      ]
      [ div
          [ props [
              ClassName "nav-left"
            ]
          ]
          [ h1
              [ props [
                  ClassName "nav-item is-brand title is-4"
                ]
              ]
              [ unbox "Elmish" ]
          ]
        navButtons
      ]
