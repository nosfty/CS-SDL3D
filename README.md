# CS-SDL3D
## 3D Cube Renderer with SDL2

This is a simple program for rendering a 3D cube using the SDL2 library in C#. The project demonstrates the basics of 3D graphics, including coordinate transformations, projection, and camera control.

## Features

- Rendering of a rotating 3D cube using SDL2
- Simple movable camera
- Mathematics for 3D transformations (rotation, translation, projection)
- Vector math basics (Vec2, Vec3)

## Controls

- **W/S**: Move camera forward/backward (Z-axis)
- **A/D**: Move camera left/right (X-axis)
- **Q/E**: Move camera up/down (Y-axis)
- **Close window**: Quit the program

## API Overview

### Core Classes

1. **WindowManager** - manages SDL2 window and renderer
   - `GetRenderer()` - gets the SDL renderer
   - `ClearBackground()` - clears the screen
   - `Present()` - displays rendered content

2. **Vec2/Vec3** - classes for 2D and 3D vector operations
   - Basic operations (+, -, *, /)
   - Normalization, dot product
   - Rotation (Vec3 only)

3. **Camera** - simple 3D camera
   - `WorldToViewSpace()` - transforms world coordinates to camera space
   - Configurable FOV, near and far clipping planes

4. **Core** - utility functions
   - `Vec3toVec2()` - projects 3D point to 2D screen
   - `Rotate()` - rotates vector around axis
   - `ScreenSpaceToSDLCoords()` - converts normalized coordinates to pixel coordinates

## Building and Running

1. Ensure you have .NET SDK installed (version 5.0 or higher)
2. Install SDL2 dependencies (via NuGet or manually)
3. Build the project: `dotnet build`
4. Run: `dotnet run`

## How to Contribute

All contributions are welcome! Here are some ideas:

1. **Graphics Improvements**:
   - Add lighting
   - Implement filled polygons instead of wireframe
   - Add texturing

2. **New Features**:
   - Load 3D models from files
   - More advanced camera controls (free flight)
   - Mouse camera control

3. **Optimizations**:
   - Improve math operations
   - Add backface culling
   - More efficient rendering

### Contribution Process

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to your fork (`git push origin feature/amazing-feature`)
5. Open a Pull Request

###Gallery

![image](https://github.com/user-attachments/assets/b8b276bb-c406-4557-ab44-5554ff99bd11)


## License

This project is licensed under the MIT License. See the LICENSE file for details.
