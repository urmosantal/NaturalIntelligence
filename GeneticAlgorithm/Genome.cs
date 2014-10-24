using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalIntelligence.GeneticAlgorithm
{
    public class Genome<TGene> : IList<TGene>, ICloneable
    {
        public virtual double Fitness { get; set; }

        #region IList

        private IList<TGene> genes;

        public Genome(params TGene[] genes) : this(genes.ToList()) { }

        public Genome() : this(new List<TGene>()) { }

        public Genome(IList<TGene> genes)
        {
            this.genes = genes;
        }

        public int IndexOf(TGene item)
        {
            return genes.IndexOf(item);
        }

        public void Insert(int index, TGene item)
        {
            genes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            genes.RemoveAt(index);
        }

        public TGene this[int index]
        {
            get
            {
                return genes[index];
            }
            set
            {
                genes[index] = value;
            }
        }

        public void Add(TGene item)
        {
            genes.Add(item);
        }

        public void Clear()
        {
            genes.Clear();
        }

        public bool Contains(TGene item)
        {
            return genes.Contains(item);
        }

        public void CopyTo(TGene[] array, int arrayIndex)
        {
            genes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return genes.Count; }
        }

        public bool IsReadOnly
        {
            get { return genes.IsReadOnly; }
        }

        public bool Remove(TGene item)
        {
            return genes.Remove(item);
        }

        public IEnumerator<TGene> GetEnumerator()
        {
            return genes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return genes.GetEnumerator();
        }

        #endregion

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Genome<TGene> Clone()
        {
            return new Genome<TGene>(genes);
        }
    }
}
