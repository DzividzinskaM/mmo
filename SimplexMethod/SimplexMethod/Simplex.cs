using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMethod
{
    public class Simplex
    {
        double[,] table;

        int m, n;

        List<int> basis; 

        public Simplex(double[,] source)
        {
            m = source.GetLength(0);
            n = source.GetLength(1);
            table = new double[m, n + m - 1];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n)
                        table[i, j] = source[i, j];
                    else
                        table[i, j] = 0;
                }
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }

            n = table.GetLength(1);
        }

        
        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; 

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                table = new_table;
                Console.WriteLine("Нова симплекс таблиця");
                showTable();
            }

            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }

            return table;
        }

        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private int findMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }

            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }


        public void showTable()
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    Console.Write("\t" + Math.Round(table[i, j], 4));
                Console.WriteLine();
            }
        }
    }
}
