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
        public string status;

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
        public Node insert(Node node, int key, string val, string direction="",int parentKey=0)
        {
            //BST insert
            if (node == null)
            {
                status += " key " + key + " has been inserted! \n\n";
                status += " key " + key + " is a "+direction+" child of: >>" + parentKey + "<<\n\n";
                return (new Node(key, val));
            }

            if (key < node.key)
            {
                status += " key "+key+" less than current node: "+node.key+", going left \n\n";
                node.left = insert(node.left, key, val,"left",node.key);
            }
            else if (key > node.key)
            {
                status += " key " + key + " larger than current node: " + node.key + ", going right \n\n";
                node.right = insert(node.right, key, val,"right", node.key);

            }
            else
            {
                status += " key is a duplicate!  \n\n";
                return node;
            }


            //update height of ancestor
            node.height = 1 + max(height(node.left),
                                height(node.right));

            //Get balance factor of ancestor to check for unbalance
            int balance = getBalance(node);

            //if unbalanced, 4 cases:            
            //Left Left Case  
            if (balance > 1 && key < node.left.key)
            {
                status += " balance factor is: " + balance + "\n\n";
                status += " left left case -> right rotate! \n\n"; return rightRotate(node);
            }
            // Right Right Case  
            if (balance < -1 && key > node.right.key)
            {
                status += " balance factor is: " + balance + "\n\n";
                status += " right right case <- left rotate! \n\n"; return leftRotate(node);
            }

            // Left Right Case  
            if (balance > 1 && key > node.left.key)
            {
                status += " balance factor is: " + balance + "\n\n";
                status += " left right case <<- double left rotate! \n\n";
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case  
            if (balance < -1 && key < node.right.key)
            {
                status += " balance factor is: " + balance + "\n\n";
                status += " right left case ->> double right rotate! \n\n";
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            //return node pointer
            return node;
        }


        Node minValueNode(Node node)
        {
            Node current = node;

            //get the leftmost node
            while (current.left != null)
                current = current.left;

            return current;
        }

        public Node deleteNode(Node root, int key)
        {
            //bst delete
            if (root == null)
                return root;

            //if key<current key go left
            if (key < root.key)
                root.left = deleteNode(root.left, key);

            //if key>current key go down right
            else if (key > root.key)
                root.right = deleteNode(root.right, key);

            //if key=current key then delete
            else
            {
                // node with only one child or no child  
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null;
                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    // No child case  
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // one child case  
                        root = temp; // copy non empty child
                }
                else
                {

                    // node with two children. get inorder successor
                    Node temp = minValueNode(root.right);

                    // Copy the inorder succ data to this
                    root.key = temp.key;

                    // Delete the inorder successor  
                    root.right = deleteNode(root.right, temp.key);
                }
            }

            // only one node, return.
            if (root == null)
                return root;

            //update height
            root.height = max(height(root.left),
                        height(root.right)) + 1;

            //get balance factor
            int balance = getBalance(root);

            // Left Left Case  
            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            // Left Right Case  
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case  
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }
    }
}
