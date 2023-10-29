using System.Drawing;

namespace BrickOut.GameLogic;

public class GameBoard
{
    public int Height { get; set; } = 640;
    public int Width { get; set; } = 480;

    public List<Brick> Bricks { get; set; } = new();
    public Ball Ball { get; set; }
    public Paddle Paddle { get; set; }

    public static GameBoard NewGame()
    {
        var board = new GameBoard();

        board.Paddle = new Paddle(0,635);
        board.Ball = new Ball(20, 630);
        
        // create bricks
        for (var row = 0; row < 6; row++)
        {
            int y = row * 10;
            // 24 columns == 480 width / 20 pixel wide bricks
            for (var col = 0; col < 24; col++)
            {
                int x = col * 20;
                board.Bricks.Add(new Brick(x, y));
            }
        }

        return board;
    }
}


public interface DisplayItem
{
    public Size Shape { get; set; }
    public Point Location { get; set; }
}

public class Brick : DisplayItem
{
    public Size Shape { get; set; }
    public Point Location { get; set; }

    public Brick(int x, int y)
    {
        Shape = new Size(20, 10);
        Location = new Point(x, y);
    }
    
}

public class Ball : DisplayItem
{
    public Size Shape { get; set; }
    public Point Location { get; set; }

    public Ball(int x, int y)
    {
        Shape = new Size(5, 5);
        Location = new Point(x, y);
    }
}

public class Paddle : DisplayItem
{
    public Size Shape { get; set; }
    public Point Location { get; set; }

    public Paddle(int x, int y)
    {
        Shape = new Size(40, 5);
        Location = new Point(x, y);
    }
}

public class BrickOutGame {}