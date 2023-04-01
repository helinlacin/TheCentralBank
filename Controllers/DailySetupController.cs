using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace TheCentralBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailySetupController : ControllerBase
    {
       [HttpPost]
        public ResponseData Run(RequestData request)
        {
            
            ResponseData result = new ResponseData();


            try
            {
              
                string tcmblink = string.Format("http://www.tcmb.gov.tr/kurlar/{0}.xml",
                    (request.IsToday) ? "today" : string.Format("{2}{1}/{0}{1}{2}",
                    request.Day.ToString().PadLeft(2, '0'), request.Month.ToString().PadLeft(2, '0'), request.Year
                    )
                    );
                result.Data = new List<ResponseDataExchangeRate>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tcmblink);
                if (doc.SelectNodes("Tarih_Date").Count < 1)
                {
                    result.Error = "Currency information not found.";
                    return result;
                }
                foreach (XmlNode node in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    ResponseDataExchangeRate exchangeRate = new ResponseDataExchangeRate();
                    exchangeRate.Code = node.Attributes["Kod"].Value;
                    exchangeRate.Name = node["Isim"].InnerText;
                    exchangeRate.UnitOf = int.Parse(node["Unit"].InnerText);
                    exchangeRate.BuyinRate = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                    exchangeRate.SalesRate = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                    exchangeRate.EffectiveBuyingRate = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                    exchangeRate.EffectiveSalesRate = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));
                    result.Data.Add(exchangeRate);

                }

            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
