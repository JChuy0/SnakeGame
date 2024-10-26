namespace SnakeGame
{
    public partial class frmMain : Form
    {
        private List<Point> snakeBody = [];
        private List<Point> wallList = [];
        Point foodSquare;
        string direction = "right";
        readonly int sqSize = 20;
        int score = 0;


        public frmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            snakeBody.Add(new Point(40, 40));
            this.KeyPreview = true;

            CreateFood();

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Paint += new PaintEventHandler(OnPaint);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            tmrUpdate.Start();
            tmrWallUpdate.Start();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (direction != "right") { direction = "left"; }
                    break;
                case Keys.Right:
                    if (direction != "left") { direction = "right"; }
                    break;
                case Keys.Up:
                    if (direction != "down") { direction = "up"; }
                    break;
                case Keys.Down:
                    if (direction != "up") { direction = "down"; }
                    break;
            }
        }

        private void UpdateSnake()
        {
            Point head = snakeBody[0];

            switch(direction)
            {
                case "left":
                    head.X -= sqSize;
                    break;
                case "right":
                    head.X += sqSize;
                    break;
                case "up":
                    head.Y -= sqSize;
                    break;
                case "down":
                    head.Y += sqSize;
                    break;
            }

            // Check if snake ate the food
            if (head == foodSquare)
            {
                snakeBody.Insert(0, head);
                score++;
                lblScore.Text = $"Score: {score}";
                CreateFood();
            }
            else
            {
                snakeBody.Insert(0, head);
                snakeBody.RemoveAt(snakeBody.Count - 1);
            }

            CheckForWallCollision();
            CheckForCollisionWithBody();

            // causes the entire form to be redrawn and updates the displayed snake
            this.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Point segment in snakeBody)
            {
                g.FillRectangle(Brushes.Green, segment.X, segment.Y, sqSize, sqSize);
            }
            
            foreach (Point segment in wallList)
            {
                g.FillRectangle(Brushes.DarkGray, segment.X, segment.Y, sqSize, sqSize);
            }

            g.FillRectangle(Brushes.Red, foodSquare.X, foodSquare.Y, sqSize, sqSize);
        }

        private void CreateFood()
        {
            Random rndNum = new Random();
            bool success = false;
            int maxX = (this.ClientSize.Width / sqSize);
            int maxY = (this.ClientSize.Height - 100) / sqSize;

            while (!success)
            {
                foodSquare = new Point(rndNum.Next(0, maxX) * sqSize, rndNum.Next(0, maxY) * sqSize);
                bool overlap = false;

                foreach (Point square in snakeBody)
                {
                    if (square == foodSquare)
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                {
                    success = true;
                }
            }
        }
        
        private void CreateWall()
        {
            Random rndNum = new Random();
            bool success = false;
            int maxX = (this.ClientSize.Width / sqSize);
            int maxY = (this.ClientSize.Height - 100) / sqSize;

            while (!success)
            {
                Point newWall = new Point(rndNum.Next(0, maxX) * sqSize, rndNum.Next(0, maxY) * sqSize);
                bool overlap = false;

                // Ensure new wall does not appear in a square occupied by the snake or food
                foreach (Point snakeSegment in snakeBody)
                {
                    if (snakeSegment == newWall || snakeSegment == foodSquare)
                    {
                        overlap = true;
                        break;
                    }
                }

                foreach (Point wallSegment in wallList)
                {
                    if (wallSegment == newWall)
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                {
                    success = true;
                    wallList.Add(newWall);
                }
            }
        }

        private void CheckForWallCollision()
        {
            Point head = snakeBody[0];

            // still need to check if snake hit any of the spawning walls
            foreach (Point wallSegment in wallList)
            {
                if (wallSegment == head)
                {
                    GameOver("You hit a wall!");
                }
            }

            // wall collision
            if (head.X < 0 || head.X + sqSize > this.ClientSize.Width || head.Y < 0 || head.Y + sqSize > this.ClientSize.Height - 60)
            {
                GameOver("You crashed into a wall!");
            }
        }

        private void CheckForCollisionWithBody()
        {
            Point head = snakeBody[0];

            // skip is important here because otherwise it will check if the head (index 0) is equal to index 0
            foreach (Point segment in snakeBody.Skip(1))
            {
                if (segment == head)
                {
                    GameOver("You ate yourself!");
                }
            }
        }

        private void GameOver(string message)
        {
            tmrUpdate.Stop();
            tmrWallUpdate.Stop();

            MessageBox.Show($"Game Over! {message}");
            Application.Exit();
            return; // application exit takes a few seconds to take effect, return is important here so that the game stops immedately
        }

        private void TmrWallUpdate_Tick(object sender, EventArgs e)
        {
            CreateWall();
        }

        private void TmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateSnake();

            if(score == 0)
            {
                tmrUpdate.Interval = 200;
            }
            else if (score == 5)
            {
                tmrUpdate.Interval = 150;
            }
            else if (score == 10)
            {
                tmrUpdate.Interval = 100;
            }
            else if (score == 15)
            {
                tmrUpdate.Interval = 50;
            }
        }

    }
}
