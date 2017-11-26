using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SensitiveWordFilter;

namespace MyFirstTree
{
    class Program
    {    //对于生成的树中的敏感词也可以进行按照拼音排序啊啊啊啊啊啊，怎么就只想到了前面呢  然排序首要还是要实现搜索
        //二分搜索树
        //目标：比XA的程序运行的快340ms，在2018年到来之前完工//以后的算法估计都可以在这上面动刀子了
        //2017/11/9新建//2017/11/21/Version 1.0//2017/11/23/Version 1.1//11/25/Version 1.2
        public static void Main(string[] args)
        {
            //StringBuilder sensitiveWordSb=new StringBuilder();
            using (StreamReader streamReader = new StreamReader(@"E:\BaiduNetdiskDownload\SensitiveWords.txt"))
            {
                List<string> sortedWordList = new List<string>();
                var tree = new Tree();
                while (!streamReader.EndOfStream)
                {
                    var currentWord = streamReader.ReadLine();
                    if (currentWord != null)
                    {
                        sortedWordList.Add(currentWord);
                    }
                }
                SortedMethods.QuickSort(ref sortedWordList, 0, sortedWordList.Count);
                foreach (var word in sortedWordList)
                {
                    tree.PlantATree(word[0], word);
                }
                tree.SearchTree("免定金雄鹰AWP");
                Console.ReadKey();
            }
        }
    }
}
