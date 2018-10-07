using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiszfeldAlgorithmGithub
{
    class Point
    {
        public double Weight {get;set;}
        public double[] Coordinates { get; set; }
       public Point(double we, double[] co)
        {
            Weight = we;
            Coordinates = co;
        }


    }
    class Program
    {
        static double Distance(Point a, Point b)
        {
            double res = 0;
            for(int i = 0; i<a.Coordinates.Length; i++)
            {
                res += Math.Pow((a.Coordinates[i] - b.Coordinates[i]) , 2);
            }
            res = Math.Pow(res, 0.5);
            return res;
        }
        static void Main(string[] args)
        {
            int n, k;//Number of dimensions and points
            n = int.Parse(Console.ReadLine());
            k = int.Parse(Console.ReadLine());
            Point[] points = new Point[k];
            for(int i = 0; i<k; i++)//Points described as W X1 X2 ... Xk
            {
                string[] input = Console.ReadLine().Split();
                double[] tmpc = new double[n];//Temporary coordinates
                for(int j = 0; j<n; j++)
                {
                    tmpc[j] = double.Parse(input[j + 1]);
                }
                points[i] = new Point(double.Parse(input[0]), tmpc);
            }
        }
    }
}
