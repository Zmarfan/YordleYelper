namespace YordleYelper.bot.data_fetcher.league_api.data; 

public readonly struct SummonerId {
    private readonly string _id;

    public SummonerId(string id) {
        _id = id;
    }

    public override string ToString() {
        return _id;
    }
}