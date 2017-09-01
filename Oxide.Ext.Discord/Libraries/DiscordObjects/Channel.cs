﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Libraries.WebSockets;

namespace Oxide.Ext.Discord.Libraries.DiscordObjects
{
    public class Channel
    {
        public string id { get; set; }
        public string type { get; set; }
        public int? position { get; set; }
        public List<object> permission_overwrites { get; set; }
        public string name { get; set; }
        public string topic { get; set; }
        public int? bitrate { get; set; }
        public int? user_limit { get; set; }
        public List<User> recipients { get; set; }
        public string icon { get; set; }

        public static void GetChannel(DiscordClient client, string channelID, Action<Channel> callback = null)
        {
            var channel = client.REST.DoRequest<Channel>($"/channels/{channelID}", "GET");
            callback?.Invoke(channel);
        }

        public void ModifyChannel(DiscordClient client, Channel newChannel, Action<Channel> callback = null)
        {
            var channel = client.REST.DoRequest<Channel>($"/channels/{id}", "PATCH");
            callback?.Invoke(channel);
        }

        public void DeleteChannel(DiscordClient client, Action<Channel> callback = null)
        {
            var channel = client.REST.DoRequest<Channel>($"/channels/{id}", "DELETE");
            callback?.Invoke(channel);
        }

        public void GetChannelMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            var messages = client.REST.DoRequest<List<Message>>($"/channels/{id}/messages", "GET");
            callback?.Invoke(messages);
        }

        public void GetChannelMessage(DiscordClient client, string messageID, Action<Message> callback = null)
        {
            var message = client.REST.DoRequest<Message>($"/channels/{id}/messages/{messageID}", "GET");
            callback?.Invoke(message);
        }

        public void CreateMessage(DiscordClient client, Message message, Action<Message> callback = null)
        {
            var resMessage = client.REST.DoRequest<Message>($"/channels/{id}/messages", "POST", message);
            callback?.Invoke(resMessage);
        }

        public void CreateReaction(DiscordClient client, string messageID, string emoji)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}/reactions/{emoji}/@me", "PUT");
        }

        public void DeleteOwnReaction(DiscordClient client, string messageID, string emoji)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}/reactions/{emoji}/@me", "DELETE");
        }

        public void DeleteOwnReaction(DiscordClient client, string messageID, string emoji, string userID)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}/reactions/{emoji}/{userID}", "DELETE");
        }

        public void GetReactions(DiscordClient client, string messageID, string emoji, Action<List<User>> callback = null)
        {
            var users = client.REST.DoRequest<List<User>>($"/channels/{id}/messages/{messageID}/reactions/{emoji}", "GET");
            callback?.Invoke(users);
        }

        public void DeleteAllReactions(DiscordClient client, string messageID)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}/reactions", "DELETE");
        }

        public void EditMessage(DiscordClient client, Message message, Action<Message> callback = null)
        {
            var newMessage = client.REST.DoRequest<Message>($"/channels/{id}/messages/{message.id}", "PATCH", message);
            callback?.Invoke(newMessage);
        }

        public void DeleteMessage(DiscordClient client, string messageID, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}", "DELETE");
        }

        public void BulkDeleteMessages(DiscordClient client, string[] messageIds)
        {
            var jsonObj = new Dictionary<string, string[]>() { { "messages", messageIds } };
            client.REST.DoRequest($"/channels/{id}/messages/bulk-delete", "POST", jsonObj);
        }
        
        public void EditChannelPermissions(DiscordClient client, string overwriteID, int allow, int deny, string type)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "allow", allow },
                { "deny", deny },
                { "type", type }
            };
            client.REST.DoRequest($"/channels/{id}/permissions/{overwriteID}", "PUT", jsonObj);
        }

        public void GetChannelInvites(DiscordClient client, Action<List<Invite>> callback = null)
        {
            var invites = client.REST.DoRequest<List<Invite>>($"/channels/{id}/invites", "GET");
            callback?.Invoke(invites);
        }
        
        public void CreateChannelInvite(DiscordClient client, Action<Invite> callback = null, int max_age = 86400, int max_uses = 0, bool temporary = false, bool unique = false)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "max_age", max_age },
                { "max_uses", max_uses },
                { "temporary", temporary },
                { "unique", unique }
            };
            var invite = client.REST.DoRequest<Invite>($"/channels/{id}/invites", "POST", jsonObj);
            callback?.Invoke(invite);
        }

        public void DeleteChannelPermission(DiscordClient client, string overwriteID)
        {
            client.REST.DoRequest($"/channels/{id}/permissions/{overwriteID}", "DELETE");
        }

        public void TriggerTypingIndicator(DiscordClient client)
        {
            client.REST.DoRequest($"/channels/{id}/typing", "POST");
        }

        public void GetPinnedMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            var messages = client.REST.DoRequest<List<Message>>($"/channels/{id}/pins", "GET");
            callback?.Invoke(messages);
        }

        public void AddPinnedChannelMessage(DiscordClient client, string messageID)
        {
            client.REST.DoRequest($"/channels/{id}/pins/{messageID}", "PUT");
        }

        public void DeletePinnedChannelMessage(DiscordClient client, string messageID)
        {
            client.REST.DoRequest($"/channels/{id}/pins/{messageID}", "DELETE");
        }

        public void GroupDMAddRecipient(DiscordClient client, string userID, string accessToken, string nick)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "access_token", accessToken },
                { "nick", nick }
            };
            client.REST.DoRequest($"/channels/{id}/recipients/{userID}", "PUT", jsonObj);
        }

        public void GroupDMRemoveRecipient(DiscordClient client, string userID)
        {
            client.REST.DoRequest($"/channels/{id}/recipients/{userID}", "DELETE");
        }
    }
}