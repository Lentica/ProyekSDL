using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekSDL
{
    class AVLTree
    {
        public Node root;

        //Mendapatkan height tree
        public int height(Node N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        //Get Max of 2 Nums
        public int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        //Right rotate
        public Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation  
            x.right = y;
            y.left = T2;

            // Update heights  
            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            // Return new root  
            return x;
        }

        //Left rotate
        public Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation  
            y.left = x;
            x.right = T2;

            // Update heights  
            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            // Return new root  
            return y;
        }

        //Get balance factor
        public int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        public Node find(Node node, int key)
        {
            if(node == null) { return null; }
            if(node.key == key) { return node; }
            else if (key <= node.key) { return find(node.left, key); }
            else { return find(node.right, key); }
        }
        public Node replace(Node node, String arah)
        {
            if(arah == "left")
            {
                if(node.right == null) { return node; }
                else { return replace(node.right, arah); }
            }
            else
            {
                if (node.left == null) { return node; }
                else { return replace(node.left, arah); }
            }
        }
        Node findparent(Node node, Node deletednode)
        {
            if(node.left == deletednode || node.right == deletednode) { return node; }
            else if(deletednode.key <= node.key) { return findparent(node.left, deletednode); }
            else { return findparent(node.right, deletednode); }
        }
        //balancenode(parentnode)
        public Node balancenode(Node node)
        {
            //Get balance factor of ancestor to check for unbalance
            int balance = getBalance(node);
            MessageBox.Show("nilai parent = " + node.key + " balance = " + balance);

            //if unbalanced, 4 cases:            
            //Left Left Case  
            if (balance > 1 && node.key < node.left.key)
            {
                Node nodeparent = findparent(root, node);
                if (nodeparent.left == node)
                { nodeparent.left = rightRotate(node); }
                else
                { nodeparent.right = rightRotate(node); }
            }
            // Right Right Case  
            if (balance < -1 && node.key > node.right.key)
            {
                Node nodeparent = findparent(root, node);
                if (nodeparent.left == node)
                { nodeparent.left = leftRotate(node); }
                else
                { nodeparent.right = leftRotate(node); }
            }
            // Left Right Case  
            if (balance > 1 && node.key > node.left.key)
            {
                node.left = leftRotate(node.left);
                Node nodeparent = findparent(root, node);
                if (nodeparent.left == node)
                { nodeparent.left = rightRotate(node); }
                else
                { nodeparent.right = rightRotate(node); }
            }
            // Right Left Case  
            if (balance < -1 && node.key < node.right.key)
            {
                node.right = rightRotate(node.right);
                Node nodeparent = findparent(root, node); 
                if(nodeparent.left == node)
                { nodeparent.left = leftRotate(node); }
                else
                { nodeparent.right = leftRotate(node); }
            }

            return null;
        }
        //delete
        public Node delete(Node node, int key)
        {
            Node deletednode = find(node, key);
            if (deletednode == null) { return null; }
            else {
                Node replacenode = null;
                if (deletednode.left != null) { replacenode = replace(deletednode.left, "left"); }
                else if(deletednode.right != null) { replacenode = replace(deletednode.right, "right"); }

                Node parentnode = null;
                if (replacenode == null)                 // yg dihapus tidak punya a
                {
                    parentnode = findparent(node, deletednode);
                    if(parentnode.left == deletednode)
                    { parentnode.left = null; }
                    else
                    { parentnode.right = null; }
                }
                else                                    // yg dihapus punya anak 
                {
                    deletednode.key = replacenode.key;
                    deletednode.value = replacenode.value;

                    parentnode = findparent(node, replacenode);
                    if (parentnode.left == replacenode)
                    { parentnode.left = replacenode.right; }
                    else
                    { parentnode.right = replacenode.left; }
                }


                // balancing from parentnode 
                balancenode(parentnode);

                return node;
            }
        }

        //insert
        public Node insert(Node node, int key, string val)
        {
            //BST insert
            if (node == null)
                return (new Node(key, val));

            if (key < node.key)
                node.left = insert(node.left, key, val);
            else if (key > node.key)
                node.right = insert(node.right, key, val);
            else //no duplicate keys 
                return node;

            //update height of ancestor
            node.height = 1 + max(height(node.left),
                                height(node.right));

            //Get balance factor of ancestor to check for unbalance
            int balance = getBalance(node);

            //if unbalanced, 4 cases:            
            //Left Left Case  
            if (balance > 1 && key < node.left.key)
                return rightRotate(node);

            // Right Right Case  
            if (balance < -1 && key > node.right.key)
                return leftRotate(node);

            // Left Right Case  
            if (balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case  
            if (balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
                
            }

            //return node pointer
            return node;
        }
    }
}
