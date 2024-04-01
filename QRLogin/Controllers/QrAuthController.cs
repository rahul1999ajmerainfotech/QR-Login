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
        /// Webapp/Desktop app will call this endpoint to generate a QR code for the given payload and wait for the user to scan the QR code.
        /// When User scans the QR code, the mobile app will send the payload with the user's credentials/JWT/Anything_which_verifies_Verifies_User to the server.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("/GetQrCode")]
        public IActionResult GenerateQr([FromBody] QrPayloadRequest request)
        {
            // If the payload is empty, generate a random payload
            var qrPayload = string.IsNullOrEmpty(request.Payload) ? Guid.NewGuid().ToString() : request.Payload;

            //Save the payload to the database with Expiry time
             
            // Create a QR code Data for the given payload
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrPayload, QRCodeGenerator.ECCLevel.Q);

            // Create a byte array that will store the QR code image
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            int pixelSize = 20;
            var qrCodeImage = qrCode.GetGraphic(pixelSize);

            // Return the image as a file
            return File(qrCodeImage, "image/png");
        }

        /// <summary>
        /// Mobile app scan the QR code and send the payload to the server with the user's credentials.
        /// This endpoint will validate the payload and the token and return a JWT token if the payload and token are valid.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/LoginWithQR")]
        public IActionResult Login([FromBody] QrLoginRequest request)
        {

            var payload = request.Payload;
            var userId = request.UserId;

            // Validate the payload.



            // If the payload is valid, then check if the token is valid

            /*
             * JWT will be automatically validated by the middleware.
             */

            // If the token is valid then Generate a JWT token and return it



            // Else return Unauthorized

            return Ok();
        }

    }
}
