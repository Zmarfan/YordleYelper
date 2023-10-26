namespace YordleYelper.bot.data_fetcher; 

public class DataFetcher {
    private const string API_BASE = "https://euw1.api.riotgames.com";

    public readonly DataDragonProxy DataDragonProxy = new();
}