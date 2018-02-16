namespace Mobiel.DSL

open System.Windows
open System.Collections
open System.Collections.Generic
open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open System.Windows.Input
open FParsec.CharParsers


module Types =
    
    [<Measure>] type mm
    [<Measure>] type g

    type part =
        | P of (float*float) list
        | R of (float*float)*float*float
        | Rotate of float *part

    type Point =
        {
        X:float;
        Y:float
        }

module Shared =
    open Types

    let weightByLength (materialConstant:float<g/mm>) (length:float<mm>) =
        materialConstant * length

    let weightByVolume (materialConstant:float<g/mm^3>) (length:float<mm>) (width:float<mm>) (height:float<mm>) =
        materialConstant * length * width * height

    let RtoP p =
        match p with
            | P(list) -> p
            | R(root,length, width) -> P [root;length + fst root,snd root;length + fst root,width + snd root;fst root,width + snd root]
            | _ -> failwith "oops"

    let toPoint (x,y) = 
        {X=x;Y=y}

module MyShapes =
    open Types
    open Shared

    type Config = {
        ChairBase:float;//<mm>;
        ChairIndent:float;//<mm> ;
        ChairRest:float;//<mm> ;
        TubeThickness:float;//<mm> ;
        RectangleHeight:float;//<mm> ;
        RectangleWidth:float;//<mm> ;
        AplhaInDegrees:float;//<degrees> ;
        AttachmentHook:float;//<mm> ;
        ChairThicknes:float;//<mm> ; //t: thickness of the wood
        ChairWidth:float;//<mm> ;
        MetalDensityStrong:float;//<g/mm> ; //g/mm
        MetalDensityNormal:float;//<g/mm> ; //g/mm
        WoodDensity:float;//<g/mm^3>  //g/mm3
        }

    //let config = {
    //    ChairBase=450.0;//<mm>;
    //    ChairIndent=350.0;//<mm>;
    //    AplhaInDegrees=15.0;//<degrees>;
    //    AttachmentHook=30.0;//<mm>;
    //    ChairRest=700.0;//<mm>;
    //    ChairThicknes=20.0;//<mm>;
    //    ChairWidth=50.0;//<mm>;
    //    MetalDensityNormal=1.77;//<g/mm>;
    //    MetalDensityStrong=2.50;//<g/mm>;
    //    RectangleHeight=1200.0;//<mm>;
    //    RectangleWidth=600.0;//<mm>;
    //    TubeThickness=30.0;//<mm>;
    //    WoodDensity=0.00093;//<g/mm^3>
    //    }

    let weightMetalStrong cfg = weightByLength  (cfg.MetalDensityStrong * 1.0<g/mm>)
    let weightMetalNormal cfg = weightByLength  (cfg.MetalDensityNormal * 1.0<g/mm>)
    let weightWood cfg = weightByVolume  (cfg.WoodDensity * 1.0<g/mm^3>)

    let lengthD cfg = cfg.ChairBase - cfg.ChairIndent - cfg.TubeThickness

    let A cfg = R ((lengthD cfg, cfg.TubeThickness + cfg.RectangleHeight),cfg.TubeThickness,cfg.TubeThickness),weightMetalStrong cfg ((cfg.RectangleWidth + 2.0* cfg.TubeThickness)  * 1.0<mm>)

    let B cfg = R ((lengthD cfg, cfg.TubeThickness), cfg.TubeThickness, cfg.RectangleHeight),2.0 * weightMetalNormal cfg (cfg.RectangleHeight * 1.0<mm>)

    let C cfg = R ((lengthD cfg, 0.0),cfg.TubeThickness,cfg.TubeThickness),weightMetalStrong cfg ((cfg.RectangleWidth + 2.0* cfg.TubeThickness)  * 1.0<mm>)

    let D cfg = R ((0.0, 0.0),lengthD cfg,cfg.TubeThickness),2.0 * weightMetalNormal cfg (lengthD cfg * 1.0<mm>)

    let E cfg = R ((cfg.ChairBase - cfg.ChairIndent, 0.0),cfg.ChairIndent, cfg.TubeThickness),2.0 * weightMetalNormal cfg (cfg.ChairIndent * 1.0<mm>)

    let F cfg = Rotate (-cfg.AplhaInDegrees, R ((cfg.ChairBase, cfg.TubeThickness),cfg.TubeThickness, cfg.ChairRest)),2.0 * weightMetalNormal cfg (cfg.ChairRest * 1.0<mm>)

    let G cfg = R ((0.0, cfg.TubeThickness),cfg.ChairBase,cfg.ChairThicknes),weightWood cfg (cfg.ChairBase * 1.0<mm>) (cfg.ChairWidth  * 1.0<mm>) (cfg.ChairThicknes  * 1.0<mm>) 

    let H cfg = Rotate (90.0-cfg.AplhaInDegrees, R ((cfg.ChairBase, cfg.TubeThickness),cfg.ChairRest,cfg.ChairThicknes)),weightWood cfg (cfg.ChairRest * 1.0<mm>) (cfg.ChairWidth  * 1.0<mm>) (cfg.ChairThicknes  * 1.0<mm>) 

    let Anker cfg = ( lengthD cfg ) + cfg.TubeThickness / 2.0 , cfg.RectangleHeight + 2.0 * cfg.TubeThickness + cfg.AttachmentHook

