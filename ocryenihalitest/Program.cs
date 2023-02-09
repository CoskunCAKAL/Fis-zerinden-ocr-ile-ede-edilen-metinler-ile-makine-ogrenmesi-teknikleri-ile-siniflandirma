using System;
using System.Diagnostics;
using Tesseract;
using System.IO;
using System.Data.SqlClient;
using Ocryenihalitest;
using System.Collections;
using System.Runtime.Intrinsics.Arm;
using System.Data.Common;
using Newtonsoft.Json;
using System.Data;

namespace ocryenihalitest
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            string line;

            StreamReader sr = new StreamReader("./Test.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                MLModel1.ModelInput sampleData = new MLModel1.ModelInput()
                {
                    URUN_ADI = line,
                };
                var predictionResult = MLModel1.Predict(sampleData);
                string hate = ($"{predictionResult.PredictedLabel}");
                DateTime d = DateTime.Now;
                SqlConnection baglanti = new SqlConnection("Data Source=coskun;Initial Catalog=Bitirme;Integrated Security=True");

                SqlCommand komut;

                SqlDataReader read;

                baglanti.Open();

                komut = new SqlCommand();

                komut.Connection = baglanti;

                komut.CommandText = "insert into Model (Urun,Kategorı,Tarih) values('" + line + "','" + hate + "','" + d + "' )";

                komut.ExecuteNonQuery();

                baglanti.Close();
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);

        }

       
        



    }
}

