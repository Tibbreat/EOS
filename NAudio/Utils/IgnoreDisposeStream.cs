// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.IgnoreDisposeStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.Utils
{
  public class IgnoreDisposeStream : Stream
  {
    public Stream SourceStream { get; private set; }

    public bool IgnoreDispose { get; set; }

    public IgnoreDisposeStream(Stream sourceStream)
    {
      this.SourceStream = sourceStream;
      this.IgnoreDispose = true;
    }

    public override bool CanRead => this.SourceStream.CanRead;

    public override bool CanSeek => this.SourceStream.CanSeek;

    public override bool CanWrite => this.SourceStream.CanWrite;

    public override void Flush() => this.SourceStream.Flush();

    public override long Length => this.SourceStream.Length;

    public override long Position
    {
      get => this.SourceStream.Position;
      set => this.SourceStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.SourceStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return this.SourceStream.Seek(offset, origin);
    }

    public override void SetLength(long value) => this.SourceStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.SourceStream.Write(buffer, offset, count);
    }

    protected override void Dispose(bool disposing)
    {
      if (this.IgnoreDispose)
        return;
      this.SourceStream.Dispose();
      this.SourceStream = (Stream) null;
    }
  }
}
