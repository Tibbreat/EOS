// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.OggWaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  internal class OggWaveFormat : WaveFormat
  {
    public uint dwVorbisACMVersion;
    public uint dwLibVorbisVersion;
  }
}
