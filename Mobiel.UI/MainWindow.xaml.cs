using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mobiel.Lib;

namespace Mobiel.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MobielFactory mobielFactory;

        public MainWindow()
        {
            InitializeComponent();

            File1.PartFactory factory = new File1.PartFactory();

            MobielConfig config = new MobielConfig();
            config.chairIndent = 250;
            config.chairRest = 700;
            config.rectangleHeight = 1200;

            mobielFactory = new MobielFactory();

            List<Color> colors = new List<Color> { Colors.Black, Colors.Blue, Colors.Green, Colors.Gray, Colors.Red, Colors.Purple, Colors.Pink, Colors.Brown, Colors.Cyan };

            Mobiel.Lib.Mobiel mobiel = mobielFactory.Create(config);
            int counter = 0;

            //Ellipse center = new Ellipse();
            //int radius = 7;
            //center.Height = radius * 2;
            //center.Width = radius * 2;
            //center.Fill = Brushes.Red;
            //center.Stroke = Brushes.Red;
            //Canvas.SetLeft(center, mobiel.CenterOfGravity.X - radius);
            //Canvas.SetTop(center, mobiel.CenterOfGravity.Y - radius);
            //canvas.Children.Add(center);

            //foreach (Particle particle in mobiel.Particles)
            //{
            //    particle.Polygon.Stroke = new SolidColorBrush(colors[counter++ % colors.Count]);
            //    particle.Polygon.StrokeThickness = 2;
            //    canvas.Children.Add(particle.Polygon);


            //    Ellipse centroid = new Ellipse();
            //    radius = 5;
            //    centroid.Height = radius * 2;
            //    centroid.Width = radius * 2;
            //    centroid.Fill = Brushes.Black;
            //    centroid.Stroke = Brushes.Black;
            //    Canvas.SetLeft(centroid, particle.Centroid.X - radius);
            //    Canvas.SetTop(centroid, particle.Centroid.Y - radius);
            //    canvas.Children.Add(centroid);

            //};

            foreach (var shape in factory.Create(factory.Config))
            {
                shape.Stroke = new SolidColorBrush(colors[counter++ % colors.Count]);
                shape.StrokeThickness = 2;
                canvas.Children.Add(shape);
            }


        }

        //private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    startPoint = e.GetPosition(canvas);

        //    rect = new Rectangle();
        //    rect.Stroke = Brushes.LightBlue;
        //    rect.StrokeThickness = 2;

        //    Canvas.SetLeft(rect, startPoint.X);
        //    Canvas.SetTop(rect, startPoint.Y);
        //    canvas.Children.Add(rect);

        //    //Polygon polygonA = polygons.A;
        //    //Polygon polygonB = polygons.B;
        //    //Polygon polygonC = polygons.C;
        //    //Polygon polygonD = polygons.D;
        //    //Polygon polygonE = polygons.E;
        //    //Polygon polygonF = polygons.F;
        //    //Polygon polygonG = polygons.G;
        //    //Polygon polygonH = polygons.H;
        //    //polygonA.Stroke = Brushes.LightBlue;
        //    //polygonA.StrokeThickness = 2;




        //}

        //private void Canvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Released || rect == null)
        //        return;

        //    var pos = e.GetPosition(canvas);

        //    var x = Math.Min(pos.X, startPoint.X);
        //    var y = Math.Min(pos.Y, startPoint.Y);

        //    var w = Math.Max(pos.X, startPoint.X) - x;
        //    var h = Math.Max(pos.Y, startPoint.Y) - y;

        //    rect.Width = w;
        //    rect.Height = h;

        //    Canvas.SetLeft(rect, x);
        //    Canvas.SetTop(rect, y);
        //}

        //private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    rect = null;
        //}
    }
}
