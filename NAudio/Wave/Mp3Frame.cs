﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Mp3Frame
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Wave
{
  public class Mp3Frame
  {
    private const int MaxFrameLength = 16384;
    private static readonly int[,,] bitRates = new int[2, 3, 15]
    {
      {
        {
          0,
          32,
          64,
          96,
          128,
          160,
          192,
          224,
          256,
          288,
          320,
          352,
          384,
          416,
          448
        },
        {
          0,
          32,
          48,
          56,
          64,
          80,
          96,
          112,
          128,
          160,
          192,
          224,
          256,
          320,
          384
        },
        {
          0,
          32,
          40,
          48,
          56,
          64,
          80,
          96,
          112,
          128,
          160,
          192,
          224,
          256,
          320
        }
      },
      {
        {
          0,
          32,
          48,
          56,
          64,
          80,
          96,
          112,
          128,
          144,
          160,
          176,
          192,
          224,
          256
        },
        {
          0,
          8,
          16,
          24,
          32,
          40,
          48,
          56,
          64,
          80,
          96,
          112,
          128,
          144,
          160
        },
        {
          0,
          8,
          16,
          24,
          32,
          40,
          48,
          56,
          64,
          80,
          96,
          112,
          128,
          144,
          160
        }
      }
    };
    private static readonly int[,] samplesPerFrame = new int[2, 3]
    {
      {
        384,
        1152,
        1152
      },
      {
        384,
        1152,
        576
      }
    };
    private static readonly int[] sampleRatesVersion1 = new int[3]
    {
      44100,
      48000,
      32000
    };
    private static readonly int[] sampleRatesVersion2 = new int[3]
    {
      22050,
      24000,
      16000
    };
    private static readonly int[] sampleRatesVersion25 = new int[3]
    {
      11025,
      12000,
      8000
    };

    public static Mp3Frame LoadFromStream(Stream input) => Mp3Frame.LoadFromStream(input, true);

    public static Mp3Frame LoadFromStream(Stream input, bool readData)
    {
      Mp3Frame frame = new Mp3Frame();
      frame.FileOffset = input.Position;
      byte[] numArray = new byte[4];
      if (input.Read(numArray, 0, numArray.Length) < numArray.Length)
        return (Mp3Frame) null;
      while (!Mp3Frame.IsValidHeader(numArray, frame))
      {
        numArray[0] = numArray[1];
        numArray[1] = numArray[2];
        numArray[2] = numArray[3];
        if (input.Read(numArray, 3, 1) < 1)
          return (Mp3Frame) null;
        ++frame.FileOffset;
      }
      int count = frame.FrameLength - 4;
      if (readData)
      {
        frame.RawData = new byte[frame.FrameLength];
        Array.Copy((Array) numArray, (Array) frame.RawData, 4);
        if (input.Read(frame.RawData, 4, count) < count)
          throw new EndOfStreamException("Unexpected end of stream before frame complete");
      }
      else
        input.Position += (long) count;
      return frame;
    }

    private Mp3Frame()
    {
    }

    private static bool IsValidHeader(byte[] headerBytes, Mp3Frame frame)
    {
      if (headerBytes[0] != byte.MaxValue || ((int) headerBytes[1] & 224) != 224)
        return false;
      frame.MpegVersion = (MpegVersion) (((int) headerBytes[1] & 24) >> 3);
      if (frame.MpegVersion == MpegVersion.Reserved)
        return false;
      frame.MpegLayer = (MpegLayer) (((int) headerBytes[1] & 6) >> 1);
      if (frame.MpegLayer == MpegLayer.Reserved)
        return false;
      int index1 = frame.MpegLayer == MpegLayer.Layer1 ? 0 : (frame.MpegLayer == MpegLayer.Layer2 ? 1 : 2);
      frame.CrcPresent = ((int) headerBytes[1] & 1) == 0;
      frame.BitRateIndex = ((int) headerBytes[2] & 240) >> 4;
      if (frame.BitRateIndex == 15)
        return false;
      int index2 = frame.MpegVersion == MpegVersion.Version1 ? 0 : 1;
      frame.BitRate = Mp3Frame.bitRates[index2, index1, frame.BitRateIndex] * 1000;
      if (frame.BitRate == 0)
        return false;
      int index3 = ((int) headerBytes[2] & 12) >> 2;
      if (index3 == 3)
        return false;
      frame.SampleRate = frame.MpegVersion != MpegVersion.Version1 ? (frame.MpegVersion != MpegVersion.Version2 ? Mp3Frame.sampleRatesVersion25[index3] : Mp3Frame.sampleRatesVersion2[index3]) : Mp3Frame.sampleRatesVersion1[index3];
      bool flag = ((int) headerBytes[2] & 2) == 2;
      int headerByte1 = (int) headerBytes[2];
      frame.ChannelMode = (ChannelMode) (((int) headerBytes[3] & 192) >> 6);
      frame.ChannelExtension = ((int) headerBytes[3] & 48) >> 4;
      if (frame.ChannelExtension != 0 && frame.ChannelMode != ChannelMode.JointStereo)
        return false;
      frame.Copyright = ((int) headerBytes[3] & 8) == 8;
      int headerByte2 = (int) headerBytes[3];
      int headerByte3 = (int) headerBytes[3];
      int num1 = flag ? 1 : 0;
      frame.SampleCount = Mp3Frame.samplesPerFrame[index2, index1];
      int num2 = frame.SampleCount / 8;
      frame.FrameLength = frame.MpegLayer != MpegLayer.Layer1 ? num2 * frame.BitRate / frame.SampleRate + num1 : (num2 * frame.BitRate / frame.SampleRate + num1) * 4;
      return frame.FrameLength <= 16384;
    }

    public int SampleRate { get; private set; }

    public int FrameLength { get; private set; }

    public int BitRate { get; private set; }

    public byte[] RawData { get; private set; }

    public MpegVersion MpegVersion { get; private set; }

    public MpegLayer MpegLayer { get; private set; }

    public ChannelMode ChannelMode { get; private set; }

    public int SampleCount { get; private set; }

    public int ChannelExtension { get; private set; }

    public int BitRateIndex { get; private set; }

    public bool Copyright { get; private set; }

    public bool CrcPresent { get; private set; }

    public long FileOffset { get; private set; }
  }
}
