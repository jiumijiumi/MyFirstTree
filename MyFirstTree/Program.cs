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
    {   
        //二分搜索树  KMP算法，Trie树，AC自动机，BFS广度优先搜索
        //目标：比XA的程序运行的快340ms，在2018年到来之前完工//以后的算法估计都可以在这上面动刀子了
        //2017/11/9新建//2017/11/21/Version 1.0//2017/11/23/Version 1.1//11/25/Version 1.2
        //还需要做一些关于AC自动机的练习题
        public static void Main(string[] args)
        {
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
                // SortedMethods.QuickSort(ref sortedWordList, 0, sortedWordList.Count);
                foreach (var word in sortedWordList)
                {
                    tree.PlantATree(word[0], word);
                }
                tree.BuildFailNodeBfs( ref tree);
                Console.ReadKey();
            }


            //AC.Trie trie = new AC.Trie();
            //trie.AddTrieNode("say", 1);
            //trie.AddTrieNode("she", 2);
            //trie.AddTrieNode("shr", 3);
            //trie.AddTrieNode("her", 4);
            //trie.AddTrieNode("he", 5);
            //trie.BuildFailNodeBFS();
            //string s = "yasherhs";

            //var hashSet = trie.SearchAC(s);

            //Console.WriteLine("在主串{0}中存在模式串的编号为:{1}", s, string.Join(",", hashSet));

            //Console.Read();


        }
    }
}
