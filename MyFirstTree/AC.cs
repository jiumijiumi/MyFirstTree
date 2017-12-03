using System.Collections;
using System.Collections.Generic;

namespace MyFirstTree
{
    public class AC
    {
        //https://www.cnblogs.com/nullzx/p/7499397.html
        public class Trie
        {
            public TrieNode trieNode = new TrieNode();

            /// <summary>
            /// 用广搜的方法来构建失败指针
            /// </summary>
            public Queue<TrieNode> queue = new Queue<TrieNode>();

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

            #region 插入操作

            /// <summary>
            /// 插入操作
            /// </summary>
            /// <param name="word"></param>
            /// <param name="id"></param>
            public void AddTrieNode(string word, int id)
            {
                AddTrieNode(ref trieNode, word, id);
            }

            /// <summary>
            /// 插入操作
            /// </summary>
            /// <param name="root"></param>
            /// <param name="s"></param>
            public void AddTrieNode(ref TrieNode root, string word, int id)
            {
                if (word.Length == 0)
                    return;

                //求字符地址，方便将该字符放入到26叉树中的哪一叉中
                int k = word[0] - 'a';

                //如果该叉树为空，则初始化
                if (root.childNodes[k] == null)
                {
                    root.childNodes[k] = new TrieNode();

                    //记录下字符
                    root.childNodes[k].nodeChar = word[0];
                }

                var nextWord = word.Substring(1);

                //说明是最后一个字符，统计该词出现的次数
                if (nextWord.Length == 0)
                {
                    root.childNodes[k].freq++;
                    root.childNodes[k].hashSet.Add(id);
                }

                AddTrieNode(ref root.childNodes[k], nextWord, id);
            }

            #endregion

            #region 构建失败指针

            /// <summary>
            /// 构建失败指针(这里我们采用BFS的做法)
            /// </summary>
            public void BuildFailNodeBFS()
            {
                BuildFailNodeBFS(ref trieNode);
            }

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
                    var temp = queue.Dequeue();

                    //失败节点
                    TrieNode failNode = null;

                    //26叉树
                    for (int i = 0; i < 26; i++)
                    {
                        //代码技巧：用BFS方式，从当前节点找其孩子节点，此时孩子节点
                        //         的父亲正是当前节点，（避免了parent节点的存在）
                        if (temp.childNodes[i] == null)
                            continue;

                        //如果当前是根节点，则根节点的失败指针指向root
                        if (temp == root)
                        {
                            temp.childNodes[i].faliNode = root;
                        }
                        else
                        {
                            //获取出队节点的失败指针
                            failNode = temp.faliNode;

                            //沿着它父节点的失败指针走，一直要找到一个节点，直到它的儿子也包含该节点。
                            while (failNode != null)
                            {
                                //如果不为空，则在父亲失败节点中往子节点中深入。
                                if (failNode.childNodes[i] != null)
                                {
                                    temp.childNodes[i].faliNode = failNode.childNodes[i];
                                    break;
                                }
                                //如果无法深入子节点，则退回到父亲失败节点并向root节点往根部延伸，直到null
                                //（一个回溯再深入的过程，非常有意思）
                                failNode = failNode.faliNode;
                            }

                            //等于null的话，指向root节点
                            if (failNode == null)
                                temp.childNodes[i].faliNode = root;
                        }
                        queue.Enqueue(temp.childNodes[i]);
                    }
                }
            }

            #endregion

            #region 检索操作

            /// <summary>
            /// 根据指定的主串，检索是否存在模式串
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public HashSet<int> SearchAC(string s)
            {
                HashSet<int> hash = new HashSet<int>();

                SearchAC(ref trieNode, s, ref hash);

                return hash;
            }

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
            #endregion
        }
    }
}