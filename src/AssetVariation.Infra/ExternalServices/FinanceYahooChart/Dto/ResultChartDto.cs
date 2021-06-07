namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto
{
    public  class ResultChartDto
    {
        public ResultChartDto()
        {
            Chart = new();
        }
        public ChartDto Chart { get; set; }
    }
}
