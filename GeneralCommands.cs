using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using Newtonsoft.Json;
using Discord.Rest;
using SteamBrosBot2.Services;

namespace SteamBrosBot2.Modules
{
    [Name("General Commands")]
    public class GeneralCommandsModule : ModuleBase<SocketCommandContext>
    {
        //Simple command taking the key from the alerts.json
        [Command("Hello"), Alias("Hi")]
        [Summary("Say hello to the bot")]
        public async Task Hello()
        {
            string name = Context.User.Username;
            var embed = new EmbedBuilder();
            embed.WithTitle("Welcome to the Steam Bros Discord Chat!");
            embed.WithDescription(Utilities.GetFormattedAlert("HELLO_&USERNAME", name));
            embed.WithColor(new Color(255, 0, 144));

            await ReplyAsync("", false, embed);
        }

        //Simple command taking the key from the alerts.json
        [Command("how are you?"), Alias("how are you")]
        [Summary("Ask the Bot how it's doing")]
        public async Task HowAreYou()
        {
            string name = Context.User.Username;
            await ReplyAsync(Utilities.GetFormattedAlert("HOWAREYOU", name));
        }

        //Simple command taking the key from the alerts.json
        [Command("Goodbye"), Alias("Bye")]
        [Summary("Say Goodbye to the Bot")]
        public async Task Goodbye()
        {
            string name = Context.User.Username;
            await ReplyAsync(Utilities.GetFormattedAlert("GOODBYE", name));
        }

        //Simple command taking the key from the alerts.json
        [Command("Thanks"), Alias("Thank You")]
        [Summary("Say Thanks to the Bot")]
        public async Task Thanks()
        {
            string name = Context.User.Username;
            await ReplyAsync(Utilities.GetFormattedAlert("THANKS", name));
        }

        //Simple command taking the key from the alerts.json
        [Command("Worst Gamer?"), Alias("WorstGamer", "WorstGamer?", "Worst Gamer")]
        [Summary("Find out who the worst gamer is")]
        public async Task WorstGamer()
        {
            string name = Context.User.Username;
            await ReplyAsync(Utilities.GetFormattedAlert("WORSTGAMER", name));
        }

        //Simple command taking the key from the alerts.json
        [Command("Best Gamer?"), Alias("BestGamer", "BestGamer?", "Best Gamer")]
        [Summary("Find out who the worst gamer is")]
        public async Task BestGamer()
        {
            string name = Context.User.Username;
            await ReplyAsync(Utilities.GetFormattedAlert("BESTGAMER", name));
        }

        //Simple command taking the key from the alerts.json
        [Command("Who is a Retard?"), Alias("Retard", "Whosaretard", "Who is a Retard")]
        [Summary("Find out who is the servers Retard")]
        public async Task WhoIsARetard()
        {
            await ReplyAsync(Utilities.GetFormattedAlert("RETARD"));
        }

        //Simple command taking the key from the alerts.json
        [Command("Favourite Film?"), Alias("FaveFilm?", "Favourite Film", "FaveFilm", "Fave Film", "Fave Film?")]
        [Summary("Find out the Bots favourite film")]
        public async Task FaveFilm()
        {
            await ReplyAsync(Utilities.GetFormattedAlert("FAVEFILM"));
        }

        //Simple command taking the key from the alerts.json
        [Command("Favourite Game?"), Alias("FaveGame?", "Favourite Game", "FaveGame", "Fave Game", "Fave Game?")]
        [Summary("Find out the Bots favourite game")]
        public async Task FaveGame()
        {
            await ReplyAsync(Utilities.GetFormattedAlert("FAVEGAME"));
        }

        //Sending a message to a user via DM
        [Command("Tell Me a Secret"), Alias("Secret")]
        [Summary("Receive a secret message from the Bot via DM")]
        public async Task Secret()
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("LOSERSECRET"));
        }

        //Sending message to user via DM who has a specific Role in the server - CURRENTLY BROKEN
        [Command("Server Secret"), Alias("ServerSecret")]
        [Summary("Receive a role specific message from the bot via DM")]
        public async Task RevealSecret()
        {
            if (!UserIsAbsolutePower((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync($"You do not have the power to view this secret\n https://media.giphy.com/media/3ohzdQ1IynzclJldUQ/source.gif");
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SERVERSECRET"));
        }

        private bool UserIsAbsolutePower(SocketGuildUser user)
        {
            string targetRoleName = "AbsolutePower";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}
