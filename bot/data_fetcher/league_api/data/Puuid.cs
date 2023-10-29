using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.data; 

public readonly struct Puuid {
    private readonly string _id;

    [JsonConstructor]
    public Puuid(string id) {
        _id = id;
    }

    public override string ToString() {
        return _id;
    }
}