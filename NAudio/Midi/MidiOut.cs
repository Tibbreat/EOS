// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiOut
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Midi
{
  public class MidiOut : IDisposable
  {
    private IntPtr hMidiOut = IntPtr.Zero;
    private bool disposed;
    private MidiInterop.MidiOutCallback callback;

    public static int NumberOfDevices => MidiInterop.midiOutGetNumDevs();

    public static MidiOutCapabilities DeviceInfo(int midiOutDeviceNumber)
    {
      MidiOutCapabilities caps = new MidiOutCapabilities();
      int uSize = Marshal.SizeOf((object) caps);
      MmException.Try(MidiInterop.midiOutGetDevCaps((IntPtr) midiOutDeviceNumber, out caps, uSize), "midiOutGetDevCaps");
      return caps;
    }

    public MidiOut(int deviceNo)
    {
      this.callback = new MidiInterop.MidiOutCallback(this.Callback);
      MmException.Try(MidiInterop.midiOutOpen(out this.hMidiOut, (IntPtr) deviceNo, this.callback, IntPtr.Zero, 196608), "midiOutOpen");
    }

    public void Close() => this.Dispose();

    public void Dispose()
    {
      GC.KeepAlive((object) this.callback);
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public int Volume
    {
      get
      {
        int lpdwVolume = 0;
        MmException.Try(MidiInterop.midiOutGetVolume(this.hMidiOut, ref lpdwVolume), "midiOutGetVolume");
        return lpdwVolume;
      }
      set
      {
        MmException.Try(MidiInterop.midiOutSetVolume(this.hMidiOut, value), "midiOutSetVolume");
      }
    }

    public void Reset() => MmException.Try(MidiInterop.midiOutReset(this.hMidiOut), "midiOutReset");

    public void SendDriverMessage(int message, int param1, int param2)
    {
      MmException.Try(MidiInterop.midiOutMessage(this.hMidiOut, message, (IntPtr) param1, (IntPtr) param2), "midiOutMessage");
    }

    public void Send(int message)
    {
      MmException.Try(MidiInterop.midiOutShortMsg(this.hMidiOut, message), "midiOutShortMsg");
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        int num = (int) MidiInterop.midiOutClose(this.hMidiOut);
      }
      this.disposed = true;
    }

    private void Callback(
      IntPtr midiInHandle,
      MidiInterop.MidiOutMessage message,
      IntPtr userData,
      IntPtr messageParameter1,
      IntPtr messageParameter2)
    {
    }

    public void SendBuffer(byte[] byteBuffer)
    {
      MidiInterop.MIDIHDR lpMidiOutHdr = new MidiInterop.MIDIHDR();
      lpMidiOutHdr.lpData = Marshal.AllocHGlobal(byteBuffer.Length);
      Marshal.Copy(byteBuffer, 0, lpMidiOutHdr.lpData, byteBuffer.Length);
      lpMidiOutHdr.dwBufferLength = byteBuffer.Length;
      lpMidiOutHdr.dwBytesRecorded = byteBuffer.Length;
      int uSize = Marshal.SizeOf((object) lpMidiOutHdr);
      int num1 = (int) MidiInterop.midiOutPrepareHeader(this.hMidiOut, ref lpMidiOutHdr, uSize);
      if (MidiInterop.midiOutLongMsg(this.hMidiOut, ref lpMidiOutHdr, uSize) != MmResult.NoError)
      {
        int num2 = (int) MidiInterop.midiOutUnprepareHeader(this.hMidiOut, ref lpMidiOutHdr, uSize);
      }
      Marshal.FreeHGlobal(lpMidiOutHdr.lpData);
    }

    ~MidiOut() => this.Dispose(false);
  }
}
