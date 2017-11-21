using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    

namespace BinarySearchTree
    {
        //二叉查找树的节点定义
        public class Node
        {
            public int data; //节点本身的数据
            public Node left; //左孩子
            public Node right;//右孩子
            public Node rootNode;
            public int GetData() => this.data;
            public void SetData(int value) => this.data = value;
        } 

        public class BinarySearchTree
        {
            // 构建二叉树是通过向二叉树插入元素得以实现的，所有小于根节点的节点插入根节点的左子树，大于根节点的，插入右子树。
            // 依此类推进行递归。直到找到位置进行插入。二叉查找树的构建过程其实就是节点的插入过程。
            public Node root;
            public BinarySearchTree()
            {
                root = null;
            }
            public void Insert(int data)
            {
                Node parent;
                //将所需插入的数据包装进节点
                Node newNode = new Node();
                newNode.data = data;
                //如果为空树，则插入根节点
                if (root == null)
                {
                    root = newNode;
                }
                //否则找到合适叶子节点位置插入
                else
                {
                    //将根节点赋值给当前节点
                    Node current = root;
                    while (true)
                    {
                        //将根节点赋值给临时父节点
                        parent = current;
                        //如果插入的数据小于当前节点的值，则插入到左节点
                        if (data < current.data)
                        {
                            current = current.left;
                            //如果当前节点值为空，则插入当前的左节点
                            if (current == null)
                            {
                                parent.left = newNode;
                                break;
                            }
                        }
                        //如果插入的数据大于当前节点的值，则插入到右节点
                        else
                        {
                            current = current.right;
                            if (current == null)
                            {
                                parent.right = newNode;
                                break;
                            }
                        }
                    }
                }
            }//end insert()
        }// end class BinarySearchTree
        
    }

