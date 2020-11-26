using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //insert
        public Node insert(Node node, int key)
        {

            //BST insert
            if (node == null)
                return (new Node(key));

            if (key < node.key)
                node.left = insert(node.left, key);
            else if (key > node.key)
                node.right = insert(node.right, key);
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
        public void preOrder(Node node)
        {
            if (node != null)
            {
                preOrder(node.left);
                preOrder(node.right);
            }
        }
    }
}
