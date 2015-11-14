using Microsoft.Xna.Framework;

public class Bricks
{
    //Boolean array of the state of the bricks.
    public bool[,] BrickLive;
    //Array of the colors of the bricks.
    Color[,] BrickColor;

    //Number of bricks across the screen.
    public static int BricksX = 12;
    //Number of bricks high.
    public static int BricksY = 10;

    //Size of the bricks.
    public static int Width = 20;
    public static int Height = 10;

    //Extra spacing on the left side/top in the placement of bricks.
    public static int SpaceX = 5;
    public static int SpaceY = 20;

    //Animation set for the bricks.
    AnimationSet BrickAnimation;
    //Speed of the animation (larger number = slower).
    int AnimationSpeed = 5;

    public Bricks()
    {
        BrickLive = new bool[BricksX, BricksY];
        BrickColor = new Color[BricksX, BricksY];

        for (int i = 0; i < BricksX; i++)
        {
            for (int j = 0; j < BricksY; j++)
            {
                BrickLive[i, j] = true;
                BrickColor[i, j] = Color.PaleGoldenrod;
            }
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
        for (int i = 0; i < BricksX; i++)
        {
            for (int j = 0; j < BricksY; j++)
            {
                if (BrickLive[i, j])
                {
                    BrickAnimation.Draw(sb, (i * Width) + SpaceX, (j * Height) + SpaceY, BrickColor[i,j]);
                }
            }
        }
    }

    public void Update(Breakout world)
    {
        //Bricks are kinda boring
    }
}
