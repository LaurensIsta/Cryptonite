using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptonite.Modules;
using Cryptonite;


namespace Cryptonite.Modules
{
    public class UserCommand: ModuleBase<SocketCommandContext>
    {
        
        
        [Command("Create user")]
        public async Task CreateUser(string name)
        {
            //Add user to userList   
            await Task.CompletedTask;
        }
        [Command("Check users")]
        public async Task CheckUser()
        {
            
            //Check all users
            await Task.CompletedTask;
        }
        [Command ("Add coin")]
        public async Task AddCoin(string name, string coin, int amount)
        {
            //Add Coin to User
            await Task.CompletedTask;
        }

    }
}
