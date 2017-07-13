using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace VB2B_HUNGTHINH_SMS_SERVICE
{
    public class cls3DES
    {
        private byte[] _EncryptionKey;
        private byte[] _IV;

        public byte[] EncryptionKey { get { return _EncryptionKey; } set { _EncryptionKey = value; } }
        public byte[] IV { get { return _IV; } set { _IV = value; } }

        public cls3DES(string encryptionKey, string IV)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentNullException("'encryptionKey' parameter cannot be null.", "encryptionKey");
            }
            if (string.IsNullOrEmpty(IV))
            {
                throw new ArgumentException("'IV' parameter cannot be null or empty.", "IV");
            }
            // Ensures length of 24 for encryption key 
            this.EncryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            // Ensures length of 8 for init. vector 
            this.IV = Encoding.UTF8.GetBytes(IV);
        }


        public string Encrypt(string textToEncrypt)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = EncryptionKey;
            tdes.IV = IV;

            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public string Decrypt(string textToDecrypt)
        {
            
            byte[] buffer = Convert.FromBase64String(textToDecrypt);

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.CBC;
            des.Key = EncryptionKey;
            des.IV = IV;

            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

    }
}
