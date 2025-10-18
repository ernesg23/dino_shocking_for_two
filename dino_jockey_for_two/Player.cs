using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

public class Player
{
    private Animation walkAnimation;
    private Animation jumpAnimation;

    public AnimatedSprite Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public float SpeedY { get; private set; }
    public bool InFloor { get; private set; }
    public Keys JumpKey { get; private set; }

    private float gravity = 0.5f;
    private float impulseJump = -10f;
    private float floorY;

    public Player(Animation walkAnim, Animation jumpAnim, Vector2 posicionInicial, Keys teclaSalto, float floorY)
    {
        walkAnimation = walkAnim;
        jumpAnimation = jumpAnim;

        Sprite = new AnimatedSprite(walkAnimation);
        Position = posicionInicial;
        JumpKey = teclaSalto;
        InFloor = true;
        SpeedY = 0;
        this.floorY = floorY;
    }


    public void Update(GameTime gameTime, KeyboardState keyboard)
    {
        if (InFloor && Sprite.Animation != walkAnimation)
            Sprite.Animation = walkAnimation;
        else if (!InFloor && Sprite.Animation != jumpAnimation)
            Sprite.Animation = jumpAnimation;

        Sprite.Update(gameTime);

        if (keyboard.IsKeyDown(JumpKey) && InFloor)
        {
            SpeedY = impulseJump;
            InFloor = false;
        }

        if (!InFloor && keyboard.IsKeyDown(JumpKey) && SpeedY < 0)
            SpeedY += gravity * 0.6f;
        else
            SpeedY += gravity;

        Position = new Vector2(Position.X, Position.Y + SpeedY);

        if (Position.Y >= floorY)
        {
            Position = new Vector2(Position.X, floorY);
            SpeedY = 0;
            InFloor = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Position);
    }

    public Rectangle GetRect()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Width, (int)Sprite.Height);
    }
}
