// Contains code from FsHtml: https://github.com/ptrelford/FsHtml by Phillip Trelford
// FsHtml is licensed under the Apache License 2.0: https://raw.githubusercontent.com/ptrelford/FsHtml/master/LICENSE 

namespace Starchan.Web

open System

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html
open WebSharper.JQuery



[<AutoOpen; JavaScript>]
module ClientExtensions =
    let rnd = System.Random()

    let jserror = JQuery.JQuery.Error 

    let info = Console.Info
    
    let error = Console.Error

    let debug (loc:string) t = info <| sprintf "DEBUG: %s: %A" (loc.ToUpper()) t
    
    let toLower (s:string) = s.ToLower()

    [<Direct "$('#term').terminal().disable()">]
    let disableTerminal() = X<unit>

    [<Direct "$('#term').terminal().enable()">]
    let enableTerminal() = X<unit>

    let terminalOutput() = JQuery(".terminal-output").Get().[0]

    let getMic() = JQuery("#microphone").Get().[0]

    [<Direct("window.lastMicData")>]
    let lastMicData() = X<Int16Array>

    let eid = attr.id
    
    let reid s = s + "-" + rnd.Next().ToString()  |> eid

    let cls n = attr.``class`` n
    
    let href s = attr.href s

    let dindex (n:int) = Attr.Create "data-index" (n.ToString())
    
    let container c = div [cls "container"] c
    
    let getContainer() = JQuery("#container").Get().[0].FirstChild |> As<Dom.Element>
    
    let createElement doc =
        let el = JS.Document.CreateElement "div"
        do JS.Document.AppendChild(el) |> ignore        
        do doc |> Doc.RunAppend el
        
    let elementHTML (d:Dom.Element) = d.InnerHTML
    
    let createCanvas (id:string) (width:string) (height:string) (parent:Dom.Element) =
        canvas[eid id; attr.width width; attr.height height][] |> Doc.Run parent
        let c = parent.FirstChild |> As<CanvasElement>
        c