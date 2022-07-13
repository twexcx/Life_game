using System;
namespace Life_game
{
    /// <summary>
    /// Class which includes singleton with random.
    /// </summary>
    public class SingleRandom
    {
        /// <summary>
        /// A variable of singleton.
        /// </summary>
        private static SingleRandom random;
        /// <summary>
        /// A variable responsible for randomising numbers.
        /// </summary>
        protected Random randomGenerator { get; private set; }

        public SingleRandom()
        {
            randomGenerator = new Random();
        }
        /// <summary>
        /// Method which creates a random instance.
        /// </summary>
        /// <returns>Object with random.</returns>
        public static SingleRandom getInstance()
        {
            if (random == null)
            {
                random = new SingleRandom();

            }
            return random;
        }
        /// <summary>
        /// Number randomizer between x and y.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <param name="y">A number different to x.</param>
        /// <returns>Random number between x and y.</returns>
        public int GetInt(int x, int y)
        {
            return randomGenerator.Next(x, y);
        }
        /// <summary>
        /// Number randomizer within x.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>Random number within x.</returns>
        public int GetInt(int x)
        {
            return randomGenerator.Next(x);
        }
        /// <summary>
        /// Number randomizer.
        /// </summary>
        /// <returns>A random number.</returns>
        public int GetInt()
        {
            return randomGenerator.Next();
        }
        /// <summary>
        /// Decimals randomizer.
        /// </summary>
        /// <returns>A random decimal.j</returns>
        public double GetDouble()
        {
            return randomGenerator.NextDouble();
        }

    }
}
