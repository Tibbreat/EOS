// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.MmTime
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Explicit)]
  internal struct MmTime
  {
    public const int TIME_MS = 1;
    public const int TIME_SAMPLES = 2;
    public const int TIME_BYTES = 4;
    [FieldOffset(0)]
    public uint wType;
    [FieldOffset(4)]
    public uint ms;
    [FieldOffset(4)]
    public uint sample;
    [FieldOffset(4)]
    public uint cb;
    [FieldOffset(4)]
    public uint ticks;
    [FieldOffset(4)]
    public byte smpteHour;
    [FieldOffset(5)]
    public byte smpteMin;
    [FieldOffset(6)]
    public byte smpteSec;
    [FieldOffset(7)]
    public byte smpteFrame;
    [FieldOffset(8)]
    public byte smpteFps;
    [FieldOffset(9)]
    public byte smpteDummy;
    [FieldOffset(10)]
    public byte smptePad0;
    [FieldOffset(11)]
    public byte smptePad1;
    [FieldOffset(4)]
    public uint midiSongPtrPos;
  }
}
