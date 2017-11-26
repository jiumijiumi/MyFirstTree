using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string FirstAlphabet;
        public Tree()
        {
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
                IsEnd = true;
            }
            else
            {
                bool haveChild = false;
                haveChild = Original.TryGetValue(key, out Tree newParent);//还需要判断父子树是否有孩子，如果没有，则return//eg免定金PCP，免定金PCPXXX
                if (haveChild)
                {
                    if (!newParent.Original.Any()) return;
                    word = word.Substring(1);
                    if (word.Length > 0)
                    {
                        key = word[0];
                        newParent.PlantATree(key, word);
                    }
                    return;
                }
                var child = new Tree()
                {
                    Parent = this,
                    ThisKey = key,
                    FirstAlphabet =ProfessionGf.GetFirstPinYin(key.ToString())
                };
                if (!IsNullOrEmpty(word.Substring(1)))
                {
                    child.PlantATree(word.Substring(1)[0], word.Substring(1));
                }
                Original.Add(key, child);
            }
        }

        public bool SearchTree(string wordToSearch)
        {
            bool ifContains = false;
            if (!IsNullOrEmpty(wordToSearch))
            {
              
                var key = wordToSearch[0];
                if (GetChildren(key, this, 0, this.Original.Count))
                {
                    SearchTree(wordToSearch.Substring(1));
                }
            }
            if (Original.Count == 0)
            {
                ifContains = true;
            }
            return ifContains;
        }
        //for (int index = 0; index<d.Count; index++)
        //{
        //    var item = d.ElementAt(index);
        //    var itemKey = item.Key;
        //    var itemValue = item.Value;
        //}


        public bool GetChildren(char key, Tree tree, int left, int right)
        {
            var middle = (left + right) / 2;
            var itemMiddle = tree.Original.ElementAt(middle).Key;
            var itemmiddleLetter = ProfessionGf.GetFirstPinYin(itemMiddle.ToString());
            var keyLetter = ProfessionGf.GetFirstPinYin(key.ToString());
            if (left > right)
            {
                return false;
            }
            if (keyLetter == itemmiddleLetter)
            {
                return true;
            }
            if (left == right)
            {
                return false;
            }
            if (CompareOrdinal(keyLetter,itemmiddleLetter)<0)
            {
                return GetChildren(key, tree, 1, middle - 1);
            }
            else
            {
                return GetChildren(key, tree, middle + 1, right);
            }
        }

    }
}
