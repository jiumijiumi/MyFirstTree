using System.Collections;
using System.Collections.Generic;

namespace MyFirstTree
{
    public class AC
    {
        //https://www.cnblogs.com/nullzx/p/7499397.html
        Queue<TrieNode> queue=new Queue<TrieNode>();
        /// <summary>
        /// 构建失败指针(这里我们采用BFS的做法)
        /// </summary>
        /// <param name="root"></param>
        public void BuildFailNodeBFS(ref TrieNode root)
        {
            //根节点入队
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                //出队
                var temp = queue.Dequeue();//temp就是父节点里面的所有孩子节点
                //失败节点
                TrieNode failNode = null;
                //26叉树
                for (int i = 0; i < 26; i++)
                {
                    if (temp.childNodes[i] == null)
                        continue;
                    if (temp == root)
                    {
                        temp.childNodes[i].faliNode = root;
                    }
                    else
                    {//TODO 理解这里，理解这里，理解这里
                        failNode = temp.faliNode;
                        while (failNode != null)
                        {
                            if (failNode.childNodes[i] != null)
                            {
                                temp.childNodes[i].faliNode = failNode.childNodes[i];
                                break;
                            }
                            failNode = failNode.faliNode;
                        }
                        if (failNode == null)
                            temp.childNodes[i].faliNode = root;
                    }
                    queue.Enqueue(temp.childNodes[i]);
                }
            }
        }
    }
    #region Trie树节点
    /// <summary>
    /// Trie树节点
    /// </summary>
    public class TrieNode
    {
        /// <summary>
        /// 26个字符，也就是26叉树
        /// </summary>
        public TrieNode[] childNodes;

        /// <summary>
        /// 词频统计
        /// </summary>
        public int freq;

        /// <summary>
        /// 记录该节点的字符
        /// </summary>
        public char nodeChar;

        /// <summary>
        /// 失败指针
        /// </summary>
        public TrieNode faliNode;

        /// <summary>
        /// 插入记录时的编号id
        /// </summary>
        public HashSet<int> hashSet = new HashSet<int>();

        /// <summary>
        /// 初始化
        /// </summary>
        public TrieNode()
        {
            childNodes = new TrieNode[26];
            freq = 0;
        }
    }
    #endregion
}