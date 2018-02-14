using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Mobiel.Tests
{
    public class Class1
    {
        public void Test()
        {
            var polygon = new Polygon();
            Point centroid = polygon.Points.Aggregate(
                new { xSum = 0.0, ySum = 0.0, n = 0 },
                (acc, p) => new{
                    xSum = acc.xSum + p.X,
                    ySum = acc.ySum + p.Y,
                    n = acc.n + 1
                 },
                acc => new Point(acc.xSum / acc.n, acc.ySum / acc.n));
        }
    }
}
