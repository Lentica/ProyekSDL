using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekSDL
{
    
    class NodeR
    {
        public int data;
        public NodeR parent;
        public NodeR root;
        public NodeR left;
        public NodeR right;
        public int color;
        public string value;
        public string status;
        // 0 = Red
        // 1 = Black

        public NodeR(int data, string value)
        {
            this.data = data;
            this.value = value;
            left = right = parent = root = null;
            color = 0;
        }

        public NodeR insert(NodeR root, int key, string val, string direction = "", int parentKey = 0)
        {
            if (root == null)
            {
                status += " key " + key + " has been inserted! \n\n";
                status += " key " + key + " is a " + direction + " child of: >>" + parentKey + "<<\n\n";
                return root = new NodeR(key, val);
            }
            else if (key < root.data)
            {
                status += " key " + key + " less than current node: " + root.data + ", going left \n\n";
                root.left = insert(root.left, key, val, "Left", root.data);
                root.left.parent = root;
                if (root.left.color == root.color)
                {
                    if (root.parent == null)
                    {

                    }
                    else if (root.parent.left != root)
                    {
                        if (root.parent.left == null)
                        {

                        }
                        else if (root.parent.left.color == root.color)
                        {
                            // Re-Color
                            status += "Recolor \n\n";
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
                        if (root.parent.right == null)
                        {

                        }
                        else if (root.parent.right.color == root.color)
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
            else if (key > root.data)
            {
                // Lebih Besar
                status += " key " + key + " larger than current node: " + root.data + ", going right \n\n";
                root.right = insert(root.right, key, val, "Right" , root.data);
                root.right.parent = root;
                if (root.right.color == root.color)
                {
                    if (root.parent == null)
                    {

                    }
                    else if (root.parent.right != root)
                    {
                        if (root.parent.right == null)
                        {

                        }
                        else if (root.parent.right.color == root.color)
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
                        if (root.parent.left == null)
                        {

                        }
                        else if (root.parent.left.color == root.color)
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
            if (root == null)
            {

            }
            else if (root.color == 0)
            {
                root.color = 1;
            }
            else if (root.color == 1)
            {
                root.color = 0;
            }
            else if (root.parent == null)
            {
                root.color = 1;
            }
        }

        public void leftRotateInsert(NodeR root)
        {
            NodeR temp;
            if (root.right == null)
            {

            }
            else
            {
                temp = root.right;
                root.right = temp.left;
                temp.left = root;
                root = temp;
            }
            
            
        }

        public void rightRotateInsert(NodeR root)
        {
            NodeR temp;
            if (root.left == null)
            {

            }
            else
            {
                temp = root.left;
                root.left = temp.right;
                temp.right = root;
                root = temp;
            }
                
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
    class Tree
    {
        
    }
}
