using System;
using SDL2;


public class WindowManager : IDisposable
{
    private readonly IntPtr _window;
    private readonly IntPtr _renderer;

    public WindowManager(int width, int height)
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
        {
            throw new Exception($"Ошибка инициализации SDL: {SDL.SDL_GetError()}");
        }

        _window = SDL.SDL_CreateWindow(
            "Triangle Example",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            width,
            height,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (_window == IntPtr.Zero)
        {
            throw new Exception($"Ошибка создания окна: {SDL.SDL_GetError()}");
        }

        _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (_renderer == IntPtr.Zero)
        {
            throw new Exception($"Ошибка создания рендерера: {SDL.SDL_GetError()}");
        }
    }

    public IntPtr GetRenderer()
    {
        return _renderer;
    }

    public void ClearBackground()
    {
        SDL.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 255); 
        SDL.SDL_RenderClear(_renderer);
    }

    public void Present()
    {
        SDL.SDL_RenderPresent(_renderer);
    }

    public void Dispose()
    {
        if (_renderer != IntPtr.Zero)
        {
            SDL.SDL_DestroyRenderer(_renderer);
        }

        if (_window != IntPtr.Zero)
        {
            SDL.SDL_DestroyWindow(_window);
        }

        SDL.SDL_Quit();
    }
}
