using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The problem as stated in the book:
// Consider a simple node-like data structure called BiNode, which has two pointers
// to two other nodes.
//  1.  public class BiNode{
//  2.      public BiNode node1, node2;
//  3.      public int data;
//  4.  }
// The data structure BiNode could be used to represent both a binary tree (where
// node1 is the left node and node2 is the right node) or a doubly linked list (where
// node1 is the previous node and node2 is the next node). Implement a method
// to convert a binary search tree (implemented with BiNode) into a double linked
// list. The values should be kept in order and the operation should be performed
// in place (that is, on the original data structure).

namespace Q17_13cs
{
    class BiNode
    {
        public int data;
        public BiNode node1;
        public BiNode node2;

        public BiNode(int x)
        {
            data = x;
            node1 = null;
            node2 = null;
        }
    }

    // This is used in the solution given in the book.
    class NodePair
    {
        public BiNode head;
        public BiNode tail;

        public NodePair(BiNode head, BiNode tail)
        {
            this.head = head;
            this.tail = tail;
        }
    }

    class Program
    {
        static BiNode tree;
        static void Main(string[] args)
        {

            Console.WriteLine("Using a \'random\' tree.");
            BuildTreeRandom();

            Console.WriteLine("Pre-order traversal:");
            PrintTree(-1, tree);
            Console.WriteLine("");

            Console.WriteLine("Calling my solution.");
            MySolution(tree);
            // Because I don't set the head and tail object, I have to move to it.
            while (tree.node1 != null)
            {
                tree = tree.node1;
            }

            Console.WriteLine("After conversion:");
            PrintList(tree);
            Console.WriteLine("");

            //Console.WriteLine("Calling solution from book.");
            //NodePair lst = BookSolution(tree);
            //tree = lst.head;

            Console.WriteLine("Using a \'balanced\' tree.");
            BuildTreeBalanced();
            Console.WriteLine("In-order traversal:");
            PrintTree(0, tree);
            Console.WriteLine("");

            Console.WriteLine("Calling my solution.");
            MySolution(tree);
            // Again lacking a head and tail object I have to move to the head.
            while (tree.node1 != null)
            {
                tree = tree.node1;
            }

            //Console.WriteLine("Calling solution from book.");
            //NodePair lst = BookSolution(tree);
            //tree = lst.head;

            Console.WriteLine("After conversion:");
            PrintList(tree);
            Console.WriteLine("");

        }

        // My solution is not as elegant as the solution in the book but I was more interested in solving it on my own w/o just looking at the solution.
        // My thinking was this: one of the goals was to keep the list in order. You can generate an in-order list of the
        // elements of a binary tree if you print the node the second time, or the in-order time, you visit it. So my solution was to 
        // traverse the tree and then manipulate the pointers for node1 when I was at the "in-order" part of the traversal. 
        // Then I manipulate the node2 pointers when I am at the "post-order" part of the traversal.
        // Because I did not use a head and tail pointer like the book my solution incurs the hit of having to loop until I find the head and tail.
        static void MySolution(BiNode n)
        {
            if (n.node1 != null)
            {
                MySolution(n.node1);
            }

            // For the left pointer (node1) of any node it should point to the "tail" of the list of
            // nodes to it's left. So loop until you get to the last node in the list.
            if (n.node1 != null)
            {
                while (n.node1.node2 != null)
                {
                    n.node1 = n.node1.node2;
                }
                n.node1.node2 = n;
            }

            if (n.node2 != null)
            {
                MySolution(n.node2);
            }

            // For the right pointer (node2) of any node it should point to the "head" of the list of
            // nodes to it's right. So loop until you get to the first node in the list.
            if (n.node2 != null)
            {
                while (n.node2.node1 != null)
                {
                    n.node2 = n.node2.node1;
                }
                n.node2.node1 = n;
            }

        }

        static NodePair BookSolution(BiNode root)
        {
            if (root == null)
            {
                return null;
            }

            NodePair part1 = BookSolution(root.node1);
            NodePair part2 = BookSolution(root.node2);

            if (part1 != null)
            {
                Concat(part1.tail, root);
            }

            if (part2 != null)
            {
                Concat(root, part2.head);
            }

            return new NodePair(part1 == null ? root : part1.head,
                                part2 == null ? root : part2.tail);
        }

        static void Concat(BiNode x, BiNode y){
            x.node2 = y;
            y.node1 = x;
        }

        static void BuildTreeRandom()
        {

            tree = null;
            tree = TreeInsert(5, tree);
            tree = TreeInsert(7, tree);
            tree = TreeInsert(6, tree);
            tree = TreeInsert(9, tree);
            tree = TreeInsert(10, tree);
            tree = TreeInsert(8, tree);
            tree = TreeInsert(4, tree);
            tree = TreeInsert(2, tree);
            tree = TreeInsert(3, tree);
            tree = TreeInsert(1, tree);
        }

        static void BuildTreeBalanced()
        {
            tree = null;
            tree = TreeInsert(8, tree);
            tree = TreeInsert(4, tree);
            tree = TreeInsert(12, tree);
            tree = TreeInsert(2, tree);
            tree = TreeInsert(6, tree);
            tree = TreeInsert(10, tree);
            tree = TreeInsert(14, tree);
            tree = TreeInsert(1, tree);
            tree = TreeInsert(3, tree);
            tree = TreeInsert(5, tree);
            tree = TreeInsert(7, tree);
            tree = TreeInsert(9, tree);
            tree = TreeInsert(11, tree);
            tree = TreeInsert(13, tree);
            tree = TreeInsert(15, tree);
        }

        static BiNode TreeInsert(int val, BiNode n)
        {
            BiNode nNew = null;

            if (n == null)
            {
                nNew = new BiNode(val);
            }
            else
            {
                if (val > n.data)
                {
                    n.node2 = TreeInsert(val, n.node2);
                }
                else
                {
                    n.node1 = TreeInsert(val, n.node1);
                }
                nNew = n;
            }

            return nNew;

        }

        static void PrintList(BiNode n)
        {
            // Go to head.
            while (n.node1 != null)
            {
                n = n.node1;
            }

            while (n.node2 != null)
            {
                Console.Write(n.data + " ");
                n = n.node2;
            }
            Console.Write(n.data + " ");

            Console.WriteLine(" ");
            Console.WriteLine("Now in reverse order:");
            while (n.node1 != null)
            {
                Console.Write(n.data + " ");
                n = n.node1;
            }
            Console.Write(n.data + " ");

        }

        static void PrintTree(int order, BiNode n)
        {
            // Pre-order
            if (order == -1)
            {
                Console.Write(n.data + " ");
            }

            if (n.node1 != null)
            {
                PrintTree(order, n.node1);
            }

            // In-order
            if (order == 0)
            {
                Console.Write(n.data + " ");
            }

            if (n.node2 != null)
            {
                PrintTree(order, n.node2);
            }

            // Post-order
            if(order == 1)
            {
                Console.Write(n.data + " ");
            }

        }

    }
}
