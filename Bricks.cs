using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Bricks
{
    //array of the state of the bricks
    public bool[,] brickLive;
    //array storing the colors of the bricks
    Color[,] brickColor;

    //Number of bricks across
    public static int bricksX = 12;
    //number of bricks high
    public static int bricksY = 10;

    //Size of the bricks
    public static int width = 20;
    public static int height = 10;

    //extra spacing on the left side/top in the placement of bricks
    public static int spaceX = 5;
    public static int spaceY = 0;

    //animation set for the bricks
    AnimationSet brick;
    //speed of the animation (larger number = slower)
    int animationSpeed = 5;

    public Bricks()
    {
        brickLive = new bool[bricksX, bricksY];
        brickColor = new Color[bricksX, bricksY];

        for (int i = 8; i < bricksX; i++)
        {
            for (int j = 5; j < bricksY; j++)
            {
                brickLive[i, j] = true;
                brickColor[i, j] = Color.PaleGoldenrod;
            }
        }

        brick = new AnimationSet(@"brick\brick.xml");
        brick.autoAnimate("shimmer", 0);
        brick.speed = animationSpeed;
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
        //Bricks are kinda boring
    }
}
