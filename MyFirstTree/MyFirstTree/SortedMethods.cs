﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstTree
{
    public class SortedMethods
    {
        //三路划分快速排序算法//对敏感词进行按照拼音排序，其中排序的大小比较按拼音首字母
        //参考博客：写的极好http://www.cppblog.com/qinqing1984/archive/2015/09/12/175379.html
        public static void QuickSort(ref List<string> wordList , int left, int right)
        {
            if(!(left < right - 1))
            {
                return;
            }
            int i = left;
            int j = right-1;
            int p = i;//p表示左边与划分元素相等的地址
            int q = j;//q表示右边与划分元素相等的地址
            int k; //用作中间元素,提前声明而已
            char leftchar = ProfessionGf.GetFirstPinYin(wordList[left].FirstOrDefault().ToString()).FirstOrDefault();
            char rightchar = ProfessionGf.GetFirstPinYin(wordList[right - 1].FirstOrDefault().ToString()).FirstOrDefault();
            char mediumchar = ProfessionGf.GetFirstPinYin(wordList[(left + right) / 2].FirstOrDefault().ToString()).FirstOrDefault();
            char vItem = (char) ((leftchar + rightchar + mediumchar) / 3);//划分元素
            
            while (true)
            {
                while (ProfessionGf.GetFirstPinYin(wordList[i].FirstOrDefault().ToString()).FirstOrDefault() < vItem) i++;
                while (ProfessionGf.GetFirstPinYin(wordList[j].FirstOrDefault().ToString()).FirstOrDefault() > vItem) j--;
                if (!(i < j)) break;
                ExchangeTwoItems(ref wordList,i,j);

                if (!(ProfessionGf.GetFirstPinYin(wordList[i].FirstOrDefault().ToString()).FirstOrDefault() < vItem) &&
                    !(ProfessionGf.GetFirstPinYin(wordList[i].FirstOrDefault().ToString()).FirstOrDefault()> vItem))
                {
                    ExchangeTwoItems(ref wordList,i,p);
                    p++;
                }
                if (!(ProfessionGf.GetFirstPinYin(wordList[j].FirstOrDefault().ToString()).FirstOrDefault() < vItem) &&
                    !(ProfessionGf.GetFirstPinYin(wordList[j].FirstOrDefault().ToString()).FirstOrDefault() > vItem))
                {
                    ExchangeTwoItems(ref wordList,j,q);
                    q--;
                }
                ++i;
                --j;
            }
            j = i - 1;
            for (k = left; k < p; --j, ++k)
            {
                ExchangeTwoItems(ref wordList,k,j);
            }
            for (k = right - 1; k > q; ++i, --k)
            {
                ExchangeTwoItems(ref wordList,k,i);
            }
            QuickSort(ref wordList, left, j+1);
            QuickSort(ref wordList,i,right);
        }
        public static void ExchangeTwoItems(ref List<string>wordListToChange, int indexOfWordA,int indexOfWordB)
        {
            string temp=wordListToChange[indexOfWordA];
            wordListToChange[indexOfWordA] = wordListToChange[indexOfWordB];
            wordListToChange[indexOfWordB] = temp;
        }
    }
}