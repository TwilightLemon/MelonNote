using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace MelonNote
{
    public class Settings
    {
        public static UserSettings USettings = new Settings.UserSettings();
        public static String path = Directory.GetLogicalDrives()[1] + "\\MelonNote\\" + "MelonNoteData.st";
        public static void SaveSettings()
        {
            string paths = Directory.GetLogicalDrives()[1] + "\\MelonNote\\";
            if (!Directory.Exists(paths))
                Directory.CreateDirectory(paths);
            File.WriteAllText(path, TextHelper.JSON.ToJSON(USettings));
        }
        public static void LoadUSettings()
        {
            String data = File.ReadAllText(path);
            var o = JObject.Parse(data)["data"];
            foreach (var dt in o) {
                USettings.data.Add(new Data {
                    conText = dt["conText"].ToString(),
                    code = dt["code"].ToString(),
                    Height = Double.Parse(dt["Height"].ToString()),
                    Width = Double.Parse(dt["Width"].ToString()),
                    left= Double.Parse(dt["left"].ToString()),
                    top= Double.Parse(dt["top"].ToString()),
                    theme=int.Parse(dt["theme"].ToString())
                });
            }
        }
        public class UserSettings
        {
            public List<Data> data = new List<Data>();
        }
        public class Data {
            public String conText { get; set; } = "";
            public String code { get; set; } = ".cs";
            public Double Height { get; set; } = 0;
            public Double Width { get; set; } = 0;
            public Double left { get; set; } = 0;
            public Double top { get; set; } = 0;
            public int theme { get; set; } = 0;
        }
    }
    public class TextHelper {
        public static string TextEncrypt(string encryptStr, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string TextDecrypt(string decryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(decryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public class JSON
        {
            public static string ToJSON(object obj)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer()
                { MaxJsonLength = Int32.MaxValue };
                return serializer.Serialize(obj);
            }
        }
        public class MD5
        {
            public static byte[] EncryptToMD5(string str)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] str1 = System.Text.Encoding.UTF8.GetBytes(str);
                byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
                md5.Clear();
                (md5 as IDisposable).Dispose();
                return str2;
            }
            public static string EncryptToMD5string(string str)
            {
                byte[] bytHash = EncryptToMD5(str);
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                return sTemp.ToLower();
            }
        }
    }
}
