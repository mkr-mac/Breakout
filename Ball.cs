using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Ball
{
    //Starting position of the ball
    double PositionX = 45;
    double PositionY = 190;
    //size of the ball (diameter)
    static int Size = 7;

    //speed in pixels/sec
    double Speed = 2;
    //angle of the ball
    double Theta = -Math.PI/4;

    //pause the ball after death
    bool Paused = true;
    //time in seconds to hold the ball in place
    double PauseTimer = 3;
    
    //game over condition
    static bool Dead = false;

    static int BallsLeft = 90000000;

    Texture2D BallTexture;

    double MaxReflect = Math.PI;

    public Ball()
    {
        BallTexture = Utils.TextureLoader("ball.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        if (!Dead)
            sb.DrawTexture(BallTexture, (int)PositionX, (int)PositionY);
        else
            Utils.DefaultFont.draw(sb, "Game Over", 40, 40, Color.Red, 4, true);

        Utils.DefaultFont.draw(sb, BallsLeft.ToString(), 250, 30, Color.White);
    }

    public void Update(Breakout world, int ms)
    {
        if (!Paused)
        {
            //Break movements into small steps for edge collision
            int steps = (int)Speed + 1;
            for (int step = 0; step != steps; step++)
            {
                
                //Move the ball based on the angle
                PositionX += (Math.Cos(Theta) * Speed) / steps;
                PositionY += (Math.Sin(Theta) * Speed) / steps;
                
                //check for collision with the world
                WorldCollide(world);
                //check for collision with the paddle
                PaddleCollide(world);
                //Check for collision with bricks
                BounceBallOffBricks(world);
            }
        }else //if the ball is being held after death
        {
            //roll the pauseTimer 
            PauseTimer -= (double)ms/1000;
            if (PauseTimer <= 0)
            {
                //The ball is released
                Paused = false;
            }
        }
    }

    private void WorldCollide(Breakout world)
    {
        //check for vertical world collision
        if ((PositionX <= 0) || (PositionX + Size >= Breakout.StageWidth))
            //reflect the x direction
            FlipThetaX();

        //check for horizontal world collision
        if (PositionY <= 0)
            //reflect the y direction
            FlipThetaY();
        else if (PositionY > Breakout.BaseHeight)
            //if the ball is off the borrom, then it dies
            BallOut(world);
    }

    private void FlipThetaX()
    {
        Theta = -(Theta + -(Math.PI / 2)) + (Math.PI / 2);
    }

    private void FlipThetaY()
    {
        Theta = -(Theta);
    }

    void BounceBallOffBricks(Breakout world)
    {
        for(int i = 0; i < Bricks.BricksX; i++)
        {
            for (int j = 0; j < Bricks.BricksY; j++)
            {
                if ((world.Bricks.BrickLive[i, j])&& Breakout.Collide((int)PositionX, (int)PositionY, Size, Size, i * Bricks.Width + Bricks.SpaceX, j * Bricks.Height + Bricks.SpaceY, Bricks.Width, Bricks.Height))
                {
                    if ((TopCollide(i, j)) || BottomCollide(i, j))
                        FlipThetaY();
                    else
                        FlipThetaX();
                    world.Bricks.BrickLive[i, j] = false;
                    SoundManager.Engine.Play2D(@"sounds\hit.wav");
                    return;
                }
            }
        }
    }

    private bool TopCollide(int i, int j)
    {
        return Math.Sin(Theta) > 0 && Breakout.Collide((int)PositionX, (int)PositionY + Size +- 1, Size, 1, i * Bricks.Width + Bricks.SpaceX, j * Bricks.Height + Bricks.SpaceY, Bricks.Width, 1);
    }

    private bool BottomCollide(int i, int j)
    {
        return Math.Sin(Theta) < 0 && Breakout.Collide((int)PositionX, (int)PositionY, Size, 1, i * Bricks.Width + Bricks.SpaceX, j * Bricks.Height + Bricks.SpaceY + Bricks.Height +- 1, Bricks.Width, 1);
    }

    public void PaddleCollide(Breakout world)
    {
        if (Breakout.Collide((int)PositionX, (int)PositionY + Size - 1, Size, 1, (int)world.Player.x, world.Player.y, world.Player.width, 1) && Math.Sin(Theta) > 0)
        {
            Theta = (((PositionX + (Size/2) +- world.Player.x) / (world.Player.width)) +- 1) * MaxReflect;
        }
    }

    void BallOut(Breakout world)
    {
        //Remove a ball from the list
        world.OutOfBounds.AddLast(this);

        if (BallsLeft > 0)
        {
            //Number of balls avaliable is lessened
            BallsLeft--;
        }
        else
            Dead = true;
            //Game Over!
    }
}