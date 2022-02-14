using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_V3
{
     class Program
     {
          static void Main(string[] args)
          {
              
               Console.WriteLine("[1] Sample RSA\n[2] Complete Test\n");
               Console.Write("\nEnter your choice [1-2]: ");
               char choice = (char)Console.ReadLine()[0];

               switch (choice)
               {
                    case '1':
                         ReadFile("SampleRSA");
                         Console.WriteLine("DONE!");
                         break;

                    case '2':
                         ReadFile("TestRSA");
                         Console.WriteLine("DONE!");
                         break;
                    default:
                         break;
               }
          }

          /* Read Two Numbers from file */
          static void ReadFile(string fileName)
          {
               FileStream file = new FileStream(fileName + ".txt", FileMode.Open, FileAccess.Read);
               StreamReader sr = new StreamReader(file);
               FileStream file2 = new FileStream(fileName + "_MyOutput.txt", FileMode.Create);
               StreamWriter sw = new StreamWriter(file2);

               int cases = int.Parse(sr.ReadLine());

               string N, e_d, M_EM;
               int Enc_Dec;
               for (int i = 0; i < cases; i++)
               {
                    Console.Write("[ " + (i + 1) + " ]  ");

                    N = sr.ReadLine();
                    e_d = sr.ReadLine();
                    M_EM = sr.ReadLine();
                    Enc_Dec = int.Parse(sr.ReadLine());

                    if (Enc_Dec == 0)
                    {
                         double Start_time = System.Environment.TickCount;
                         sw.WriteLine(RSA.Encrypt(M_EM, e_d, N).ToString());
                         double End_time = System.Environment.TickCount;
                         Console.WriteLine("Time: " + (End_time - Start_time) / 1000 + " Second");
                    }
                    else if (Enc_Dec == 1)
                    {
                         double Start_time = System.Environment.TickCount;
                         sw.WriteLine(RSA.Decrypt(M_EM, e_d, N).ToString());
                         double End_time = System.Environment.TickCount;
                         Console.WriteLine("Time: " + (End_time - Start_time) / 1000 + " Second");
                    }
               }
               sw.Close(); file2.Close();
               sr.Close(); file.Close();
          }

          
     }
}
