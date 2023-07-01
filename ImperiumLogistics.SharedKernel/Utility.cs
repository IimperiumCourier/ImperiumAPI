using QRCoder;
using System.Drawing;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace ImperiumLogistics.SharedKernel
{
    public class Utility
    {
        public static string DefaultRiderPassword = "Rider@1234";
        public static string DefaultAdminPassword = "Admin@1234";
        public static int DefaultPageNumber = 1;
        public static int DefaultPageSize = 10;
        public static int MaxPageSize = 20;
        public static int RefreshTokenValidityInDays = 1;
        public static DateTime GetNigerianTime() => DateTime.UtcNow.AddHours(1);

        public const string WelcomeTemplatePath = @"Templates\WelcomeTemplate.html";
        public const string RiderWelcomeTemplatePath = @"Templates\RiderWelcomeTemplate.html";
        public const string AdminWelcomeTemplatePath = @"Templates\AdminWelcomeTemplate.html";

        public static string WelcomeTemplate(string customerName, string companyName, string serviceUrl)
        {
            var path = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), WelcomeTemplatePath));
            var emailBody = path.ReadToEnd();
            emailBody = emailBody
                .Replace("[Customer Name]", customerName.ToSentenceCase())
                  .Replace("[Your Service URL]", serviceUrl);

            return emailBody;
        }

        public static string RiderWelcomeTemplate(string username, string password)
        {
            var path = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), RiderWelcomeTemplatePath));
            var emailBody = path.ReadToEnd();
            emailBody = emailBody
                .Replace("[username]", username)
                  .Replace("[password]", password);

            return emailBody;
        }

        public static string AdminWelcomeTemplate(string username, string password)
        {
            var path = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), AdminWelcomeTemplatePath));
            var emailBody = path.ReadToEnd();
            emailBody = emailBody
                .Replace("[username]", username)
                  .Replace("[password]", password);

            return emailBody;
        }

        public static string GenerateQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode qrCodeHelper = new BitmapByteQRCode(qrCodeData);

            string base64String = Convert.ToBase64String(qrCodeHelper.GetGraphic(5));

            return base64String;
        }

        public static string ConvertImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }



        public static string CompressToBase64(string inputString)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);

            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gzipStream.Write(inputBytes, 0, inputBytes.Length);
                }

                byte[] compressedBytes = outputStream.ToArray();
                string base64String = Convert.ToBase64String(compressedBytes);

                return base64String;
            }
        }

        public static string DecompressFromBase64(string compressedBase64)
        {
            byte[] compressedBytes = Convert.FromBase64String(compressedBase64);

            using (MemoryStream inputStream = new MemoryStream(compressedBytes))
            {
                using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(gzipStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

    }
}
