using System;

namespace ConwaysGameOfLife
{
    internal class Program
    {
        //static string[,] ConwayGrid = GenerateGrid(5, 5);
        static string[,] ConwayGrid;
        // alive = ☒, dead = ☐
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Enter a number for the the height of the grid.");
            int y = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter a number for the the width of the grid.");
            int x = Int32.Parse(Console.ReadLine());
            ConwayGrid = GenerateGrid(y, x);
            Console.Clear();
            Console.WriteLine("alive = ☒, dead = ☐");
            PrintGrid(ConwayGrid);
            Console.WriteLine("Write \"run\" each time you want to generate the next generation!");

            //var temp = AdjecentChecker(2, 1);
            while(Console.ReadLine() == "run")
            { 
                Console.WriteLine("alive = ☒, dead = ☐");
                PrintGrid(CheckAllSpots());
                Console.WriteLine("Write \"run\" each time you want to generate the next generation!");
            }



            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintGrid(string[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static string[,] CheckAllSpots()
        {
            string[,] array = (string[,])ConwayGrid.Clone();
            for(int i = 0; i < ConwayGrid.GetLength(0); i++)
            {
                for(int j = 0; j < ConwayGrid.GetLength(1); j++)
                {
                    var adjacentScore = AdjecentChecker(i, j);
                    if(ConwayGrid[i,j] == " ☒ ")
                    {
                        if (adjacentScore < 2)
                        {
                            //underpopulation
                            array[i, j] = " ☐ ";
                        }
                        else if(adjacentScore == 2 || adjacentScore == 3)
                        {
                            //survive
                        }
                        else if(adjacentScore > 3)
                        {
                            //overpopulation
                            array[i, j] = " ☐ ";
                        }
                    }
                    else if(ConwayGrid[i,j] == " ☐ ")
                    {
                        if(adjacentScore == 3)
                        {
                            array[i, j] = " ☒ ";
                        }
                    }
                }
            }
            ConwayGrid = (string[,])array.Clone();
            return array;
        }

        static string[,] GenerateGrid(int verticalLenght, int horizontalLength)
        {
            var random = new Random();
            var returnArray = new string[verticalLenght, horizontalLength];
            for (int i = 0; i < returnArray.GetLength(0); i++)
            {
                for (int j = 0; j < returnArray.GetLength(1); j++)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        returnArray[i, j] = " ☐ ";
                    }
                    else
                    {
                        returnArray[i, j] = " ☒ ";
                    }
                }
            }
            return returnArray;
        }


        static int counter(string[,] returnArray)
        {

            int counter = 0;
            for (int i = 0; i < returnArray.GetLength(0); i++)
            {
                for (int j = 0; j < returnArray.GetLength(1); j++)
                {
                    if (returnArray[i,j] == " ☒ ")
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        static int AdjecentChecker(int i, int j)
        {

            int adjecentScore = 0;

            //Gets rows
            var temp1 = ConwayGrid.GetLength(0);
            //Gets columns
            var temp2 = ConwayGrid.GetLength(1);

            if (i == 0 && j == 0)
            {
                //for top left corner
                adjecentScore = CheckRight(i, j, adjecentScore);
                adjecentScore = CheckBottom(i, j, adjecentScore);
                adjecentScore = CheckBottomRight(i, j, adjecentScore);
            }
            else if (i == 0 && j == (ConwayGrid.GetLength(1) - 1))
            {
                //For top right corner
                adjecentScore = CheckLeft(i, j, adjecentScore);
                adjecentScore = CheckBottom(i, j, adjecentScore);
                adjecentScore = CheckBottomLeft(i, j, adjecentScore);
            }
            else if (i == ConwayGrid.GetLength(0) - 1 && j == ConwayGrid.GetLength(1) - 1)
            {
                //for the bottom right corner
                adjecentScore = CheckLeft(i, j, adjecentScore);
                adjecentScore = CheckAbove(i, j, adjecentScore);
                adjecentScore = CheckTopLeft(i, j, adjecentScore);
            }
            else if (i == ConwayGrid.GetLength(0) - 1 && j == 0)
            {
                //for the bottom left
                adjecentScore = CheckRight(i, j, adjecentScore);
                adjecentScore = CheckAbove(i, j, adjecentScore);
                adjecentScore = CheckTopRight(i, j, adjecentScore);
            }
            else if (j == 0)
            {
                //left side
                adjecentScore = CheckAbove(i, j, adjecentScore);
                adjecentScore = CheckTopRight(i, j, adjecentScore);
                adjecentScore = CheckRight(i, j, adjecentScore);
                adjecentScore = CheckBottomRight(i, j, adjecentScore);
                adjecentScore = CheckBottom(i, j, adjecentScore);
            }
            else if (i == 0)
            {
                //top side
                adjecentScore = CheckRight(i, j, adjecentScore);
                adjecentScore = CheckBottomRight(i, j, adjecentScore);
                adjecentScore = CheckBottom(i, j, adjecentScore);
                adjecentScore = CheckBottomLeft(i, j, adjecentScore);
                adjecentScore = CheckLeft(i, j, adjecentScore);
            }
            else if (j == ConwayGrid.GetLength(1) - 1)
            {
                //right side
                adjecentScore = CheckBottom(i,j, adjecentScore);
                adjecentScore = CheckBottomLeft(i, j, adjecentScore);
                adjecentScore = CheckLeft(i,j, adjecentScore);
                adjecentScore = CheckTopLeft(i, j, adjecentScore);
                adjecentScore = CheckAbove(i, j, adjecentScore);
            }
            else if (i == ConwayGrid.GetLength(0) - 1)
            {
                //bottom side
                adjecentScore = CheckLeft(i, j, adjecentScore);
                adjecentScore = CheckTopLeft(i, j, adjecentScore);
                adjecentScore = CheckAbove(i, j, adjecentScore);
                adjecentScore = CheckTopRight(i, j, adjecentScore); 
                adjecentScore = CheckRight(i, j, adjecentScore);
            }
            else
            {
                adjecentScore = CheckAbove(i,j, adjecentScore);
                adjecentScore = CheckTopRight(i,j,adjecentScore);
                adjecentScore = CheckRight(i, j, adjecentScore);
                adjecentScore = CheckBottomRight(i, j, adjecentScore);
                adjecentScore = CheckBottom(i, j, adjecentScore);
                adjecentScore = CheckBottomLeft(i, j, adjecentScore);
                adjecentScore = CheckLeft(i, j, adjecentScore);
                adjecentScore = CheckTopLeft(i, j, adjecentScore);
            }
                return adjecentScore;
        }

        private static int CheckTopRight(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i - 1, j + 1] == " ☒ ")
            {
                //top right
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckTopLeft(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i - 1, j - 1] == " ☒ ")
            {
                //top left
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckAbove(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i - 1, j] == " ☒ ")
            {
                //above
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckBottomLeft(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i + 1, j - 1] == " ☒ ")
            {
                //bottom left
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckLeft(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i, j - 1] == " ☒ ")
            {
                //left
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckBottomRight(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i + 1, j + 1] == " ☒ ")
            {
                //bottom right
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckBottom(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i + 1, j] == " ☒ ")
            {
                //bottom
                adjecentScore++;
            }

            return adjecentScore;
        }

        private static int CheckRight(int i, int j, int adjecentScore)
        {
            if (ConwayGrid[i, j + 1] == " ☒ ")
            {
                //right
                adjecentScore++;
            }

            return adjecentScore;
        }
    }
}
