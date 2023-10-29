namespace BrickOut.GameLogic;

public class BrickOutGame
{
    public GameBoard GameBoard { get; set; } = GameBoard.NewGame();

    public void UpdateBallPosition()
    {
        var newX = GameBoard.Ball.Location.X + GameBoard.Ball.VelocityX;
        var newY = GameBoard.Ball.Location.Y + GameBoard.Ball.VelocityY;

        if (newX >= GameBoard.Width -5 || newX <=0)
        {
            GameBoard.Ball.VelocityX *= -1;
        }

        if (newY >= GameBoard.Height -5 || newY <= 0)
        {
            GameBoard.Ball.VelocityY *= -1;
        }
        
        GameBoard.Ball.SetLocation(newX, newY);
    }
}