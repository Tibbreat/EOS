// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.IWaveBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave
{
  public interface IWaveBuffer
  {
    byte[] ByteBuffer { get; }

    float[] FloatBuffer { get; }

    short[] ShortBuffer { get; }

    int[] IntBuffer { get; }

    int MaxSize { get; }

    int ByteBufferCount { get; }

    int FloatBufferCount { get; }

    int ShortBufferCount { get; }

    int IntBufferCount { get; }
  }
}
