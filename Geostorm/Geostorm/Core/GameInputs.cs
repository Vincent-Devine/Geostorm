using System.Numerics;
using Raylib_cs;

namespace Geostorm.Core
{
    public class GameInputs
    {
        public Vector2 screenSize;
        public float deltaTime;

        public Vector2 moveAxis;

        public bool shoot;
        public Vector2 shootTarget;

        public bool buyUpgradeWeapon;

        public GameInputs(Vector2 screenSize)
        {
            this.screenSize = screenSize;
            deltaTime = 0f;
            moveAxis = new Vector2();
            shoot = false;
            shootTarget = new Vector2();
            buyUpgradeWeapon = false;
        }

        public void Update()
        {
            // Player
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                moveAxis.Y--;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                moveAxis.Y++;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                moveAxis.X++;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                moveAxis.X--;

            // Weapon
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                shoot = true;

            shootTarget.X = Raylib.GetMouseX();
            shootTarget.Y = Raylib.GetMouseY();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                buyUpgradeWeapon = true;
        }

        public void Initialise()
        {
            moveAxis.X = 0f;
            moveAxis.Y = 0f;
            shoot = false;
            buyUpgradeWeapon = false;
        }
    }
}
