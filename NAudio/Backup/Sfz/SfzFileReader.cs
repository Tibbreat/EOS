// Decompiled with JetBrains decompiler
// Type: NAudio.Sfz.SfzFileReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.Sfz
{
  internal class SfzFileReader
  {
    public SfzFileReader(string fileName)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      using (StreamReader streamReader = new StreamReader(fileName))
      {
        List<Region> regionList = new List<Region>();
        Region region = (Region) null;
        StringBuilder stringBuilder2 = new StringBuilder();
        StringBuilder stringBuilder3 = new StringBuilder();
        int num = 0;
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
          ++num;
          int startIndex = str.IndexOf('/');
          if (startIndex != -1)
            str = str.Substring(startIndex);
          for (int index = 0; index < str.Length; ++index)
          {
            char c = str[index];
            if (char.IsWhiteSpace(c))
            {
              if (stringBuilder2.Length != 0)
              {
                if (stringBuilder3.Length == 0)
                  throw new FormatException(string.Format("Invalid Whitespace Line {0}, Char {1}", (object) num, (object) index));
                stringBuilder3.Append(c);
              }
            }
            else if (c != '=' && c == '<')
            {
              if (str.Substring(index).StartsWith("<region>"))
              {
                if (region != null)
                  regionList.Add(region);
                region = new Region();
                stringBuilder2.Length = 0;
                stringBuilder3.Length = 0;
                index += 7;
              }
              else
              {
                if (!str.Substring(index).StartsWith("<group>"))
                  throw new FormatException(string.Format("Unrecognised section Line {0}, Char {1}", (object) num, (object) index));
                if (region != null)
                  regionList.Add(region);
                stringBuilder2.Length = 0;
                stringBuilder3.Length = 0;
                region = (Region) null;
                Group group = new Group();
                index += 6;
              }
            }
          }
        }
      }
    }
  }
}
