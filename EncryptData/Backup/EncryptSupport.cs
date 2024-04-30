// Decompiled with JetBrains decompiler
// Type: EncryptData.EncryptSupport
// Assembly: EncryptData, Version=1.0.7820.39554, Culture=neutral, PublicKeyToken=null
// MVID: 971DA8CD-00E7-41D3-AE29-E64D98BB4E42
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EncryptData.dll

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace EncryptData
{
  public class EncryptSupport
  {
    public static byte[] ObjectToByteArray(object obj)
    {
      if (obj == null)
        return (byte[]) null;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      MemoryStream serializationStream = new MemoryStream();
      binaryFormatter.Serialize((Stream) serializationStream, obj);
      return serializationStream.ToArray();
    }

    public static object ByteArrayToObject(byte[] arrBytes)
    {
      MemoryStream serializationStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      serializationStream.Write(arrBytes, 0, arrBytes.Length);
      serializationStream.Seek(0L, SeekOrigin.Begin);
      return binaryFormatter.Deserialize((Stream) serializationStream);
    }

    public static bool EncryptQuestions_SaveToFile(string fname, byte[] data, string key)
    {
      try
      {
        FileStream fileStream = new FileStream(fname, FileMode.Create, FileAccess.Write);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(key);
        cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(key);
        CryptoStream cryptoStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.Close();
        fileStream.Close();
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static byte[] DecryptQuestions_FromFile(string fname, string key)
    {
      try
      {
        FileStream fileStream = new FileStream(fname, FileMode.Open, FileAccess.Read);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(key);
        cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(key);
        CryptoStream cryptoStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read);
        byte[] buffer = new byte[fileStream.Length];
        cryptoStream.Read(buffer, 0, (int) fileStream.Length);
        cryptoStream.Close();
        fileStream.Close();
        return buffer;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static string Encryption(byte[] data, string key)
    {
      DES des = (DES) new DESCryptoServiceProvider();
      des.Key = Encoding.ASCII.GetBytes(key);
      des.IV = des.Key;
      des.Padding = PaddingMode.PKCS7;
      MemoryStream memoryStream = new MemoryStream();
      ICryptoTransform encryptor = des.CreateEncryptor();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(data, 0, data.Length);
      cryptoStream.FlushFinalBlock();
      memoryStream.Position = 0L;
      string empty = string.Empty;
      string base64String = Convert.ToBase64String(memoryStream.ToArray());
      cryptoStream.Close();
      return base64String;
    }

    public static string Decryption(byte[] data, string key)
    {
      DES des = (DES) new DESCryptoServiceProvider();
      des.Key = Encoding.ASCII.GetBytes(key);
      des.IV = des.Key;
      des.Padding = PaddingMode.PKCS7;
      CryptoStream cryptoStream = new CryptoStream((Stream) new MemoryStream(data), des.CreateDecryptor(), CryptoStreamMode.Read);
      byte[] numArray = new byte[data.Length];
      cryptoStream.Read(numArray, 0, numArray.Length);
      cryptoStream.Close();
      return Encoding.Unicode.GetString(numArray);
    }

    public static string GetMD5(string msg)
    {
      return Encoding.Unicode.GetString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(msg)));
    }
  }
}
