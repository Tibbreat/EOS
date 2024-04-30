// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.IEEE
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Utils
{
  public static class IEEE
  {
    private static double UnsignedToFloat(ulong u)
    {
      return (double) ((long) u - (long) int.MaxValue - 1L) + 2147483648.0;
    }

    private static double ldexp(double x, int exp) => x * Math.Pow(2.0, (double) exp);

    private static double frexp(double x, out int exp)
    {
      exp = (int) Math.Floor(Math.Log(x) / Math.Log(2.0)) + 1;
      return 1.0 - (Math.Pow(2.0, (double) exp) - x) / Math.Pow(2.0, (double) exp);
    }

    private static ulong FloatToUnsigned(double f)
    {
      return (ulong) (long) (f - 2147483648.0) + (ulong) int.MaxValue + 1UL;
    }

    public static byte[] ConvertToIeeeExtended(double num)
    {
      int num1;
      if (num < 0.0)
      {
        num1 = 32768;
        num *= -1.0;
      }
      else
        num1 = 0;
      int num2;
      ulong num3;
      ulong num4;
      if (num == 0.0)
      {
        num2 = 0;
        num3 = 0UL;
        num4 = 0UL;
      }
      else
      {
        int exp1;
        double x = IEEE.frexp(num, out exp1);
        if (exp1 > 16384 || x >= 1.0)
        {
          num2 = num1 | (int) short.MaxValue;
          num3 = 0UL;
          num4 = 0UL;
        }
        else
        {
          int exp2 = exp1 + 16382;
          if (exp2 < 0)
          {
            x = IEEE.ldexp(x, exp2);
            exp2 = 0;
          }
          num2 = exp2 | num1;
          double d = IEEE.ldexp(x, 32);
          double f = Math.Floor(d);
          num3 = IEEE.FloatToUnsigned(f);
          num4 = IEEE.FloatToUnsigned(Math.Floor(IEEE.ldexp(d - f, 32)));
        }
      }
      return new byte[10]
      {
        (byte) (num2 >> 8),
        (byte) num2,
        (byte) (num3 >> 24),
        (byte) (num3 >> 16),
        (byte) (num3 >> 8),
        (byte) num3,
        (byte) (num4 >> 24),
        (byte) (num4 >> 16),
        (byte) (num4 >> 8),
        (byte) num4
      };
    }

    public static double ConvertFromIeeeExtended(byte[] bytes)
    {
      if (bytes.Length != 10)
        throw new Exception("Incorrect length for IEEE extended.");
      int num1 = ((int) bytes[0] & (int) sbyte.MaxValue) << 8 | (int) bytes[1];
      uint u1 = (uint) ((int) bytes[2] << 24 | (int) bytes[3] << 16 | (int) bytes[4] << 8) | (uint) bytes[5];
      uint u2 = (uint) ((int) bytes[6] << 24 | (int) bytes[7] << 16 | (int) bytes[8] << 8) | (uint) bytes[9];
      double num2;
      if (num1 == 0 && u1 == 0U && u2 == 0U)
        num2 = 0.0;
      else if (num1 == (int) short.MaxValue)
      {
        num2 = double.NaN;
      }
      else
      {
        int num3 = num1 - 16383;
        int num4;
        int num5;
        num2 = IEEE.ldexp(IEEE.UnsignedToFloat((ulong) u1), num4 = num3 - 31) + IEEE.ldexp(IEEE.UnsignedToFloat((ulong) u2), num5 = num4 - 32);
      }
      return ((int) bytes[0] & 128) == 128 ? -num2 : num2;
    }
  }
}
