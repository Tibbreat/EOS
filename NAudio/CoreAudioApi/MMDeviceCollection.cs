// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.MMDeviceCollection
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class MMDeviceCollection : IEnumerable<MMDevice>, IEnumerable
  {
    private IMMDeviceCollection _MMDeviceCollection;

    public int Count
    {
      get
      {
        int numDevices;
        Marshal.ThrowExceptionForHR(this._MMDeviceCollection.GetCount(out numDevices));
        return numDevices;
      }
    }

    public MMDevice this[int index]
    {
      get
      {
        IMMDevice device;
        this._MMDeviceCollection.Item(index, out device);
        return new MMDevice(device);
      }
    }

    internal MMDeviceCollection(IMMDeviceCollection parent) => this._MMDeviceCollection = parent;

    public IEnumerator<MMDevice> GetEnumerator()
    {
      for (int index = 0; index < this.Count; ++index)
        yield return this[index];
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
