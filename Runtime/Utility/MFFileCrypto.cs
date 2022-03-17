using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Moonflow.Utility
{
    /// <summary>
    /// 文件加密功能
    /// </summary>
    /// <typeparam name="T">加密的类</typeparam>
    public class MFFileCrypto<T>
    {
        private static string key = "12348578902223367877723456789012";

        /// <summary>
        /// 从工程相对路径解码一个文件
        /// </summary>
        /// <param name="relationPath">文件的相对路径</param>
        /// <returns>返回解码的类实例</returns>
        public static T Decrypt(string relationPath)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
        
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            string path = Application.dataPath + relationPath;
            string EncryptedData = File.ReadAllText(path);
            byte[] toEncryptArray = Convert.FromBase64String(EncryptedData);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string DecryptedJsonData = UTF8Encoding.UTF8.GetString(resultArray);
            return JsonUtility.FromJson<T>(DecryptedJsonData);
        }

        /// <summary>
        /// 向工程相对路径加密写入一个文件
        /// </summary>
        /// <param name="waitForEncrypt">被执行加密的实例</param>
        /// <param name="relationPath">生成文件的相对路径</param>
        /// <returns></returns>
        
        public static void EncryptFile(T waitForEncrypt, string relationPath)
        {
            string json_Text = JsonUtility.ToJson(waitForEncrypt);
        
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
        
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(json_Text);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string EncryptedJsonData = Convert.ToBase64String(resultArray, 0, resultArray.Length);

            string path = Application.dataPath;
            File.WriteAllText(path+"/"+ relationPath, EncryptedJsonData);
        }

        /// <summary>
        /// 从Resource中简单读取一个基于Json存储的文件
        /// </summary>
        /// <param name="relationPath">读取路径，是Resources下的相对路径</param>
        /// <returns></returns>
        public static T JsonResLoad(string relationPath)
        {
            string path = Application.dataPath + "/Resources/" + relationPath;
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(data);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 以Json格式简单存储自定格式到Resources文件夹内
        /// </summary>
        /// <param name="waitForSave">待保存的类</param>
        /// <param name="relationPath">保存路径，是Resources下的相对路径</param>
        /// <returns></returns>+
        public static void JsonResSave(T waitForSave, string relationPath)
        {
            string json_Text = JsonUtility.ToJson(waitForSave);
            string path = Application.dataPath + "/Resources/";
            File.WriteAllText(path + relationPath, json_Text);
        }
    }
}
