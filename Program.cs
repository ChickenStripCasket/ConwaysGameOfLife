namespace ConwaysGameOfLife
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();

            GameOfLife game = new GameOfLife(120);
            game.ClientSize = new Size(game.GridWidth * game.CellSize, game.GridHeight * game.CellSize);
            Application.Run(game);
        }
    }
}