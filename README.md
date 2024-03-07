# YordleYelper

**YordleYelper** is a League of Legends statistics discord bot developed using [DSharpPlus](https://dsharpplus.github.io/DSharpPlus/).
It utilizes the offical [League of Legends API](https://developer.riotgames.com/apis) as well as the [Data Dragon API](https://developer.riotgames.com/docs/lol#data-dragon_versions).
**YordleYelper** also has it's own [MySQL](https://www.mysql.com/) database where it stores match data for registered users. The application will continuously (with regards to rate limits) fetch all available match data for registered users and base statistics from that data.
Users can interact with the bot (register and query data) through Discord Bot Slash Commands!

## Commands

### Requires No Registration

| Type | Command | Parameters | Description | Image |
| -| - | - | - | - |
| `POST` | `/register` | `riotId` | Register Riot Id for statistics collection | ![register-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/register.png?raw=true) |
| `GET` | `/champion` | `champion` | General overview of a champion: Name, title, lore and tips! | ![champion-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/champion.png?raw=true) |
| `GET` | `/ability` | `champion` `ability` | Detailed information about a champion ability! | ![ability-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/ability.png?raw=true) |
| `GET` | `/item` | `item` | Detailed information about an item! | ![item-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/item.png?raw=true) |

### Requires Registration

| Type | Command | Parameters | Optional Parameters | Description | Image |
| -| - | - | - | - | - |
| `GET` | `/playtime` | `riotId` `champion` | `compareAgainstAll` `separateGameMode` | View playtime for a champion and customize how the generated graph should be displayed! | ![playtime-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/playtime.png?raw=true) |
| `GET` | `/lastplayed` | `riotId` `champion` | - | Shows when a player last played a given champion! | ![lastplayed-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/lastplayed.png?raw=true) |
| `GET` | `/lastplayedmultiple` | `riotId` | `amount` `sortOrder` | Shows when a player last played a multitude of champions! | ![lastplayedmultiple-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/lastplayedmultiple.png?raw=true) |
| `GET` | `/masteryMultiple` | `riotId` | `amount` `filterMastered` | List of champions with most mastery points by a summoner! | ![masterymultiple-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/masterymultiple.png?raw=true) |
| `GET` | `/mastery` | `riotId` `champion` | - | Provides mastery statistics for specific champion! | ![mastery-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/mastery.png?raw=true) |
| `GET` | `/masterydistribution` | `riotId` | `showAvailableChests` | Provides a pie chart over mastery points per champion! | ![masterydistribution-image](https://github.com/Zmarfan/YordleYelper/blob/main/readme-images/masterydistribution.png?raw=true) |
