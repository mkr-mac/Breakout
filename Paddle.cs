using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Paddle
{
    //paddle's X and Y position
    public double x = 0;
    public readonly int y = 200;

    public int width = 41;
    public int height = 10;

    double speed = 2;

    public int livesLeft = 3;

    Texture2D PaddleTexture;

    public Paddle()
    {
        PaddleTexture = Utils.TextureLoader(@"paddleNormal.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        sb.DrawTexture(PaddleTexture, (int)x, y);
    }

    public void Update(Breakout world, Microsoft.Xna.Framework.Input.KeyboardState ks)
    {
        if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) && (x > 0))
        {
            x -= speed;
            if (x < 0)
                x = 0;
        }
        else if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))&& (x < Breakout.StageWidth - width))
        {
            x += speed;
            if (x > Breakout.StageWidth - width)
                x = Breakout.StageWidth - width;
        }
    }
}
