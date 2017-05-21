using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    public class Vector
    {
        public static Vector ex, ey, ez, e0, e1, e2;
        public int[] data;
        public Vector(int[] values = null)
        {
            ex = e0 = new Vector(new int[3] { 1, 0, 0 });
            ey = e1 = new Vector(new int[3] { 0, 1, 0 });
            ez = e2 = new Vector(new int[3] { 0, 0, 1 });

            if (values == null)
                data = new int[3] { 0, 0, 0 };
            else
                data = values;
        }

        public int x
        {
            get { return data[0]; }
            set { data[0] = value; }
        }

        public int y
        {
            get { return data[1]; }
            set { data[1] = value; }
        }

        public int z
        {
            get { return data[2]; }
            set { data[2] = value; }
        }


    }
    public class Orientation
    {
        public int[,] data;
        public Orientation(int[,] values = null)
        {
            if (values == null)
            {
                data = new int[3, 3]
                {
                    { 1, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 0, 1 }
                };
            }
            else
                data = values;
        }

        public void rotate_xb(int angle)
        {
            rotate_b(Vector.e0, angle);
        }

        public void rotate_yb(int angle)
        {
            rotate_b(Vector.e1, angle);
        }

        public void rotate_zb(int angle)
        {
            rotate_b(Vector.e2, angle);
        }

        public void rotate_b(Vector axis, int angle)
        {
            var o = new Orientation();
            o.set_axis_angle(axis, angle);
            //copy(o * self)
        }

        public void set_axis_angle(Vector axis, int angle)
        {
        //    axis = axis.array
        //# Force normalization
        //axis /= np.linalg.norm(axis)
        //x = axis[0]
        //y = axis[1]
        //z = axis[2]
        //ca = np.cos(angle)
        //sa = np.sin(angle)
        //self._data[:, :] = np.array([
        //    [ca + (1 - ca) * x * *2,
        //     (1 - ca) * x * y - sa * z,
        //     (1 - ca) * x * z + sa * y],
        //    [(1 - ca) * x * y + sa * z,
        //     ca + (1 - ca) * y**2,
        //     (1 - ca) * y * z - sa * x],
        //    [(1 - ca) * x * z - sa * y,
        //     (1 - ca) * y * z + sa * x,
        //     ca + (1 - ca) * z**2]])
        }

    }
    public class Transform
    {
        private int[,] data;
        public Transform(int [,] matrix = null)
        {
            data = new int[4, 4]
            {
                {1,0,0,0},
                {0,1,0,0},
                {0,0,1,0},
                {0,0,0,1}
            };
            if (matrix == null)
            {
                v = new Vector();
                o = new Orientation();
            }
            else
            {
                int[,] orient = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                        orient[i, j] = matrix[i, j];
                }
                o = new Orientation(orient);

                int[] vec = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    vec[i] = matrix[i, 3];
                }
                v = new Vector(vec);
            }
            
        }
        public static Transform operator *(Transform lhs, Transform rhs)
        {

            return new Transform();
        }

        private Orientation o;
        public Orientation orient
        {
            get { return o; }
            set { o = value; }
        }

        private Vector v;
        public Vector pos
        {
            get { return v; }
            set { v = value; }
        }

    }
}
