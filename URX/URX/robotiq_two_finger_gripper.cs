using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    class RobotiqScript : URScript
    {
        public static string ACT = "ACT";
        public static string GTO = "GTO";
        public static string ATR = "ATR";
        public static string ARD = "ARD";
        public static string FOR = "FOR";
        public static string SPE = "SPE";
        public static string OBJ = "OBJ";
        public static string STA = "STA";
        public static string FLT = "FLT";
        public static string POS = "POS";

        public const string SOCKET_HOST = "127.0.0.1";
        public const int SOCKET_PORT = 63352;
        public const string SOCKET_NAME = "gripper_socket";

        public string socket_host;
        public int socket_port;
        public string socket_name;

        public RobotiqScript(string socket_host = SOCKET_HOST, int socket_port = SOCKET_PORT, string socket_name = SOCKET_NAME) :
            base()
        {
            this.socket_host = socket_host;
            this.socket_port = socket_port;
            this.socket_name = socket_name;

            // Reset connection to gripper
            socket_close(socket_name);
            socket_open(socket_host, socket_port, socket_name);
        }

        private void import_rq_script()
        {
            var dir_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rq_script = Path.Combine(dir_path, "rq_script.script");
            using (BinaryReader reader = new BinaryReader(File.Open(rq_script, FileMode.Open)))
            {
                byte[] data = reader.ReadBytes(int.MaxValue);
                add_header_to_program(data);
            }

        }

        private void rq_get_var(string var_name, int nbytes)
        {
            socket_send_string(string.Format("GET {0}", var_name));
            socket_read_byte_list(nbytes);
        }

        private void _get_gripper_fault()
        {
            rq_get_var(FLT, 2);
        }

        private void get_gripper_object()
        {
            rq_get_var(OBJ, 1);
        }

        private void get_gripper_status()
        {
            rq_get_var(STA, 1);
        }

        public void set_gripper_activate()
        {
            socket_set_var(GTO, 1, socket_name);
        }

        public void set_gripper_force(int value)
        {
            constrain_unsigned_char(value);
            socket_set_var(FOR, value, socket_name);
        }

        public void set_gripper_position(int value)
        {
            value = constrain_unsigned_char(value);
            socket_set_var(POS, value, socket_name);
        }

        public void set_gripper_speed(int value)
        {
            constrain_unsigned_char(value);
            socket_set_var(SPE, value, socket_name);
        }

        public void set_robot_activate()
        {
            socket_set_var(ACT, 1, socket_name);
        }
    }

    public class Robotiq_Two_Finger_Gripper
    {
        public Robot robot;
        public double payload;
        public int speed;
        public double force;
        public string socket_host;
        public int socket_port;
        public string socket_name;
        public Robotiq_Two_Finger_Gripper(Robot robot, double payload = 0.85, int speed = 255, double force = 50,
            string socket_host = RobotiqScript.SOCKET_HOST,
            int socket_port = RobotiqScript.SOCKET_PORT,
            string socket_name = RobotiqScript.SOCKET_NAME)
        {
            this.robot = robot;
            this.payload = payload;
            this.speed = speed;
            this.force = force;
            this.socket_host = socket_host;
            this.socket_port = socket_port;
            this.socket_name = socket_name;
        }

        private RobotiqScript get_new_urscript()
        {
            var urscript = new RobotiqScript(socket_host, socket_port, socket_name);

            urscript.set_analog_inputrange(0, 0);
            urscript.set_analog_inputrange(1, 0);
            urscript.set_analog_inputrange(2, 0);
            urscript.set_analog_inputrange(3, 0);
            urscript.set_analog_outputdomain(0, 0);
            urscript.set_analog_outputdomain(1, 0);
            urscript.set_tool_voltage(0);
            urscript.set_runstate_outputs();

            urscript.set_payload(payload);
            urscript.set_gripper_speed(speed);
            urscript.set_gripper_force(force);

            urscript.set_robot_activate();
            urscript.set_gripper_activate();

            return urscript;
        }

        public void gripper_action(int value)
        {
            var urscript = get_new_urscript();
            double sleep = 2.0;


            urscript.set_gripper_position(value);
            urscript.sleep(sleep);

            robot.send_program(urscript.call());
            time.sleep(sleep);
        }

        public void open_gripper()
        {
            gripper_action(0);
        }

        public void close_gripper()
        {
            gripper_action(255);
        }
    }
}