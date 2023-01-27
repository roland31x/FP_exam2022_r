using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam2022_r
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(cifreComuneMax(2034932,3253923)); // EX1
            generareSir(15); // EX2
            int[,] mat = genereMatrice();  // EX3
            ordonareMatrice(mat); // EX3
            ordonarePutere("input.in", "output.out"); // EX4
        }
        /*
         * Scrieți o funcție cu numele cifreComuneMax care primește doi parametri x și y de tip întreg
            și întoarce cel mai mare număr natural format din cifre distincte care se poate construi
            folosind cifrele comune din x și y.
         * 
         */
        static int cifreComuneMax(int x, int y) // EX1
        {
            int[] frx = new int[10];
            int[] fry = new int[10];
            int toreturn = 0;

            while(x > 0) // vectori de frecventa pt x si y
            {
                frx[x % 10] += 1;
                x /= 10;
            }
            while(y > 0)
            {
                fry[y % 10] += 1;
                y /= 10;
            }

            for(int i = 9; i >= 0; i--) // cel mai mare nr natural din cifre distincte comune
            {
                if (frx[i] > 0 && fry[i] > 0) // parcurgem vectorii de frecventa de la 9 la 0, verificam daca au cifre comune si formam numarul de returnat
                {
                    toreturn *= 10;
                    toreturn += i;
                }
            }
            return toreturn;
        }
        //Scrieți o funcție care primește ca parametru un număr natural nenul n și afișează primele n
        //elemente ale șirului 1,1,1,2,2,1,1,1,2,2,2,3,3,3,...
        // 1
        // 11 22
        // 111 222 333
        // 1111 2222 3333 4444
        static void generareSir(int n)  // EX2
        {
            int contor = 0;
            int i = 1;
            while (contor < n)
            {
                for (int j = 1; j <= i; j++)
                {
                    for (int k = 1; k <= i; k++)
                    {
                        Console.Write(j + " ");
                        contor++;
                        if (contor == n)
                        {
                            return;
                        }
                    }
                }
                i++;
            }
        }
        /*
         * Scrieți o funcție cu numele ordonareMatrice care primește ca parametru un tablou
            bidimensional cu n linii și m coloane. Funcția va rearanja liniile matricei în ordine crescătoare
            după suma elementelor de pe linii. Dupa rearanajare matricea va fi afișată pe ecran cu
            ajutorul unei funcții cu numele afisareMatrice.
         */
        static void ordonareMatrice(int[,] mat) // EX3  
        {
            afisareMatrice(mat);
            int n = mat.GetLength(0);
            int m = mat.GetLength(1);

            int[] sumalinii = new int[n];
            for(int i = 0; i < n; i++)   // se calculeaza suma liniilor
            {
                for(int j = 0; j < m; j++)
                {
                    sumalinii[i] += mat[i, j];
                }
            }

            for(int i = 0; i < n - 1; i++)  // se aranjeaza liniile dupa selectionsort, si se face swap la elementele din matrici
            {
                for(int j = i + 1; j < n; j++)
                {
                    if (sumalinii[i] > sumalinii[j])
                    {
                        (sumalinii[i], sumalinii[j]) = (sumalinii[j], sumalinii[i]);
                        for(int k = 0; k < m; k++)
                        {
                            (mat[i,k], mat[j,k]) = (mat[j,k], mat[i,k]);
                        }
                    }
                }
            }
            afisareMatrice(mat);

        }
        static void afisareMatrice(int[,] mat)
        {
            Console.WriteLine();
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write($" {mat[i,j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static int[,] genereMatrice()
        {
            Random rng = new Random();
            int[,] mat = new int[rng.Next(2, 6), rng.Next(2, 6)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = rng.Next(-9, 10);
                }
               
            }
            return mat;
        }
        /*
         * Scrieți o funcție cu numele ordonarePutere care primește doi parametri de tip string cu
            numele fin și fout, parametri ce reprezintă numele a două fișiere. Fișierul indicat de fin
            conține pe prima linie un număr natural n iar pe a doua linie un șir de n numere naturale
            separate prin spații. Programul va scrie în fișierul indicat de fout numerele citite de pe a
            doua linie a primului fișier ordonate crescător după puterea lor, iar dacă puterea este egală,
            descrescător după valoarea numerelor. Puterea unui număr este egală cu numărul de valori
            egale cu 1 din reprezentarea sa binară.
         * 
         */
        static void ordonarePutere(string fin, string fout) // EX4
        {
            int[] num;
            int[] weights;
            using (StreamReader sr = new StreamReader(fin))   // etapa parsing
            {
                int n = int.Parse(sr.ReadLine());
                string[] tokens = sr.ReadLine().Split(' ');
                num = new int[n];
                for(int i = 0; i < n; i++)
                {
                    num[i] = int.Parse(tokens[i]);
                }
                weights = new int[n];
            }
            for(int i = 0; i < num.Length; i++)   // etapa in care gasim puterea fiecarui numar
            {
                int count = 0;
                int aux = num[i];
                while(aux > 0)
                {
                    if(aux % 2 == 1)
                    {
                        count++;
                    }
                    aux /= 2;
                }
                weights[i] = count;
            }
            for(int i = 0; i < num.Length - 1; i++) // sortare bi-criteriala
            {
                for(int j = i + 1; j < num.Length; j++)
                {
                    if (weights[i] > weights[j])
                    {
                        (weights[i], weights[j]) = (weights[j], weights[i]);
                        (num[i], num[j]) = (num[j], num[i]);
                    }
                    if (weights[i] == weights[j])
                    {
                        if (num[i] < num[j])
                        {
                            (weights[i], weights[j]) = (weights[j], weights[i]);
                            (num[i], num[j]) = (num[j], num[i]);
                        }
                    }
                }
            }
            File.Create(fout).Dispose();  // scriere in fisier
            using (StreamWriter sw = new StreamWriter(fout))
            {
                for (int i = 0; i < num.Length; i++)
                {
                    sw.Write(num[i] + " ");
                }
            }
        }
    }
}
