using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MyFirstTree
{
    public class KMP
    {
        //尝试理解KMP算法https://www.cnblogs.com/tangzhengyue/p/4315393.html
        //获取next数组
        public static void GetNext(string keyWord,ref int[]next)
        {
            if (string.IsNullOrEmpty(keyWord))
                return;
           // int[] next = new int[keyWord.Length];
            next[0] = -1;//当k为-1的时候，next[j+1]应该等于0，把J退回到首位
            int k = -1;//假设next[j]=-1
            int j = 0;//keyWord中的指针
            while (j < keyWord.Length - 1)//根据已知的前j位，推测j+1位
            {
                if (k == -1 || keyWord[j] == next[k])
                {
                    if (keyWord[j + 1] == keyWord[k + 1])
                    {
                        j++;
                        k++;
                        next[j] = next[k];
                    }
                    else
                    {
                        next[j++] = k++;
                    }
                }
                else
                {
                    k = next[k];
                }
            }
        }
        /// <summary>
        /// source要搜索的字符串，keyWord关键词
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyWord"></param>
        /// <returns>keyWord在字符串中的开始位置</returns>
        public static bool KMPSearch(string source, string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord) || string.IsNullOrEmpty(source))
                return false;
            bool boolToReturn=false;
            int i = 0, j = 0;
            var next = new int[keyWord.Length];
            GetNext(keyWord,ref next);
            while (i < source.Length && j < keyWord.Length)
            {
                if (j == -1 || source[i] == keyWord[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }
            if (j == keyWord.Length)
            {
                boolToReturn = true;
            }
            return boolToReturn;
        }
    }
}
