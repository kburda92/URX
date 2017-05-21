using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    public class Robot : URRobot
    {
        public Robot(string host, bool use_rt = false) : base(host, use_rt)
        {
            csys = new Transform();
        }

        private void get_lin_dist(Transform target)
        {
            var pose = base.getl(true);
            target = new Transform(target);
            pose = new Transform(pose);
            return pose.dist(target);
        }

        public void set_tcp(int tcp)
        {
            //    if isinstance(tcp, m3d.Transform):
            //        tcp = tcp.pose_vector
            base.set_tcp(tcp);
        }

        public void set_csys(Transform transform)
        {
            csys = transform;
        }

        public void set_orientation(Orientation orient, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            var trans = get_pose();
            trans.orient = orient;
            set_pose(trans, acc, vel, wait, "movel", threshold);
        }

        public void translate_tool(Vector vect, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            var t = new Transform();
            //    if not isinstance(vect, m3d.Vector):
            //        vect = m3d.Vector(vect)
            t.pos += vect;
            add_pose_tool(t, acc, vel, wait, "movel", threshold);
        }

        public void back(double z = 0.05, double acc = 0.01, double vel = 0.01)
        {   
            translate_tool(new Vector(new int [3]{0, 0, -z}), acc, vel);
        }

        public void set_pos(Vector vect, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    if not isinstance(vect, m3d.Vector):
            //        vect = m3d.Vector(vect)
            var trans = new Transform(get_orientation(), new Vector(vect));
            set_pose(trans, acc, vel, wait, "movel", threshold);
        }

        public Transform movec(Vector pose_via, Vector pose_to, double acc = 0.01, double vel = 0.01, bool wait= true, int threshold = 0)
        {

            var _pose_via = csys * new Transform(pose_via);
            var _pose_to = csys * new Transform(pose_to);
            var pose = base.movec(pose_via.pose_vector, pose_to.pose_vector, acc, vel, wait, threshold);
            if (pose != null)
                return csys.inverse * new Transform(new Vector(pose));
        }

        public Transform set_pose(Transform trans, double acc = 0.01, double vel= 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            //    self.logger.debug("Setting pose to %s", trans.pose_vector)
            var t = csys * trans;
            var pose = base.movex(command, t.pose_vector, acc, vel, wait, threshold);
            if (pose != null)
                return csys.inverse * new Transform(new Vector(pose));
        }

        public void add_pose_base(Transform trans, double acc = 0.01, double vel = 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            var pose = get_pose();
            set_pose(trans * pose, acc, vel, wait, command, threshold);
        }

        public Transform add_pose_tool(Transform trans, double acc = 0.01, double vel = 0.01, bool wait = true, string command = "movel", int threshold = 0)
        {
            var pose = get_pose();
            return set_pose(pose * trans, acc, vel, wait, command, threshold);
        }

        public Transform get_pose(bool wait = false, bool _log = true)
        {
            var pose = base.getl(wait, _log);
            var trans = csys.inverse * new Transform(new Vector(pose));
            //    if _log:
            //        self.logger.debug("Returning pose to user: %s", trans.pose_vector)
            return trans;
        }

        public Orientation get_orientation(bool wait = false)
        {
            var trans = get_pose(wait);
            return trans.orient;
        }

        public Vector get_pos(bool wait = false)
        {
            var trans = get_pose(wait);
            return trans.pos;
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
            var pose = get_pose();
            //    v = pose.orient* m3d.Vector(velocities[:3])
            //    w = pose.orient* m3d.Vector(velocities[3:])
            //speedl(np.concatenate((v.array, w.array)), acc, min_time);
        }

        public Transform movex(int velocities, string command, Vector pose, double acc= 0.01, double vel= 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            var t = new Transform(pose);
            if (relative)
            {
                add_pose_base(t, acc, vel, wait, command, threshold);
                return null;
            }
            else
                return set_pose(t, acc, vel, wait, command, threshold);
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

        public void movex_tool(string command, Vector pose, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            var t = new Transform(pose);
            add_pose_tool(t, acc, vel, wait, command, threshold);
        }

        public void getl(bool wait = false, bool _log = true)
        {
            var t = get_pose(wait, _log);
            return t.pose_vector.tolist();
        }

        public void set_gravity(Vector vector)
        {
            //    if isinstance(vector, m3d.Vector):
            //        vector = vector.list
            base.set_gravity(vector);
        }

        public void new_csys_from_xpy()
        {
            //    # Set coord. sys. to 0
            csys = new Transform();

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
                var p = get_pos();
                p.x = value;
                set_pos(p);
            }
        }

        private int _y;
        public int y
        {
            get { return _y; }
            set
            {
                var p = get_pos();
                p.y = value;
                set_pos(p);
            }
        }

        private int _z;
        public int z
        {
            get { return _z; }
            set
            {
                var p = get_pos();
                p.z = value;
                set_pos(p);
            }
        }

        private int _rx;
        public int rx
        {
            get { return 0; }
            set
            {
                var p = get_pose();
                p.orient.rotate_xb(value);
                set_pose(p);
            }
        }

        private int _ry;
        public int ry
        {
            get { return 0; }
            set
            {
                var p = get_pose();
                p.orient.rotate_yb(value);
                set_pose(p);
            }
        }

        private int _rz;
        public int rz
        {
            get { return 0; }
            set
            {
                var p = get_pose();
                p.orient.rotate_zb(value);
                set_pose(p);
            }
        }

        private int _x_t;
        public int x_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.pos.x += value;
                add_pose_tool(t);
            }
        }

        private int _y_t;
        public int y_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.pos.y += value;
                add_pose_tool(t);
            }
        }

        private int _z_t;
        public int z_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.pos.z += value;
                add_pose_tool(t);
            }
        }

        private int _rx_t;
        public int rx_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.orient.rotate_xb(value);
                add_pose_tool(t);
            }
        }

        private int _ry_t;
        public int ry_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.orient.rotate_yb(value);
                add_pose_tool(t);
            }
        }

        private int _rz_t;
        public int rz_t
        {
            get { return 0; }
            set
            {
                var t = new Transform();
                t.orient.rotate_zb(value);
                add_pose_tool(t);
            }
        }
    }
}
