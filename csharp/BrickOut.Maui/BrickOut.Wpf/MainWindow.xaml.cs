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
        public GameBoard CurrentGame { get; set; } = GameBoard.NewGame();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameCanvas.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5E2E0"));
            GameCanvas.MouseMove += GameCanvasOnMouseMove;
            DrawGameBoard();
        }

        private void GameCanvasOnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(GameCanvas);
            // CurrentGame.Paddle.Shape  // .X = position.X;
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


        private void DrawGameBoard()
        {
            GameCanvas.Children.Clear();

            foreach (var brick in CurrentGame.Bricks)
            {
                AddRectangleToCanvas(
                    GetNextBrickColorBrush(),
                    brick.Location.X,
                    brick.Location.Y,
                    brick.Shape.Width,
                    brick.Shape.Height);
            }

            AddRectangleToCanvas(
                new SolidColorBrush(Colors.Red),
                CurrentGame.Paddle.Location.X,
                CurrentGame.Paddle.Location.Y,
                CurrentGame.Paddle.Shape.Width,
                CurrentGame.Paddle.Shape.Height);
            
             AddRectangleToCanvas(
                            new SolidColorBrush(Colors.Green),
                            CurrentGame.Ball.Location.X,
                            CurrentGame.Ball.Location.Y,
                            CurrentGame.Ball.Shape.Width,
                            CurrentGame.Ball.Shape.Height);
        }

        private void AddRectangleToCanvas(SolidColorBrush brush, int x, int y, int width, int height)
        {
            var wpfRect = new Rectangle();
            wpfRect.Fill = brush;
            wpfRect.Width = width;
            wpfRect.Height = height;
            GameCanvas.Children.Add(wpfRect);
            Canvas.SetTop(wpfRect, y);
            Canvas.SetLeft(wpfRect, x);
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