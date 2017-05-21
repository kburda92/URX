using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    public class RobotException : Exception
    {
        public RobotException(string message) : base(message)
        {

        }
    }

    public class URRobot
    {
        public string host;
        protected Transform csys;
        public double joinEpsilon = 0.01;
        public int max_float_length = 6;
        public SecondaryMonitor secmon;
        public URRTMonitor rtmon;
        public URRobot(string host, bool use_rt = false)
        {
            //    self.logger = logging.getLogger("urx")
            this.host = host;

            //    self.logger.debug("Opening secondary monitor socket")
            secmon = new SecondaryMonitor(host);
            rtmon = get_realtime_monitor();
            secmon.wait();
        }

        public override string ToString()
        {
            return string.Format("Robot Object (IP={0}, state={1})", host, secmon.get_all_data()["RobotModeData"]);
        }

        public bool is_running()
        {
            return secmon.running;
        }

        public bool is_program_running()
        {
            return secmon.is_program_running();
        }

        public void send_program(string prog)
        {
            //    self.logger.info("Sending program: " + prog)
           secmon.send_program(prog);
        }

        public double[] get_tcp_force(bool wait = true)
        {
            return rtmon.getTCFForce(wait);
        }

        public double get_force(bool wait = true)
        {
            var tcpf = get_tcp_force(wait);
            double force = 0;
            foreach (var i in tcpf)
                force += Math.Pow(i, 2);
                
            return Math.Pow(force, 0.05);
        }

        public void set_tcp(int tcp)
        {
            var prog = string.Format("set_tcp(p[{0}, {1}, {2}, {3}, {4}, {5}])", tcp);
            send_program(prog);
        }

        public void set_payload(double weight, List<double> cog = null)
        {
            string prog = "";
            if (cog != null)
            {
                cog.Add(weight);
                prog = string.Format("set_payload({0}, ({1},{2},{3}))", cog);
            }
            else
                prog = string.Format("set_payload({0})", weight);
            send_program(prog);
        }

        public void set_gravity(Vector vector)
        {
            string prog = string.Format("set_gravity({0})", vector);
            send_program(prog);
        }

        public void set_gravity(string msg)
        {
            string prog = string.Format("textmsg(% s)", msg);
            send_program(prog);
        }

        public void set_digital_out(string output, bool val)
        {
            string Val= "False";
            if (val)
                Val = "True";
            send_program(string.Format("digital_out[{0}]={1}", output, Val));
        }

        public void get_analog_inputs()
        {
            return secmon.get_analog_inputs();
        }

        public void get_analog_in(int nb, bool wait = false)
        {
            return secmon.get_analog_in(nb, wait);
        }

        public void get_digital_in_bits()
        {
            return secmon.get_digital_in_bits();
        }

        public void get_digital_in(int nb, bool wait = false)
        {
            return secmon.get_digital_in(nb, wait);
        }

        public void get_digital_out(int val, bool wait = false)
        {
            return secmon.get_digital_out(val, wait);
        }

        public void get_digital_out_bits(bool wait = false)
        {
            return secmon.get_digital_out_bits(wait);
        }

        public void set_analog_out(int output, int val)
        {
            var prog = string.Format("set_analog_out({0}, {1})", output, val);
            send_program(prog);
        }

        public void set_analog_out(int val)
        {
            var prog = string.Format("set_tool_voltage(%s)", val);
            send_program(prog);
        }

        private void wait_for_move(double[] target, double threshold, int timeout = 5, bool joints = false)
        {
            //    self.logger.debug("Waiting for move completion using threshold %s and target %s", threshold, target)
            var start_dist = get_dist(target, joints);
            if (threshold == 0)
            {
                threshold = start_dist * 0.8;
                if (threshold < 0.001)
                    threshold = 0.001;
                //        self.logger.debug("No threshold set, setting it to %s", threshold)
            }
            int count = 0;
            while(true)
            {
                if (!is_running())
                    throw new RobotException("Robot stopped");
                var dist = get_dist(target, joints);
                if (!secmon.is_program_running())
                {
                    if (dist < threshold)
                    {
                        //                self.logger.debug("we are threshold(%s) close to target, move has ended", threshold)
                        return;
                    }
                    count += 1;
                    if (count > timeout * 10)
                        throw new RobotException(string.Format("Goal not reached but no program has been running for {0} seconds. dist is {1}, threshold is {2}, target is {3}, current pose is {4}", timeout, dist, threshold, target, getl()));
                }
                else
                    count = 0;
            }
        }

        private void get_dist(int target, bool joints = false)
        {
            if (joints)
                return get_joints_dist(target);
            else
                return get_lin_dist(target);
        }

        private double get_lin_dist(double[] target)
        {
            //    # FIXME: we have an issue here, it seems sometimes the axis angle received from robot
            var pose = getl(true);
            double dist = 0;
            for (int i = 0; i < 3; i++)
                dist += Math.Pow(target[i] - pose[i], 2);
            for (int i = 3; i < 6; i++)
                dist += Math.Pow(((target[i] - pose[i]) / 5), 2);

            return Math.Pow(dist, 0.5)
        }

        private double get_joints_dist(double[] target)
        {
            var joints = getj(true);
            double dist = 0;
            for (int i = 0; i < 6; i++)
                dist += Math.Pow(target[i] - joints[i], 2);

            return Math.Pow(dist, 0.5);
        }

        public void getj(bool wait = false)
        {
            //    jts = self.secmon.get_joint_data(wait)
            //    return [jts["q_actual0"], jts["q_actual1"], jts["q_actual2"], jts["q_actual3"], jts["q_actual4"], jts["q_actual5"]]
        }

        public void speedx(string command, double[] velocities, double acc, double min_time)
        {
            List<double> vels = new List<double>();
            for (int i = 0; i < velocities.Length; i++)
                vels.Add(Math.Round(velocities[i], max_float_length));
            vels.Add(acc);
            vels.Add(min_time);
            var prog = string.Format("{0}([{1},{2},{3},{4},{5},{6}], a={7}, t_min={8})", command, vels);
            send_program(prog);
        }

        public void movej(int joints, double acc = 0.1, double vel = 0.05, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    if relative:
            //        l = self.getj()
            //        joints = [v + l[i] for i, v in enumerate(joints)]
            //    prog = self._format_move("movej", joints, acc, vel)
            //    self.send_program(prog)
            //    if wait:
            //        self._wait_for_move(joints[:6], threshold=threshold, joints=True)
            //        return self.getj()
        }

        public void movel(double[] tpose, double acc = 0.1, double vel = 0.1, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("movel", tpose, acc, vel, wait, relative, threshold)
        }

        public void movep(double[] tpose, double acc = 0.1, double vel = 0.1, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("movep", tpose, acc, vel, wait, relative, threshold)
        }

        public void servoc(double[] tpose, double acc = 0.01, double vel = 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("servoc", tpose, acc, vel, wait, relative, threshold)
        }

        private string format_move(string command, double[] tpose, double acc, double vel, double radius= 0, string prefix= "")
        {
            List<double> _tpose = new List<double>();
            foreach (double i in tpose)
                _tpose.Add(Math.Round(i, max_float_length));
            _tpose.Add(acc);
            _tpose.Add(vel);
            _tpose.Add(radius);
            return string.Format("{0}({1}[{2},{3},{4},{5},{6},{7}], a={8}, v={9}, r={10})", command, prefix, tpose);
        }

        public double[] movex(string command, double[] tpose, double acc= 0.01, double vel = 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            if(relative)
            {
                var l = getl();
                //var tpose = [v + l[i] for i, v in enumerate(tpose)];
            }

            var prog = format_move(command, tpose, acc, vel, 0, "p");
            send_program(prog);
            if(wait)
            {
                var pose_six = new double[6];
                for(int i = 0; i < 6 ;i++)
                    pose_six[i] = tpose[i];
                wait_for_move(pose_six, threshold);
                return getl();
            }
            return null;
        }

        public double[] getl(bool wait = false, bool _log = true)
        {
            var pose = secmon.get_cartesian_info(wait);
            double[] _pose = new double[6];
            if (pose != null)
            {
                _pose[0] = pose["X"];
                _pose[1] = pose["Y"];
                _pose[2] = pose["Z"];
                _pose[3] = pose["Rx"];
                _pose[4] = pose["Ry"];
                _pose[5] = pose["Rz"];

            }
            //    if _log:
            //        self.logger.debug("Received pose from robot: %s", pose)
            return _pose;
        }

        public double[] movec(double[] pose_via, double[] pose_to, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            for (int i= 0;i<pose_via.Length;i++)
                pose_via[i] = Math.Round(pose_via[i], max_float_length);
            for (int i = 0; i < pose_via.Length; i++)
                pose_to[i] = Math.Round(pose_to[i], max_float_length);
            var prog = string.Format("movec(p{0}, p{1}, a={2}, v={3}, r={4})", pose_via, pose_to, acc, vel, "0");
            send_program(prog);
            if (wait)
                wait_for_move(pose_to, threshold);
            return getl();
        }

        //public void movels(int pose_list, double acc = 0.01, double vel = 0.01, double radius = 0.01, bool wait = true, int threshold = 0)
        //{
        //    //    return self.movexs("movel", pose_list, acc, vel, radius, wait, threshold = threshold)
        //}

        //public void movexs(string command, double[] pose_list, double acc = 0.01, double vel = 0.01, double radius = 0.01, bool wait = true, int threshold = 0)
        //{
        //    var header = "def myProg():\n";
        //    var end = "end\n";
        //    var prog = header;
        //    //    for idx, pose in enumerate(pose_list):
        //    //        if idx == (len(pose_list) - 1):
        //    //            radius = 0
        //    //        prog += self._format_move(command, pose, acc, vel, radius, prefix = "p") + "\n"
        //    //    prog += end
        //    //    self.send_program(prog)
        //    //    if wait:
        //    //        self._wait_for_move(target = pose_list[-1], threshold = threshold)
        //    //        return self.getl()
        //}

        public void stopl(double acc = 0.5)
        {
            send_program(string.Format("stopl{0}", acc));
        }

        public void stopj(double acc = 1.5)
        {
            send_program(string.Format("stopl{0}", acc));
        }

        public void stop()
        {
            stopj();
        }

        public void close()
        {
            //    self.logger.info("Closing sockets to robot")
            secmon.close();
            if (rtmon != null)
                rtmon.stop();
        }

        public void set_freedrive(bool val)
        {
            if (val)
                send_program("set robotmode freedrive");
            else
                send_program("set robotmode run");
        }

        public void set_simulation(bool val)
        {
            if (val)
                send_program("set sim");
            else
                send_program("set real");
        }

        public URRTMonitor get_realtime_monitor()
        {
            if(rtmon == null)
            {
                //        self.logger.info("Opening real-time monitor socket")
                //        self.rtmon = urrtmon.URRTMonitor(self.host)  # som information is only available on rt interface
                //        self.rtmon.start()
            }
            rtmon.set_csys(csys);
            return rtmon;
        }

        public double[] translate(double[] vect, double acc = 0.01, double vel = 0.01, bool wait = true, string command= "movel")
        {
            var p = getl();
            p[0] += vect[0];
            p[1] += vect[1];
            p[2] += vect[2];
            return movex(command, p, vel, acc, wait);
        }

        public void up(double z= 0.05, double acc = 0.01, double vel = 0.01)
        {
            var p = getl();
            p[2] += z;
            movel(p, acc, vel);
        }

        public void down(double z = 0.05, double acc = 0.01, double vel = 0.01)
        {
            up(-z, acc, vel);
        }
    }
}
