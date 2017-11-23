using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SensitiveWordFilter;

namespace MyFirstTree
{
    class Program
    {
        //目标：比XA的程序运行的快，在2018年到来之前完工
        //2017/11/9新建//2017/11/21/Version 1.0//2017/11/23/Version 1.1
        public static void Main(string[] args)
        {
            //StringBuilder sensitiveWordSb=new StringBuilder();
            using (StreamReader streamReader = new StreamReader(@"E:\BaiduNetdiskDownload\SensitiveWords.txt"))
            {
                List<string> sortedWordList = new List<string>();
                List<string> startWithNum = new List<string>();
                // var tree = new Tree();
                while (!streamReader.EndOfStream)
                {
                    var currentWord = streamReader.ReadLine();
                    if (currentWord != null)
                    {
                        if (char.IsNumber(GetFirstAlphabet.GetCharSpellCode(currentWord.First().ToString())))
                        {
                            startWithNum.Add(currentWord);
                        }
                        else
                        {
                            sortedWordList.Add(currentWord);//由于敏感词列表中以数字开头的十分的少，所以没有必要再进行排序
                        }
                    }
                    // tree.PlantATree(currentWord[0], currentWord);
                    // sensitiveWordSb.Append(currentWord);//从当前流中读取一行字符并将数据作为字符串返回。
                }
                SortedMethods.QuickSort(ref sortedWordList, 0, sortedWordList.Count);
                sortedWordList.AddRange(startWithNum);
                //foreach (var word in sortedWordList)
                //{
                //    tree.PlantATree(word[0], word);
                //}
                Console.ReadKey();
            }
        }
    }
}
