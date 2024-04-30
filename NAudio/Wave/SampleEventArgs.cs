// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleEventArgs
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class SampleEventArgs : EventArgs
  {
    public float Left { get; set; }

    public float Right { get; set; }

    public SampleEventArgs(float left, float right)
    {
      this.Left = left;
      this.Right = right;
    }
  }
}
