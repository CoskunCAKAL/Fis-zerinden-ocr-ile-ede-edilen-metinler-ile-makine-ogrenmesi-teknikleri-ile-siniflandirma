using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Microsoft.OpenApi.Writers;

namespace Ocr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Valuesıd : ControllerBase
    {
        public string Get(int ay,int yıl)
        {
            SqlConnection baglanti = new SqlConnection("Data Source = coskun; Initial Catalog = Sunum; Integrated Security = True");

            SqlCommand komut;

            
            SqlDataAdapter da = new SqlDataAdapter("select * from model where MONTH(CONVERT(date, Tarih, 105)) ='" + ay + "' and YEAR(CONVERT(date, Tarih, 105)) = '" + yıl + "' ", baglanti);
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
