using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekSDL
{
    class NodeRBT
    {
        public int data;
        public NodeRBT parent;
        public NodeRBT root;
        public NodeRBT left;
        public NodeRBT right;
        public int color;

        public NodeRBT(int data)
        {
            this.data = data;
            left = right = parent = null;
            color = 0;
        }

        public void Insert(int item)
        {
            NodeRBT newItem = new NodeRBT(item);
            if (root == null)
            {
                root = newItem;
                root.color = 1;
                return;
            }
            NodeRBT Y = null;
            NodeRBT X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data < Y.data)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.color = 0;
            InsertFixUp(newItem);
        }
        private void InsertFixUp(NodeRBT item)
        {
            //Checks Red-Black Tree properties
            while (item != root && item.parent.color == 1)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    NodeRBT Y = item.parent.parent.right;
                    if (Y != null && Y.color == 1)//Case 1: uncle is red
                    {
                        item.parent.color = 0;
                        Y.color = 0;
                        item.parent.parent.color = 1;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        //Case 3: recolour & rotate
                    item.parent.color = 0;
                    item.parent.parent.color = 1;
                    RightRotate(item.parent.parent);
                    }
 
                }
                else
                {
                    //mirror image of code above
                    NodeRBT X = null;
 
                    X = item.parent.parent.left;
                    if (X != null && X.color == 1)//Case 1
                    {
                         item.parent.color = 0;
                         X.color = 1;
                         item.parent.parent.color = 0;
                         item = item.parent.parent;
                    }
                    else //Case 2
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        //Case 3: recolour & rotate
                    item.parent.color = 1;
                    item.parent.parent.color = 0;
                    LeftRotate(item.parent.parent);
 
                    }
 
                }
                root.color = 1;//re-colour the root black as necessary
            }
        }

        private void LeftRotate(NodeRBT X)
        {
            NodeRBT Y = X.right;
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
        private void RightRotate(NodeRBT Y)
        {
            NodeRBT X = Y.left;
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
        public NodeRBT Find(int key)
        {
            bool isFound = false;
            NodeRBT temp = root;
            NodeRBT item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                if (key > temp.data)
                {
                    temp = temp.right;
                }
                if (key == temp.data)
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        public void Delete(int key)
        {
            NodeRBT item = Find(key);
            NodeRBT X = null;
            NodeRBT Y = null;

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
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
                root = X;
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
                DeleteFixUp(X);
            }

        }

        private void DeleteFixUp(NodeRBT X)
        {

            while (X != null && X != root && X.color == 1)
            {

                if (X == X.parent.left)
                {

                    NodeRBT W = X.parent.right;

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

                    NodeRBT W = X.parent.left;

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
                X.color = 1;
        }
        private NodeRBT Minimum(NodeRBT X)
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
        private NodeRBT TreeSuccessor(NodeRBT X)
        {

            if (X.left != null)
            {

                return Minimum(X);

            }
            else
            {
                NodeRBT Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }

                return Y;
            }
        }
    }


}
