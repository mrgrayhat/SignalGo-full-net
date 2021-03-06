﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SignalGo.Publisher.Engines.Models
{
    public class PasswordEncoder
    {
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        /// <summary>
        /// compute input hash using SHA256 Algoritm
        /// </summary>
        /// <param name="input">string to hash</param>
        /// <returns>Hashed string</returns>
        public static string ComputeHash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = SHA256.Create().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        public static string ComputeHash(string input, HashAlgorithm algorithm, Byte[] salt)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Combine salt and input bytes
            Byte[] saltedInput = new Byte[salt.Length + inputBytes.Length];
            salt.CopyTo(saltedInput, 0);
            inputBytes.CopyTo(saltedInput, salt.Length);

            Byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

            return BitConverter.ToString(hashedBytes);
        }
        string mEncryptedPassword;
        byte[] mInitializationVector = { 0x01, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xf7, 0xEF };

        public PasswordEncoder()
        {
        }

        public PasswordEncoder(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, ByteArray);
        }

        public string EncryptWithByteArray(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, ByteArray);
            return mEncryptedPassword;
        }

        private string EncryptWithByteArray(string inPassword, string inByteArray)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = Encoding.UTF8.GetBytes(inByteArray.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = Encoding.UTF8.GetBytes(inPassword);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputArray, 0, inputArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DecryptWithByteArray()
        {
            return DecryptWithByteArray(mEncryptedPassword, ByteArray);
        }
        public string DecryptWithByteArray(string password)
        {
            return DecryptWithByteArray(password, ByteArray);
        }
        private string DecryptWithByteArray(string strText, string strEncrypt)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptedPassword
        {
            get { return mEncryptedPassword; }
            set { mEncryptedPassword = value; }
        }

        public string ByteArray { get; set; } = "%$#>#%232s+as#l)URa0$!@";
    }
}
