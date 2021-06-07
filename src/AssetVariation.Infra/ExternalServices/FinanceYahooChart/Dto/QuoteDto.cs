namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto
{
    public class QuoteDto
    {
        public decimal?[] Close { get; set; }
        public decimal?[] Volume { get; set; }
        public decimal?[] Open { get; set; }
        public decimal?[] High { get; set; }
    }
}
