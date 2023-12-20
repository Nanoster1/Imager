using Dapr.Client;

using Imager.Dapr;

using Imager.ImageResizerAggregator.Server.Services.Interfaces;

namespace Imager.ImageResizerAggregator.Server.Services.Implementations;

public class EmailService(DaprClient daprClient) : IEmailService
{
    private const string CreateBindingOperation = "create";

    private readonly DaprClient _daprClient = daprClient;

    public Task SendOrderConfirmationAsync(byte[] image, string format, string userEmail)
    {
        var message = CreateEmailBody(image, format);

        return _daprClient.InvokeBindingAsync(
            DaprComponentsNames.ImagerEmail,
            CreateBindingOperation,
            message,
            new Dictionary<string, string>
            {
                ["emailFrom"] = "i06863489@gmail.com",
                ["emailTo"] = userEmail,
                ["subject"] = $"Your Image from Imager"
            });
    }

    private static string CreateEmailBody(byte[] image, string format) =>
        $"""
        <html>
            <body>
                <h1>Your Image</h1>
                <p>Thank you for your using Imager!</p>
                <p>Your image has been resized.</p>
                <img src="data:image/{format};base64,{Convert.ToBase64String(image)}"/>
                <p>The Imager Team</p>
            </body>
            </html>
        """;
}