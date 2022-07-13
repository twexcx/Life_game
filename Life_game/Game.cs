using System;
namespace Life_game
{
    /// <summary>
    /// The main class in which the game will take place.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The array in which the Vilagers are located including their position.
        /// </summary>
        private Villager[,] villagers;
        /// <summary>
        /// An array that includes the properties of the Villagers (1-death, 2-add, 3-remain in the same state).
        /// </summary>
        private int[,] consistences;
        /// <summary>
        /// A variable responsible for the number of Villagers.
        /// </summary>
        private int players;
        /// <summary>
        /// Singleton with random.
        /// </summary>
        public SingleRandom random;
        public Game()
        {
            random = SingleRandom.getInstance();
            consistences = new int[10, 10];
            villagers = new Villager[10, 10];
            players = 0;
        }
        /// <summary>
        /// The method in which the game starts.
        /// </summary>
        public void startGame()
        {
            Console.WriteLine("how many groups of 3 villsgers do you need? select from 1 to 33");
            int.TryParse(Console.ReadLine(), out int groupCount);
            Console.Clear();

            if (groupCount >= 1 && groupCount < 34)
            {
                createPlayers(0, 0, 0, 0, groupCount);
                Console.ReadLine();
                while (players > 0)
                {
                    move();
                }
            }
            else
            {
                Console.WriteLine("You cannot choose the number of groups you have chosen");
            }
        }
        /// <summary>
        /// A method that calculates the number of neighbours (villagers) around a villager relative to position of him.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>the number of neighbours relative to position of villager</returns>
        public int neighbourCount(int posX, int posY)
        {
            if (posY == 0 && posX < 8 && posX > 1)
                return checkUpperSide(posX, posY);

            else
            if (posX == 0 && posY < 8 && posY > 1)
                return checkLeftSide(posX, posY);

            else
            if (posX == 9 && posY < 8 && posY > 1)
                return checkRightSide(posX, posY);

            else
            if (posY == 9 &&
               posX < 8 && posX > 1)
                return checkBottomSide(posX, posY);

            else
            if (posY == 0 && posX == 0)
                return checkTopLeftAngle(posX, posY);

            else
            if (posY == 9 && posX == 0)
                return checkBottomLeftAngle(posX, posY);

            else
            if (posY == 0 && posX == 9)
                return checkTopRightAngle(posX, posY);

            else
            if (posY == 9 && posX == 9)
                return checkBottomRightAngle(posX, posY);

            else
            if (posY > 0 && posY <= 8
            && posX > 0 && posX <= 8)
            {
                return checkPole(posX, posY);
            }
            return 0;
        }
        /// <summary>
        /// Basic game method.
        /// </summary>
        public void move()
        {
            int neighbours = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    neighbours = neighbourCount(i, j);

                    if (villagers[i, j] == null)
                    {
                        if (neighbours >= 2 && neighbours < 4)
                        {
                            consistences[i, j] = 2;
                        }
                        else
                        {
                            consistences[i, j] = 3;
                        }
                    }
                    if (villagers[i, j] != null)
                    {
                        if (neighbours < 2 || neighbours >= 4)
                        {
                            consistences[i, j] = 1;
                        }
                        else
                        {
                            consistences[i, j] = 3;
                        }
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (consistences[i, j] == 1)
                    {
                        villagers[i, j] = null;
                        Console.SetCursorPosition(i, j);
                        Console.Write(" ");
                        players--;
                    }

                    if (consistences[i, j] == 2)
                    {
                        villagers[i, j] = new Villager();
                        Console.SetCursorPosition(i, j);
                        Console.Write("@");
                        players++;
                    }
                }
            }
            consistences = new int[10, 10];
            Console.SetCursorPosition(12, 5);
            Console.ReadLine();
        }
        /// <summary>
        /// Method for creating villagers.
        /// </summary>
        /// <param name="startPosX">Villager's starting position relative to X.</param>
        /// <param name="startPosY">Villager's starting position relative to Y.</param>
        /// <param name="ShiftPosX">Shift of the position of the villager relative to X.</param>
        /// <param name="ShiftPosY">Shift of the position of the villager relative to Y.</param>
        /// <param name="groups">Number of groups of 3 Villager each.</param>
        public void createPlayers(int startPosX, int startPosY, int ShiftPosX, int ShiftPosY, int groups)
        {
            int maxPlayersInGroup = 0;

            for (int i = 0; i < groups; i++)
            {
                startPosX = random.GetInt(0, 10);
                startPosY = random.GetInt(0, 10);
                villagers[startPosX, startPosY] = new Villager(startPosX, startPosY);
                Console.SetCursorPosition(startPosX, startPosY);
                Console.Write('@');
                players++;

                while (maxPlayersInGroup != 2)
                {
                    ShiftPosX = random.GetInt(-1, 2);
                    ShiftPosY = random.GetInt(-1, 2);

                    while (startPosX + ShiftPosX > 9 || startPosY + ShiftPosY > 9 || startPosX + ShiftPosX < 0 || startPosY + ShiftPosY < 0)
                    {
                        ShiftPosX = random.GetInt(-1, 2);
                        ShiftPosY = random.GetInt(-1, 2);
                    }

                    if (villagers[startPosX + ShiftPosX, startPosY + ShiftPosY] == null)
                    {
                        villagers[startPosX + ShiftPosX, startPosY + ShiftPosY] =
                         new Villager(startPosX + ShiftPosX, startPosY + ShiftPosY);
                        Console.SetCursorPosition(startPosX + ShiftPosX, startPosY + ShiftPosY);
                        Console.Write('@');
                        players++;
                        maxPlayersInGroup++;
                    }
                }
                maxPlayersInGroup = 0;
            }
        }
        /// <summary>
        /// Checking the top left corner for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>number of neighbours</returns>
        public int checkTopLeftAngle(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            if (villagers[posX + 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking the bottom left corner for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkBottomLeftAngle(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX + 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking the top right corner for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkTopRightAngle(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            if (villagers[posX - 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking the bottom right corner for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkBottomRightAngle(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            if (villagers[posX - 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking upper side for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkUpperSide(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            if (villagers[posX + 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking left side for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkLeftSide(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            if (villagers[posX + 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            if (villagers[posX + 1, posY + 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking bottom side for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkBottomSide(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX + 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking right side for neighbors.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkRightSide(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX - 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY + 1] != null) neighbours += 1;
            return neighbours;
        }
        /// <summary>
        /// Checking for a place that is not at the edge of the pole.
        /// </summary>
        /// <param name="posX">Villager position relative to X.</param>
        /// <param name="posY">Villager position relative to Y.</param>
        /// <returns>Number of neighbours.</returns>
        public int checkPole(int posX, int posY)
        {
            int neighbours = 0;
            if (villagers[posX + 1, posY] != null) neighbours += 1;
            if (villagers[posX + 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX, posY + 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY + 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY] != null) neighbours += 1;
            if (villagers[posX - 1, posY - 1] != null) neighbours += 1;
            if (villagers[posX, posY - 1] != null) neighbours += 1;
            if (villagers[posX - 1, posY + 1] != null) neighbours += 1;
            return neighbours;


        }
    }
}

