using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Balls
{

    public static double positionX = 45;
    public static double positionY = 190;

    //speed in pixels/sec
    public static double speedX = .5;
    public static double speedY = .5;
    //angle ball is moving in deg.
    public static double theta = 45;

    public static int size = 7;

    Texture2D ball;

    public Balls()
    {
        ball = Utils.TextureLoader(@"ball.png");
    }

    public void draw(AD2SpriteBatch sb)
    {
        sb.drawTexture(ball, (int)positionX, (int)positionY);
    }

    public void update(Breakout world)
    {
        positionX += speedX;
        positionY -= speedY;

        //if (positionX + size >= Breakout.baseWidth)
            //ribble

            
        if ((positionX <= 0)|| (positionX + size >= Breakout.stageWidth))
            speedX = -speedX;
        if (positionY <= 0)
            speedY = -speedY;
    }
}
