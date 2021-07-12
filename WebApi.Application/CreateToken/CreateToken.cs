using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Application.Interfaces;

namespace WebApi.Application.CreateToken
{
    public class CreateToken : ICreateToken
    {
        
        public long CreatTokenAsync(CreateTokenRequest request)
        {
            try
            {
                if(request.CardNumber == 0 || request.Cvv == 0)
                {
                    Log.Information("Card Number or Cvv invalid.");
                    throw new Exception("Card Number or Cvv invalid.");
                }
                   

                string fourNumberCardString = ExtractDigits(request.CardNumber);

                List<int> listFourDigits = SeparatorDigits(fourNumberCardString);

                long token = GenerateToken(listFourDigits, request.Cvv);

               Log.Information("token created...");

                return token;
            }
            catch (Exception ex)
            {
                Log.Error("Error to create token:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public long GenerateToken(List<int> listFourDigits, int cvv)
        {
            for (int i = 0; i < cvv; i++)
            {
                int lengthDigits = listFourDigits.Count;
                listFourDigits.Insert(0, listFourDigits[listFourDigits.Count - 1]);

                listFourDigits = listFourDigits.Take(lengthDigits).ToList();
            }

            return Encrypting(string.Join("", listFourDigits));
        }

        public string ExtractDigits(long numberCard)
        {
            string convertNumber = numberCard.ToString();
            string fourDigits = convertNumber.Substring(Math.Max(0, convertNumber.Length - 4));

            return fourDigits;
        }

        public List<int> SeparatorDigits(string digits)
        {
            return digits.Select(x =>
                Convert.ToInt32(x.ToString()))
                .ToList();
        }

        public long Encrypting(string digits)
        {
            //string digistEncrypted = Encrypt.Cryptography.Encrypt(digits);

            byte[] convertByte = Encoding.ASCII.GetBytes(digits);

            //long ase = BitConverter.ToInt64(convertByte);

            //Array.Resize(ref convertByte, 1);

            string tokenString = string.Join("",convertByte);

            long token = Convert.ToInt64(tokenString);

            return token;
        }
    }
}
