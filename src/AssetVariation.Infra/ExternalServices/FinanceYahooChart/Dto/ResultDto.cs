namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto
{
    public class ResultDto
    {
        public ResultDto()
        {
            Meta = new();
            Indicators = new();
        }
        public MetaDto Meta { get; set; }
        public long[] Timestamp { get; set; }
        public IndicatorsDto Indicators { get; set; }
    }
}
