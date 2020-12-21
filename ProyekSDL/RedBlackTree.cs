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

                root = new NodeR(key, val);
                
                return root;
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
                        recolorInsert(root);
                        status += "Root of the tree, thus recolor, new color: " + root.color + "\n\n";
                    }
                    else if (root.parent.left != root)
                    {               
                        if (root.parent.left == null)
                        {
                            leftRotateInsert(root.parent);
                            status += "Left Rotate \n\n";
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
                            doubleLeftRotateInsert(root.parent);
                            recolorInsert(root);
                            recolorInsert(root.left);
                            status += " Double Left Rotate \n\n";
                        }
                    }
                    else
                    {
                        if (root.parent.right == null)
                        {
                            rightRotateInsert(root.parent);
                            status += " Right Rotate \n\n";
                        }
                        else if (root.parent.right.color == root.color)
                        {
                            status += "Recolor \n\n";
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.left);
                        }
                        else
                        {
                            // Line Rotate
                            rightRotateInsert(root.parent);
                            recolorInsert(root);
                            recolorInsert(root.right);
                            status += "Right Rotate \n\n";
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
                        recolorInsert(root);
                        status += "Root of the tree, thus recolor, new color: " + root.color + "\n\n";
                    }
                    else if (root.parent.right != root)
                    {
                        if (root.parent.right == null)
                        {
                            rightRotateInsert(root.parent);
                            status += "Right Rotate \n\n";
                        }
                        else if (root.parent.right.color == root.color)
                        {
                            // Re-Color
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.right);
                            status += "Recolor \n\n";
                        }
                        else
                        {
                            //  Triangle Rotate
                            doubleRightRotateInsert(root.parent);
                            recolorInsert(root);
                            recolorInsert(root.right);
                            status += "Double Right Rotate \n\n";
                        }
                    }
                    else
                    {
                        if (root.parent.left == null)
                        {
                            leftRotateInsert(root.parent);
                            status += "Left Rotate \n\n";
                        }
                        else if (root.parent.left.color == root.color)
                        {
                            recolorInsert(root.parent);
                            recolorInsert(root.parent.left);
                            recolorInsert(root.parent.right);
                            recolorInsert(root.right);
                            status += "Recolor \n\n";
                        }
                        else
                        {
                            // Line Rotate
                            leftRotateInsert(root.parent);
                            recolorInsert(root);
                            recolorInsert(root.left);
                            status += "Left Rotate \n\n";
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

        public void rightRotateInsert(NodeR root)
        {

            NodeR temp;
            if (root == null)
            {
            }
            else if (root.right == null)
            {

            }
            else
            {
                temp = root.right;
                root.right = temp.left;
                temp.left = root;
                root = temp;
                status += " Right Rotate \n\n";
            }
            
            
        }

        public void leftRotateInsert(NodeR root)
        {
            NodeR temp;
            if (root == null)
            {

            }
            else if (root.left == null)
            {

            }
            else
            {
                temp = root.left;
                root.left = temp.right;
                temp.right = root;
                root = temp;
                status += " Left Rotate \n\n";
            }
                
        }

        public void doubleLeftRotateInsert(NodeR root)
        {
            rightRotateInsert(root.right);
            leftRotateInsert(root);
            status += " Double Left Rotate \n\n";
        }

        public void doubleRightRotateInsert(NodeR root)
        {
            leftRotateInsert(root.left);
            rightRotateInsert(root);
            status += " Double Right Rotate \n\n";
        }
    
        public NodeR Find(int key)
        {
            bool isFound = false;
            NodeR temp=root;
            NodeR item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                else
                {
                    if (key < temp.data)
                    {
                    status+=key +" lebih kecil dari "+temp.data+" turun ke kiri \n\n";
                    temp = temp.left;
                    }
                    else if (key > temp.data)
                    {
                    status+=key+" lebih besar dari "+temp.data+" turun ke kanan \n\n";
                    temp = temp.right;
                    }
                    else if (key == temp.data)
                    {
                    isFound = true;
                    item = temp;
                    }
                }
            }
            if (isFound)
            {
                status+=key+" was found " + "\n\n";
                return item;
            }
            else
            {
                status+=key + " was not found " + "\n\n";
                return item;
            }
        }

        public void Delete(int key)
        {
            NodeR item = Find(key);
            NodeR X = null;
            NodeR Y = null;
            if (item == null)
            {
                status+=" Nothing to delete! " + "\n\n";
                return;
            }
            else
            {
                if(item.left==null&&item.right==null)
                {                    
                    status+=item.data+" tidak memiliki anak \n\n";
                    if(item.parent.left!=null)
                    {
                        if(item.parent.left.data==item.data)
                        {
                            item.parent.left=null;
                        }
                    }
                    if(item.parent.right!=null)
                    {
                        if(item.parent.right.data==item.data)
                        {
                            item.parent.right=null;
                        }
                    }
                    status+=key+" Node deleted \n\n";
                }
                else if(item.left!=null)
                {
                    status+=item.data+" memiliki anak kiri \n\n";
                    status+=item.left.data+" menggantikan posisi "+item.data+"\n\n";
                    status+=item.data+" Node deleted "+" \n\n";
                }
                else if(item.right!=null)    
                {
                    status+=item.data+" memiliki anak kanan \n\n";
                    status+=item.right.data+" menggantikan posisi "+item.data+"\n\n";
                    status+=item.data+" Node deleted "+" \n\n";
                }

                if (item.left == null || item.right == null)
                {
                    Y = item;
                }
                else
                {
                    Y = TreeSuccessor(item);
                }

                if (Y.left != null)
                {
                    X = Y.left;
                }
                else
                {
                    X = Y.right;
                }

                if (X != null)
                {
                    X.parent = Y;
                }

                if (Y.parent == null)
                {
                    item.root = X;
                }
                else if (Y == Y.parent.left)
                {
                    Y.parent.left = X;
                }
                else
                {
                    Y.parent.left = X;
                }   

                if (Y != item)
                {
                    item.data = Y.data;
                }
                if (Y.color == 1)
                {
                    //DeleteFixUp(X);
                }
            }
            


        }
    
        private void DeleteFixUp(NodeR X)
        {

            while (X != null && X != root && X.color == 1)
            {
                if (X == X.parent.left)
                {
                    NodeR W = X.parent.right;
                    
                        if (W.color == 0)
                        {
                            W.color = 1;
                            X.parent.color = 0;
                            LeftRotate(X.parent);
                            W = X.parent.right;
                        }

                        if (W.left.color == 1 && W.right.color == 1)
                        {
                            W.color = 0;
                            X = X.parent;
                        }
                        else if (W.right.color == 1)
                        {
                            W.left.color = 1;
                            W.color = 0;
                            RightRotate(W);
                            W = X.parent.right;
                        }

                        W.color = X.parent.color;
                        X.parent.color = 1;
                        W.right.color = 1;
                        LeftRotate(X.parent);
                        X = root;
                    
                }
                else
                {
                    NodeR W = X.parent.left;
                    
                        if (W.color == 0)
                        {
                            W.color = 1;
                            X.parent.color = 0;
                            RightRotate(X.parent);
                            W = X.parent.left;
                        }

                        if (W.right.color == 1 && W.left.color == 1)
                        {
                            W.color = 1;
                            X = X.parent;
                        }
                        else if (W.left.color == 1)
                        {
                            W.right.color = 1;
                            W.color = 0;
                            LeftRotate(W);
                            W = X.parent.left;
                        }

                        W.color = X.parent.color;
                        X.parent.color = 1;
                        W.left.color = 1;
                        RightRotate(X.parent);
                        X = root;
                    
                }
            }
            if (X != null)
            {
                X.color = 1;
            }
                
        }

        private NodeR Minimum(NodeR X)
        {

            while (X.left.left != null)
            {
                X = X.left;
            }

            if (X.left.right != null)
            {
                X = X.left.right;
            }

            return X;
        }

        private NodeR TreeSuccessor(NodeR X)
        {

            if (X.left != null)
            {
                status+= Minimum(X).data+" adalah successor";
                return Minimum(X);

            }
            else
            {
                NodeR Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                status+=Y.data+" adalah successor \n\n";
                return Y;
            }
        }
    
        private void LeftRotate(NodeR X)
        {
            NodeR Y = X.right;
            X.right = Y.left;
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;
            }
            if (X.parent == null)
            {
                root = Y;
            }
            if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X;
            if (X != null)
            {
                X.parent = Y;
            }

        }

        private void RightRotate(NodeR Y)
        {
            NodeR X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;
            if (Y != null)
            {
                Y.parent = X;
            }
        }

        public NodeR update(NodeR node, int key, string val)
        {
            if (node == null)
            {
                status += " key " + key + " was not found! >_< \n\n";
                return node;
            }
            else if (key < node.data)
            {
                status += " key " + key + " less than current node: " + node.data + ", going left \n\n";
                update(node.left, key, val);
            }
            else if (key > node.data)
            {
                status += " key " + key + " larger than current node: " + node.data + ", going right \n\n";
                update(node.right, key, val);
            }
            else
            {
                status += " key has been found!  \n\n";
                string tempVal = node.value;
                node.value = val;
                status += " value of: " + node.data + " has been replaced \n\n";
                status += " before: " + tempVal + " || after: " + node.value + "\n\n";
                return node;
            }
            return node;
        }
    }
}
