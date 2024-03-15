using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ConwaysGameOfLife
{
    public partial class GameOfLife : Form
    {
        public int CellSize;
        public int GridWidth;
        public int GridHeight;

        private bool[,] grid;
        private bool[,] nextGeneration;
        private bool running;
        private Thread gameThread;
        private int generation = 0;

        public GameOfLife(int gridSize)
        {
            InitializeComponent();
            InitializeGrid(gridSize);
            StartGame();
        }

        private void InitializeGrid(int size)
        {
            CellSize = 10;
            GridWidth = size;
            GridHeight = size;
            running = true;
            grid = new bool[GridWidth, GridHeight];
            nextGeneration = new bool[GridWidth, GridHeight];
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
                Invalidate();
                Thread.Sleep(100);
            }
        }

        private void UpdateGameState()
        {
            generation++;
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);
                    bool isAlive = grid[x, y];

                    if (isAlive && (liveNeighbors < 2 || liveNeighbors > 3))
                    {
                        nextGeneration[x, y] = false;
                    }
                    else if (!isAlive && liveNeighbors == 3)
                    {
                        nextGeneration[x, y] = true;
                    }
                    else
                    {
                        nextGeneration[x, y] = isAlive;
                    }
                }
            }
            Array.Copy(nextGeneration, grid, GridWidth * GridHeight);
            UpdateWindowTitle();
        }

        public int CountLiveNeighbors(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = (x + i + GridWidth) % GridWidth;
                    int neighborY = (y + j + GridHeight) % GridHeight;
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
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    Brush brush = grid[x, y] ? Brushes.White : Brushes.Black;
                    g.FillRectangle(brush, x * CellSize, y * CellSize, CellSize, CellSize);
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            running = false;
        }
        private void UpdateWindowTitle()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Text = "Game of Life - Generation " + generation; }));
                }
                else
                {
                    this.Text = "Game of Life - Generation " + generation;
                }

            }
            catch (System.ComponentModel.InvalidAsynchronousStateException ex)
            {
                // Game was Closed
            }
            catch (System.ObjectDisposedException ex)
            {
                // Game was Closed
            }
        }

        public bool[,] GetGrid()
        {
            return grid;
        }
    }
}
