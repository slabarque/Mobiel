﻿module File1

open System.Windows.Shapes
open System.Windows.Media
open System.Windows
open System.Collections
open System.Collections.Generic
open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open System.Windows.Input

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

let config = {
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

[<Measure>] type mm
[<Measure>] type g

let mmTom = 0.0001<metre/mm>
let mmTom = 0.0001<kilogram/g>

let test = mmTom * 1000.0<mm>

let weightByLength (materialConstant:float<g/mm>) (length:float<mm>) =
    materialConstant * length

let weightByVolume (materialConstant:float<g/mm^3>) (length:float<mm>) (width:float<mm>) (height:float<mm>) =
    materialConstant * length

type part =
    | P of (float*float) list
    | R of (float*float)*float*float
    | Rotate of float *part

let RtoP p =
    match p with
        | P(list) -> p
        | R(root,length, width) -> P [root;length + fst root,snd root;length + fst root,width + snd root;fst root,width + snd root]
        | _ -> failwith "oops"

let lengthD cfg = cfg.ChairBase - cfg.ChairIndent - cfg.TubeThickness

let A cfg = R ((lengthD cfg, cfg.TubeThickness + cfg.RectangleHeight),cfg.TubeThickness,cfg.TubeThickness)

let B cfg = R ((lengthD cfg, cfg.TubeThickness), cfg.TubeThickness, cfg.RectangleHeight)

let C cfg = R ((lengthD cfg, 0.0),cfg.TubeThickness,cfg.TubeThickness)

let D cfg = R ((0.0, 0.0),lengthD cfg,cfg.TubeThickness)

let E cfg = R ((cfg.ChairBase - cfg.ChairIndent, 0.0),cfg.ChairIndent, cfg.TubeThickness)

let F cfg = Rotate (-cfg.AplhaInDegrees, R ((cfg.ChairBase, cfg.TubeThickness),cfg.TubeThickness, cfg.ChairRest))

let G cfg = R ((0.0, cfg.TubeThickness),cfg.ChairBase,cfg.ChairThicknes)

let H cfg = Rotate (90.0-cfg.AplhaInDegrees, R ((cfg.ChairBase, cfg.TubeThickness),cfg.ChairRest,cfg.ChairThicknes))




let toShape cfg (makePart:Config -> part) = 
    let makePolygon (points:list<Point>) =
        let shape = new Polygon()
        shape.Points <- new PointCollection(points)
        shape

    let doRotate angle (polygon:Polygon) =
        polygon.RenderTransform <- new RotateTransform(angle, polygon.Points.[0].X,polygon.Points.[0].Y) 
        polygon
    
    let rec partToShape part= 
        match part with
            | P(list) -> makePolygon (list |> List.map (fun x -> new Point(fst x, snd x ))) 
            | R(_,_,_) -> part |> RtoP |> partToShape
            | Rotate(angle, p) -> p |> partToShape |> doRotate angle
    
    cfg |> makePart |> partToShape

let centroid (polygon:Polygon) =
    let seed = (0.0,0.0,0.0);
    let f  (xSum, ySum, n) (p:Point) = 
        (xSum + p.X,ySum + p.Y,n + 1.0)

    let x,y,n  = List.ofSeq(polygon.Points)|>List.fold f seed
    
    new Point(x/ n, y/n)

let centerOfGravity (components: list<Point*float>) =
    let seed =(0.0,0.0,0.0);
    let f  (xSum, ySum, n) (c:Point,w:float) = 
        (xSum + c.X * w,ySum + c.Y * w,n + w)
    let x,y,w  = components|>List.fold f seed
    (new Point(x/ w, y/w),w)

type Part ={
    Polygon:Polygon;
    Centroid:Point;
    Weight:float;
}


    
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
        let shapeForConfig = toShape cfg

        [A ;B ;C ;D ;E ; F ; G ; H ] |> List.map shapeForConfig
    