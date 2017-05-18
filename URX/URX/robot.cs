using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    public class robot
    {
        public robot(string host, bool use_rt = false)
        {
            //URRobot.__init__(self, host, use_rt)
            //self.csys = m3d.Transform()
        }

        private void get_lin_dist(int target)
        {
            //pose = URRobot.getl(self, wait = True)
            //target = m3d.Transform(target)
            //pose = m3d.Transform(pose)
            //return pose.dist(target)
        }

        public void set_tcp(int tcp)
        {
            //    if isinstance(tcp, m3d.Transform):
            //        tcp = tcp.pose_vector
            //    URRobot.set_tcp(self, tcp)
        }

        public void set_csys(int transform)
        {
            //    self.csys = transform
        }

        public void set_orientation(int orient, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    if not isinstance(orient, m3d.Orientation):
            //        orient = m3d.Orientation(orient)
            //    trans = self.get_pose()
            //    trans.orient = orient
            //    self.set_pose(trans, acc, vel, wait= wait, threshold= threshold)
        }

        public void translate_tool(int vect, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    t = m3d.Transform()
            //    if not isinstance(vect, m3d.Vector):
            //        vect = m3d.Vector(vect)
            //    t.pos += vect
            //    return self.add_pose_tool(t, acc, vel, wait=wait, threshold=threshold)
        }

        public void back(double z = 0.05, double acc = 0.01, double vel = 0.01)
        {
            int a = 0;
            translate_tool(/*(0, 0, -z)*/a, acc, vel);
        }

        public void set_pos(int vect, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    if not isinstance(vect, m3d.Vector):
            //        vect = m3d.Vector(vect)
            //    trans = m3d.Transform(self.get_orientation(), m3d.Vector(vect))
            //    return self.set_pose(trans, acc, vel, wait=wait, threshold=threshold)
        }

        public void movec(int pose_via, int pose_to, double acc = 0.01, double vel = 0.01, bool wait= true, int threshold = 0)
        {
            //    pose_via = self.csys* m3d.Transform(pose_via)
            //    pose_to = self.csys* m3d.Transform(pose_to)
            //    pose = URRobot.movec(self, pose_via.pose_vector, pose_to.pose_vector, acc=acc, vel=vel, wait=wait, threshold=threshold)
            //    if pose is not None:
            //        return self.csys.inverse* m3d.Transform(pose)
        }

        public void set_pose(int trans, double acc = 0.01, double vel= 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            //    self.logger.debug("Setting pose to %s", trans.pose_vector)
            //    t = self.csys* trans
            //    pose = URRobot.movex(self, command, t.pose_vector, acc=acc, vel=vel, wait=wait, threshold=threshold)
            //    if pose is not None:
            //        return self.csys.inverse* m3d.Transform(pose)
        }

        public void add_pose_base(int trans, double acc = 0.01, double vel = 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            //    pose = self.get_pose()
            //    return self.set_pose(trans* pose, acc, vel, wait = wait, command = command, threshold = threshold)
        }

        public void add_pose_tool(int trans, double acc = 0.01, double vel = 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            //    pose = self.get_pose()
            //    return self.set_pose(pose* trans, acc, vel, wait = wait, command = command, threshold = threshold)
        }

        public void get_pose(bool wait = false, bool _log = true)
        {
            //    pose = URRobot.getl(self, wait, _log)
            //    trans = self.csys.inverse* m3d.Transform(pose)
            //    if _log:
            //        self.logger.debug("Returning pose to user: %s", trans.pose_vector)
            //    return trans
        }

        public void get_orientation(bool wait = false)
        {
            //    trans = self.get_pose(wait)
            //    return trans.orient
        }

        public void get_pos(bool wait = false)
        {
            //    trans = self.get_pose(wait)
            //    return trans.pos
        }

        public void speedl(int velocities, double acc, int min_time)
        {
            //    v = self.csys.orient* m3d.Vector(velocities[:3])
            //    w = self.csys.orient* m3d.Vector(velocities[3:])
            //    vels = np.concatenate((v.array, w.array))
            //    return self.speedx("speedl", vels, acc, min_time)
        }

        public void speedj(int velocities, double acc, int min_time)
        {
            //    return self.speedx("speedj", velocities, acc, min_time)
        }

        public void speedl_tool(int velocities, double acc, int min_time)
        {
            //    pose = self.get_pose()
            //    v = pose.orient* m3d.Vector(velocities[:3])
            //    w = pose.orient* m3d.Vector(velocities[3:])
            //    self.speedl(np.concatenate((v.array, w.array)), acc, min_time)
        }

        public void movex(int velocities, int command, int pose, double acc= 0.01, double vel= 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            //   t = m3d.Transform(pose)
            //    if relative:
            //        return self.add_pose_base(t, acc, vel, wait= wait, command= command, threshold= threshold)
            //    else:
            //        return self.set_pose(t, acc, vel, wait=wait, command=command, threshold=threshold)
        }

        public void movex(int command, int pose_list, double acc = 0.01, double vel = 0.01, double radius = 0.01, bool wait = true, int threshold = 0)
        {
            //    new_poses = []
            //    for pose in pose_list:
            //        t = self.csys* m3d.Transform(pose)
            //        pose = t.pose_vector
            //        new_poses.append(pose)
            //    return URRobot.movexs(self, command, new_poses, acc, vel, radius, wait=wait, threshold=threshold)
        }

        public void movel_tool(int pose, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //return movex_tool("movel", pose, acc, vel, wait, threshold)
        }

        public void movex_tool(string command, int pose, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    t = m3d.Transform(pose)
            //    self.add_pose_tool(t, acc, vel, wait=wait, command=command, threshold=threshold)
        }

        public void getl(bool wait = false, bool _log = true)
        {
            //    t = self.get_pose(wait, _log)
            //    return t.pose_vector.tolist()
        }

        public void set_gravity(int vector)
        {
            //    if isinstance(vector, m3d.Vector):
            //        vector = vector.list
            //    return URRobot.set_gravity(self, vector)
        }

        public void new_csys_from_xpy()
        {
            //    # Set coord. sys. to 0
            //    self.csys = m3d.Transform()

            //    print("A new coordinate system will be defined from the next three points")
            //    print("Firs point is X, second Origin, third Y")
            //    print("Set it as a new reference by calling myrobot.set_csys(new_csys)")
            //    input("Move to first point and click Enter")
            //    # we do not use get_pose so we avoid rounding values
            //    pose = URRobot.getl(self)
            //    print("Introduced point defining X: {}".format(pose[:3]))
            //    px = m3d.Vector(pose[:3])
            //    input("Move to second point and click Enter")
            //    pose = URRobot.getl(self)
            //    print("Introduced point defining Origo: {}".format(pose[:3]))
            //    p0 = m3d.Vector(pose[:3])
            //    input("Move to third point and click Enter")
            //    pose = URRobot.getl(self)
            //    print("Introduced point defining Y: {}".format(pose[:3]))
            //    py = m3d.Vector(pose[:3])

            //    new_csys = m3d.Transform.new_from_xyp(px - p0, py - p0, p0)
            //    self.set_csys(new_csys)

            //    return new_csys
        }

        private int _x;
        public int x
        {
            get { return _x; }
            set
            {
                //def x(self, val):
                //    p = self.get_pos()
                //    p.x = val
                //    self.set_pos(p)
            }
        }

        private int _y;
        public int y
        {
            get { return _y; }
            set
            {
                //    p = self.get_pos()
                //    p.y = val
                //    self.set_pos(p)
            }
        }

        private int _z;
        public int z
        {
            get { return _z; }
            set
            {
                //    p = self.get_pos()
                //    p.z = val
                //    self.set_pos(p)
            }
        }

        private int _rx;
        public int rx
        {
            get { return 0; }
            set
            {
                //    p = self.get_pose()
                //    p.orient.rotate_xb(val)
                //    self.set_pose(p)
            }
        }

        private int _ry;
        public int ry
        {
            get { return 0; }
            set
            {
                //    p = self.get_pose()
                //    p.orient.rotate_yb(val)
                //    self.set_pose(p)
            }
        }

        private int _rz;
        public int rz
        {
            get { return 0; }
            set
            {
                //    p = self.get_pose()
                //    p.orient.rotate_zb(val)
                //    self.set_pose(p)
            }
        }

        private int _x_t;
        public int x_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.pos.x += val
                //    self.add_pose_tool(t)
            }
        }

        private int _y_t;
        public int y_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.pos.y += val
                //    self.add_pose_tool(t)
            }
        }

        private int _z_t;
        public int z_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.pos.z += val
                //    self.add_pose_tool(t)
            }
        }

        private int _rx_t;
        public int rx_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.orient.rotate_xb(val)
                //    self.add_pose_tool(t)
            }
        }

        private int _ry_t;
        public int ry_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.orient.rotate_yb(val)
                //    self.add_pose_tool(t)
            }
        }

        private int _rz_t;
        public int rz_t
        {
            get { return 0; }
            set
            {
                //    t = m3d.Transform()
                //    t.orient.rotate_zb(val)
                //    self.add_pose_tool(t)
            }
        }
    }
}
