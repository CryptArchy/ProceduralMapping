using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Solver
{
    public static class Constraints
    {
        public static Action<Variable<T>> HasValue<T>(T val)
        {
            return (x => { if (x.Domain.Contains(val)) x.Update(new HashSet<T>() { val }); });
        }

        public static void AreSame<T>(Variable<T> x, Variable<T> y)
        {
            var xiy = new HashSet<T>(x.Domain);
            xiy.IntersectWith(y.Domain);
            x.Update(xiy);
            y.Update(xiy);
        }

        public static void AreDifferent<T>(Variable<T> x, Variable<T> y)
        {
            if (!x.IsSolved || !y.IsSolved || !x.Domain.SetEquals(y.Domain))
            {
                if (x.IsSolved && x.Domain.IsProperSubsetOf(y.Domain))
                {
                    var ydx = new HashSet<T>(y.Domain);
                    ydx.ExceptWith(x.Domain);
                    y.Update(ydx);
                }

                if (y.IsSolved && y.Domain.IsProperSubsetOf(x.Domain))
                {
                    var xdy = new HashSet<T>(x.Domain);
                    xdy.ExceptWith(y.Domain);
                    x.Update(xdy);
                }
            }
        }

        public static void LessThan<T>(Variable<T> x, Variable<T> y)
        {
            var ymax = y.Domain.Max();
            var xmin = x.Domain.Min();
            var comparer = Comparer<T>.Default;
            var rx = new HashSet<T>(x.Domain);
            var ry = new HashSet<T>(y.Domain);

            rx.RemoveWhere(vx => comparer.Compare(vx, ymax) < 0);
            ry.RemoveWhere(vy => comparer.Compare(vy, xmin) > 0);

            if (x.Domain != rx) x.Update(rx);
            if (y.Domain != ry) y.Update(ry);
        }
    }
}
