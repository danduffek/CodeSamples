using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The problem as stated in the book:
// Write a method to return all subsets of a set.
namespace Q9_4cs
{
    class Program
    {
        static string baseSet = "ABCD";
        static void Main(string[] args)
        {
            string pSet;
            string sSet;
            int i, j;

            Console.Write("Enter a string to use as the base set: ");
            baseSet = Console.ReadLine();

            while (baseSet.Length != 0)
            {
                pSet = "";
                i = 0;
                j = 0;

                Console.WriteLine("Base set: " + baseSet);

                // Even though this problem is in the chapter on recursion this solution is not.
                // Not very pretty but it does solve the problem.

                while (i < baseSet.Length)
                {
                    while (j < baseSet.Length)
                    {

                        if (i == j)
                        {
                            sSet = baseSet[i].ToString();
                        }
                        else
                        {
                            // pSet (prefixSet) is the subset up to this element.
                            // For example given the set ABCD and i is pointing to 'C' pSet would be AB.
                            sSet = pSet + baseSet[i] + baseSet[j];
                        }

                        Console.WriteLine(sSet);
                        j++;

                    }

                    if ((i > 0) && (pSet.Length == 0))
                    {
                        pSet = baseSet.Substring(0, i);
                        j = i + 1;
                    }
                    else
                    {
                        i++;
                        j = i;
                        pSet = "";
                    }

                }

                // Special case empty set.
                Console.WriteLine("<empty set>");
                Console.WriteLine();

                Console.Write("Enter a string to use as the base set: ");
                baseSet = Console.ReadLine();

            }

        }
    }
}
