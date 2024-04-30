// Decompiled with JetBrains decompiler
// Type: QuestionLib.GZipHelper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System.IO;
using System.IO.Compression;

#nullable disable
namespace QuestionLib
{
  public class GZipHelper
  {
    public static byte[] Compress(byte[] bytData)
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream();
        Stream stream = (Stream) new GZipStream((Stream) memoryStream, CompressionMode.Compress);
        stream.Write(bytData, 0, bytData.Length);
        stream.Close();
        return memoryStream.ToArray();
      }
      catch
      {
        return (byte[]) null;
      }
    }

    public static byte[] DeCompress(byte[] bytInput, int originSize)
    {
      Stream stream = (Stream) new GZipStream((Stream) new MemoryStream(bytInput), CompressionMode.Decompress);
      byte[] buffer = new byte[originSize];
      stream.Read(buffer, 0, originSize);
      return buffer;
    }
  }
}
