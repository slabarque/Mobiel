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
    let aplhaInDegrees = float 30
    let attachmentHook = float 30

    let metalDensity = float 1.394 //g/mm
    let coefficientD = 2
    let coefficientB = 2
    
    
    let lengthD x z d=
         x-d-z

    let radians degrees =
        Math.PI*degrees/float 180

    let degrees radians =
        radians*float 180/Math.PI

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

    let weight length =
        length * metalDensity

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
        printfn "lengthCos: %A" lengthCos
        printfn "lengthLeg: %A" lengthLeg

        let angle = Math.Acos(lengthCos/lengthLeg)
        degrees angle
                
    
    let D = centroid (polygonD chairBase chairIndent tubeThickness)
    let B = centroid (polygonB chairBase chairIndent tubeThickness rectangleHeight)
    let E = centroid (polygonE chairBase chairIndent tubeThickness)
    let F = centroid (polygonF chairBase tubeThickness chairRest aplhaInDegrees)
    let A = centroid (polygonA chairBase chairIndent tubeThickness rectangleHeight)
    let C = centroid (polygonC chairBase chairIndent tubeThickness)
    let weightD = weight (lengthD chairBase chairIndent tubeThickness)
    let weightB = weight rectangleHeight
    let weightE = weight chairIndent
    let weightF = weight chairRest
    let weightA = weight (rectangleWidth+tubeThickness+tubeThickness)
    let weightC = weight (rectangleWidth+tubeThickness+tubeThickness)
    let center = centerOfGravity [{point=B;weight=weightB*float coefficientB};
                                    {point=D;weight=weightD*float coefficientB};
                                    {point=E;weight=weightE*float coefficientB};
                                    {point=F;weight=weightF*float coefficientB};
                                    {point=A;weight=weightA};
                                    {point=C;weight=weightC};
                                ]

    let angle = seatAngle center chairBase chairIndent tubeThickness rectangleHeight attachmentHook

    printfn "A: %A : %A" A weightA
    printfn "B: %A : %A" B weightB
    printfn "C: %A : %A" C weightC
    printfn "D: %A : %A" D weightD
    printfn "E: %A : %A" E weightE
    printfn "F: %A : %A" F weightF
    printfn "Center: %A" center
    printfn "Angle: %Adegrees" angle
    0 // return an integer exit code
