using System;
using Raylib_cs;
using ImGuiNET;

namespace Geostorm
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            const int screenWidth = 1280;
            const int screenHeight = 720;

            // Initialization
            //--------------------------------------------------------------------------------------
            Raylib.SetTraceLogCallback(&Logging.LogConsole);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.InitWindow(screenWidth, screenHeight, "ImGui demo");
            Raylib.SetTargetFPS(60);

            Raylib.InitAudioDevice();

            // ImGUI
            ImguiController controller = new ImguiController();
            // Game
            Core.GameInputs inputs = new Core.GameInputs(new System.Numerics.Vector2(screenWidth, screenHeight));
            Core.Game game = new Core.Game(inputs);
            Renderer.Graphics graphics = new Renderer.Graphics();
            game.AddEventListener(graphics);
            graphics.allAnimation.Add(new Renderer.NewLevelAnimation());

            controller.Load(screenWidth, screenHeight);
            graphics.Load();
            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!Raylib.WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                float dt = Raylib.GetFrameTime();

                // Feed the input events to our ImGui controller, which passes them through to ImGui.
                controller.Update(dt);

                // Game
                inputs.Update();
                inputs.deltaTime = Raylib.GetFrameTime();
                game.Update(inputs);
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                //ImGui.Begin("Debug");
                //ImGui.SliderAngle("Player rotation", ref inputs.rotation);
                //ImGui.SliderFloat2("Player pos", ref inputs.moveAxis, 0, 1280);
                //ImGui.End();

                controller.Draw();

                // Game
                game.Render(graphics);

                Raylib.EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            controller.Dispose();
            graphics.Unload();

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
