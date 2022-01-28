namespace IcyBot.Logic;

public static class Handlers
{
    public static void Add(DiscordClient sender)
    {
        sender.ChannelCreated += ChannelCreated;
        sender.ChannelDeleted += ChannelDeleted;
        sender.ChannelUpdated += ChannelUpdated;
        sender.ChannelPinsUpdated += ChannelPinsUpdated;

        sender.GuildRoleCreated += GuildRoleCreated;
        sender.GuildRoleDeleted += GuildRoleDeleted;
        sender.GuildRoleUpdated += GuildRoleUpdated;

        sender.GuildBanAdded += GuildBanAdded;
        sender.GuildBanRemoved += GuildBanRemoved;

        sender.GuildEmojisUpdated += GuildEmojisUpdated;
        sender.GuildUpdated += GuildUpdated;

        sender.GuildMemberAdded += GuildMemberAdded;
        sender.GuildMemberRemoved += GuildMemberRemoved;
        sender.GuildMemberUpdated += GuildMemberUpdated;

        sender.InviteCreated += InviteCreated;
        sender.InviteDeleted += InviteDeleted;

        sender.MessageCreated += MessageCreated;
        sender.MessageDeleted += MessageDeleted;
        sender.MessageUpdated += MessageUpdated;
        sender.MessagesBulkDeleted += MessagesBulkDeleted;

        sender.MessageReactionAdded += MessageReactionAdded;
        sender.MessageReactionRemoved += MessageReactionRemoved;
        sender.MessageReactionRemovedEmoji += MessageReactionRemovedEmoji;
        sender.MessageReactionsCleared += MessageReactionsCleared;

        sender.VoiceServerUpdated += VoiceServerUpdated;
        sender.VoiceStateUpdated += VoiceStateUpdated;

        Log.Info("Added all handlers.", ConsoleColor.Yellow, "Logs");
    }

    #region Channel
    public static Task ChannelCreated(DiscordClient sender, ChannelCreateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task ChannelDeleted(DiscordClient sender, ChannelDeleteEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task ChannelUpdated(DiscordClient sender, ChannelUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task ChannelPinsUpdated(DiscordClient sender, ChannelPinsUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Role
    public static Task GuildRoleCreated(DiscordClient sender, GuildRoleCreateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildRoleDeleted(DiscordClient sender, GuildRoleDeleteEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildRoleUpdated(DiscordClient sender, GuildRoleUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Ban
    public static Task GuildBanAdded(DiscordClient sender, GuildBanAddEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildBanRemoved(DiscordClient sender, GuildBanRemoveEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Other
    public static Task GuildEmojisUpdated(DiscordClient sender, GuildEmojisUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildUpdated(DiscordClient sender, GuildUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Member
    public static Task GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task GuildMemberUpdated(DiscordClient sender, GuildMemberUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Invite
    public static Task InviteCreated(DiscordClient sender, InviteCreateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task InviteDeleted(DiscordClient sender, InviteDeleteEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Message
    public static Task MessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task MessageDeleted(DiscordClient sender, MessageDeleteEventArgs e)
    {
        try
        {
            if (Shared.Snipes.Find(Snipe => Snipe.Channel.ID == e.Channel.Id) is SnipeModel Snipe)
            {
                Snipe.User = new(e.Message.Author.Username, e.Message.Author.Id);
                Snipe.ID = e.Message.Id;
                Snipe.Content = e.Message.Content;
                Snipe.AttachmentUrls = e.Message.Attachments.Select(x => x.Url).ToArray();
                Snipe.DateTime = DateTime.UtcNow;
            }
            else
                Shared.Snipes.Add(new(
                    new(e.Channel.Name, e.Channel.Id),
                    new(e.Message.Author.Username, e.Message.Author.Id),
                    e.Message.Id,
                    e.Message.Content,
                    e.Message.Attachments.Select(x => x.Url).ToArray(),
                    DateTime.UtcNow));

            Json.SerializeToFile(Shared.Snipes, "Snipes");
            Log.Info($"Message Deleted - Channel: {e.Channel.Name}, User: {e.Message.Author.Username}, Attachments: {e.Message.Attachments.Count}, ID: {e.Message.Id}", ConsoleColor.Yellow, "Logs");
        }
        catch { Log.Error("Message Deleted - Error", ConsoleColor.Yellow, "Logs"); }

        return Task.CompletedTask;
    }
    public static Task MessageUpdated(DiscordClient sender, MessageUpdateEventArgs e)
    {
        try
        {
            if (Shared.ESnipes.Find(ESnipe => ESnipe.Channel.ID == e.Channel.Id) is SnipeModel ESnipe)
            {
                ESnipe.User = new(e.Message.Author.Username, e.Message.Author.Id);
                ESnipe.ID = e.Message.Id;
                ESnipe.Content = e.MessageBefore.Content;
                ESnipe.DateTime = DateTime.UtcNow;
                ESnipe.ContentAft = e.Message.Content;
            }
            else
                Shared.ESnipes.Add(new(
                    new(e.Channel.Name, e.Channel.Id),
                    new(e.Message.Author.Username, e.Message.Author.Id),
                    e.Message.Id,
                    e.MessageBefore.Content,
                    Array.Empty<string>(),
                    DateTime.UtcNow,
                    e.Message.Content));

            Json.SerializeToFile(Shared.ESnipes, "ESnipes");
            Log.Info($"Message Edited - Channel: {e.Channel.Name}, User: {e.Message.Author.Username}, ID: {e.Message.Id}", ConsoleColor.Yellow, "Logs");
        }
        catch { Log.Error("Message Edited - Error", ConsoleColor.Yellow, "Logs"); }

        return Task.CompletedTask;
    }
    public static Task MessagesBulkDeleted(DiscordClient sender, MessageBulkDeleteEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Reaction
    public static Task MessageReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task MessageReactionRemoved(DiscordClient sender, MessageReactionRemoveEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task MessageReactionRemovedEmoji(DiscordClient sender, MessageReactionRemoveEmojiEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task MessageReactionsCleared(DiscordClient sender, MessageReactionsClearEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Voice
    public static Task VoiceServerUpdated(DiscordClient sender, VoiceServerUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    public static Task VoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion
}