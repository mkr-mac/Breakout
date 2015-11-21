using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class Brick
{
    //Boolean array of the state of the bricks.
    public bool BrickLive = true;
    //Array of the colors of the bricks.
    Color BrickColor;

    public int Position;
    public int PositionX;
    public int PositionY;

    //Size of the bricks.
    public int Width = 20;
    public int Height = 10;
    
    //Extra spacing on the left side/top in the placement of bricks.
    //public static int SpaceX = 5;
    //public static int SpaceY = 20;

    //Animation set for the bricks.
    AnimationSet BrickAnimation;
    //Speed of the animation (larger number = slower).
    int AnimationSpeed = 5;

    public Brick(int i)
    {
        string[] d = InGame.Level["brick" + i.ToString()].First.ToString().Split(',');
            PositionX = int.Parse(d[0]);
            PositionY = int.Parse(d[1]);
        BrickColor = Color.PaleGoldenrod;
        BrickAnimation = new AnimationSet(@"brick\brick.xml");
        BrickAnimation.AutoAnimate("shimmer", 0);
        BrickAnimation.Speed = AnimationSpeed;
    }

    public void Draw(AD2SpriteBatch sb)
    {
        //Update the brick animation.
        BrickAnimation.Update();
        //Now go through all the bricks and draw those that are alive.
        if (BrickLive)
            BrickAnimation.Draw(sb, PositionX, PositionY, BrickColor);
    }

    public void Update(InGame world)
    {
    }
}
