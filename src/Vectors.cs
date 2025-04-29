
    using System;
    using System.Collections.Generic;

    // Two-dimensional vector class
    public sealed class Vec2
    {

        public readonly float X;
        public readonly float Y;

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }


        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }


        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }


        public static Vec2 operator *(Vec2 a, float scalar)
        {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }


        public static Vec2 operator /(Vec2 a, float scalar)
        {
            if (scalar == 0f)
                throw new DivideByZeroException("Cannot divide by zero.");

            return new Vec2(a.X / scalar, a.Y / scalar);
        }


        public float Dot(Vec2 other)
        {
            return X * other.X + Y * other.Y;
        }


        public float Magnitude()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }


        public Vec2 Normalized()
        {
            var magnitude = Magnitude();
            return magnitude > 0 ? this / magnitude : new Vec2(0, 0);
        }


        public List<float> ToList()
        {
            return new List<float>() { X, Y };
        }


        public override string ToString()
        {
            return $"Vec2({X}, {Y})";
        }
    }


    public sealed class Vec3
    {

        public float X;
        public float Y;
        public float Z;


        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }


        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }


        public static Vec3 operator *(Vec3 a, float scalar)
        {
            return new Vec3(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }


        public static Vec3 operator /(Vec3 a, float scalar)
        {
            if (scalar == 0f)
                throw new DivideByZeroException("Cannot divide by zero.");

            return new Vec3(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }


        public float Dot(Vec3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }


        public float Magnitude()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }


        public Vec3 Normalized()
        {
            var magnitude = Magnitude();
            return magnitude > 0 ? this / magnitude : new Vec3(0, 0, 0);
        }


        public List<float> ToList()
        {
            return new List<float>() { X, Y, Z };
        }


        public override string ToString()
        {
            return $"Vec3({X}, {Y}, {Z})";
        }
    }
