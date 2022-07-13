using System;
namespace Life_game
{
    /// <summary>
    /// A class which contains information about Villager.
    /// </summary>
    public class Villager
    {
        /// <summary>
        /// variable responsible for the number of neighbors.
        /// </summary>
        public int neighbours { get; set; }
        /// <summary>
        /// Variable responsible for the position of the villager relative to X.
        /// </summary>
        public int positionX { get; set; }
        /// <summary>
        /// Variable responsible for the position of the villager relative to Y.
        /// </summary>
        public int positionY { get; set; }
        /// <summary>
        /// A variable responsible for whether the villager is alive or not.
        /// </summary>
        public bool alive { get; set; }

        public Villager()
        {
            alive = true;
            positionX = 0;
            positionY = 0;
        }
        public Villager(int _positionX, int _positionY)
        {
            positionX = _positionX;
            positionY = _positionY;
        }
    }
}


