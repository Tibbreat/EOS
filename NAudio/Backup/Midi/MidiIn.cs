// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiIn
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Midi
{
  public class MidiIn : IDisposable
  {
    private IntPtr hMidiIn = IntPtr.Zero;
    private bool disposed;
    private MidiInterop.MidiInCallback callback;

    public event EventHandler<MidiInMessageEventArgs> MessageReceived;

    public event EventHandler<MidiInMessageEventArgs> ErrorReceived;

    public static int NumberOfDevices => MidiInterop.midiInGetNumDevs();

    public MidiIn(int deviceNo)
    {
      this.callback = new MidiInterop.MidiInCallback(this.Callback);
      MmException.Try(MidiInterop.midiInOpen(out this.hMidiIn, (IntPtr) deviceNo, this.callback, IntPtr.Zero, 196608), "midiInOpen");
    }

    public void Close() => this.Dispose();

    public void Dispose()
    {
      GC.KeepAlive((object) this.callback);
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void Start() => MmException.Try(MidiInterop.midiInStart(this.hMidiIn), "midiInStart");

    public void Stop() => MmException.Try(MidiInterop.midiInStop(this.hMidiIn), "midiInStop");

    public void Reset() => MmException.Try(MidiInterop.midiInReset(this.hMidiIn), "midiInReset");

    private void Callback(
      IntPtr midiInHandle,
      MidiInterop.MidiInMessage message,
      IntPtr userData,
      IntPtr messageParameter1,
      IntPtr messageParameter2)
    {
      switch (message)
      {
        case MidiInterop.MidiInMessage.Data:
          if (this.MessageReceived == null)
            break;
          this.MessageReceived((object) this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
          break;
        case MidiInterop.MidiInMessage.Error:
          if (this.ErrorReceived == null)
            break;
          this.ErrorReceived((object) this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
          break;
      }
    }

    public static MidiInCapabilities DeviceInfo(int midiInDeviceNumber)
    {
      MidiInCapabilities capabilities = new MidiInCapabilities();
      int size = Marshal.SizeOf((object) capabilities);
      MmException.Try(MidiInterop.midiInGetDevCaps((IntPtr) midiInDeviceNumber, out capabilities, size), "midiInGetDevCaps");
      return capabilities;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        int num = (int) MidiInterop.midiInClose(this.hMidiIn);
      }
      this.disposed = true;
    }

    ~MidiIn() => this.Dispose(false);
  }
}
