using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    class RobotiqScript : URScript
    {
        const string SOCKET_HOST = "127.0.0.1";
        const int SOCKET_PORT = 63352;
        const string SOCKET_NAME = "gripper_socket";

        string socket_host = "127.0.0.1";
        int socket_port = 63352;
        string socket_name = "gripper_socket";

        public RobotiqScript(string socket_host = SOCKET_HOST, int socket_port = SOCKET_PORT, string socket_name = SOCKET_NAME) :
            base()
        {
        //# Reset connection to gripper
        //    self._socket_close(self.socket_name)
        //self._socket_open(self.socket_host, self.socket_port, self.socket_name)
        }

        private void import_rq_script()
        {
            //    dir_path = os.path.dirname(os.path.realpath(__file__))
            //    rq_script = os.path.join(dir_path, 'rq_script.script')
            //    with open(rq_script, 'rb') as f:
            //        rq_script = f.read()
            //        self.add_header_to_program(rq_script)
        }

        private void rq_get_var(string var_name, int nbytes)
        {
            //    self._socket_send_string(b"GET {}".format(var_name))
            //    self._socket_read_byte_list(nbytes)
        }

        private void _get_gripper_fault()
        {
            //    self._rq_get_var(FLT, 2)
        }

        private void get_gripper_object()
        {
            //    self._rq_get_var(OBJ, 1)
        }

        private void get_gripper_status()
        {
            //    self._rq_get_var(STA, 1)
        }

        private void set_gripper_activate()
        {
            //    self._socket_set_var(GTO, 1, self.socket_name)
        }

        private void set_gripper_force(int value)
        {
            //    value = self._constrain_unsigned_char(value)
            //    self._socket_set_var(FOR, value, self.socket_name)
        }

        private void set_gripper_position(int value)
        {
            //    value = self._constrain_unsigned_char(value)
            //    self._socket_set_var(POS, value, self.socket_name)
        }

        private void set_gripper_speed(int value)
        {
            //    value = self._constrain_unsigned_char(value)
            //    self._socket_set_var(SPE, value, self.socket_name)
        }

        private void set_robot_activate(int value)
        {
            //    self._socket_set_var(ACT, 1, self.socket_name)
        }
    }
}
