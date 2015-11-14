using Microsoft.Xna.Framework.Input;

public class Breakout : AD2Game
{
    //The title.
    public Title T;
    //The ingame Class that controls the gameplay.
    public InGame Game;

    //The states the game can be in.
    public enum State { Title, InGame }
    State GameState;

    //Designates the speed the game runs at (1/gameSpeed = FPS).
    public static readonly int GameSpeed = 16;
    //Game Dimensions.
    public static readonly int BaseWidth = 320;
    public static readonly int BaseHeight = 224;
    public static readonly int StageWidth = 250;


    public Breakout() : base(BaseWidth, BaseHeight, GameSpeed)
    {
        Renderer.Resolution = Renderer.ResolutionType.WindowedLarge;
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {

        //Kill the program if escape is pressed.
        if (keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        State newState;
        //See what state the game is in.
        switch (GameState)
        {
            //Title screen!
            case State.Title:
                newState = T.Update(keyboardState);
                //If the title screen says move on to the game,
                if (newState == State.InGame)
                {
                    //Kill title screen sounds,
                    SoundManager.Engine.StopAllSounds();
                    //and let the games begin!
                    GameState = State.InGame;
                    Game = new InGame();
                }
                break;

            //Playing the Game!
            case State.InGame:
                newState = Game.Update(ms, keyboardState);
                //Check to see if the Game's state has returned to the title screen
                if (newState == State.Title)
                {
                    //Kill the game's sounds.
                    SoundManager.Engine.StopAllSounds();
                    //Go back to the title screen.
                    GameState = State.Title;
                    T = new Title();
                }
                break;
        }
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        switch (GameState)
        {
            //Draw appropriate graphics.
            case State.Title:
                T.Draw(primarySpriteBatch);
                break;
            case State.InGame:
                Game.Draw(primarySpriteBatch);
                break;
        }
    }

    protected override void AD2LoadContent()
    {
        //The State starts in the title.
        GameState = State.Title;
        //Load the title.
        T = new Title();
    }
}

