using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocr.API.Models;
using System;
using System.IO;
using Tesseract;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Ocryenihalitest;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace Ocr.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {

        [Route("UploadFile")]
        public Response UploadFile([FromForm] FileModel fileModel)
        {

            Response response = new Response();
            try
            {
                string path = Path.Combine(@"C:\Users\cosku\Desktop\ocryenihalitest\Ocr.API\MyImages", fileModel.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                    string folderName = @"C:\Users\cosku\Desktop\ocryenihalitest\Ocr.API\MyImages";
                    string name = fileModel.FileName;
                    var testImagePath = folderName + "\\" + name;
                    using (var engine = new TesseractEngine(@"C:\Users\cosku\Desktop\ocryenihalitest\ocryenihalitest\tessdata", "tur", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromFile(testImagePath))
                        {
                            using (var page = engine.Process(img))
                            {
                                var text = page.GetText();


                                Console.WriteLine("Text:\r\n{0}", text);
                                string textadı = @"C:\Users\cosku\Desktop\ocryenihalitest\Test.txt";
                                FileStream st = new FileStream(textadı,FileMode.OpenOrCreate);
                                using (StreamWriter ws = new StreamWriter(st))
                                {
                                    ws.WriteLine("{0}", text);
                                    ws.Close();
                                }
                                


                            }
                        }
                    }

                }
                response.StatusCode = 200;
                response.ErrorMessage = "Image creted successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Some error occured" + ex.Message;
            }
            string line;

            StreamReader sr = new StreamReader(@"C:\Users\cosku\Desktop\ocryenihalitest\Test.txt");
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
                SqlConnection baglanti = new SqlConnection("Data Source = coskun; Initial Catalog = Sunum; Integrated Security = True");

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
            
            
               System.IO.File.Delete(@"C:\Users\cosku\Desktop\ocryenihalitest\Test.txt");
            
            return response;

        }


    }
}
