namespace ELearning.Notifications.Firebase;

using Microsoft.Extensions.DependencyInjection;

public static class FirebaseAdminExtensionMethods
{
    public static void AddFirebaseAdmin(this IServiceCollection services)
    {
        //FirebaseApp.Create(new AppOptions()
        //{
        //    Credential = GoogleCredential.FromFile(path: FirebaseAdminExtensionMethods.GetCredentialsFilePath()),// Replace path
        //});
    }
    private static string GetCredentialsFilePath()
    {
        const string credentialsFileName = "google-services.json";
        string currentDirectory = Directory.GetCurrentDirectory();
        string credentialsFilePath = Path.Combine(currentDirectory, credentialsFileName);

        if (File.Exists(credentialsFilePath))
        {
            return credentialsFilePath;
        }
        else
        {
            throw new FileNotFoundException($"Credentials file '{credentialsFileName}' not found in the application directory.");
        }
    }
}
