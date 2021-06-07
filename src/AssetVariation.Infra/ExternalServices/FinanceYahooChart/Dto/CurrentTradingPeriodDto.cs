namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto
{
    public class CurrentTradingPeriodDto
    {
        public CurrentTradingPeriodDto()
        {
            Pre = new();
            Regular = new();
            Post = new();
        }

        public ItemDto Pre { get; set; }
        public ItemDto Regular { get; set; }
        public ItemDto Post { get; set; }

    }
}
