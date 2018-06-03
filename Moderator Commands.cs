using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace SteamBrosBot2.Modules
{
    [Name("Moderator Commands")]
    [RequireContext(ContextType.Guild)]
    public class Moderator_Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Kick")]
        [Summary("Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick([Remainder]SocketGuildUser user)
        {
            await ReplyAsync($"Get in the Sea {user.Mention} :middle_finger: ");
            await user.KickAsync();
        }
    }
}
