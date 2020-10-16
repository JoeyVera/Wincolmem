using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Wincolmem
{
    public class HighScore : IComparable<HighScore>
    {
        public int points;
        public string name;

        public int CompareTo(HighScore other)
        {
            if (other == null)
                return 1;

            else
                return this.points.CompareTo(other.points);
        }
    }

    public class HighScores : List<HighScore>
    {
        public HighScores()
        {
            FileInfo fInfo = new FileInfo("highScores");

            if (!fInfo.Exists) //Add default high scores to the list.
            {
                for (int i = 0; i < 5; i++)
                    this.Add(new HighScore { points = i + 1, name = "JV" });
                this.Sort((a, b) => b.CompareTo(a));
            }
            else
                LoadHighScores();
        }

        public void AddHighScore(HighScore item)
        {
            base.Add(item);
            this.Sort((a, b) => b.CompareTo(a));
            this.RemoveAt(5);           
        }

        public void SaveHighScores()
        {
            using (FileStream fs = File.Create("highScores"))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (HighScore hs in this)                    
                        writer.WriteLine(Encrypt.EncryptString(hs.name + "|#|" + hs.points.ToString(),"justDoIt"));                    
                }                
            }

        }

        private void EmptyList()
        {
            this.RemoveAll(item => true);
        }

        public  void LoadHighScores()
        {

            using (FileStream fs = File.OpenRead("highScores"))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    string line;
                    string Decryptedline;
                    EmptyList();
                    string[] separator = { "|#|" };
                    String[] strlist;

                    while ((line = reader.ReadLine()) != null)
                    {
                        Decryptedline = Encrypt.DecryptString(line, "justDoIt");
                        strlist = Decryptedline.Split(separator,2, StringSplitOptions.RemoveEmptyEntries);
                        this.Add(new HighScore { points = Int16.Parse(strlist[1]), name = strlist[0] });
                    }

                    this.Sort((a, b) => b.CompareTo(a));

                }

            }

        }       

        public int GetLowerScore()
        {
            return this[4].points;
        }
    }

    // The Free to Use Encryption and Decryption C# Code
    // from: https://tekeye.uk/visual_studio/encrypt-decrypt-c-sharp-string

    public static class Encrypt
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
