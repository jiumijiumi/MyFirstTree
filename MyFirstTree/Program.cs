using System;
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
        //2017/11/9新建//2017/11/21/Version 1.0
        public static void Main(string[] args)
        {
            StringBuilder sensitiveWordSb=new StringBuilder();
            using (StreamReader streamReader = new StreamReader(@"E:\BaiduNetdiskDownload\SensitiveWords.txt"))
            {
                var tree = new Tree();
               // var wordTree = new WordTree();
                while (!streamReader.EndOfStream)
                {
                    var currentWord = streamReader.ReadLine();
                    if (currentWord != null)
                   // wordTree.PushIn(currentWord);
                    tree.PlantATree(currentWord[0], currentWord);
                    sensitiveWordSb.Append(currentWord);//从当前流中读取一行字符并将数据作为字符串返回。
                }
                Console.ReadKey();
            }
        }
    }
}
