// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.WindowsMediaMp3Decoder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  public class WindowsMediaMp3Decoder : IDisposable
  {
    private MediaObject mediaObject;
    private IPropertyStore propertyStoreInterface;
    private WindowsMediaMp3DecoderComObject mediaComObject;

    public WindowsMediaMp3Decoder()
    {
      this.mediaComObject = new WindowsMediaMp3DecoderComObject();
      this.mediaObject = new MediaObject((IMediaObject) this.mediaComObject);
      this.propertyStoreInterface = (IPropertyStore) this.mediaComObject;
    }

    public MediaObject MediaObject => this.mediaObject;

    public void Dispose()
    {
      if (this.propertyStoreInterface != null)
      {
        Marshal.ReleaseComObject((object) this.propertyStoreInterface);
        this.propertyStoreInterface = (IPropertyStore) null;
      }
      if (this.mediaObject != null)
      {
        this.mediaObject.Dispose();
        this.mediaObject = (MediaObject) null;
      }
      if (this.mediaComObject == null)
        return;
      Marshal.ReleaseComObject((object) this.mediaComObject);
      this.mediaComObject = (WindowsMediaMp3DecoderComObject) null;
    }
  }
}
