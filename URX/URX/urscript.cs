using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{

    class URScript
    {
        public static int[] CONTROLLER_PORTS = { 0, 1 };
        public static int[] CONTROLLER_VOLTAGE = {
            0,  // 0-5V
            2,  // 0-10V
        };

        public static int[] TOOL_PORTS = { 2, 3 };
        public static int[] TOOL_VOLTAGE = {
            0,  // 0-5V
            1,  // 0-10V
            2,  // 4-20mA
        };

        public static int[] OUTPUT_DOMAIN_VOLTAGE = {
            0,  // 4-20mA
            1,  // 0-10V
        };
        byte[] header = new byte[100];
        byte[] program = new byte[100];

        public string call()
        {
            if(program.Length == 0)
            {
                //        self.logger.debug(u"urscript program is empty")
                return "";
            }

            var myprog = string.Format("def myProg():{0}\nend", program);

            string script = "";
            if(header.Length > 0)
                script = string.Format("{0}\n\n", header);
            script = string.Format("{0}{1}", script, myprog);
            return script;
        }

        public void reset()
        {
            Array.Clear(header, 0, header.Length);
            Array.Clear(program, 0, program.Length);
        }

        public void add_header_to_program(string header_line)
        {
            header = Encoding.ASCII.GetBytes(string.Format("{0}\n{1}", header, header_line));
        }

        public void add_line_to_program(string new_line)
        {
            program = Encoding.ASCII.GetBytes(string.Format("{0}\n\t{1}", program, new_line));
        }

        private int constrain_unsigned_char(int value)
        {
            if (value < 0)
                value = 0;
            else if (value > 255)
                value = 255;
            return value;
        }

        public void set_analog_inputrange(int port, int vrange)
        {
            //    if port in CONTROLLER_PORTS:
            //        assert(vrange in CONTROLLER_VOLTAGE)
            //    elif port in TOOL_PORTS:
            //        assert(vrange in TOOL_VOLTAGE)
            var msg = string.Format("set_analog_inputrange({0},{1})", port, vrange);
            add_line_to_program(msg);
        }

        public void set_analog_output(int input_id, int signal_level)
        {
            //    assert(input_id in [0, 1])
            //    assert(signal_level in [0, 1])
            string msg = string.Format("set_analog_output({0}, {1})", input_id, signal_level);
            add_line_to_program(msg);
        }

        public void set_analog_outputdomain(int port, int domain)
        {
            //    assert(domain in OUTPUT_DOMAIN_VOLTAGE)
            string msg = string.Format("set_analog_outputdomain({0},{1})", port, domain);
            add_line_to_program(msg);
        }

        public void set_payload(double mass, List<int> cog = null)
        {
            string msg = string.Format("set_payload({0}", mass);
            if (cog.Count > 0)
            {
                Debug.Assert(cog.Count == 3);
                msg = string.Format("{0},{1}", msg, cog);
            }
            msg = string.Format("{})", msg);
            add_line_to_program(msg);

        }

        public void set_runstate_outputs(List<int> outputs = null)
        {
            if (outputs == null)
                outputs = new List<int>();
            var msg = string.Format("set_runstate_outputs({0}))", outputs);
            add_line_to_program(msg);
        }

        public void set_tool_voltage(int voltage)
        {
            Debug.Assert(voltage == 0 || voltage == 12 || voltage == 24);
            var msg = string.Format("set_tool_voltage({})", voltage);
            add_line_to_program(msg);
        }

        public void sleep(double value)
        {
            var msg = string.Format("sleep({0})", value);
            add_line_to_program(msg);
        }

        private void socket_close(string socket_name)
        {
            var msg = string.Format("socket_close(\"{0}\")", socket_name);
            add_line_to_program(msg);
        }

        private void socket_get_var(int var, string socket_name)
        {
            var msg = string.Format("socket_get_var(\"{0}\",\"{1}\")", var, socket_name);
            add_line_to_program(msg);
            sync();
        }

        private void socket_open(int var, string socket_host, int socket_port, string socket_name)
        {
            var msg = string.Format("socket_open(\"{}\",{},\"{}\")", socket_host, socket_port, socket_name);
            add_line_to_program(msg);
        }

        private void socket_read_byte_list(int nbytes, string socket_name)
        {
            var msg = string.Format("global var_value = socket_read_byte_list({},\"{}\")", nbytes, socket_name);
            add_line_to_program(msg);
            sync();
        }

        private void socket_send_string(string message, string socket_name)
        {
            var msg = string.Format("socket_send_string(\"{}\",\"{}\")", message, socket_name);
            add_line_to_program(msg);
            sync();
        }
 
        private void socket_set_var(int var, int value, string socket_name)
        {
            var msg = string.Format("socket_set_var(\"{}\",{},\"{}\")", var, value, socket_name);
            add_line_to_program(msg);
            sync();
        }

        private void sync()
        {
            var msg = "sync()";
            add_line_to_program(msg);
        }
    }
}