module Parsing =
    open FParsec
    open Types
    open MyShapes

    let test p str =
        match run p str with
        | Success(result, _, _)   -> printfn "Success: %A" result
        | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

    
    let str s = pstring s
    //let sepByComma = (str ",") |> sepBy
    //let floatCommaFloat = sepByComma pfloat
    let coordinates = pfloat .>>. (str "," >>. pfloat)
    let surroundedBy s1 s2 p = str s1 >>. p .>> str s2
    let pointParser = tuple3 (coordinates |> surroundedBy "(" ")") (str "," >>. pfloat) (str "," >>. pfloat)
    let v = run pointParser "(0,0),200,20"
    let rect def = 
        match def with
            | Success(result, _, _)   -> R result
            | Failure(errorMsg, _, _) -> failwith errorMsg
    let X (cfg:Config) = ((rect v),10000.0<g>)

open Parsing

module Gravity =
    open Shared
    open Types
    open MyShapes
    open System

    let radians degrees =
        Math.PI*degrees/float 180

    let degrees radians =
        radians * float 180/Math.PI

    let angleBetween point1 anglePoint point2 =
        let d p1 p2 =
            Math.Sqrt(Math.Pow(p1.X - p2.X, 2.0) + Math.Pow(p1.Y - p2.Y,2.0))
        Math.Acos((Math.Pow(d point1 anglePoint,2.0) + Math.Pow(d point2 anglePoint, 2.0) - Math.Pow(d point1 point2, 2.0))/( 2.0 * (d point1 anglePoint) * (d point2 anglePoint) ))

    let rotatePoint angle pointAnker (point:Point) = 
        let s = sin(radians angle)
        let c = cos(radians angle)
        let point2 = { X = point.X - pointAnker.X; Y =  point.Y - pointAnker.Y};
        let xnew = point2.X * c - point2.Y * s
        let ynew = point2.X * s + point2.Y * c
        let point3 = { X = xnew +  pointAnker.X; Y =  ynew + pointAnker.Y}
        point3

    let toShape cfg (makePart:Config -> part) = 
        let rotate angle (points:list<Point>) =
            let rotateAroundFirst = rotatePoint angle points.[0]
            points |> List.map rotateAroundFirst
    
        let rec partToShape part= 
            match part with
                | P(list) -> list |> List.map (fun x -> {X=fst x; Y=  snd x})
                | R(_,_,_) -> part |> RtoP |> partToShape
                | Rotate(angle, p) -> p |> partToShape |> rotate angle
    
        cfg |> makePart |> partToShape

    let centroid (points:list<Point>) =
        let seed = (0.0,0.0,0.0);
        let f  (xSum, ySum, n) (p:Point) = 
            (xSum + p.X,ySum + p.Y,n + 1.0)

        //let rotate (p:Point) = 
        //    polygon.RenderTransform.Value.Transform(p)

        let x,y,n  = points|>List.fold f seed
    
        {X=x/ n; Y=y/n}

    let centerOfGravity (components: list<Point*float>) =
        let seed =(0.0,0.0,0.0);
        let f  (xSum, ySum, n) (c:Point,w:float) = 
            (xSum + c.X * w,ySum + c.Y * w,n + w)
        let x,y,w  = components|>List.fold f seed
        (({X=x/ w;Y= y/w}),w)

    type Part ={
        Polygon:list<Point>;
        Centroid:Point;
        Weight:float<g>;
    }

    type Object2D={
        Parts:list<Part>;
        CenterOfGravity:Point;
        Weight:float;
    }

    let performGravitationalPull object anker =
        let gravitationalPullAngle anker center =   
            angleBetween {X=anker.X; Y=anker.Y - 100.0} anker center
        let angle = gravitationalPullAngle anker object.CenterOfGravity
        let rot = rotatePoint (degrees -angle) anker
        let mapPart part =
            { part with 
                Polygon = part.Polygon |> List.map rot;
                Centroid = rot part.Centroid
                }
        let result = 
            {object with 
                Parts = object.Parts |> List.map mapPart;
                CenterOfGravity = rot object.CenterOfGravity;
            }
        result
    
    type PartFactory() =
        member this.Config = {
            ChairBase=450.0;//<mm>;
            ChairIndent=350.0;//<mm>;
            AplhaInDegrees=15.0;//<degrees>;
            AttachmentHook=30.0;//<mm>;
            ChairRest=700.0;//<mm>;
            ChairThicknes=20.0;//<mm>;
            ChairWidth=50.0;//<mm>;
            MetalDensityNormal=1.77;//<g/mm>;
            MetalDensityStrong=2.50;//<g/mm>;
            RectangleHeight=1200.0;//<mm>;
            RectangleWidth=600.0;//<mm>;
            TubeThickness=30.0;//<mm>;
            WoodDensity=0.00093;//<g/mm^3>
        }
        member this.Create cfg =
            let v1 = v

            
            let shapeForConfig s =
                let _, weight = s cfg
                let shape = toShape cfg (fun c->fst (s c));
                {
                    Polygon = shape;
                    Centroid = centroid shape;
                    Weight = weight
                }
            let parts = [A ;B ;C ;D ;E ; F ; G ; H; X ] |> List.map shapeForConfig
            let center,weight =  parts |> List.map (fun p -> (p.Centroid, (float p.Weight))) |>  centerOfGravity 
            let result = {
                Parts = parts;
                CenterOfGravity = center;
                Weight = weight;
            }

            cfg |> Anker |> toPoint |>  performGravitationalPull result




    