using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Ball
{

    public static double positionX = 45;
    public static double positionY = 190;

    //speed in pixels/sec
    public static double speed = 2;
    public static double theta = -Math.PI/4;

    //pause the ball after death
    bool paused = false;
    //time in seconds to hold the ball in place
    double pauseTimer = 3;
    readonly double defaultPauseTimer = 3;

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
            int steps = (int)speed + 1;
            for (int step = 0; step != steps; step++)
            {
                positionX += (Math.Cos(theta) * speed) / steps;
                positionY += (Math.Sin(theta) * speed) / steps;

                if ((positionX <= 0) || (positionX + size >= Breakout.stageWidth))
                    theta = -(theta + -(Math.PI / 2)) + (Math.PI / 2);
                if (positionY <= 0)
                    theta = -(theta);
                else if (positionY >= Breakout.baseHeight)
                    ballOut();
                bounceBallOffBricks(world);
            }
        }else
        {
            pauseTimer -= (double)ms/1000;
            if (pauseTimer <= 0)
            {
                paused = false;
                pauseTimer = defaultPauseTimer;
            }
        }
    }

    void bounceBallOffBricks(Breakout world)
    {
        for(int i = 0; i < Bricks.bricksX; i++)
        {
            for (int j = 0; j < Bricks.bricksY; j++)
            {
                if ((world.bricks.brickLive[i, j])&& Breakout.collide((int)positionX, (int)positionY, size, size, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY, Bricks.width, Bricks.height))
                {
                    if ((topCollide(i, j)) || bottomCollide(i, j))
                        theta = -(theta);
                    else
                        theta = -(theta + -(Math.PI / 2)) + (Math.PI / 2);
                    world.bricks.brickLive[i, j] = false;
                    return;
                }
            }
        }
    }

    private bool topCollide(int i, int j)
    {
        return Math.Sin(theta) > 0 && Breakout.collide((int)positionX, (int)positionY + size +- 1, size, 1, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY, Bricks.width, 1);
    }

    private bool bottomCollide(int i, int j)
    {
        return Math.Sin(theta) < 0 && Breakout.collide((int)positionX, (int)positionY, size, 1, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY + Bricks.height +- 1, Bricks.width, 1);
    }

    void ballOut()
    {
        if (ballsRemaining > 0)
        {
            ballsRemaining--;

            paused = true;
            positionX = 45;
            positionY = 190;
            speed = 2;
            theta = -Math.PI / 4;
        }
        else
            dead = true;
            //Game Over!
    }
}
