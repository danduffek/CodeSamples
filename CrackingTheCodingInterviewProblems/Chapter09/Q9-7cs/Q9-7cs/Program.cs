using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The problem as stated in the book:
// Implement the "paint fill" function that one might see on many image editing
// programs. That is, given a screen (represented by a two-dimensional array of
// colors), a point, and a new color, fill in the surrounding area until the color
// changes from the original color.
namespace Q9_7cs
{
    class Program
    {
        const int MAX_COLS = 25;
        const int MAX_ROWS = 25;

        static int[,] screen = new int[MAX_ROWS, MAX_COLS];
        static int fillColor = 1;
        static int colorToReplace = 0;
        static int otherColor = 2;

        static void Main(string[] args)
        {

            initScreen();
            Console.WriteLine("Screen after initialization:");
            printScreen();
            // Setting the point to start filling at 10, 10.
            fillScreen(10, 10);
            Console.WriteLine("Screen after fill:");
            printScreen();

        }

        static void fillScreen(int r, int c)
        {

            if (screen[r, c] == colorToReplace)
            {
                screen[r, c] = fillColor;
            }

            // For each square I am going to see if each of the four
            // neighboring squares can be filled. I will start by looking at
            // the square to the right and then progress clockwise to the 
            // square below, then to the left and finally to the one above.
            if ((c + 1 < MAX_COLS) && screen[r, c + 1] == colorToReplace)
                fillScreen(r, c + 1);

            if ((r + 1 < MAX_ROWS) && screen[r + 1, c] == colorToReplace)
                fillScreen(r + 1, c);

            if ((c - 1 >= 0) && screen[r, c - 1] == colorToReplace)
                fillScreen(r, c - 1);

            if ((r - 1 >= 0) && screen[r - 1, c] == colorToReplace)
                fillScreen(r - 1, c);

        }

        static void initScreen()
        {
            int r, c, c1;

            // Fill the screen with the default color.
            for (r = 0; r < MAX_ROWS; r++)
            {
                for (c = 0; c < MAX_COLS; c++)
                {
                    screen[r, c] = colorToReplace;
                }
            }

            // This is a variation on the problem. I am drawing some 
            // object on the screen that will block the fill process.
            // This should give a box on the screen with the
            // bottom of the screen acting as one of the sides.
            r = MAX_ROWS / 2;
            c = MAX_COLS / 3;
            c1 = (2 * MAX_COLS) / 3;

            for (r = MAX_ROWS / 2; r < MAX_ROWS; r++)
            {
                screen[r, c] = otherColor;
                screen[r, c1] = otherColor;
            }

            r = MAX_ROWS / 2;
            for (c = MAX_COLS / 3; c < c1; c++)
            {
                screen[r, c] = otherColor;
            }

        }

        static void printScreen()
        {

            for (int r = 0; r < MAX_ROWS; r++)
            {
                for (int c = 0; c < MAX_COLS; c++)
                {
                    Console.Write(screen[r, c] + "\t");
                }
                Console.WriteLine("");
            }
        }

    }
}
