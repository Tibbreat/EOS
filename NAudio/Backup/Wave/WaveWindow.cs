// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveWindow
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Wave
{
  internal class WaveWindow : Form
  {
    private WaveInterop.WaveCallback waveCallback;

    public WaveWindow(WaveInterop.WaveCallback waveCallback) => this.waveCallback = waveCallback;

    protected override void WndProc(ref Message m)
    {
      WaveInterop.WaveMessage msg = (WaveInterop.WaveMessage) m.Msg;
      switch (msg)
      {
        case WaveInterop.WaveMessage.WaveOutOpen:
        case WaveInterop.WaveMessage.WaveOutClose:
        case WaveInterop.WaveMessage.WaveInOpen:
        case WaveInterop.WaveMessage.WaveInClose:
          this.waveCallback(m.WParam, msg, IntPtr.Zero, (WaveHeader) null, IntPtr.Zero);
          break;
        case WaveInterop.WaveMessage.WaveOutDone:
        case WaveInterop.WaveMessage.WaveInData:
          IntPtr wparam = m.WParam;
          WaveHeader waveHeader = new WaveHeader();
          Marshal.PtrToStructure(m.LParam, (object) waveHeader);
          this.waveCallback(wparam, msg, IntPtr.Zero, waveHeader, IntPtr.Zero);
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }
  }
}
