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
        public Tree fail;
        public StringBuilder sb;
        public Tree()
        {
            Original = new Dictionary<char, Tree>();
            sb=new StringBuilder();
        }
        /// <summary>
        /// 形成一棵树
        /// </summary>
        /// <param name="key"></param>
        /// <param name="word"></param>
        public void PlantATree(char key, string word)//递归递归可以改成for循环写成的
        {
            if (IsNullOrEmpty(word))
            {
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
                    ThisKey = key
                };
                if (!IsNullOrEmpty(word.Substring(1)))
                {
                    child.PlantATree(word.Substring(1)[0], word.Substring(1));
                }
                else
                {
                    child.IsEnd = true;
                }
                Original.Add(key, child);
                sb.Append(key);
            }
        }

        public void BuildAC(ref Tree tree)
        {
            Tree root = new Tree {Original = new Dictionary<char, Tree> {{'r', tree}}};
            Queue<Tree>queue=new Queue<Tree>();
            //将根结点的所有孩子结点的fail指向根结点，然后将根结点的所有孩子结点依次入列。
            foreach (char key in tree.sb.ToString())
            {
                tree.Original[key].fail = root;
                queue.Enqueue(tree.Original[key]);
            }
            while (queue.Count > 0)
            {
                Tree nodeTree = queue.Dequeue();
                for (int i = 0; i < nodeTree.sb.Length; i++)
                {
                    if (nodeTree.Original[sb[i]] != null)
                    {
                        queue.Enqueue(nodeTree.Original[sb[i]]);
                        Tree failTo = nodeTree.fail;
                        while (true)
                        {
                            //如果找到了根节点还是没有找到
                            if (failTo == null)
                            {
                                nodeTree.Original[sb[i]].fail = root;
                                break;
                            }
                            if (failTo.Original[sb[i]]!=null)
                            {
                                failTo.Original[sb[i]].fail = failTo.Original[sb[i]];
                                break;
                            }
                            else
                            {
                                failTo = failTo.fail;
                            }
                        }
                    }
                }
            }
        }

        public bool FindKeyWord(string keyWord)
        {
            bool boolToReturn = false;



            return boolToReturn;
        }
        /// <summary>
        /// 根据指定的主串，检索是否存在模式串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //public HashSet<int> SearchAC(string s)
        //{
        //    HashSet<int> hash = new HashSet<int>();

        //    SearchAC(ref trieNode, s, ref hash);

        //    return hash;
        //}

        /// <summary>
        /// 根据指定的主串，检索是否存在模式串
        /// </summary>
        /// <param name="root"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public void SearchAC(ref TrieNode root, string s, ref HashSet<int> hashSet)
        {
            int freq = 0;

            TrieNode head = root;

            foreach (var c in s)
            {
                //计算位置
                int index = c - 'a';

                //如果当前匹配的字符在trie树中无子节点并且不是root，则要走失败指针
                //回溯的去找它的当前节点的子节点
                while ((head.childNodes[index] == null) && (head != root))
                    head = head.faliNode;

                //获取该叉树
                head = head.childNodes[index];

                //如果为空，直接给root,表示该字符已经走完毕了
                if (head == null)
                    head = root;

                var temp = head;

                //在trie树中匹配到了字符，标记当前节点为已访问，并继续寻找该节点的失败节点。
                //直到root结束，相当于走了一个回旋。(注意：最后我们会出现一个freq=-1的失败指针链)
                while (temp != root && temp.freq != -1)
                {
                    freq += temp.freq;

                    //将找到的id追加到集合中
                    foreach (var item in temp.hashSet)
                        hashSet.Add(item);

                    temp.freq = -1;

                    temp = temp.faliNode;
                }
            }
        }
    }
}
