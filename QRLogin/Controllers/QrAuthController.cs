using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QRLogin.Models;

namespace QRLogin.Controllers
{
    public class QrAuthController : ControllerBase
    {
        public required QRCodeGenerator qrGenerator;
        public QrAuthController(QRCodeGenerator qrGenerator)
        {
            this.qrGenerator = qrGenerator;
        }

        /// <summary>
        /// Generates a QR code for the given payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("/GetQrCode")]
        public IActionResult GenerateQr([FromBody] QrPayloadRequest request)
        {
            // Create a QR code for the given payload
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(request.Payload, QRCodeGenerator.ECCLevel.Q);

            // Create a byte array that will store the QR code image
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            int pixelSize = 20;
            var qrCodeImage = qrCode.GetGraphic(pixelSize);

            // Return the image as a file
            return File(qrCodeImage, "image/png");

        }
    }
}
