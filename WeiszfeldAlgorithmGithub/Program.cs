using System;
using System.Collections.Generic;

namespace WeiszfeldAlgorithm
{

    /// <summary>
    /// Класс точек.
    /// Содержит определение взвешенной точки, 
    /// а также операции сложения двух точек (аналогично сложению векторов) 
    /// и умножения точки на число (Аналогично умножению вектора на число).
    /// </summary>
    class Point
    {
        public double Weight {get;set;}//Вес точки
        public double[] Coordinates { get; set; }//Координаты точки

       //Конструкторы точек
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


        public Point Multiplicate(double m)//Умножение
        {
            Point a = new Point(this.Coordinates.Length);
             for(int i = 0; i<Coordinates.Length; i++)
            {
                a.Coordinates[i] = this.Coordinates[i]* m;
            }
            return a;
        }
        public Point Add(Point a)//Сложение
        {
            for (int i = 0; i < Coordinates.Length; i++)
            {
                Coordinates[i] += a.Coordinates[i];
            }
            return this;
        }
    }


    /// <summary>
    /// Собственно программа
    /// </summary>
    class Program
    {

        static double Distance(Point a, Point b)//Функция для нахождения расстояния между двумя точками
        {
            double res = 0;
            for(int i = 0; i<a.Coordinates.Length; i++)
            {
                res += Math.Pow((a.Coordinates[i] - b.Coordinates[i]) , 2);
            }
            res = Math.Pow(res, 0.5);
            return res;
        }
    
        static void WeiszfeldAlgorithmProcess(int it, List<Point> points)//Собственно алгоритм Вайсфельда
        {
            Random rnd = new Random();
            Point tmp = points[0];
            int n = points[0].Coordinates.Length;
            int k = points.Count;
            //Генерация случайной точки, не входящей в исходное множество точек
            while (points.Contains(tmp))
            {
                double tmpw = rnd.Next();
                double[] tmpc = new double[n];
                for (int i = 0; i<n; i++)
                {
                    tmpc[i] = rnd.NextDouble();
                }
                tmp = new Point(tmpw, tmpc);
            }
            //Теперь, когда есть случайная точка, выполняется основной алгоритм
            for (int cnt = 0; cnt<it; cnt++)
            {
                Point numerator = new Point(n);
                double denominator = new double();
                for(int i = 0; i<points.Count; i++)
                {
                    Point a = new Point(points[i].Weight, points[i].Coordinates);
                    Point asd = (a.Multiplicate(points[i].Weight)).Multiplicate(1 / (Distance(tmp, points[i])));
                    //В каждой итерации цикла числитель и знаменатель складываются с i-тым слагаемым
                    numerator.Add(asd);
                    denominator += points[i].Weight / (Distance(tmp, points[i]));
                }
                tmp = numerator.Multiplicate(1 / denominator);
            }
            //Вывод координат точки, полученной после it итераций
            Console.WriteLine("Результирующая точка имеет координаты:");
            for(int i = 0; i<n; i++)
            {
                Console.WriteLine(tmp.Coordinates[i]);
            }
        }

        static void Main(string[] args)
        {
            int n, k;//Количество измерений и точек
            Console.WriteLine("Введите количество измерений");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество точек");
            k = int.Parse(Console.ReadLine());
            List<Point> points = new List<Point>();
            Console.WriteLine("Введите веса и координаты точек как 'W X1...Xk'");
            for (int i = 0; i<k; i++)//Ввод точек
            {
                string[] input = Console.ReadLine().Split();
                double[] tmpc = new double[n];//Временный массив для координат
                for(int j = 0; j<n; j++)
                {
                    tmpc[j] = double.Parse(input[j + 1]);
                }
                //Генерация новой точки и её добавление в список
                points.Add(new Point(double.Parse(input[0]), tmpc));
            }
            
            Console.WriteLine("Введите количество итераций");
            int iter = int.Parse(Console.ReadLine());
            WeiszfeldAlgorithmProcess(iter,points);//Вызов самого алгоритма
            Console.ReadKey();
        }
    }
}
