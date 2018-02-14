
namespace Mobiel.Lib

open System.Windows.Shapes
open System.Windows.Media
open System.Windows
open System

type centroidAcc = { 
    xSum:float;
    ySum: float;
    n:float; 
}

type Particle ={
    Polygon:Polygon;
    Centroid:Point;
    Weight:float;
}

type Mobiel={
    Particles:list<Particle>;
    CenterOfGravity:Point;
}

type MobielConfig() =
    member val chairBase:float = float 450 with get,set
    member val chairIndent:float = float 400 with get,set
    member val chairRest:float = float 800 with get,set
    member val tubeThickness :float= float 25 with get,set
    member val rectangleHeight:float = float 1000 with get,set
    member val rectangleWidth:float = float 700 with get,set
    member val aplhaInDegrees:float = float 15 with get,set
    member val attachmentHook:float = float 30 with get,set
    member val chairThicknes:float = float 20  with get,set//t: thickness of the wood
    member val chairWidth:float = float 500 with get,set
    member val metalDensityStrong:float = float 1.77  with get,set//g/mm
    member val metalDensityNormal:float = float 2.47  with get,set//g/mm
    member val woodDensity:float = float 0.00093  with get,set//g/mm3

type MobielFactory() =
    
    let coefficientDoubles = 2

    let lengthD x z d=
         x-d-z

    let radians degrees =
        Math.PI*degrees/float 180

    let degrees radians =
        radians*float 180/Math.PI

    

    let centroid (polygon:Polygon) =
        let seed = {
            xSum=float 0;
            ySum= float 0;
            n=float 0
        }
        let accResult  = List.ofSeq(polygon.Points)|>List.fold (fun acc p ->{xSum=acc.xSum + p.X;ySum = acc.ySum + p.Y;n = acc.n + float 1}) seed
        new Point(accResult.xSum/ float accResult.n, accResult.ySum/float accResult.n)

    let centerOfGravity (components:Particle list) =
        let seed = {
            xSum=float 0;
            ySum= float 0;
            n=float 0
        }
        let accResult  = components|>List.fold (fun acc p ->{
                                                            xSum=acc.xSum + p.Centroid.X * p.Weight;
                                                            ySum = acc.ySum + p.Centroid.Y * p.Weight;
                                                            n = acc.n + p.Weight}) seed
        new Point(accResult.xSum/ float accResult.n, accResult.ySum/float accResult.n)

    let seatAngle (center:Point) x z d h a =
        let xHook = x-z-(d/float 2)
        let yHook = d+d+h+a
        let  hook = new Point(xHook, yHook)
        let lengthCos = yHook - center.Y
        let lengthLeg = Math.Sqrt (Math.Pow(xHook-center.X,float 2)+Math.Pow(yHook-center.Y,float 2))
        printfn "lengthSin: %A" (center.X - xHook)
        printfn "lengthCos: %A" lengthCos
        printfn "lengthLeg: %A" lengthLeg

        let angle = Math.Acos(lengthCos/lengthLeg)
        degrees angle

    member this.Create (config:MobielConfig) : Mobiel =
        let weightMetalStrong length =
            length * config.metalDensityStrong
    
        let weightMetalNormal length =
            length * config.metalDensityNormal

        let weightWood length width height =
            let w = length * width * height * config.woodDensity
            w
        
        let A = this.polygonA config.chairBase config.chairIndent config.tubeThickness config.rectangleHeight
        let B = this.polygonB config.chairBase config.chairIndent config.tubeThickness config.rectangleHeight
        let C = this.polygonC config.chairBase config.chairIndent config.tubeThickness
        let D = this.polygonD config.chairBase config.chairIndent config.tubeThickness
        let E = this.polygonE config.chairBase config.chairIndent config.tubeThickness
        let F = this.polygonF config.chairBase config.tubeThickness config.chairRest config.aplhaInDegrees
        let G = this.chairBottom config.chairBase config.chairThicknes config.tubeThickness
        let H = this.chairBack config.chairBase config.chairRest config.chairThicknes config.tubeThickness config.aplhaInDegrees

        let weightA = weightMetalStrong (config.rectangleWidth+config.tubeThickness+config.tubeThickness)
        let weightB = (weightMetalNormal config.rectangleHeight)*float coefficientDoubles
        let weightC = weightMetalStrong (config.rectangleWidth+config.tubeThickness+config.tubeThickness)
        let weightD = (weightMetalNormal (lengthD config.chairBase config.chairIndent config.tubeThickness))*float coefficientDoubles
        let weightE = (weightMetalNormal config.chairIndent)*float coefficientDoubles
        let weightF = (weightMetalNormal config.chairRest)*float coefficientDoubles
        let weightG = weightWood config.chairWidth config.chairBase config.chairThicknes
        let weightH = weightWood config.chairWidth config.chairRest config.chairThicknes
        let particles = [{Polygon=A;Centroid=centroid A ;Weight=weightA};
                                    {Polygon=B;Centroid=centroid B ;Weight=weightB};
                                    {Polygon=C;Centroid=centroid C ;Weight=weightC};
                                    {Polygon=D;Centroid=centroid D ;Weight=weightD};
                                    {Polygon=E;Centroid=centroid E ;Weight=weightE};
                                    {Polygon=F;Centroid=centroid F ;Weight=weightF};
                                    {Polygon=G;Centroid=centroid G ;Weight=weightG};
                                    {Polygon=H;Centroid=centroid H ;Weight=weightH};
                                ]
        let mobiel = {
            Particles = particles
            CenterOfGravity = centerOfGravity particles
        }

        //let angle = seatAngle center config.chairBase config.chairIndent config.tubeThickness config.rectangleHeight config.attachmentHook

        mobiel
    
    member this.chairBottom x t d =
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(float 0 , d); new Point(x, d); new Point(x, d + t);new Point(float 0, d+ t)])
        shape
    
    member this.chairBack x r t d alpha =
        let x2 = x+r*Math.Sin(radians alpha)
        let y2 = d+r*Math.Cos(radians alpha)
        let x3 = x+r*Math.Sin(radians alpha)-t*Math.Cos(radians alpha)
        let y3 = y2+t*Math.Sin(radians alpha)
        let x4 = x-t*Math.Cos(radians alpha)
        let y4 = d+t*Math.Sin(radians alpha)
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(float x , d); new Point(x2, y2); new Point(x3, y3);new Point(x4, y4)])
        shape

    

    member this.polygonA x z d h =
        let x1 = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , d + h); new Point(x1+d, d+h); new Point(x1+d, d+d+h); new Point(x1, d+d+h)])
        shape

    member this.polygonC x z d =
        let x1 = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , float 0); new Point(x1+d, float 0); new Point(x1+d, d); new Point(x1, d)])
        shape

    member this.polygonF x d r alpha =
        let x2 = x+d*Math.Cos(radians alpha)
        let y2 = d-d*Math.Sin(radians alpha)
        let x3 = x2+r*Math.Sin(radians alpha)
        let y3 = y2+r*Math.Cos(radians alpha)
        let x4 = x+r*Math.Sin(radians alpha)
        let y4 = d+r*Math.Cos(radians alpha)
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x , d); new Point(x2, y2); new Point(x3, y3); new Point(x4, y4)])
        shape
    
    member this.polygonE x z d=
        let x1 = x-z
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , float 0); new Point(x1 + z, float 0); new Point(x1 + z, d);new Point(x1 , d)])
        shape

    member this.polygonD x z d =
        let length = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(float 0, float 0);new Point(length , float 0); new Point(length, d); new Point(float 0, d)])
        shape

    member this.polygonB x z d h =
        let x1 = x-d-z
        let width = x-z
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , d); new Point(x1 + d, d); new Point(x1+d, d+ h);new Point(x1, d+ h)])
        shape

    

    