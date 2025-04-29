using System;
using System.ComponentModel;
using System.Drawing;

using System;
using System.Drawing;

public class Camera
{
    public Vec3 Position;  // Просто поле, без readonly
    public Vec3 Rotation;  // Просто поле, без readonly
    public int FOV { get; set; }
    public float NearClip { get; set; } = 0.1f;
    public float FarClip { get; set; } = 100.0f;

    public Camera(Vec3 position, int fov = 60)
    {
        Position = position;
        FOV = fov;
        Rotation = new Vec3(0, 0, 0);
    }

    public Vec3 WorldToViewSpace(Vec3 point)
    {
        // 1. Переводим точку в пространство камеры (учитываем позицию камеры)
        Vec3 translated = point - Position;

        // 2. Применяем вращение камеры (обратное вращение)
        translated = translated.Rotate('x', -Rotation.X)
                              .Rotate('y', -Rotation.Y)
                              .Rotate('z', -Rotation.Z);

        return translated;
    }
}

public static class Core
{
    public static Vec2 Vec3toVec2(Vec3 pointInViewSpace, Camera camera)
    {
        if (pointInViewSpace.Z <= camera.NearClip || pointInViewSpace.Z >= camera.FarClip)
            return null;

        float angleRadians = camera.FOV * MathF.PI / 180.0f;
        float tanHalfFov = MathF.Tan(angleRadians / 2);

        float xProj = pointInViewSpace.X / (pointInViewSpace.Z * tanHalfFov);
        float yProj = pointInViewSpace.Y / (pointInViewSpace.Z * tanHalfFov);

        // Клиппинг по границам экрана
        if (Math.Abs(xProj) > 2.0f || Math.Abs(yProj) > 2.0f)
            return null;

        float xNormalized = (xProj + 1.0f) / 2.0f;
        float yNormalized = (1.0f - yProj) / 2.0f;

        return new Vec2(xNormalized, yNormalized);
    }

    public static Vec3 Rotate(this Vec3 vector3, char axis, float angle)
    {
        // Rotation of 3D Vector3 around a given axis.
        // Using: Rotate(Vector3[x,y,z], 'x' or 'y' or 'z', angle)

        float cosAngle = MathF.Cos(angle);
        float sinAngle = MathF.Sin(angle);

        float[,] rotationMatrixX = new float[,]
        {
            {1, 0, 0},
            {0, cosAngle, -sinAngle},
            {0, sinAngle, cosAngle}
        };

        float[,] rotationMatrixY = new float[,]
        {
            {cosAngle, 0, sinAngle},
            {0, 1, 0},
            {-sinAngle, 0, cosAngle}
        };

        float[,] rotationMatrixZ = new float[,]
        {
            {cosAngle, -sinAngle, 0},
            {sinAngle, cosAngle, 0},
            {0, 0, 1}
        };

        float[] vecArray = { vector3.X, vector3.Y, vector3.Z };
        float[] result;

        switch (axis)
        {
            case 'x':
                result = MultiplyMatrixWithVector(rotationMatrixX, vecArray);
                break;
            case 'y':
                result = MultiplyMatrixWithVector(rotationMatrixY, vecArray);
                break;
            case 'z':
                result = MultiplyMatrixWithVector(rotationMatrixZ, vecArray);
                break;
            default:
                throw new ArgumentException("Invalid axis. Must be 'x', 'y', or 'z'.");
        }

        return new Vec3(result[0], result[1], result[2]);
    }

    private static float[] MultiplyMatrixWithVector(float[,] matrix, float[] vector)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        float[] result = new float[rows];

        for (int i = 0; i < rows; i++)
        {
            float sum = 0;
            for (int j = 0; j < cols; j++)
            {
                sum += matrix[i, j] * vector[j];
            }
            result[i] = sum;
        }
        return result;
    }

    public static Vec2 ScreenSpaceToSDLCoords(Vec2 vector2, int screenWidth, int screenHeight)
    {
        // Screen Space to Game Coordinates
        // Like [-1,1] to [600,400]

        return new Vec2(
            (int)(vector2.X * screenWidth),
            (int)(vector2.Y * screenHeight)
        );
    }

    public static Vec3 Translate3D(Vec3 vector3, Vec3 cortVec3)
    {
        // Translate the 3D vector3 around the cartesian origin
        // Note: This is a simplified version that just adds the vectors
        return new Vec3(
            vector3.X + cortVec3.X,
            vector3.Y + cortVec3.Y,
            vector3.Z + cortVec3.Z
        );
    }
}