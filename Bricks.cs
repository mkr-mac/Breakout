using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bricks
{
    //array of the state of the bricks
    bool[,] brickLive;
    //array storing the colors of the bricks
    Color[,] brickColor;

    //Number of bricks across
    int bricksX = 12;
    //number of bricks high
    int bricksY = 10;

    //Size of the bricks
    int width = 20;
    int height = 10;

    //extra spacing on the left side/top in the placement of bricks
    int spaceX = 5;
    int spaceY = 0;

    //animation set for the bricks
    AnimationSet brick;

    public Bricks()
    {
        brickLive = new bool[bricksX, bricksY];
        brickColor = new Color[bricksX, bricksY];

        for (int i = 0; i < bricksX; i++)
        {
            for (int j = 0; j < bricksY; j++)
            {
                brickLive[i, j] = true;
                brickColor[i, j] = Color.SteelBlue;
            }
        }

        brick = new AnimationSet(@"brick\brick.xml");
        brick.autoAnimate("shimmer", 0);
        brick.speed = 5;
    }

    public void draw(AD2SpriteBatch sb)
    {
        brick.update();
        for (int i = 0; i < bricksX; i++)
        {
            for (int j = 0; j < bricksY; j++)
            {
                if (brickLive[i, j])
                {
                    brick.draw(sb, (i * width) + spaceX, (j * height) + spaceY, brickColor[i,j]);
                }
            }
        }
    }

    public void update(Breakout world)
    {
        bool revX = false;
        bool revY = false;
        for (int i = 0; i < bricksX; i++)
        {
            for (int j = 0; j < bricksY; j++)
            {
                if ((brickLive[i, j])&&(Breakout.collide((int)Ball.positionX, (int)Ball.positionY, Ball.size, Ball.size, i*width, j*height, width, height)))
                {
                    if (Breakout.collideY((int)Ball.positionX, (int)Ball.positionY, Ball.size, Ball.size, i * width, j * height, width, height))
                    {
                        brickLive[i, j] = false;

                        if (!revX)
                        {
                            Ball.speedX = -(Ball.speedX);
                        }
                        revX = true;
                    }
                    //if (Breakout.collide((int)Balls.positionX, (int)Balls.positionY, Balls.size, Balls.size, i * width, j * height, width, height))
                    else{
                        brickLive[i, j] = false;

                        if (!revY)
                        {
                            //Balls.speedY = -(Balls.speedY);
                        }
                        revY = true;
                    }
                    SoundManager.engine.Play2D(@"sounds\hit.wav");
                }
            }
        }
    }
}
