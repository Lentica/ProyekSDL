using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekSDL
{
    class Node
    {
        public int key, height;
        public Node left, right;

        public Node(int d)
        {
            key = d;
            height = 1;
        }
    }
}
