using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using SensitiveWordFilter;
using static System.String;

namespace MyFirstTree
{
    public class Tree
    {
        public bool IsEnd;
        public Tree Parent;
        public Dictionary<char, Tree> Original;
        public char ThisKey;
        public char FirstAlphabet;

        public Tree()
        {
            // Capital=new Dictionary<char, Tree>();
            Original = new Dictionary<char, Tree>();
        }

        /// <summary>
        /// 形成一棵树
        /// </summary>
        /// <param name="key"></param>
        /// <param name="word"></param>
        public void PlantATree(char key, string word)//递归递归
        {
            if (IsNullOrEmpty(word))
            {
            }
            else
            {
                bool haveChild = false;
                haveChild =Original.TryGetValue(key, out Tree newParent);//还需要判断父子树是否有孩子，如果没有，则return//eg免定金PCP，免定金PCPXXX
                if (haveChild)
                {
                    if (!newParent.Original.Any()) return;
                    word = word.Substring(1);
                    key = word[0];
                    newParent.PlantATree(key, word);
                    return;
                }
                var child = new Tree()
                {
                    Parent = this,
                    ThisKey = key,
                };
                if (!IsNullOrEmpty(word.Substring(1)))
                {
                    child.PlantATree(word.Substring(1)[0], word.Substring(1));
                }
                Original.Add(key, child);
            }
        }

        public bool SearchTree(Tree treeToSearch)
        {
            bool Remain = false;

            return Remain;
        }
    }
}
