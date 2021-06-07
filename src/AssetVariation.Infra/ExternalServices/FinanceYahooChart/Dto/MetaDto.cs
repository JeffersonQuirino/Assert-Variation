namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto
{
    public class MetaDto
    {
        public MetaDto()
        {
            CurrentTradingPeriod = new();
        }

        public string Currency { get; set; }
        public string Symbol { get; set; }
        public string ExchangeName { get; set; }
        public int FirstTradeDate { get; set; }
        public int RegularMarketTime { get; set; }
        public string InstrumentType { get; set; }
        public short Gmtoffset { get; set; }
        public string Timezone { get; set; }
        public string ExchangeTimezoneName { get; set; }
        public decimal RegularMarketPrice { get; set; }
        public decimal ChartPreviousClose { get; set; }
        public decimal PpreviousClose { get; set; }
        public short Scale { get; set; }
        public short PriceHint { get; set; }
        public CurrentTradingPeriodDto CurrentTradingPeriod { get; set; }
        public ItemDto[][] TradingPeriods { get; set; }
        public string DataGranularity { get; set; }
        public string Range { get; set; }
        public string[] ValidRanges { get; set; }
    }
}
