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

        private void UpdateGameState()
        {
            bool[,] newGrid = new bool[GridWidth, GridHeight];
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);
                    if (grid[x, y])
                    {
                        newGrid[x, y] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        newGrid[x, y] = liveNeighbors == 3;
                    }
                }
            }
            grid = newGrid;
        }

        private int CountLiveNeighbors(int x, int y)
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
