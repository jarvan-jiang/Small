using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SudokuTest
{
    class GetSudokuSolution
    {
        private static int[,] arrResult = null;
        private static List<int[,]> lstResult = new List<int[,]>();
        public static DataTable GetSolution(DataTable srcDt)
        {
            DataTable retDt = srcDt.Copy();
            int[,] arrAll = GetArrayFromTable(srcDt);
            if (!CheckBaseData(arrAll))
                return null;
            if (!FindSolution(arrAll))
                return null;
            else
            {
                retDt = GetTableFromArray(arrResult, srcDt);
            }
            return retDt;
        }
        private static bool CheckBaseData(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] != 0 && !CheckInputNum(arr, i, j, arr[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool FindSolution(int[,] arrAll)
        {
            bool isEmpty = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (arrAll[i, j] == 0)
                    {
                        isEmpty = false;
                        return TrySetValue(arrAll, i, j);
                    }
                }
            }
            if (isEmpty)
                arrResult = arrAll;
            return isEmpty;
        }
        private static bool TrySetValue(int[,] arrAll, int x, int y)
        {
            int[,] arrTemp = null;
            arrTemp = arrAll.Clone() as int[,];
            for (int i = 1; i < 10; i++)
            {
                if (CheckInputNum(arrTemp, x, y, i))
                {
                    arrTemp[x, y] = i;
                    if (FindSolution(arrTemp))
                    {
                        arrAll = arrTemp.Clone() as int[,];
                        return true;
                    }
                    else
                    {
                        arrTemp = arrAll.Clone() as int[,];
                        continue;
                    }
                }
            }
            return false;
        }

        private static int[,] GetArrayFromTable(DataTable srcDt)
        {
            int[,] arrAll = new int[srcDt.Columns.Count, srcDt.Rows.Count];
            for (int i = 0; i < srcDt.Rows.Count; i++)
            {
                for (int j = 0; j < srcDt.Columns.Count; j++)
                {
                    arrAll[i, j] = Convert.ToInt32(srcDt.Rows[i][j]);
                }
            }
            return arrAll;
        }
        private static DataTable GetTableFromArray(int[,] arrAll, DataTable dt)
        {
            for (int i = 0; i < arrAll.GetLength(0); i++)
            {
                for (int j = 0; j < arrAll.GetLength(1); j++)
                {
                    dt.Rows[i][j] = arrAll[i, j];
                }
            }
            return dt;
        }


        private static bool CheckInputNum(int[,] arrAll, int x, int y, int value)
        {
            for (int i = 0; i < arrAll.GetLength(0); i++)
            {
                if (i == x)
                    continue;
                if (arrAll[i, y] == value)
                {
                    return false;
                }
            }

            for (int i = 0; i < arrAll.GetLength(1); i++)
            {
                if (i == y)
                    continue;
                if (arrAll[x, i] == value)
                {
                    return false;
                }
            }

            int width = x / 3;
            int height = y / 3;
            for (int i = width * 3; i < width * 3 + 3; i++)
            {
                for (int j = height * 3; j < height * 3 + 3; j++)
                {
                    if (i == x && j == y)
                        continue;
                    if (arrAll[i, j] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
