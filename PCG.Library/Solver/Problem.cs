using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Solver
{
    public class Problem<T>
    {
        public List<Variable<T>> Variables { get; set; }

        public List<List<T>> Solutions()
        {
            var lastStep = Variables;
            var nextStep = Run(Variables);
            var counter = 0;
            while (!nextStep.SequenceEqual(lastStep) && counter < 10000)
            {
                lastStep = nextStep;
                nextStep = Run(lastStep);
                counter++;
            }

            IEnumerable<IEnumerable<T>> results = new[] { Enumerable.Empty<T>() };
            //results = vs.Aggregate(results, (acc, v) => 
            //    from accseq in acc
            //    from item in v
            //    select accseq.Concat(new[] {item}));

            results = nextStep.Aggregate(results, (acc, vss) =>
                acc.SelectMany(a =>
                    vss.Domain.Select(v =>
                        a.Concat(new[] { v })
                    )
                )
            );
            return results.Select(xs => xs.ToList()).ToList();
        }

        public List<Variable<T>> Run(List<Variable<T>> vars)
        {
            return vars.Select(v => v.Clone().Run()).ToList();
        }

        public Problem<T> Update(Variable<T> vx)
        {
            Variables[vx.Id] = vx;
            return this;
        }

        public Problem<T> Constrain(Action<Variable<T>> f, Variable<T> vx)
        {
            vx.Constraints.Add(f);
            return this;
        }

        public Problem<T> Constrain(Action<Variable<T>, Variable<T>> f, Variable<T> vx, Variable<T> vy)
        {
            Constrain(x => f(x, vy), vx);
            Constrain(y => f(vx, y), vy);
            return this;
        }

        public Problem<T> Constrain(Variable<T> vx, Action<Variable<T>> f)
        {
            return this.Constrain(f, vx);
        }

        public Problem<T> Constrain(Variable<T> vx, Action<Variable<T>, Variable<T>> f, Variable<T> vy)
        {
            return this.Constrain(f, vx, vy);
        }

        public Problem<T> Constrain(Action<Variable<T>> f)
        {
            foreach (var vx in Variables) { Constrain(f, vx); }
            return this;
        }

        public Problem<T> Constrain(Action<Variable<T>, Variable<T>> f)
        {
            return Constrain(f, Variables);
        }

        public Problem<T> Constrain(Action<Variable<T>, Variable<T>> f, IList<Variable<T>> vs)
        {
            for (int i = 0; i < vs.Count; i++)
            {
                var vx = vs[i];
                for (int j = i + 1; j < vs.Count; j++)
                {
                    var vy = vs[j];
                    Constrain(f, vx, vy);
                }
            }
            return this;
        }
    }
}
