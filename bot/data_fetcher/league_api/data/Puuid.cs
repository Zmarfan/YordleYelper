using System;

namespace YordleYelper.bot.data_fetcher.league_api.data; 

public readonly struct Puuid {
    private readonly string _id;

    public Puuid(string id) {
        if (id.Length != 78 && !id.StartsWith("BOT")) {
            throw new ArgumentException("Invalid Player Universal Unique Identifier due to length!");
        }
        
        _id = id;
    }

    public override string ToString() {
        return _id;
    }
}