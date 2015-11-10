using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Paddle
{
    double positionX = 0;
    readonly int positionY = 200;

    int width = 41;
    int height = 10;

    double speed = 2;

    Texture2D background;
    Texture2D paddle;

    bool noStick = false;

    public Paddle()
    {

        background = Utils.TextureLoader(@"vaporsky.png");
        paddle = Utils.TextureLoader(@"paddleNormal.png");
    }

    public void draw(AD2SpriteBatch sb)
    {
        sb.drawTexture(background, 0, 0);
        sb.drawTexture(paddle, (int)positionX, (int)positionY);
    }

    public void update(Breakout world, Microsoft.Xna.Framework.Input.KeyboardState ks)
    {
        if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) && (positionX > 0))
        {
            positionX -= speed;
            if (positionX < 0)
                positionX = 0;
        }
        else if ((ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))&& (positionX < Breakout.stageWidth - width))
        {
            positionX += speed;
            if (positionX > Breakout.stageWidth - width)
                positionX = Breakout.stageWidth - width;
        }
        
        if ((Breakout.collide((int)Ball.positionX, (int)Ball.positionY, Ball.size, Ball.size, (int)positionX, positionY, width, height)))
        {
            if(!noStick)
                Ball.speedY = -(Ball.speedY);
            noStick = true;
        }else
            noStick = false;
        
    }
}
