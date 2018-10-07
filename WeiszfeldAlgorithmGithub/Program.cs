using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiszfeldAlgorithmGithub
{
    class Point: ICloneable
    {
        public double Weight {get;set;}
        public double[] Coordinates { get; set; }
       public Point(double we, double[] co)
        {
            Weight = we;
            Coordinates = co;
        }
        public Point(int n)
        {
            Weight = 0;
            Coordinates = new double[n];
        }
        public Point Multiplicate(double m)
        {
            Point a = new Point(this.Coordinates.Length);
             for(int i = 0; i<Coordinates.Length; i++)
            {
                a.Coordinates[i] = this.Coordinates[i]* m;
            }
            return a;
        }
        public Point Add(Point a)
        {
            for (int i = 0; i < Coordinates.Length; i++)
            {
                Coordinates[i] += a.Coordinates[i];
            }
            return this;
        }

        public object Clone()
        {
            return Coordinates.Clone();
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

        static void WeiszfeldAlgorithmProcess(int it, List<Point> points)
        {
            Random rnd = new Random();//We need new random point for the first iteration
            Point tmp = points[0];
            int n = points[0].Coordinates.Length;
            int k = points.Count;

            string[] input = Console.ReadLine().Split();
            double[] tmpc = new double[n];//Temporary coordinates
            for (int j = 0; j < n; j++)
            {
                tmpc[j] = double.Parse(input[j + 1]);
            }

         tmp = new Point(double.Parse(input[0]), tmpc);



            /*while (points.Contains(tmp))
            {
                double tmpw = rnd.Next();
                double[] tmpc = new double[n];
                for (int i = 0; i<n; i++)
                {
                    tmpc[i] = rnd.NextDouble();
                }
                tmp = new Point(tmpw, tmpc);
            }*/

            for (int cnt = 0; cnt<it; cnt++)
            {
                Point numerator = new Point(n);
                double denominator = new double();
                for(int i = 0; i<points.Count; i++)
                {
                    Point a = new Point(points[i].Weight, points[i].Coordinates);
                    Point asd = (a.Multiplicate(points[i].Weight)).Multiplicate(1 / (Distance(tmp, points[i])));
                    numerator.Add(asd);
                    denominator += points[i].Weight / (Distance(tmp, points[i]));
                }
                tmp = numerator.Multiplicate(1 / denominator);
            }
            Console.WriteLine("Result:" + tmp.Weight);
            for(int i = 0; i<n; i++)
            {
                Console.WriteLine(tmp.Coordinates[i]);
            }

        }

        static void Main(string[] args)
        {
            int n, k;//Number of dimensions and points
            Console.WriteLine("Number of dimensions");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Number of points");
            k = int.Parse(Console.ReadLine());
            List<Point> points = new List<Point>();
            Console.WriteLine("Write points as 'W X1...Xk'");
            for (int i = 0; i<k; i++)//Points described as W X1 X2 ... Xk
            {
                string[] input = Console.ReadLine().Split();
                double[] tmpc = new double[n];//Temporary coordinates
                for(int j = 0; j<n; j++)
                {
                    tmpc[j] = double.Parse(input[j + 1]);
                }
                //points[i] = new Point(double.Parse(input[0]), tmpc);
                points.Add(new Point(double.Parse(input[0]), tmpc));
            }
            
            Console.WriteLine("Number of iterations");
            int iter = int.Parse(Console.ReadLine());
            WeiszfeldAlgorithmProcess(iter,points);
        }
    }
}
