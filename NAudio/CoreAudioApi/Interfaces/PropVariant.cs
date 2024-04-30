// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.PropVariant
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [StructLayout(LayoutKind.Explicit)]
  public struct PropVariant
  {
    [FieldOffset(0)]
    private short vt;
    [FieldOffset(2)]
    private short wReserved1;
    [FieldOffset(4)]
    private short wReserved2;
    [FieldOffset(6)]
    private short wReserved3;
    [FieldOffset(8)]
    private sbyte cVal;
    [FieldOffset(8)]
    private byte bVal;
    [FieldOffset(8)]
    private short iVal;
    [FieldOffset(8)]
    private ushort uiVal;
    [FieldOffset(8)]
    private int lVal;
    [FieldOffset(8)]
    private uint ulVal;
    [FieldOffset(8)]
    private int intVal;
    [FieldOffset(8)]
    private uint uintVal;
    [FieldOffset(8)]
    private long hVal;
    [FieldOffset(8)]
    private long uhVal;
    [FieldOffset(8)]
    private float fltVal;
    [FieldOffset(8)]
    private double dblVal;
    [FieldOffset(8)]
    private bool boolVal;
    [FieldOffset(8)]
    private int scode;
    [FieldOffset(8)]
    private DateTime date;
    [FieldOffset(8)]
    private System.Runtime.InteropServices.ComTypes.FILETIME filetime;
    [FieldOffset(8)]
    private Blob blobVal;
    [FieldOffset(8)]
    private IntPtr pointerValue;

    public static PropVariant FromLong(long value)
    {
      return new PropVariant() { vt = 20, hVal = value };
    }

    private byte[] GetBlob()
    {
      byte[] destination = new byte[this.blobVal.Length];
      Marshal.Copy(this.blobVal.Data, destination, 0, destination.Length);
      return destination;
    }

    public T[] GetBlobAsArrayOf<T>()
    {
      int length1 = this.blobVal.Length;
      int num = Marshal.SizeOf((object) (T) Activator.CreateInstance(typeof (T)));
      if (length1 % num != 0)
        throw new InvalidDataException(string.Format("Blob size {0} not a multiple of struct size {1}", (object) length1, (object) num));
      int length2 = length1 / num;
      T[] blobAsArrayOf = new T[length2];
      for (int index = 0; index < length2; ++index)
      {
        blobAsArrayOf[index] = (T) Activator.CreateInstance(typeof (T));
        Marshal.PtrToStructure(new IntPtr((long) this.blobVal.Data + (long) (index * num)), (object) blobAsArrayOf[index]);
      }
      return blobAsArrayOf;
    }

    public VarEnum DataType => (VarEnum) this.vt;

    public object Value
    {
      get
      {
        VarEnum dataType = this.DataType;
        switch (dataType)
        {
          case VarEnum.VT_I2:
            return (object) this.iVal;
          case VarEnum.VT_I4:
            return (object) this.lVal;
          case VarEnum.VT_I1:
            return (object) this.bVal;
          case VarEnum.VT_UI4:
            return (object) this.ulVal;
          case VarEnum.VT_I8:
            return (object) this.hVal;
          case VarEnum.VT_UI8:
            return (object) this.uhVal;
          case VarEnum.VT_INT:
            return (object) this.iVal;
          case VarEnum.VT_LPWSTR:
            return (object) Marshal.PtrToStringUni(this.pointerValue);
          case VarEnum.VT_BLOB:
          case VarEnum.VT_UI1 | VarEnum.VT_VECTOR:
            return (object) this.GetBlob();
          case VarEnum.VT_CLSID:
            return (object) (Guid) Marshal.PtrToStructure(this.pointerValue, typeof (Guid));
          default:
            throw new NotImplementedException("PropVariant " + dataType.ToString());
        }
      }
    }

    public void Clear() => PropVariant.PropVariantClear(ref this);

    [DllImport("ole32.dll")]
    private static extern int PropVariantClear(ref PropVariant pvar);
  }
}
