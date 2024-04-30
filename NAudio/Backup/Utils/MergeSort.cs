// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.MergeSort
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace NAudio.Utils
{
  internal class MergeSort
  {
    private static void Sort<T>(IList<T> list, int lowIndex, int highIndex, IComparer<T> comparer)
    {
      if (lowIndex >= highIndex)
        return;
      int highIndex1 = (lowIndex + highIndex) / 2;
      MergeSort.Sort<T>(list, lowIndex, highIndex1, comparer);
      MergeSort.Sort<T>(list, highIndex1 + 1, highIndex, comparer);
      int num = highIndex1;
      int index1 = highIndex1 + 1;
      while (lowIndex <= num && index1 <= highIndex)
      {
        if (comparer.Compare(list[lowIndex], list[index1]) <= 0)
        {
          ++lowIndex;
        }
        else
        {
          T obj = list[index1];
          for (int index2 = index1 - 1; index2 >= lowIndex; --index2)
            list[index2 + 1] = list[index2];
          list[lowIndex] = obj;
          ++lowIndex;
          ++num;
          ++index1;
        }
      }
    }

    public static void Sort<T>(IList<T> list) where T : IComparable<T>
    {
      MergeSort.Sort<T>(list, 0, list.Count - 1, (IComparer<T>) Comparer<T>.Default);
    }

    public static void Sort<T>(IList<T> list, IComparer<T> comparer)
    {
      MergeSort.Sort<T>(list, 0, list.Count - 1, comparer);
    }
  }
}
