using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Ball
{
    //Starting position of the ball
    double x = 45;
    double y = 190;
    //size of the ball (diameter)
    static int size = 7;

    //speed in pixels/sec
    double speed = 2;
    //angle of the ball
    double theta = -Math.PI/4;

    //pause the ball after death
    bool paused = true;
    //time in seconds to hold the ball in place
    double pauseTimer = 3;
    
    //game over condition
    bool dead = false;

    Texture2D ball;

    public Ball()
    {
        ball = Utils.TextureLoader("ball.png");
    }

    public void draw(AD2SpriteBatch sb)
    {
        if (!dead)
            sb.drawTexture(ball, (int)x, (int)y);
        else
            Utils.defaultFont.draw(sb, "Game Over", 40, 40, Color.Red, 4, true);
    }

    public void update(Breakout world, int ms)
    {
        if (!paused)
        {
            //Break movements into small steps for edge collision
            int steps = (int)speed + 1;
            for (int step = 0; step != steps; step++)
            {
                
                //Move the ball based on the angle
                x += (Math.Cos(theta) * speed) / steps;
                y += (Math.Sin(theta) * speed) / steps;
                
                //check for collision with the world
                worldCollide(world);
                //check for collision with the paddle
                paddleCollide(world);
                //Check for collision with bricks
                bounceBallOffBricks(world);
            }
        }else //if the ball is being held after death
        {
            //roll the pauseTimer 
            pauseTimer -= (double)ms/1000;
            if (pauseTimer <= 0)
            {
                //The ball is released
                paused = false;
            }
        }
    }

    private void worldCollide(Breakout world)
    {
        //check for vertical world collision
        if ((x <= 0) || (x + size >= Breakout.stageWidth))
            //reflect the x direction
            flipThetaX();

        //check for horizontal world collision
        if (y <= 0)
            //reflect the y direction
            flipThetaY();
        else if (y >= Breakout.baseHeight)
            //if the ball is off the borrom, then it dies
            ballOut(world);
    }

    private void flipThetaX()
    {
        theta = -(theta + -(Math.PI / 2)) + (Math.PI / 2);
    }

    private void flipThetaY()
    {
        theta = -(theta);
    }

    void bounceBallOffBricks(Breakout world)
    {
        for(int i = 0; i < Bricks.bricksX; i++)
        {
            for (int j = 0; j < Bricks.bricksY; j++)
            {
                if ((world.bricks.brickLive[i, j])&& Breakout.collide((int)x, (int)y, size, size, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY, Bricks.width, Bricks.height))
                {
                    if ((topCollide(i, j)) || bottomCollide(i, j))
                        flipThetaY();
                    else
                        flipThetaX();
                    world.bricks.brickLive[i, j] = false;
                    SoundManager.engine.Play2D(@"sounds\hit.wav");
                    return;
                }
            }
        }
    }

    private bool topCollide(int i, int j)
    {
        return Math.Sin(theta) > 0 && Breakout.collide((int)x, (int)y + size +- 1, size, 1, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY, Bricks.width, 1);
    }

    private bool bottomCollide(int i, int j)
    {
        return Math.Sin(theta) < 0 && Breakout.collide((int)x, (int)y, size, 1, i * Bricks.width + Bricks.spaceX, j * Bricks.height + Bricks.spaceY + Bricks.height +- 1, Bricks.width, 1);
    }

    public void paddleCollide(Breakout world)
    {
        if (Breakout.collide((int)x, (int)y + size - 1, size, 1, (int)world.player.x, world.player.y, world.player.width, 1) && Math.Sin(theta) > 0)
        {
            flipThetaY();
        }
    }

    void ballOut(Breakout world)
    {
        if (world.player.livesLeft >= 0)
        {
            //Number of balls avaliable is lessened
            world.player.livesLeft--;

            //Remove a ball from the list
            world.outOfBounds.AddLast(this);
        }
        else
            dead = true;
            //Game Over!
    }
}