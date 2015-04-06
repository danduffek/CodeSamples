using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The problem as stated in the book:
// Imagine a robot sitting on the upper left corner of an X by Y grid.
// The robot can only move in two directions: right and down. How many possible 
// paths are there for the robot to go from (1, 1) to (X, Y)?
//
// FOLLOW UP
// Imagine certain spots are "off limits", such that the robot cannot step on them.
// Desing an algorithm to find a path for the robot from the top left to the bottom right.

namespace Q9_2cs
{
    class Program
    {
        const int INVALID_VAL = -1;
        static int xMax;
        static int yMax;
        static int pathCount = 0;
        static List<KeyValuePair<int, int>> excludeList = new List<KeyValuePair<int, int>>();

        static void Main(string[] args)
        {
            int x = 1, y = 1;
            string path = "", msg = "";

            xMax = getxyMax("X");
            yMax = getxyMax("Y");
            getExclude();
            FindPath(x, y, path);

            if(pathCount != 0){
                if (pathCount == 1)
                {
                    msg = "There is 1 path to go from (1, 1) to (" + xMax + ", " + yMax + ")."; 
                }
                else
                {
                    msg = "There are " + pathCount + " paths to go from (1, 1) to (" + xMax + ", " + yMax + ").";
                }
            }
            else
            {
                msg = "There are no paths from (1, 1) to (" + xMax + ", " + yMax + ").";
            }

            Console.WriteLine(msg);

        } // End: Main

        static int getxyMax(string dimension)
        {
            string input;
            int max = 1;
            Boolean getVal = true;

            while (getVal)
            {
                Console.Write("Enter a value for the " + dimension + " dimension: ");
                input = Console.ReadLine();

                max = convertStringToInt(input);

                // Keep trying until they give a valid val.
                getVal = (max == INVALID_VAL) ? true : false;

            }

            return max;

        } // End: getxyMax

        static void getExclude(){
            int x = 0, y = 0;
            string input;
            bool cont = true, isValid = false, isValidSquare = true;

            Console.WriteLine("Enter a list of squares that are 'blocked off'.");

            while (cont)
            {

                isValid = false;

                while (!isValid)
                {
                    Console.Write("Enter a X value for the square (0 to end): ");
                    input = Console.ReadLine();
                    x = convertStringToInt(input);
                    isValid = (x != INVALID_VAL) ? true : false;
                }

                isValid = false;
                while ((!isValid) && (x != 0))
                {
                    Console.Write("Enter a Y value for the square (0 to end): ");
                    input = Console.ReadLine();
                    y = convertStringToInt(input);
                    isValid = (y != INVALID_VAL) ? true : false;
                }

                cont = ((x == 0) || (y == 0)) ? false : true;

                // If the user hasn't decided to end see if square is 'ok' and add it to the list.
                if (cont)
                {
                    isValidSquare = ((x + y) != 2) && ((x + y) != (xMax + yMax)) ? true : false;
                    //isValidSquare = ((x != 1) && (y != 1)) ? true : false;
                    //isValidSquare = ((x != xMax) && (y != yMax)  && isValidSquare) ? true : false;

                    // At this point the only squares that would not be allowed would be the 
                    // starting (1, 1) and ending (xMax, yMax) squares.
                    if (isValidSquare)
                    {
                        excludeList.Add(new KeyValuePair<int, int>(x, y));
                    }
                    else
                    {
                        Console.WriteLine("Can not exclude squares (1, 1) and (" + xMax + ", " + yMax + ")");
                    }
                }

            }

        } // End: getExclude

        static int convertStringToInt(string input){
            int num = INVALID_VAL;

            try
            {
                num = Convert.ToInt32(input);
            }
            catch (FormatException e)
            {
                num = INVALID_VAL;
                Console.WriteLine("Input string is not a digit.");
            }
            catch (OverflowException e)
            {
                num = INVALID_VAL;
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return num;

        } // End: convertStringToInt

        // My solution was to treat this in the same way I would a b-tree traversal. 
        // That is in a b-tree traversal you can go either down the left or right branch,
        // and in this solution the robot can move only left or right.
        static void FindPath(int x, int y, string path)
        {
            if ((x < xMax) && !isSquareBlocked(x, y))
            {
                path = path + "(" + x + ", " + y + ")\r\n";
                FindPath(x + 1, y, path);
                // This is a hack. I am recording one move twice in this flow.
                path = path.Remove(path.Length - 8);
            }
            if ((y < yMax) && !isSquareBlocked(x, y))
            {
                path = path + "(" + x + ", " + y + ")\r\n";
                FindPath(x, y + 1, path);
            }
            if ((x == xMax) && (y == yMax))
            {
                Console.WriteLine("Path found: ");
                path = path + "(" + x + ", " + y + ")\r\n";
                Console.WriteLine(path);
                pathCount++;
                Console.Write("Hit <enter> to find the next path.");
                Console.ReadLine();
            }

        } // End: FindPath

        static bool isSquareBlocked(int x, int y)
        {
            if(excludeList.Contains(new KeyValuePair<int,int>(x,y))){
                return true;
            }
            else
            {
                return false;
            }

        } // End: isSquareBlocked
    }
}
