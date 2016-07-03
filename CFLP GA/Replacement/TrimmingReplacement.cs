using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class TrimmingReplacement : ReplacementBase
    {
        int surviving = 300;
        public TrimmingReplacement(int surviving=300)
        {
            this.surviving = surviving;
        }
        protected override GenePopulation replace(GenePopulation parents, GenePopulation children)
        {
            GenePopulation whole = parents.Append(children);
            GenePopulation survivors = new GenePopulation();
            //Console.WriteLine("count pre:"+whole.Count);
            #region enumerator
            var iterator = whole.GetEnumerator();
            int counter = 0;
            while (counter < surviving)
            {
                if (!iterator.MoveNext())
                {
                    break;
                }
                survivors.Include(iterator.Current);
                counter++;
                //Console.WriteLine(iterator.Current.fitness());
            }
            #endregion
            return survivors;
        }
    }
}
