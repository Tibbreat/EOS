// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.WaveViewer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class WaveViewer : UserControl
  {
    private System.ComponentModel.Container components;
    private WaveStream waveStream;
    private int samplesPerPixel = 128;
    private long startPosition;
    private int bytesPerSample;

    public WaveViewer()
    {
      this.InitializeComponent();
      this.DoubleBuffered = true;
    }

    public WaveStream WaveStream
    {
      get => this.waveStream;
      set
      {
        this.waveStream = value;
        if (this.waveStream != null)
          this.bytesPerSample = this.waveStream.WaveFormat.BitsPerSample / 8 * this.waveStream.WaveFormat.Channels;
        this.Invalidate();
      }
    }

    public int SamplesPerPixel
    {
      get => this.samplesPerPixel;
      set
      {
        this.samplesPerPixel = value;
        this.Invalidate();
      }
    }

    public long StartPosition
    {
      get => this.startPosition;
      set => this.startPosition = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.waveStream != null)
      {
        this.waveStream.Position = 0L;
        byte[] buffer = new byte[this.samplesPerPixel * this.bytesPerSample];
        this.waveStream.Position = this.startPosition + (long) (e.ClipRectangle.Left * this.bytesPerSample * this.samplesPerPixel);
        for (float x = (float) e.ClipRectangle.X; (double) x < (double) e.ClipRectangle.Right; ++x)
        {
          short num1 = 0;
          short num2 = 0;
          int num3 = this.waveStream.Read(buffer, 0, this.samplesPerPixel * this.bytesPerSample);
          if (num3 != 0)
          {
            for (int startIndex = 0; startIndex < num3; startIndex += 2)
            {
              short int16 = BitConverter.ToInt16(buffer, startIndex);
              if ((int) int16 < (int) num1)
                num1 = int16;
              if ((int) int16 > (int) num2)
                num2 = int16;
            }
            float num4 = (float) (((double) num1 - (double) short.MinValue) / (double) ushort.MaxValue);
            float num5 = (float) (((double) num2 - (double) short.MinValue) / (double) ushort.MaxValue);
            e.Graphics.DrawLine(Pens.Black, x, (float) this.Height * num4, x, (float) this.Height * num5);
          }
          else
            break;
        }
      }
      base.OnPaint(e);
    }

    private void InitializeComponent() => this.components = new System.ComponentModel.Container();
  }
}
