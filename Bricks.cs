using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class Brick
{
    //Boolean array of the state of the bricks.
    public bool BrickLive = true;
    //Array of the colors of the bricks.
    Color BrickColor;
    
    public int PositionX;
    public int PositionY;
    public string BrickType;

    //Size of the bricks.
    public int Width = 20;
    public int Height = 10;

    //Animation set for the bricks.
    AnimationSet BrickAnimation;
    //Speed of the animation (larger number = slower).
    int AnimationSpeed = 5;

    public Brick(string brickData)
    {
        string[] a = brickData.Split(',');

        PositionX = int.Parse(a[0]);
        PositionY = int.Parse(a[1]);
        BrickType = a[2];

        switch (BrickType)
        {
            case ("normal"):
            default:
                BrickColor = Color.Khaki;
                break;

            case ("blue"):
                BrickColor = Color.CornflowerBlue;
                break;
        }

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
