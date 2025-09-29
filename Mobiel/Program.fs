// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Windows.Shapes
open System.Windows.Media
open System.Windows
open System.Linq
open System
open System.Threading

type centroidAcc = { 
  xSum:float;
  ySum: float;
  n:float; 
}

type ParticleWeight ={
    point:Point;
    weight:float;
}

[<EntryPoint>]
[<STAThread>]
let main argv = 
    let root = Point(float 0, float 0)
    let chairBase = float 450
    let chairIndent = float 400
    let chairRest = float 800
    let tubeThickness = float 25
    let rectangleHeight = float 1000
    let rectangleWidth = float 700
    let aplhaInDegrees = float 15
    let attachmentHook = float 30
    let chairThicknes = float 20 //t: thickness of the wood
    let chairWidth = float 500

    let metalDensityStrong = float 1.77 //g/mm
    let metalDensityNormal = float 2.47 //g/mm
    let woodDensity = float 0.00093 //g/mm3
    let coefficientDoubles = 2
    
    let lengthD x z d=
         x-d-z

    let radians degrees =
        Math.PI*degrees/float 180

    let degrees radians =
        radians*float 180/Math.PI
    
    let chairBottom x t d =
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(float 0 , d); new Point(x, d); new Point(x, d + t);new Point(float 0, d+ t)])
        shape
    
    let chairBack x r t d alpha =
        let x2 = x+r*Math.Sin(radians alpha)
        let y2 = d+r*Math.Cos(radians alpha)
        let x3 = x+r*Math.Sin(radians alpha)-t*Math.Cos(radians alpha)
        let y3 = y2+r*Math.Cos(radians alpha)+t*Math.Sin(radians alpha)
        let x4 = x-t*Math.Cos(radians alpha)
        let y4 = d+t*Math.Sin(radians alpha)
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(float x , d); new Point(x2, y2); new Point(x3, y3);new Point(x4, y4)])
        shape

    let polygonA x z d h =
        let x1 = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , d + h); new Point(x1+d, d+h); new Point(x1+d, d+d+h); new Point(x1, d+d+h)])
        shape

    let polygonC x z d =
        let x1 = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , float 0); new Point(x1+d, float 0); new Point(x1+d, d); new Point(x1, d)])
        shape

    let polygonF x d r alpha =
        let x2 = x+d*Math.Cos(radians alpha)
        let y2 = d-d*Math.Sin(radians alpha)
        let x3 = x2+r*Math.Sin(radians alpha)
        let y3 = y2+r*Math.Cos(radians alpha)
        let x4 = x+r*Math.Sin(radians alpha)
        let y4 = d+r*Math.Cos(radians alpha)
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x , d); new Point(x2, y2); new Point(x3, y3); new Point(x4, y4)])
        shape

    let polygonE x z d=
        let x1 = x-z
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , float 0); new Point(x1 + z, float 0); new Point(x1 + z, d);new Point(x1 , d)])
        shape

    let polygonD x z d =
        let length = lengthD x z d
        let shape = new Polygon()
        shape.Points <- new PointCollection([root;new Point(length , float 0); new Point(length, d); new Point(float 0, d)])
        shape

    let polygonB x z d h =
        let x1 = x-d-z
        let width = x-z
        let shape = new Polygon()
        shape.Points <- new PointCollection([new Point(x1 , d); new Point(x1 + d, d); new Point(x1+d, d+ h);new Point(x1, d+ h)])
        shape

    let weightMetalStrong length =
        length * metalDensityStrong
    
    let weightMetalNormal length =
        length * metalDensityNormal

    let weightWood length width height =
        let w = length * width * height * woodDensity
        w

    let centroid (polygon:Polygon) =
        let seed = {
            xSum=float 0;
            ySum= float 0;
            n=float 0
        }
        let accResult  = List.ofSeq(polygon.Points)|>List.fold (fun acc p ->{xSum=acc.xSum + p.X;ySum = acc.ySum + p.Y;n = acc.n + float 1}) seed
        new Point(accResult.xSum/ float accResult.n, accResult.ySum/float accResult.n)

    let centerOfGravity (components:ParticleWeight list) =
        let seed = {
            xSum=float 0;
            ySum= float 0;
            n=float 0
        }
        let accResult  = components|>List.fold (fun acc p ->{
                                                            xSum=acc.xSum + p.point.X * p.weight;
                                                            ySum = acc.ySum + p.point.Y * p.weight;
                                                            n = acc.n + p.weight}) seed
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
                
    let polA = polygonA chairBase chairIndent tubeThickness rectangleHeight
    let polB = polygonB chairBase chairIndent tubeThickness rectangleHeight
    let polC = polygonC chairBase chairIndent tubeThickness
    let polD = polygonD chairBase chairIndent tubeThickness
    let polE = polygonE chairBase chairIndent tubeThickness
    let polF = polygonF chairBase tubeThickness chairRest aplhaInDegrees
    let polG = chairBottom chairBase chairThicknes tubeThickness
    let polH = chairBack chairBase chairRest chairThicknes tubeThickness aplhaInDegrees

    let D = centroid (polD)
    let B = centroid (polB)
    let E = centroid (polE)
    let F = centroid (polF)
    let A = centroid (polA)
    let C = centroid (polC)
    let G = centroid (polG)
    let H = centroid (polH)
    let weightA = weightMetalStrong (rectangleWidth+tubeThickness+tubeThickness)
    let weightB = (weightMetalNormal rectangleHeight)*float coefficientDoubles
    let weightC = weightMetalStrong (rectangleWidth+tubeThickness+tubeThickness)
    let weightD = weightMetalNormal (lengthD chairBase chairIndent tubeThickness)*float coefficientDoubles
    let weightE = weightMetalNormal chairIndent*float coefficientDoubles
    let weightF = weightMetalNormal chairRest*float coefficientDoubles
    let weightG = weightWood chairWidth chairBase chairThicknes
    let weightH = weightWood chairWidth chairRest chairThicknes
    let center = centerOfGravity [{point=B;weight=weightB};
                                    {point=D;weight=weightD};
                                    {point=E;weight=weightE};
                                    {point=F;weight=weightF};
                                    {point=A;weight=weightA};
                                    {point=C;weight=weightC};
                                    {point=G;weight=weightG};
                                    {point=H;weight=weightH};
                                ]

    let angle = seatAngle center chairBase chairIndent tubeThickness rectangleHeight attachmentHook

    printfn "x (chairBase): %A" chairBase
    printfn "z (chairIndent): %A" chairIndent 
    printfn "r (chairRest): %A" chairRest
    printfn "d (tubeThickness): %A" tubeThickness
    printfn "h (rectangleHeight): %A"rectangleHeight
    printfn "b (rectangleWidth): %A"rectangleWidth
    printfn "a (attachmentHook): %A"attachmentHook
    printfn "t (chairThicknes): %A"chairThicknes
    printfn "aplhaInDegrees: %A"aplhaInDegrees
    printfn "chairWidth: %A"chairWidth
    printfn "metalDensityStrong (g/mm): %A"metalDensityStrong//g/mm
    printfn "metalDensityNormal (g/mm): %A"metalDensityNormal//g/mm
    printfn "woodDensity (g/mm3): %A"woodDensity//g/mm3

    printfn "A: %A : %A" A weightA
    printfn "B: %A : %A" B weightB
    printfn "C: %A : %A" C weightC
    printfn "D: %A : %A" D weightD
    printfn "E: %A : %A" E weightE
    printfn "F: %A : %A" F weightF
    printfn "G: %A : %A" G weightG
    printfn "H: %A : %A" H weightH
    printfn "Center: %A" center
    printfn "Angle: %Adegrees" angle

    let key = Console.ReadKey()
    0 // return an integer exit code
