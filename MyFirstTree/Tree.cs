using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
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
        // private Tree root;
        public Tree()
        {
            Original = new Dictionary<char, Tree>();
            sb = new StringBuilder();
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
                haveChild = Original.TryGetValue(key, out Tree childOfThisKey);//还需要判断父子树是否有孩子，
                if (haveChild)
                {
                    if (!childOfThisKey.Original.Any()) return; //如果没有，则return//eg免定金PCP，免定金PCPXXX
                    word = word.Substring(1);
                    if (word.Length > 0)
                    {
                        key = word[0];
                        childOfThisKey.PlantATree(key, word);//判断这个关键字是否有了孩子，如果有了的话就在后面接上
                    }
                    else
                    {
                        childOfThisKey.IsEnd = true;//免定金PCPXXX在前面，免定金PCP在后面，则把免定金PCP后面打一个结束标记
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

        /// <summary>
        /// 构建失败指针(BFS广度优先搜索)
        /// </summary>
        /// <param name="root"></param>
        public void BuildFailNodeBfs(ref Tree root)
        {
            Queue<Tree> queue = new Queue<Tree>();
            //根节点入队
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                //出队
                var currentNode = queue.Dequeue();
                //失败节点
                for (int i = 0; i < currentNode.sb.Length; i++)
                {

                    if (currentNode.Original[currentNode.sb[i]] == null)
                        continue;
                    //如果当前是根节点，则根节点的失败指针指向root
                    if (currentNode == root)
                    {
                        currentNode.Original[currentNode.sb[i]].fail = root;
                    }
                    else
                    {
                        //获取当前节点的失败指针
                        Tree currentFailNode = currentNode.fail;
                        while (currentFailNode != null)
                        {
                            //bool currentNodeHaveChild = currentNode.Original.TryGetValue(sb[i], out Tree childOfCurrentNode);
                            //bool currentFailNodeHaveCfhild = currentFailNode.Original.TryGetValue(sb[i], out Tree childOfCurrentFailNode);
                            if (currentFailNode.Original.TryGetValue(sb[i],out Tree childOfFailNode))//如果当前节点的孩子与当前节点的失败节点的孩子相同
                            {
                                currentNode.Original[currentNode.sb[i]].fail = childOfFailNode;
                                break;
                            }
                            currentFailNode = currentFailNode.fail;
                        }
                        //等于null的话，指向root节点
                        if (currentFailNode == null)
                            currentNode.Original[currentNode.sb[i]].fail = root;
                    }
                    queue.Enqueue(currentNode.Original[currentNode.sb[i]]);//当前节点的孩子节点入队BFS广度优先搜索
                }
            }
        }

        public bool SearchAC(ref Tree root, string sToCheck)
        {
            bool boolToReturn = false;
            Tree currentNode = root;
            int i = 0;
            while (i<sToCheck.Length)
            {
                if (currentNode.Original.TryGetValue(sToCheck[i], out Tree currentKeyChild))
                {
                    currentNode = currentKeyChild;
                    if (currentNode.IsEnd)
                    {
                        boolToReturn = true;
                        break;
                    }
                    i++;
                }
                else
                {
                    currentNode = currentNode.fail;
                    if (currentNode == null)
                    {
                        currentNode = root;
                        i++;
                    }
                }
            }
            return boolToReturn;
        }

        public class Timer
        {
            private static readonly Random _ran = new Random();
            public static void Run(Action act, string actName = "")
            {
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    act();
                    Console.ForegroundColor = (ConsoleColor)_ran.Next(2, 16);
                    Console.WriteLine("[{1}]运行用时:{0}ms", sw.ElapsedMilliseconds, string.IsNullOrEmpty(actName) ? act.Method.Name : actName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("程序运行出错:");
                    Console.WriteLine(e);
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }
    }
}
