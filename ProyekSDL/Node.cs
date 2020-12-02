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
        public string value;
        public Node left, right;
        public int testCommit;

        public Node(int d, string val="")
        {
            key = d;
            height = 1;
            value = val;
            testCommit = 0;
        }
    }
}
