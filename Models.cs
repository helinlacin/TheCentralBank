
namespace TheCentralBank
{
    public class RequestData
    {
        public int Day { get; set; }    
        public int Month { get; set; }
        public int Year { get; set; }   
        public bool IsToday { get; set; }   
    }
    public class ResponseDataExchangeRate
    {
        public string Code  { get; set; }
        public string Name { get; set; }
        public int  UnitOf { get; set; }
        public decimal BuyinRate { get; set; } 
        public decimal SalesRate { get; set; }  
        public decimal EffectiveBuyingRate { get; set;}
        public decimal EffectiveSalesRate { get; set; }
    }
    public class ResponseData
    {
        public List<ResponseDataExchangeRate> Data { get; set; }
        public string Error { get; set; } 
    }
}
