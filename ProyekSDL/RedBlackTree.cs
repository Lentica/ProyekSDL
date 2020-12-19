using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    
    class NodeR
    {
        public int data;
        public NodeR parent;
        public NodeR left;
        public NodeR right;
        public int color;
        // 0 = Red
        // 1 = Black

        public NodeR(int data)
        {
            this.data = data;
            left = right = parent = null;
            color = 0;
        }
    }
    class Tree
    {
        public NodeR insert(NodeR root, int val)
        {
            if (root == null)
            {
                root = new NodeR(val);
            }
            else if (val < root.data)
            {
                root.left = insert(root.left, val);
                root.left.parent = root;
                if (root.left.color == root.color)
                {
                    if (root.parent.left != root)
                    {
                        if (root.parent.left.color == root.color)
                        {
                            // Re-Color
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.left);
                        }
                        else
                        {
                            //  Triangle Rotate
                            doubleLeftRotateInsert(root);
                            recolorInsert(root);
                            recolorInsert(root.left);
                        }
                    }
                    else
                    {
                        if (root.parent.right.color == root.color)
                        {
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.left);
                        }
                        else
                        {
                            // Line Rotate
                            rightRotateInsert(root);
                            recolorInsert(root);
                            recolorInsert(root.right);
                        }
                        
                    }
                }
            }
            else if (val > root.data)
            {
                // Lebih Besar
                root.right = insert(root.right, val);
                root.right.parent = root;
                if (root.right.color == root.color)
                {
                    if (root.parent.right != root)
                    {
                        if (root.parent.right.color == root.color)
                        {
                            // Re-Color
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.right);
                        }
                        else
                        {
                            //  Triangle Rotate
                            doubleRightRotateInsert(root);
                            recolorInsert(root);
                            recolorInsert(root.right);
                        }
                    }
                    else
                    {
                        if (root.parent.left.color == root.color)
                        {
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.right);
                        }
                        else
                        {
                            // Line Rotate
                            leftRotateInsert(root);
                            recolorInsert(root);
                            recolorInsert(root.left);
                        }

                    }
                }
            }
            return root;
        }

        public void recolorInsert(NodeR root)
        {
            if (root.color == 0)
            {
                root.color = 1;
            }
            else if (root.color == 1)
            {
                root.color = 0;
            }else if (root.parent == null)
            {
                root.color = 1;
            }
        }

        public void leftRotateInsert(NodeR root)
        {
            NodeR temp = root.right;
            root.right = temp.left;
            temp.left = root;
            root = temp;
        }

        public void rightRotateInsert(NodeR root)
        {
            NodeR temp = root.left;
            root.left = temp.right;
            temp.right = root;
            root = temp;
        }

        public void doubleLeftRotateInsert(NodeR root)
        {
            rightRotateInsert(root.right);
            leftRotateInsert(root);
        }

        public void doubleRightRotateInsert(NodeR root)
        {
            leftRotateInsert(root.left);
            rightRotateInsert(root);
        }
    }
}
