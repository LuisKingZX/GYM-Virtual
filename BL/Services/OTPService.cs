using System;

namespace BL.Services
{
    public class OTPService
    {
        public string GenerateOTP()
        {
            // Implementa la l�gica para generar un OTP
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            return otp;
        }

        public bool VerifyOTP(string otp, string userInput)
        {
            // Implementa la l�gica para verificar el OTP
            return otp == userInput;
        }
    }
}
