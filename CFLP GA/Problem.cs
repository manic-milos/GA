using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CFLP_GA
{
    class Problem
    {
        public int n;
        public int m;
        public int k;
        public int[] s;
        public int[] f;
        public int[] d;
        public int[,] c;
        private double sumDemands=-1;
        public int[] sortedDemandIndices;
        public void load(StreamReader sr)
        {
            //try
            //{
                //n m
                string nm = sr.ReadLine();
                string[] nim = nm.Split(' ');
                n = int.Parse(nim[0]);
                m = int.Parse(nim[1]);
                //k
                nm = sr.ReadLine();
                k = int.Parse(nm);
                initProblem(n, m, k);
                //d[j]
                nm = sr.ReadLine();
                nim = nm.Split(' ');
                for(int j=0;j<n;j++)
                {
                    d[j] = int.Parse(nim[j]);
                }
                //f[i]
                nm = sr.ReadLine();
                nim = nm.Split(' ');
                for (int i = 0; i < m; i++)
                {
                    f[i] = int.Parse(nim[i]);
                }
                //s[i]
                nm = sr.ReadLine();
                nim = nm.Split(' ');
                for (int i = 0; i < m; i++)
                {
                    s[i] = int.Parse(nim[i]);
                }
                //c[j,i]
                for(int j=0;j<n;j++)
                {
                    nm = sr.ReadLine();
                    nim = nm.Split(' ');
                    for(int i=0;i<m;i++)
                    {
                        c[i, j] = int.Parse(nim[i]);
                    }
                }
                sortDemandIndices();
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
        }
        void sortDemandIndices()
        {
            sortedDemandIndices = new int[n];
            for(int i=0;i<n;i++)
            {
                sortedDemandIndices[i] = i;
            }
            int[] d1=new int[n];
            Array.Copy(d, d1, n);
            Array.Sort(d1, sortedDemandIndices);
        }
        public void destroy()
        {
            n=-1;
            m=-1;
            k=-1;
            s=null;
            f=null;
            d=null;
            c=null;
            sumDemands=-1;
        }
        public double SumDemands
        {
            get
            {
                if(sumDemands>0)
                {
                    return sumDemands;
                }
                sumDemands = 0;
                for(int j=0;j<n;j++)
                {
                    sumDemands += d[j];
                }
                return sumDemands;
            }
        }
        public void initProblem(int n, int m, int k)
        {
            this.n = n;
            this.m = m;
            this.k = k;
            s = new int[m];
            f = new int[m];
            d = new int[n];
            c = new int[m, n];
        }
        public int calculateObjectiveFunction(int[,] x, int[] y)
        {
            int objective = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    objective += c[i, j] * x[i, j];
                }
            }
            for (int j = 0; j < n; j++)
            {
                objective += f[j] * y[j];
            }
            return objective;
        }
        public bool checkDemandCondition(int[,] x)
        {
            for (int j = 0; j < n; j++)
            {
                int s = 0;
                for (int i = 0; i < m; i++)
                {
                    s += x[i, j];
                }
                if (s != d[j])
                {
                    return false;
                }
            }
            return true;
        }
        public bool checkCapacityCondition(int[,] x, int[] y)
        {
            for (int i = 0; i < m; i++)
            {

                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += x[i, j];
                }
                if (sum > y[i] * s[i])
                    return false;
            }
            return true;
        }
        public bool checkCardinalityContraint(int[] y)
        {
            int sum = 0;
            for (int i = 0; i < m; i++)
            {
                sum += y[i];
            }
            if (sum > k)
                return false;
            return true;
        }
        public bool checkAllContraints(int[,] x, int[] y)
        {
            return checkCapacityCondition(x, y) && checkCardinalityContraint(y) && checkDemandCondition(x);
        }
        public string print()
        {
            string sumstring = "";
            //n m / k
            sumstring += n + " " + m + Environment.NewLine + k + Environment.NewLine;

            //d[j]
            for (int j = 0; j < n; j++)
            {
                sumstring += d[j] + " ";
            }
            sumstring += Environment.NewLine;
            
            //f[i]
            for (int i = 0; i < m; i++)
            {
                sumstring += f[i] + " ";
            }

            sumstring += Environment.NewLine;
            //s[i]
            for (int i = 0; i < m; i++)
            {
                sumstring += s[i] + " ";
            }

            sumstring += Environment.NewLine;
            //c[i][j]
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sumstring += c[i, j] + " ";
                }
            }
            sumstring += Environment.NewLine+"----------------------------------------------------";
            return sumstring;
        }
        public bool checkSolution(int[] vars, int sumCapacity)
        {
            if (!checkCardinalityContraint(vars))
                return false;
            int sum = sumCapacity;
            if (sum < SumDemands)
            {
                return false;
            }
            return true;
        }
        public int sumCapacity(int[] vars)
        {
            int sum = 0;
            for(int i=0;i<vars.Length;i++)
            {
                sum += vars[i] * s[i];
            }
            return sum;

        }
    }
}
