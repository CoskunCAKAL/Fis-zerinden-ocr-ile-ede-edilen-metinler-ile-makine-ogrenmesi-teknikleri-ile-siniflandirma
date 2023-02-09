using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace Ocr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string Get()
        {
            SqlConnection baglanti = new SqlConnection("Data Source = coskun; Initial Catalog = Sunum; Integrated Security = True");

            SqlCommand komut;


            SqlDataAdapter da = new SqlDataAdapter("select * from model", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "Veri bulunamadı";
            }



        }
       
       
    }
}
