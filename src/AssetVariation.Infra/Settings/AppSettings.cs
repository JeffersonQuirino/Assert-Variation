namespace AssetVariation.Infra.Settings
{
    public  class AppSettings
    {
        public AppSettings()
        {
            this.FinanceYahoo = new();
        }

        public FinanceYahooSettings FinanceYahoo { get; set; }
    }
}
