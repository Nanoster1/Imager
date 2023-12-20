namespace Imager.ImageResizerAggregator.Server.Services.Interfaces;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(byte[] image, string format, string userEmail);
}