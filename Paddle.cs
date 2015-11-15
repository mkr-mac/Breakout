using Microsoft.Xna.Framework.Graphics;

public class Paddle
{
    //The paddle's X and Y position.
    public double PositionX;
    public readonly int PositionY = 200;

    //Size of the paddle.
    public int Width = 41 ;
    //Height isn't used currently.
    //public int Height = 10;

    //How fast the paddle moves.
    double Speed = 2;

    //Texture of the paddle.
    Texture2D PaddleTexture;

    public Paddle()
    {
        //Center up the paddle on the stage to start.
        PositionX = (Breakout.StageWidth / 2) +- (Width / 2);
        //Load in the texture of the paddle.
        PaddleTexture = Utils.TextureLoader(@"paddleNormal.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        sb.DrawTexture(PaddleTexture, (int)PositionX, PositionY);
    }

    public void Update(InGame world, Microsoft.Xna.Framework.Input.KeyboardState ks)
    {
        //Checking for input.
        if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) && (PositionX > 0))
        {
            PositionX -= Speed;
            //The paddle can't go off the left side of the screen.
            if (PositionX < 0)
                PositionX = 0;
        }
        else if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))&& (PositionX < Breakout.StageWidth - Width))
        {
            PositionX += Speed;
            //The paddle can't go beyond the right side of the stage.
            if (PositionX > Breakout.StageWidth - Width)
                //If we try, We are set next to the edge.
                PositionX = Breakout.StageWidth - Width;
        }
    }
}
