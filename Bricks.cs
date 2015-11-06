using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bricks
{
    bool[,] brickLive;
    Color[,] brickColor;

    int bricksX = 12;
    int bricksY = 10;

    int width = 20;
    int height = 10;

    int spaceX = 5;
    int spaceY = 0;

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
                brickColor[i, j] = Color.Blue;
            }
        }

        brick = new AnimationSet(@"brick\brick.xml");
        brick.autoAnimate("shimmer", 0);
    }

    public void draw(AD2SpriteBatch sb)
    {
        for (int i = 0; i < bricksX; i++)
        {
            for (int j = 0; j < bricksY; j++)
            {
                if (brickLive[i, j])
                {
                    brick.draw(sb, (i * width) + spaceX, (j * height) + spaceY);
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
                if ((brickLive[i, j])&&(Breakout.collide((int)Balls.positionX, (int)Balls.positionY, Balls.size, Balls.size, i*width, j*height, width, height)))
                {
                    if (Breakout.collideY((int)Balls.positionX, (int)Balls.positionY, Balls.size, Balls.size, i * width, j * height, width, height))
                    {
                        brickLive[i, j] = false;

                        if (!revX)
                        {
                            Balls.speedX = -(Balls.speedX);
                        }
                        revX = true;
                    }
                    //if (Breakout.collide((int)Balls.positionX, (int)Balls.positionY, Balls.size, Balls.size, i * width, j * height, width, height))
                    else{
                        brickLive[i, j] = false;

                        if (!revY)
                        {
                            Balls.speedY = -(Balls.speedY);
                        }
                        revY = true;
                    }
                    SoundManager.engine.Play2D(@"sounds\hit.wav");
                }
            }
        }
    }
}
