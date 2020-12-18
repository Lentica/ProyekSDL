using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    
    class NodeRBT
    {
        public int data;
        public NodeRBT parent;
        public NodeRBT left;
        public NodeRBT right;
        public int color;
        // 0 = Red
        // 1 = Black

        public NodeRBT(int data)
        {
            this.data = data;
            left = right = parent = null;
            color = 0;
        }
    }
    class RedBlackTree
    {
        public NodeRBT insert(NodeRBT root, int val)
        {
            if (root == null)
            {
                root = new NodeRBT(val);
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
                            recolor(root.parent);
                            recolor(root.parent.left);
                            recolor(root.parent.right);
                            recolor(root.left);
                        }
                        else
                        {
                            //  Triangle Rotate
                            doubleLeftRotate(root);
                            recolor(root);
                            recolor(root.left);
                        }
                    }
                    else
                    {
                        if (root.parent.right.color == root.color)
                        {
                            recolor(root.parent);
                            recolor(root.parent.left);
                            recolor(root.parent.right);
                            recolor(root.left);
                        }
                        else
                        {
                            // Line Rotate
                            rightRotate(root);
                            recolor(root);
                            recolor(root.right);
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
                            recolor(root.parent);
                            recolor(root.parent.left);
                            recolor(root.parent.right);
                            recolor(root.right);
                        }
                        else
                        {
                            //  Triangle Rotate
                            doubleRightRotate(root);
                            recolor(root);
                            recolor(root.right);
                        }
                    }
                    else
                    {
                        if (root.parent.left.color == root.color)
                        {
                            recolor(root.parent);
                            recolor(root.parent.left);
                            recolor(root.parent.right);
                            recolor(root.right);
                        }
                        else
                        {
                            // Line Rotate
                            leftRotate(root);
                            recolor(root);
                            recolor(root.left);
                        }

                    }
                }
            }
            return root;
        }

        public void recolor(NodeRBT root)
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

        public void leftRotate(NodeRBT root)
        {
            NodeRBT temp = root.right;
            root.right = temp.left;
            temp.left = root;
            root = temp;
        }

        public void rightRotate(NodeRBT root)
        {
            NodeRBT temp = root.left;
            root.left = temp.right;
            temp.right = root;
            root = temp;
        }

        public void doubleLeftRotate(NodeRBT root)
        {
            rightRotate(root.right);
            leftRotate(root);
        }

        public void doubleRightRotate(NodeRBT root)
        {
            leftRotate(root.left);
            rightRotate(root);
        }
    }
}
