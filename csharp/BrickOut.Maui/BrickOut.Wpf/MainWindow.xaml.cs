using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using BrickOut.GameLogic;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace BrickOut.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BrickOutGame currentGame { get; set; } = new(); // GameBoard.NewGame();
        private DispatcherTimer refreshTimer;
        private Rectangle paddleRectangle;
        private Rectangle ballRectangle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameCanvas.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5E2E0"));
            GameCanvas.MouseMove += GameCanvasOnMouseMove;
            DrawGameBoard();
            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = new TimeSpan(0, 0, 0, 0, 33);
            refreshTimer.Tick += RefreshTimerOnTick;
            refreshTimer.Start();
        }

        private void RefreshTimerOnTick(object? sender, EventArgs e)
        {
            UpdateGameBoard();
        }


        private void GameCanvasOnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(GameCanvas);
            currentGame.GameBoard.Paddle.SetLocation((int)position.X, 0);
        }

        private List<Color> BrickColors = new()
        {
            (Color)ColorConverter.ConvertFromString("#BC4A3C"),
            (Color)ColorConverter.ConvertFromString("#CE7064"),
            (Color)ColorConverter.ConvertFromString("#D88D83"),
            (Color)ColorConverter.ConvertFromString("#E2A9A2"),
            (Color)ColorConverter.ConvertFromString("#AB4336"),
            (Color)ColorConverter.ConvertFromString("#8C372C"),
            (Color)ColorConverter.ConvertFromString("#6D2B22"),
        };

        private void UpdateGameBoard()
        {
            if (paddleRectangle == null)
            {
                return;
            }

            Canvas.SetLeft(paddleRectangle, currentGame.GameBoard.Paddle.Location.X);

            UpdateBallPosition();
        }

        private void UpdateBallPosition()
        {
            var newX = currentGame.GameBoard.Ball.Location.X + currentGame.GameBoard.Ball.VelocityX;
            var newY = currentGame.GameBoard.Ball.Location.Y + currentGame.GameBoard.Ball.VelocityY;
            currentGame.GameBoard.Ball.SetLocation(newX, newY);
            Canvas.SetLeft(ballRectangle, currentGame.GameBoard.Ball.Location.X);
            Canvas.SetTop(ballRectangle, currentGame.GameBoard.Ball.Location.Y);
        }

        private void DrawGameBoard()
        {
            GameCanvas.Children.Clear();

            foreach (var brick in currentGame.GameBoard.Bricks)
            {
                AddRectangleToCanvas(
                    GetNextBrickColorBrush(),
                    brick.Location.X,
                    brick.Location.Y,
                    brick.Shape.Width,
                    brick.Shape.Height);
            }

            paddleRectangle = AddRectangleToCanvas(
                new SolidColorBrush(Colors.Red),
                currentGame.GameBoard.Paddle.Location.X,
                currentGame.GameBoard.Paddle.Location.Y,
                currentGame.GameBoard.Paddle.Shape.Width,
                currentGame.GameBoard.Paddle.Shape.Height);

            ballRectangle = AddRectangleToCanvas(
                new SolidColorBrush(Colors.Green),
                currentGame.GameBoard.Ball.Location.X,
                currentGame.GameBoard.Ball.Location.Y,
                currentGame.GameBoard.Ball.Shape.Width,
                currentGame.GameBoard.Ball.Shape.Height);
        }

        private Rectangle AddRectangleToCanvas(SolidColorBrush brush, int x, int y, int width, int height)
        {
            var wpfRect = new Rectangle();
            wpfRect.Fill = brush;
            wpfRect.Width = width;
            wpfRect.Height = height;
            GameCanvas.Children.Add(wpfRect);
            Canvas.SetTop(wpfRect, y);
            Canvas.SetLeft(wpfRect, x);
            return wpfRect;
        }

        private int nextBrickColorIndex = 0;

        private SolidColorBrush GetNextBrickColorBrush()
        {
            var brush = new SolidColorBrush(BrickColors[nextBrickColorIndex]);
            nextBrickColorIndex += 1;
            if (nextBrickColorIndex >= BrickColors.Count)
            {
                nextBrickColorIndex = 0;
            }

            return brush;
        }
    }
}