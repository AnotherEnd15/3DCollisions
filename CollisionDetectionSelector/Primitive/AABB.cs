﻿using System;
using OpenTK.Graphics.OpenGL;
using Math_Implementation;

namespace CollisionDetectionSelector.Primitive {
    class AABB {
        public Point Min = new Point();
        public Point Max = new Point();

        public Point Center {
            get {
                return new Point(Min.X+(Max.X - Min.X) / 2f, Min.Y+(Max.Y - Min.Y) / 2f, Min.Z+(Max.Z - Min.Z) / 2f);
            }
        }
        public Vector3 Extents {
            get {
                return new Vector3((Max.X - Center.X), (Max.Y - Center.Y), (Max.Z - Center.Z));
            }
        }
        public bool IsValid {
            get {
                return (Min.X < Max.X && Min.Y < Max.Y && Min.Z < Max.Z);
            }
        }
        public void Fix() {
            //store old Min
            Point _max = new Point(Max);
            //change new "Min" to previous max
           if (Min.X > Max.X) {
                Max.X = Min.X;
                Min.X = _max.X;
            }
           if (Min.Y > Max.Y) {
                Max.Y = Min.Y;
                Min.Y = _max.Y;
            }
           if (Min.Z > Max.Z) {
                Max.Z = Min.Z;
                Min.Z = _max.Z;
            }
        }
        public AABB() {
            //make unit AABB
            //min=-1, max=+1 on all axiis
            Min = new Point(-1, -1, -1);
            Max = new Point(1, 1, 1);
        }
        public AABB(Point min, Point max) {
            //set min/max
            Min = min;
            Max = max;
            //can be invalid and might need fix
            if (!IsValid) {
                Fix();
            }
        }
        public AABB(Point center,Vector3 extents) {
            //min == center - extents, max == c+e
            Min.X = center.X - extents.X;
            Min.Y = center.Y - extents.Y;
            Min.Z = center.Z - extents.Z;

            Max.X = center.X + extents.X;
            Max.Y = center.Y + extents.Y;
            Max.Z = center.Z + extents.Z;
            if (!IsValid) {
                Fix();
            }
        }

        
        public void Render() {
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex3(Min.X, Min.Y, Max.Z);
            GL.Vertex3(Max.X, Min.Y, Max.Z);
            GL.Vertex3(Max.X, Max.Y, Max.Z);
            GL.Vertex3(Min.X, Max.Y, Max.Z);

            GL.Vertex3(Max.X, Min.Y, Max.Z);
            GL.Vertex3(Max.X, Min.Y, Min.Z);
            GL.Vertex3(Max.X, Max.Y, Min.Z);
            GL.Vertex3(Max.X, Max.Y, Max.Z);

            GL.Vertex3(Min.X, Max.Y, Max.Z);
            GL.Vertex3(Max.X, Max.Y, Max.Z);
            GL.Vertex3(Max.X, Max.Y, Min.Z);
            GL.Vertex3(Min.X, Max.Y, Min.Z);

            GL.Vertex3(Min.X, Min.Y, Min.Z);
            GL.Vertex3(Min.X, Max.Y, Min.Z);
            GL.Vertex3(Max.X, Max.Y, Min.Z);
            GL.Vertex3(Max.X, Min.Y, Min.Z);

            GL.Vertex3(Min.X, Min.Y, Min.Z);
            GL.Vertex3(Max.X, Min.Y, Min.Z);
            GL.Vertex3(Max.X, Min.Y, Max.Z);
            GL.Vertex3(Min.X, Min.Y, Max.Z);

            GL.Vertex3(Min.X, Min.Y, Min.Z);
            GL.Vertex3(Min.X, Min.Y, Max.Z);
            GL.Vertex3(Min.X, Max.Y, Max.Z);
            GL.Vertex3(Min.X, Max.Y, Min.Z);

            GL.End();
        }

        public override string ToString() {
            string result = "Min: (" + Min.X + ", " + Min.Y + ", " + Min.Z + "), ";
            result += "Max: ( " + Max.X + ", " + Max.Y + ", " + Max.Z + ")";
            return result;
        }
    }
}
