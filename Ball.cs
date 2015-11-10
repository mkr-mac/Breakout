using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ball
{

    public static double positionX = 45;
    public static double positionY = 190;

    //speed in pixels/sec
    public static double speedX = 1;
    public static double speedY = 1;

    //pause the ball after death
    bool paused = false;
    //time in seconds to hold the ball in place
    double pauseTimer = 3;
    readonly double defaultPauseTimer = 3;

    //angle ball is moving in deg.
    public static double theta = 45;

    public static int size = 7;

    int ballsRemaining = 3;
    bool dead = false;

    Texture2D ball;

    public Ball()
    {
        ball = Utils.TextureLoader("ball.png");
    }

    public void draw(AD2SpriteBatch sb)
    {
        if (!dead)
            sb.drawTexture(ball, (int)positionX, (int)positionY);
        else
            Utils.defaultFont.draw(sb, "Game Over", 40, 40, Color.Red, 4);
    }

    public void update(Breakout world, int ms)
    {
        if (!paused)
        {
            positionX += speedX;
            positionY -= speedY;

            if ((positionX <= 0) || (positionX + size >= Breakout.stageWidth))
                speedX = -speedX;
            if (positionY <= 0)
                speedY = -speedY;
            else if (positionY >= Breakout.baseHeight)
                ballOut();
        }else
        {
            pauseTimer -= (double)ms/1000;
            if (pauseTimer <= 0)
            {
                paused = false;
                //dirty hardcode
                pauseTimer = defaultPauseTimer;
            }
        }
    }

    void ballOut()
    {
        if (ballsRemaining > 0)
        {
            ballsRemaining--;

            paused = true;
            positionX = 45;
            positionY = 190;
            speedX = 1;
            speedY = 1;
        }
        else
            dead = true;
            //Game Over!
    }
}
