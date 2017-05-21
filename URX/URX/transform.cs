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
        public double[] data;
        public Vector(double[] values = null)
        {
            ex = e0 = new Vector(new double[3] { 1, 0, 0 });
            ey = e1 = new Vector(new double[3] { 0, 1, 0 });
            ez = e2 = new Vector(new double[3] { 0, 0, 1 });

            if (values == null)
                data = new double[3] { 0, 0, 0 };
            else
                data = values;
        }

        public double x
        {
            get { return data[0]; }
            set { data[0] = value; }
        }

        public double y
        {
            get { return data[1]; }
            set { data[1] = value; }
        }

        public double z
        {
            get { return data[2]; }
            set { data[2] = value; }
        }


    }
    public class Orientation
    {
        public double[,] data;
        public Orientation(double[,] values = null)
        {
            if (values == null)
            {
                data = new double[3, 3]
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
        private double[,] data;
        public Transform(double[,] matrix = null)
        {
            data = new double[4, 4]
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
                double[,] orient = new double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                        orient[i, j] = matrix[i, j];
                }
                o = new Orientation(orient);

                double[] vec = new double[3];
                for (int i = 0; i < 3; i++)
                {
                    vec[i] = matrix[i, 3];
                }
                v = new Vector(vec);
            }
            
        }
        public static Transform operator *(Transform lhs, Transform rhs)
        {
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for(int j=0;j<4;j++)
                    result[i,j] = lhs.data[i,j] * rhs.data[j,i];
            }
            return new Transform(result);
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
