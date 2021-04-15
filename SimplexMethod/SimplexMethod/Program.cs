using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] table = { {48, -6,  8},
                                {192, 12, 8},
                                {0,  -8,  -7}
            };

            double[] result = new double[2];
            double[,] table_result;
            Simplex S = new Simplex(table);

            //канонічна форма 
            Console.WriteLine("Початковий вигляд");
            S.showTable();

            S.Calculate(result);

            Console.WriteLine();
            Console.WriteLine("Розв'язок:");
            Console.WriteLine("X[1] = " + result[0]);
            Console.WriteLine("X[2] = " + result[1]);
            Console.WriteLine("Z = " + (result[0]*-table[2,1] + result[1] * -table[2, 2]));
            Console.ReadLine();
        }
    }
}
