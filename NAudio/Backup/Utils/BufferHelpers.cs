// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.BufferHelpers
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Utils
{
  public static class BufferHelpers
  {
    public static byte[] Ensure(byte[] buffer, int bytesRequired)
    {
      if (buffer == null || buffer.Length < bytesRequired)
        buffer = new byte[bytesRequired];
      return buffer;
    }

    public static float[] Ensure(float[] buffer, int samplesRequired)
    {
      if (buffer == null || buffer.Length < samplesRequired)
        buffer = new float[samplesRequired];
      return buffer;
    }
  }
}
