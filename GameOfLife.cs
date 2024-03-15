using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConwaysGameOfLife
{
    public partial class GameOfLife : Form
    {
        public int CellSize;
        public int GridWidth;
        public int GridHeight;

        private bool[,] grid;
        private bool running;
        private Thread gameThread;

        public GameOfLife(int GridSize)
        {
            InitializeComponent();
            InitializeGrid(GridSize);
            StartGame();
        }

        private void InitializeGrid(int size)
        {
            CellSize = 10;
            GridWidth = size;
            GridHeight = size;
            running = true;
            grid = new bool[GridWidth, GridHeight];
            Random random = new Random();
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    grid[x, y] = random.Next(2) == 0;
                }
            }
        }

        private void StartGame()
        {
            gameThread = new Thread(GameLoop);
            gameThread.Start();
        }

        private void GameLoop()
        {
            while (running)
            {
                UpdateGameState();
                Thread.Sleep(100);
                Invalidate();
            }
        }
        #region BeansAndTaters
        private void UpdateGameState()
        {
            bool[,] newGrid = new bool[GridWidth, GridHeight];
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);
                    bool isAlive = grid[x, y];

                    // Apply Conway's rules

                    // Any live cell with fewer than two live neighbors dies, as if by underpopulation.
                    // Any live cell with more than three live neighbors dies, as if by overpopulation.
                    if (isAlive && (liveNeighbors < 2 || liveNeighbors > 3))
                    {
                        newGrid[x, y] = false;
                    }

                    // Any live cell with two or three live neighbors lives on to the next generation.
                    if (isAlive && (liveNeighbors == 2 || liveNeighbors == 3))
                    {
                        newGrid[x, y] = true;
                    }

                    // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                    if (!isAlive && liveNeighbors == 3)
                    {
                        newGrid[x, y] = true;
                    }
                }
            }
            grid = newGrid;
        }
        #endregion
        private int CountLiveNeighbors(int x, int y)
        {
            int count = 0;
            // Iterate through the neighbors of the current cell
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Calculate the coordinates of the neighbor cell
                    int neighborX = (x + i + GridWidth) % GridWidth;
                    int neighborY = (y + j + GridHeight) % GridHeight;

                    // Count the live neighbors
                    if (grid[neighborX, neighborY] && !(i == 0 && j == 0))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            using (BufferedGraphics bufferedGraphics = context.Allocate(e.Graphics, ClientRectangle))
            {
                Graphics g = bufferedGraphics.Graphics;
                g.Clear(Color.White);

                for (int x = 0; x < GridWidth; x++)
                {
                    for (int y = 0; y < GridHeight; y++)
                    {
                        Brush brush = grid[x, y] ? Brushes.Black : Brushes.White;
                        g.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                    }
                }

                bufferedGraphics.Render();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            running = false;
        }
    }
}
