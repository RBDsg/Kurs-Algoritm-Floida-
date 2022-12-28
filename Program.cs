using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace kurs2
{
    internal class Program
    {
        struct graf
        {
            public int[,] matrSM;
        }
        const int w = 30;
        const int h = 8;

        //АЛГОРИТМ ФЛОЙДА \/ \/ \/
        static void floid(int[,]grf , int[][,] matr)
        {
            int ras = Convert.ToInt32(Math.Sqrt(grf.Length));
            for(int i = 0; i < ras; i++)
            {
                for (int j = 0; j < ras; j++)
                {
                    matr[0][i,j] = grf[i,j];
                }
            }
            for (int v = 0; v < ras; v++)
            {
                for (int i = 0; i < ras; i++)
                {
                    for (int j = 0; j < ras; j++)
                    {
                        matr[v + 1][i, j] = matr[v][i, j];
                    }
                }
                for (int i = 0; i < ras; i++)
                {
                    for (int j = 0; j < ras; j++)
                    {
                        if (i == v || j == v) { continue; }
                        if (i == j) { continue; }
                        if (matr[v][v, j] != 0 && matr[v][i, v] != 0)
                        {
                            if (matr[v][i, j] == 0)
                            {
                                matr[v+1][i, j] = matr[v][v, j] + matr[v][i, v];
                            }
                            else
                            {
                                if (matr[v][i, j] > matr[v][v, j] + matr[v][i, v]) { matr[v+1][i, j] = matr[v][v, j] + matr[v][i, v]; }
                            }
                        }

                    }
                }
            }
        }
        //ГЕНЕРАЦИЯ ГРАФА \/ \/ \/
        static void graf_gen(int[,] grf)
        {
            int ras = Convert.ToInt32(Math.Sqrt(grf.Length));
            Random rand = new Random(ras);
            for (int i = 0; i < ras; i++)
            {
                for (int j = 0; j < ras; j++)
                {
                    if (i == j) { grf[i, j] = 0; }
                    else { grf[i, j] = rand.Next(3); }
                }
            }
        }
        //ВЫВОД МАТРИЦЫ ГРАФА \/ \/ \/
        static void graf_print(int[,] grf, int x = 1, int y = 1)
        {
            Console.SetCursorPosition(x, y);
            int ras = Convert.ToInt32(Math.Sqrt(grf.Length));
            for (int i = 0; i <= ras; i++)
            {
                for (int j = 0; j <= ras; j++)
                {
                    if (i == 0 && j != 0)
                    {
                        Console.Write(" " + Convert.ToChar(64 + j));
                        continue;
                    }
                    if (j == 0 && i != 0)
                    {
                        Console.Write(Convert.ToChar(64 + i));
                        continue;
                    }
                    if (i == 0 && j == 0)
                    {
                        Console.Write("#");
                        continue;
                    }
                    if (true) { if (grf[i-1, j-1] == 0) { sc(2); } else { sc(1); } }
                    Console.Write(" " + grf[i-1, j-1]);
                    sc(0);
                }
                Console.SetCursorPosition(x, y+1+i);
            }
        }
        //ИЗМЕНЕНИЕ РАЗМЕРА КОНСОЛИ \/ \/ \/
        static void consize(int weigh, int hight)
        {
            Console.SetWindowSize(weigh - 1, hight - 1);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.SetWindowSize(weigh, hight);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
        //ИЗМЕНЕНИЕ ЦВЕТА КОНСОЛИ \/ \/ \/ (0 - жёлтый; 1 - зелённый; 2 - красный)
        static void sc(byte mod)
        {
            switch (mod)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
            }
        }



        static void Main(string[] args)
        {
            Console.Title = "АЛГОРИТМ ФЛОЙДА";
            Console.CursorVisible = false;
            string path = "E:\\result.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLineAsync();
            }
            int mod = 0;
            graf grf = new graf();
            int[][,] matr = new int[1][,];
            sc(0);
            consize(w, h);
        MenuMain:
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine(" 1) Сгенерировать граф");
            Console.WriteLine(" 2) Вывести граф");
            Console.WriteLine(" 3) Вывести результат");
            Console.WriteLine(" 4) Промежуточные матрицы\n");
            Console.Write("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            mod = Convert.ToInt32(Console.ReadKey().KeyChar);
            if ((mod < 49 || mod > 52) && mod != 32)
            {
                Console.Clear();
                consize(w, h);
                goto MenuMain;
            }
            switch (mod)
            {
                case 49://СОЗДАНИЕ ГРАФА
                    {
                        int ras;
                        Console.Clear();
                        consize(w, 8);
                    ERROR1:
                        Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                        Console.WriteLine("     Введите размер графа");
                        Console.WriteLine("         (от 2 до 14)\n\n\n");
                        Console.Write("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                        Console.SetCursorPosition(15, Console.WindowHeight - 3);
                        if (int.TryParse(Console.ReadLine(), out ras) == false)
                        {
                            Console.Clear();
                            consize(w, 12);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine("  введено некоректное число\n");
                            sc(0);
                            goto ERROR1;
                        }
                        if (ras < 2)
                        {
                            Console.Clear();
                            consize(w, 13);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine("     Размер матрицы должен");
                            Console.WriteLine("         быть больше!\n");
                            sc(0);
                            goto ERROR1;
                        }
                        if (ras > 14)
                        {
                            Console.Clear();
                            consize(w, 12);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine(" превышен максимальный размер\n");
                            sc(0);
                            goto ERROR1;
                        }
                        Console.Clear();
                        consize(w, 7);
                        ERROR2:
                        Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                        Console.WriteLine("  Введит тип ввода:");
                        Console.WriteLine("  1)Автоматический");
                        Console.WriteLine("  2)Ручной\n");
                        Console.Write("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                        mod = Convert.ToInt32(Console.ReadKey().KeyChar);
                        if (mod < 49 || mod > 50)
                        {
                            Console.Clear();
                            consize(w, 7);
                            goto ERROR2;
                        }
                        grf.matrSM = new int[ras, ras];
                        matr = new int[ras+1][,];
                        for(int i = 0; i < ras+1; i++) { matr[i] = new int[ras, ras]; }
                        if (mod == 49) { graf_gen(grf.matrSM); }
                        else
                        {
                            Console.Clear();
                            consize(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 3, Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) + 3);
                            Console.CursorVisible = true;
                            for(int i = 0; i < Math.Sqrt(grf.matrSM.Length); i++)
                            {
                                for (int j = 0; j < Math.Sqrt(grf.matrSM.Length); j++)
                                {
                                    Console.SetCursorPosition(1+j*2, i+1);
                                    if (i == j)
                                    {
                                        grf.matrSM[i, j] = 0;
                                        Console.Write(grf.matrSM[i, j]);
                                    }
                                    else
                                    {
                                        grf.matrSM[i, j] = Convert.ToInt32(Console.ReadKey().KeyChar) - 48;
                                    }
                                }
                            }
                            Console.CursorVisible = false;
                        }
                        floid(grf.matrSM, matr);
                        using (StreamWriter writer = new StreamWriter(path, true))
                        {
                            for(int i = 0; i< ras;i++)
                            {
                                for (int j = 0; j < ras; j++)
                                {
                                    writer.WriteAsync(matr[ras][i,j] + " ");
                                }
                                writer.WriteLineAsync();
                            }
                            writer.WriteLineAsync();
                        }
                        Console.Clear();
                        consize(w, h + 8);
                        Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                        sc(1);
                        Console.WriteLine("    !граф успешно создан!\n");
                        Console.WriteLine("     результат алгоритма");
                        Console.WriteLine("         сохранён в:");
                        Console.WriteLine("        " + path + "\n");
                        sc(0);
                        goto MenuMain;
                    }
                case 50://ВЫВОД ГРАФА
                    {
                        if (grf.matrSM == null)
                        {
                            Console.Clear();
                            consize(w, h + 4);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine("     графы не сгенерированы\n");
                            sc(0);
                            goto MenuMain;
                        }
                        Console.Clear();
                        Console.WriteLine();
                        consize(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 3, Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) + 3);
                        graf_print(grf.matrSM);
                        Console.ReadKey();
                        Console.Clear();
                        consize(w, h);
                        goto MenuMain;
                    }
                case 51://ВЫВОД Результата
                    {
                        if (grf.matrSM == null)
                        {
                            Console.Clear();
                            consize(w, h + 4);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine("     графы не сгенерированы\n");
                            sc(0);
                            goto MenuMain;
                        }
                        Console.Clear();
                        Console.WriteLine();
                        consize(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 3, Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) + 3);
                        graf_print(matr[Convert.ToInt32(Math.Sqrt(grf.matrSM.Length))]);
                        Console.ReadKey();
                        Console.Clear();
                        consize(w, h);
                        goto MenuMain;
                    }
                case 52://ВЫВОД промежеточных матриц
                    {
                        if (grf.matrSM == null)
                        {
                            Console.Clear();
                            consize(w, h + 4);
                            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                            sc(2);
                            Console.WriteLine("     графы не сгенерированы\n");
                            sc(0);
                            goto MenuMain;
                        }
                        Console.Clear();
                        int mc = 0;
                        consize((Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 3)*2 + 3, Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) + 7);
                        Point:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        Console.SetCursorPosition(1, 1);
                        Console.Write("A-назад,В-вперёд");
                        Console.SetCursorPosition(1, 3);
                        if (mc == 0) { Console.Write("Ориг "); }
                        else { Console.Write("M:" + mc); }
                        Console.SetCursorPosition(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 7, 3);
                        if (mc + 1 == Convert.ToInt32(Math.Sqrt(grf.matrSM.Length))) { Console.Write("Рез "); }
                        else { Console.Write("M:" + (mc + 1)); }
                        graf_print(matr[mc], 1, 5);
                        graf_print(matr[mc+1], Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 7, 5);
                        for(int i = 0; i < Math.Sqrt(grf.matrSM.Length)+1; i++)
                        {
                            Console.SetCursorPosition(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2+4, 5+i);
                            Console.Write(">");
                        }
                        Console.SetCursorPosition(0, 0);
                        mod = Convert.ToInt32(Console.ReadKey().KeyChar);
                        if (mod != 65 && mod != 97 && mod != 68 && mod != 100)
                        {
                            Console.Clear();
                            consize(w, h);
                            goto MenuMain;
                        }
                        if(mod == 65 || mod == 97)
                        {
                            Console.Write(" ");
                            if (mc == 0) { goto Point; }
                            else 
                            {
                                mc--; 
                                Console.SetCursorPosition(1, 3); 
                                Console.Write("    "); 
                                Console.SetCursorPosition(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 7, 3);
                                Console.Write("    ");
                                goto Point; 
                            }
                        }
                        if (mod == 68 || mod == 100)
                        {
                            Console.Write(" ");
                            if (mc+1 == Convert.ToInt32(Math.Sqrt(grf.matrSM.Length))) { goto Point; }
                            else 
                            {
                                mc++; 
                                Console.SetCursorPosition(1, 3);
                                Console.Write("    ");
                                Console.SetCursorPosition(Convert.ToInt32(Math.Sqrt(grf.matrSM.Length)) * 2 + 7, 3);
                                Console.Write("    ");
                                goto Point;
                            }
                        }
                        break;
                    }
            }
        }
    }
}
