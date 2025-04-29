using SDL2;

class Program
{
    const int ScreenWidth = 800;
    const int ScreenHeight = 600;

    static void Main(string[] args)
    {
        using (var windowManager = new WindowManager(ScreenWidth, ScreenHeight))
        {
            bool running = true;
            SDL.SDL_Event e;
            float angle = 0.0f;

            // Создаем камеру
            Camera camera = new Camera(new Vec3(0, 0, -2), 45);

            // Вершины куба
            List<Vec3> cubeVertices = new List<Vec3>
            {
                new Vec3(-0.5f, -0.5f, -0.5f),
                new Vec3(0.5f, -0.5f, -0.5f),
                new Vec3(0.5f, 0.5f, -0.5f),
                new Vec3(-0.5f, 0.5f, -0.5f),
                new Vec3(-0.5f, -0.5f, 0.5f),
                new Vec3(0.5f, -0.5f, 0.5f),
                new Vec3(0.5f, 0.5f, 0.5f),
                new Vec3(-0.5f, 0.5f, 0.5f)
            };

            // Ребра куба
            List<(int, int)> edges = new List<(int, int)>
            {
                (0,1), (1,2), (2,3), (3,0),
                (4,5), (5,6), (6,7), (7,4),
                (0,4), (1,5), (2,6), (3,7)
            };

            while (running)
            {
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                        running = false;

                    // Управление камерой
                    if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        float moveSpeed = 0.1f;
                        switch (e.key.keysym.sym)
                        {
                            case SDL.SDL_Keycode.SDLK_w:
                                camera.Position.Z += moveSpeed;
                                break;
                            case SDL.SDL_Keycode.SDLK_s:
                                camera.Position.Z -= moveSpeed;
                                break;
                            case SDL.SDL_Keycode.SDLK_a:
                                camera.Position.X -= moveSpeed;
                                break;
                            case SDL.SDL_Keycode.SDLK_d:
                                camera.Position.X += moveSpeed;
                                break;
                            case SDL.SDL_Keycode.SDLK_q:
                                camera.Position.Y += moveSpeed;
                                break;
                            case SDL.SDL_Keycode.SDLK_e:
                                camera.Position.Y -= moveSpeed;
                                break;
                        }
                    }
                }

                windowManager.ClearBackground();
                IntPtr renderer = windowManager.GetRenderer();
                SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

                foreach (var edge in edges)
                {
                    // Поворачиваем точки в мировом пространстве
                    Vec3 worldA = cubeVertices[edge.Item1].Rotate('x', angle).Rotate('y', angle * 0.7f);
                    Vec3 worldB = cubeVertices[edge.Item2].Rotate('x', angle).Rotate('y', angle * 0.7f);

                    // Переводим в пространство камеры
                    Vec3 viewA = camera.WorldToViewSpace(worldA);
                    Vec3 viewB = camera.WorldToViewSpace(worldB);

                    // Проекция на 2D
                    Vec2 screenA = Core.Vec3toVec2(viewA, camera);
                    Vec2 screenB = Core.Vec3toVec2(viewB, camera);

                    if (screenA == null || screenB == null)
                        continue;

                    // Преобразование в экранные координаты
                    Vec2 sdlA = Core.ScreenSpaceToSDLCoords(screenA, ScreenWidth, ScreenHeight);
                    Vec2 sdlB = Core.ScreenSpaceToSDLCoords(screenB, ScreenWidth, ScreenHeight);

                    SDL.SDL_RenderDrawLine(renderer, (int)sdlA.X, (int)sdlA.Y, (int)sdlB.X, (int)sdlB.Y);
                }

                windowManager.Present();
                angle += 0.01f;
                SDL.SDL_Delay(16);
            }
        }
    }
}