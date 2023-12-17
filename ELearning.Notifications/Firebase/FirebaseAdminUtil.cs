namespace ELearning.Notifications.Firebase;

using FirebaseAdmin.Messaging;

using System.Threading.Tasks;

public class FirebaseAdminUtil
{
    public virtual async Task SendMulticastNotificationAsync(List<string> clientsTokens, string title, string body)
    {
        // Construct the message payload
        var message = new MulticastMessage()
        {
            Tokens = clientsTokens,
            Data = new Dictionary<string, string>()
            {
                {"title", title},
                {"body", body},
            }
        };
        var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message).ConfigureAwait(true);
    }

    public virtual async Task SendSingleCasttNotificationAsync(string clientToken, string title, string body)
    {
        // Construct the message payload
        var message = new Message()
        {
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            Token = clientToken
        };

        // Send the message
        var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine($"Successfully sent message: {response}");
    }
}
