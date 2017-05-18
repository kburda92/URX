using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    class RobotException
    {
    }

    class URRobot
    {
        string host;
        int csys;
        URRobot(string host, bool use_rt = false)
        {
            //    self.logger = logging.getLogger("urx")
            this.host = host;
            csys = 0;

            //    self.logger.debug("Opening secondary monitor socket")
            //    self.secmon = ursecmon.SecondaryMonitor(self.host)  # data from robot at 10Hz

            //    self.rtmon = None
            //    if use_rt:
            //        self.rtmon = self.get_realtime_monitor()
            //    # precision of joint movem used to wait for move completion
            //    # the value must be conservative! otherwise we may wait forever
            //    self.joinEpsilon = 0.01
            //    # It seems URScript is  limited in the character length of floats it accepts
            //    self.max_float_length = 6  # FIXME: check max length!!!

            //    self.secmon.wait()  # make sure we get data from robot before letting clients access our methods
        }

        public override string ToString()
        {
            return "";
            //return "Robot Object (IP=%s, state=%s)" % (self.host, self.secmon.get_all_data()["RobotModeData"])
        }

        public bool is_running()
        {
            return true;
            //    return self.secmon.running
        }

        public bool is_program_running()
        {
            return true;
            //return self.secmon.is_program_running()
        }

        public void send_program(string prog)
        {
            //    self.logger.info("Sending program: " + prog)
            //    self.secmon.send_program(prog)
        }

        public void get_tcp_force(bool wait = true)
        {
            //    return self.rtmon.getTCFForce(wait)
        }

        public void get_force(bool wait = true)
        {
            //    tcpf = self.get_tcp_force(wait)
            //    force = 0
            //    for i in tcpf:
            //        force += i**2
            //    return force**0.5
        }

        public void set_tcp(int tcp)
        {
            string prog = "";// "set_tcp(p[{}, {}, {}, {}, {}, {}])".format(*tcp)
            send_program(prog);
        }

        public void set_payload(double weight, int cog = 0)
        {
            string prog = "";
            //    if cog:
            //        cog = list(cog)
            //        cog.insert(0, weight)
            //        prog = "set_payload({}, ({},{},{}))".format(*cog)
            //    else:
            //        prog = "set_payload(%s)" % weight
            send_program(prog)
        }

        public void set_gravity(int vector)
        {
            string prog = "";// = "set_gravity(%s)" % list(vector)
            send_program(prog);
        }

        public void set_gravity(string msg)
        {
            string prog = "";//"textmsg(%s)" % msg
            send_program(prog);
        }

        public void set_digital_out(string output, bool val)
        {
            //    if val in (True, 1):
            //        val = "True"
            //    else:
            //        val = "False"
            //    self.send_program('digital_out[%s]=%s' % (output, val))
        }

        public void get_analog_inputs()
        {
            //    return self.secmon.get_analog_inputs()
        }

        public void get_analog_in(int nb, bool wait = false)
        {
            //    return self.secmon.get_analog_in(nb, wait=wait)
        }

        public void get_digital_in_bits()
        {
            //    return self.secmon.get_digital_in_bits()
        }

        public void get_digital_in(int nb, bool wait = false)
        {
            //    return self.secmon.get_digital_in(nb, wait)
        }

        public void get_digital_out(int val, bool wait = false)
        {
            //    return self.secmon.get_digital_out(val, wait=wait)
        }

        public void get_digital_out_bits(bool wait = false)
        {
            //    return self.secmon.get_digital_out_bits(wait=wait)
        }

        public void set_analog_out(int output, int val)
        {
            string prog = "";//set_analog_out(%s, %s)" % (output, val)
            send_program(prog);
        }

        public void set_analog_out(int val)
        {
            string prog = "";// "set_tool_voltage(%s)" % (val);
            send_program(prog);
        }

        private void wait_for_move(int target, int threshold, int timeout = 5, bool joints = false)
        {
            //    self.logger.debug("Waiting for move completion using threshold %s and target %s", threshold, target)
            //    start_dist = self._get_dist(target, joints)
            //    if threshold is None:
            //        threshold = start_dist* 0.8
            //        if threshold< 0.001:  # roboten precision is limited
            //            threshold = 0.001
            //        self.logger.debug("No threshold set, setting it to %s", threshold)
            //    count = 0
            //    while True:
            //        if not self.is_running():
            //            raise RobotException("Robot stopped")
            //        dist = self._get_dist(target, joints)
            //        self.logger.debug("distance to target is: %s, target dist is %s", dist, threshold)
            //        if not self.secmon.is_program_running():
            //            if dist<threshold:
            //                self.logger.debug("we are threshold(%s) close to target, move has ended", threshold)
            //                return
            //            count += 1
            //            if count > timeout* 10:
            //                raise RobotException("Goal not reached but no program has been running for {} seconds. dist is {}, threshold is {}, target is {}, current pose is {}".format(timeout, dist, threshold, target, URRobot.getl(self)))
            //        else:
            //            count = 0
        }

        private void get_dist(int target, bool joints = false)
        {
            //    if joints:
            //        return get_joints_dist(target)
            //    else:
            //        return get_lin_dist(target)
        }

        private void get_lin_dist(int target)
        {
            //    # FIXME: we have an issue here, it seems sometimes the axis angle received from robot
            //    pose = URRobot.getl(self, wait=True)
            //    dist = 0
            //    for i in range(3):
            //        dist += (target[i] - pose[i]) ** 2
            //    for i in range(3, 6):
            //        dist += ((target[i] - pose[i]) / 5) ** 2  # arbitraty length like
            //    return dist** 0.5
        }

        private void get_joints_dist(int target)
        {
            //    joints = getj(true)
            int dist = 0;
            //for (int i = 0; i < 6; i++)
                //dist += (target[i] - joints[i]) * *2;
            //    for i in range(6):
            //        dist += (target[i] - joints[i]) ** 2
            //    return dist** 0.5
        }

        public void getj(bool wait = false)
        {
            //    jts = self.secmon.get_joint_data(wait)
            //    return [jts["q_actual0"], jts["q_actual1"], jts["q_actual2"], jts["q_actual3"], jts["q_actual4"], jts["q_actual5"]]
        }

        public void speedx(string command, int velocities, double acc, int min_time)
        {
            //    vels = [round(i, self.max_float_length) for i in velocities]
            //    vels.append(acc)
            //    vels.append(min_time)
            //    prog = "{}([{},{},{},{},{},{}], a={}, t_min={})".format(command, * vels)
            //    self.send_program(prog)
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

        public void movel(int tpose, double acc = 0.1, double vel = 0.1, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("movel", tpose, acc, vel, wait, relative, threshold)
        }

        public void movep(int tpose, double acc = 0.1, double vel = 0.1, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("movep", tpose, acc, vel, wait, relative, threshold)
        }

        public void servoc(int tpose, double acc = 0.01, double vel = 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    return self.movex("servoc", tpose, acc, vel, wait, relative, threshold)
        }

        private void format_move(string command, int tpose, double acc, double vel, int adius= 0, string prefix= "")
        {
            //    tpose = [round(i, self.max_float_length) for i in tpose]
            //    tpose.append(acc)
            //    tpose.append(vel)
            //    tpose.append(radius)
            //    return "{}({}[{},{},{},{},{},{}], a={}, v={}, r={})".format(command, prefix, * tpose)
        }

        public void movex(string command, int tpose, double acc= 0.01, double vel = 0.01, bool wait = true, bool relative = false, int threshold = 0)
        {
            //    if relative:

            //       l = self.getl()

            //       tpose = [v + l[i] for i, v in enumerate(tpose)]

            //   prog = self._format_move(command, tpose, acc, vel, prefix = "p")

            //   self.send_program(prog)
            //    if wait:
            //        self._wait_for_move(tpose [:6], threshold=threshold)
            //        return self.getl()
        }

        public void getl(bool wait = false, bool _log = true)
        {
            //    pose = self.secmon.get_cartesian_info(wait)
            //    if pose:
            //        pose = [pose["X"], pose["Y"], pose["Z"], pose["Rx"], pose["Ry"], pose["Rz"]]
            //    if _log:
            //        self.logger.debug("Received pose from robot: %s", pose)
            //    return pose
        }

        public void movec(int pose_via, int pose_to, double acc = 0.01, double vel = 0.01, bool wait = true, int threshold = 0)
        {
            //    pose_via = [round(i, self.max_float_length) for i in pose_via]
            //    pose_to = [round(i, self.max_float_length) for i in pose_to]
            //    prog = "movec(p%s, p%s, a=%s, v=%s, r=%s)" % (pose_via, pose_to, acc, vel, "0")
            //        self.send_program(prog)
            //    if wait:
            //        self._wait_for_move(pose_to, threshold = threshold)
            //        return self.getl()
        }

        public void movels(int pose_list, double acc = 0.01, double vel = 0.01, double radius = 0.01, bool wait = true, int threshold = 0)
        {
            //    return self.movexs("movel", pose_list, acc, vel, radius, wait, threshold = threshold)
        }

        public void movexs(string command, int pose_list, double acc = 0.01, double vel = 0.01, double radius = 0.01, bool wait = true, int threshold = 0)
        {
            //    header = "def myProg():\n"
            //    end = "end\n"
            //    prog = header
            //    for idx, pose in enumerate(pose_list):
            //        if idx == (len(pose_list) - 1):
            //            radius = 0
            //        prog += self._format_move(command, pose, acc, vel, radius, prefix = "p") + "\n"
            //    prog += end
            //    self.send_program(prog)
            //    if wait:
            //        self._wait_for_move(target = pose_list[-1], threshold = threshold)
            //        return self.getl()
        }

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
            //    self.secmon.close()
            //    if self.rtmon:
            //        self.rtmon.stop()
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

        public void get_realtime_monitor()
        {
            //    if not self.rtmon:
            //        self.logger.info("Opening real-time monitor socket")
            //        self.rtmon = urrtmon.URRTMonitor(self.host)  # som information is only available on rt interface
            //        self.rtmon.start()
            //    self.rtmon.set_csys(self.csys)
            //    return self.rtmon
        }

        public void translate(int vect, double acc = 0.01, double vel = 0.01, bool wait = true, string command= "movel")
        {
            //    p = self.getl()
            //    p[0] += vect[0]
            //    p[1] += vect[1]
            //    p[2] += vect[2]
            //    return self.movex(command, p, vel=vel, acc=acc, wait=wait)
        }

        public void up(double z= 0.05, double acc = 0.01, double vel = 0.01)
        {
            //    p = self.getl()
            //    p[2] += z
            //    self.movel(p, acc= acc, vel= vel)
        }

        public void down(double z = 0.05, double acc = 0.01, double vel = 0.01)
        {
            up(-z, acc, vel);
        }
    }
}
