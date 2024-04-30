// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioClockClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioClockClient : IDisposable
  {
    private IAudioClock audioClockClientInterface;

    internal AudioClockClient(IAudioClock audioClockClientInterface)
    {
      this.audioClockClientInterface = audioClockClientInterface;
    }

    public int Characteristics
    {
      get
      {
        uint characteristics;
        Marshal.ThrowExceptionForHR(this.audioClockClientInterface.GetCharacteristics(out characteristics));
        return (int) characteristics;
      }
    }

    public ulong Frequency
    {
      get
      {
        ulong frequency;
        Marshal.ThrowExceptionForHR(this.audioClockClientInterface.GetFrequency(out frequency));
        return frequency;
      }
    }

    public bool GetPosition(out ulong position, out ulong qpcPosition)
    {
      int position1 = this.audioClockClientInterface.GetPosition(out position, out qpcPosition);
      if (position1 == -1)
        return false;
      Marshal.ThrowExceptionForHR(position1);
      return true;
    }

    public ulong AdjustedPosition
    {
      get
      {
        ulong num1 = 10000000UL / this.Frequency;
        int num2 = 0;
        ulong position;
        ulong qpcPosition;
        do
          ;
        while (!this.GetPosition(out position, out qpcPosition) && ++num2 != 5);
        if (Stopwatch.IsHighResolution)
        {
          ulong num3 = ((ulong) ((Decimal) Stopwatch.GetTimestamp() * 10000000M / (Decimal) Stopwatch.Frequency) - qpcPosition) / 100UL / num1;
          position += num3;
        }
        return position;
      }
    }

    public bool CanAdjustPosition => Stopwatch.IsHighResolution;

    public void Dispose()
    {
      if (this.audioClockClientInterface == null)
        return;
      Marshal.ReleaseComObject((object) this.audioClockClientInterface);
      this.audioClockClientInterface = (IAudioClock) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
