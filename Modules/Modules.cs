using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using System.Globalization;

namespace Cryptonite.Modules
{
    public class Modules : ModuleBase<SocketCommandContext>
    {
        /* TODO:
         * Add User system: 
         * Users tell the system what currencies they have and how many.
         * System can return their current NET WORTH (based on current price of a currency * owned amount of that currency)
         * System can return their list.
         * System is able to remove/adapt data.
         */

        
        // Shows all available commands
        [Command("help")]
        public async Task help()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Gold);
            embed.AddField("get [crypto name]", "`Gets the EUR value of [crypto name]`");
        

            
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        // Get Coin-EUR
        [Command("get")]
        public async Task getEur(string str1)
        {
            string json = "";
            string imgsource = "";
            using (WebClient client = new WebClient())
            {
                Console.WriteLine("Opening WebClients...");
                json = client.DownloadString("https://api.cryptonator.com/api/ticker/"+ str1 + "-eur");
                imgsource = client.DownloadString("https://www.cryptocompare.com/api/data/coinlist/");
            }
            Console.WriteLine("Webclients Closed.");

            //object deserialize
                 bool error=false;
                 var dataObject = JsonConvert.DeserializeObject<dynamic>(json);
                 var imgObject = JsonConvert.DeserializeObject<dynamic>(imgsource);
                
                
            



            if (error==false)
            {

            
            //Price DATA
            string priceString = "The price of " + str1.ToUpper()+" is ";
            float price = float.Parse(dataObject["ticker"]["price"].ToString());
            float changeFloat = float.Parse(dataObject["ticker"]["change"].ToString());
            



            //Img getter
            string imgString = "";
            string imgGet = imgObject["Data"][str1.ToUpper()]["ImageUrl"].ToString();
            imgString = "https://www.cryptocompare.com" + imgGet;

            //Embed
            var embed = new EmbedBuilder();
            embed.AddField(str1.ToUpper() + " Price", priceString + (price / 100000000).ToString() + " ***EUR***");
            if (changeFloat >= 0)
            {
                    //TODO FIX BTC CHANGE
                embed.WithColor(Color.Green);
                embed.AddField("Change", (changeFloat / 10000000).ToString() + "% in the last hour");
            }
            if (changeFloat < 0)
            {
                    //TODO FIX BTC CHANGE
                embed.WithColor(Color.Red);
                embed.AddField("Change", (changeFloat / 10000000).ToString() + "% in the last hour");
            }
            embed.WithThumbnailUrl(imgString);



            await Context.Channel.SendMessageAsync("", false, embed);
            }

            
        }



    }
}
