using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel
{
    public class TrackingNumberGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerateTrackingNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int trackingNumberLength = 14;

            char[] trackingNumber = new char[trackingNumberLength];

            for (int i = 0; i < trackingNumberLength; i++)
            {
                trackingNumber[i] = chars[random.Next(chars.Length)];
            }

            return new string(trackingNumber);
        }


    }

}
