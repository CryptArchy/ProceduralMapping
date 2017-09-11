using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Solver
{
    public class Variable<T> : IEquatable<Variable<T>>
    {
        public readonly static Variable<T> Empty = new Variable<T>(0, new HashSet<T>());

        public int Id { get; private set; }
        public HashSet<T> Domain { get; private set; }
        public List<Action<Variable<T>>> Constraints { get; private set; }
        public bool IsEmpty { get { return Domain.Count == 0; } }
        public bool IsSolved { get { return Domain.Count == 1; } }

        public Variable(int id, T constant)
        {
            Id = id;
            Domain = new HashSet<T>() { constant };
            Constraints = new List<Action<Variable<T>>>();
        }

        public Variable(int id, HashSet<T> domain)
        {
            Id = id;
            Domain = new HashSet<T>(domain);
            Constraints = new List<Action<Variable<T>>>();
        }

        public Variable(int id, HashSet<T> domain, List<Action<Variable<T>>> constraints)
        {
            Id = id;
            Domain = new HashSet<T>(domain);
            Constraints = constraints;
        }

        public Variable<T> Run()
        {
            foreach (var c in Constraints)
            {
                if (Domain.Count > 1)
                    c(this);
                else
                    break;
            }
            return this;
        }

        public Variable<T> Clone()
        {
            return new Variable<T>(this.Id, this.Domain, this.Constraints);
        }

        public Variable<T> DeepClone()
        {
            return new Variable<T>(this.Id, this.Domain, new List<Action<Variable<T>>>(this.Constraints));
        }

        public Variable<T> Update(HashSet<T> vals)
        {
            this.Domain = vals;
            return this;
        }

        public Variable<T> Constrain(Action<Variable<T>> constraint)
        {
            this.Constraints.Add(constraint);
            return this;
        }

        public Variable<T> Constrain(IEnumerable<Action<Variable<T>>> constraints)
        {
            this.Constraints.AddRange(constraints);
            return this;
        }

        public override bool Equals(object obj)
        {
            var that = obj as Variable<T>;
            return that != null && Equals(that);
        }

        bool IEquatable<Variable<T>>.Equals(Variable<T> other)
        {
            return this.Domain.SetEquals(other.Domain);
        }            
    }
}
