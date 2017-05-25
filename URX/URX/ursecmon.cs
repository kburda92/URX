using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URX;

namespace URX
{
    class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {

        }
    }

    class Program
    {
        int program;
        public Program(int prog)
        {
            program = prog;
            //self.condition = Condition()
        }

        public override string ToString()
        {
            return string.Format("Program{0}", program);
        }
    }

    class TimeoutException
    {

    }

    public class Pair
    {
        public Pair(int first, int second)
        {
            First = first;
            Second = second;
        }

        public static bool operator < (Pair lhs, Pair rhs)
        {
            return lhs.First < rhs.First || (lhs.First == rhs.First && lhs.Second < rhs.Second);
        }

        public static bool operator >(Pair lhs, Pair rhs)
        {
            return rhs.First < lhs.First || (rhs.First == lhs.First && rhs.Second < lhs.Second);
        }

        public static bool operator <=(Pair lhs, Pair rhs)
        {
            return lhs.First < rhs.First || (lhs.First == rhs.First && lhs.Second <= rhs.Second);
        }

        public static bool operator >=(Pair lhs, Pair rhs)
        {
            return rhs.First < lhs.First || (rhs.First == lhs.First && rhs.Second <= lhs.Second);
        }

        public int First { get; set; }
        public int Second { get; set; }
    }


    public class ParserUtils
    {
        private Logger logger;
        public Pair version;
        public ParserUtils()
        {
            logger = new Logger("ursecmon");
            version = new Pair(0, 0);
        }

        public void parse(List<byte> data)
        {
            var allData = new Dictionary<string, List<byte>>();
            while(data.Count > 0)
            {
                var p = analyze_header(data);
                var psize = p.Item1;
                var ptype = p.Item2;
                var pdata = p.Item3;
                data = p.Item4.ToList();

                if(ptype == 16)
                {
                    allData["SecondaryClientData"] = get_data(pdata, "!iB", ("size", "type"));
                    // data = (pdata + data)[5:]  # This is the total size so we resend data to parser

                }
                else if(ptype == 0)
                {
                    if(psize == 38)
                    {
                        version.First = 3;
                        version.Second = 0;
                        allData['RobotModeData'] = get_data(pdata, "!IBQ???????BBdd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "controlMode", "speedFraction", "speedScaling"));
                    }
                    else if(psize == 46)
                    {
                        version.First = 3;
                        version.Second = 2;
                        allData['RobotModeData'] = get_data(pdata, "!IBQ???????BBdd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "controlMode", "speedFraction", "speedScaling", "speedFractionLimit"));
                    }
                    else
                        allData["RobotModeData"] = get_data(pdata, "!iBQ???????Bd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "speedFraction"))

                }
                else if (ptype == 1)
                {
                    var tmpstr = new List<string>();
                    tmpstr.Add("size");
                    tmpstr.Add("type");

                    for(int i=0;i<6;i++)
                    {
                        tmpstr.Add(string.Format("q_actual{0}", i));
                        tmpstr.Add(string.Format("q_target{0}", i));
                        tmpstr.Add(string.Format("qd_actual{0}", i));
                        tmpstr.Add(string.Format("I_actual{0}", i));
                        tmpstr.Add(string.Format("V_actual{0}", i));
                        tmpstr.Add(string.Format("T_motor{0}", i));
                        tmpstr.Add(string.Format("T_micro{0}", i));
                        tmpstr.Add(string.Format("jointMode{0}", i));
                    }
                    allData["JointData"] = get_data(pdata, "!iB dddffffB dddffffB dddffffB dddffffB dddffffB dddffffB", tmpstr)
                }
                else if (ptype == 4)
                {
                    if(version < new Pair(3,2))
                        allData["CartesianInfo"] = get_data(pdata, "iBdddddd", ("size", "type", "X", "Y", "Z", "Rx", "Ry", "Rz"))
                    else
                        allData["CartesianInfo"] = get_data(pdata, "iBdddddddddddd", ("size", "type", "X", "Y", "Z", "Rx", "Ry", "Rz", "tcpOffsetX", "tcpOffsetY", "tcpOffsetZ", "tcpOffsetRx", "tcpOffsetRy", "tcpOffsetRz"))
                }
                else if(ptype == 5)
                    allData["LaserPointer(OBSOLETE)"] = get_data(pdata, "iBddd", ("size", "type"));
                else if(ptype == 3)
                {
                    if (version >= new Pair(3, 0))
                        fmt = "iBiibbddbbddffffBBb";
                    else
                        fmt = "iBhhbbddbbddffffBBb";
                    //                allData["MasterBoardData"] = self._get_data(pdata, fmt, ("size", "type", "digitalInputBits", "digitalOutputBits", "analogInputRange0", "analogInputRange1", "analogInput0", "analogInput1", "analogInputDomain0", "analogInputDomain1", "analogOutput0", "analogOutput1", "masterBoardTemperature", "robotVoltage48V", "robotCurrent", "masterIOCurrent"))  # , "masterSafetyState" ,"masterOnOffState", "euromap67InterfaceInstalled"   ))

                }
                else if(ptype == 2)
                    allData["ToolData"] = get_data(pdata, "iBbbddfBffB", ("size", "type", "analoginputRange2", "analoginputRange3", "analogInput2", "analogInput3", "toolVoltage48V", "toolOutputVoltage", "toolCurrent", "toolTemperature", "toolMode"));
                else if (ptype == 9)
                    continue;
                else if(ptype == 8 && version >= new Pair(3,2))
                    allData["AdditionalInfo"] = get_data(pdata, "iB??", ("size", "type", "teachButtonPressed", "teachButtonEnabled"))
                else if(ptype == 7 && version >= new Pair(3,2))
                    allData["ForceModeData"] = get_data(pdata, "iBddddddd", ("size", "type", "x", "y", "z", "rx", "ry", "rz", "robotDexterity"))
                else if(ptype == 20)
                {
                    var tmp = get_data(pdata, "!iB Qbb", ("size", "type", "timestamp", "source", "robotMessageType"));
                    if (tmp["robotMessageType"] == 3)
                        allData["VersionMessage"] = get_data(pdata, "!iBQbb bAbBBiAb", ("size", "type", "timestamp", "source", "robotMessageType", "projectNameSize", "projectName", "majorVersion", "minorVersion", "svnRevision", "buildDate"))
                    else if (tmp["robotMessageType"] == 6)
                        allData["robotCommMessage"] = get_data(pdata, "!iBQbb iiAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "messageText"))
                    else if (tmp["robotMessageType"] == 1)
                        allData["labelMessage"] = get_data(pdata, "!iBQbb iAc", ("size", "type", "timestamp", "source", "robotMessageType", "id", "messageText"))
                    else if (tmp["robotMessageType"] == 2)
                        allData["popupMessage"] = get_data(pdata, "!iBQbb ??BAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "warning", "error", "titleSize", "messageTitle", "messageText"))
                    else if (tmp["robotMessageType"] == 0)
                        allData["messageText"] = get_data(pdata, "!iBQbb Ac", ("size", "type", "timestamp", "source", "robotMessageType", "messageText"))
                    else if (tmp["robotMessageType"] == 8)
                        allData["varMessage"] = get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))
                    else if (tmp["robotMessageType"] == 7)
                        allData["keyMessage"] = get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))
                    else if (tmp["robotMessageType"] == 5)
                        allData["keyMessage"] = get_data(pdata, "!iBQbb iiAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "messageText"))
                    else
                        logger.debug(string.Format("Message type parser not implemented {0}", tmp));
                }
                else
                    logger.debug(string.Format("Unknown packet type {0} with size {1}", ptype, psize));
            }
            return allData;
        }

        private Dictionary<string, List<byte>>get_data(List<byte> data, string fmt, List<string> names)
        {
            var tmpdata = data.ToList();
            fmt = fmt.Trim();
            var d = new Dictionary<string, List<byte>>();
            int i = 0;
            int j = 0;
            while(j < fmt.Length && i < names.Count)
            {
                var f = fmt[j];
                if (f == ' ' || f == '!' || f == '>' || f == '<')
                    j++;
                else if (f == 'A')
                {
                    int arraysize;
                    string asn;
                    if (j == fmt.Length - 2)
                        arraysize = tmpdata.Count;
                    else
                    {
                        asn = names[i - 1];
                        if (!asn.EndsWith("Size"))
                            throw new ParsingException(string.Format("Error, array without size ! {0} {1}", asn, i));
                        else
                            arraysize = 0;//TODO d[asn];
                    }

                    d[names[i]] = tmpdata.GetRange(0, arraysize);
                    tmpdata = tmpdata.GetRange(arraysize, tmpdata.Count);
                    j += 2;
                    i += 1;
                }
                else
                {
                    //                fmtsize = struct.calcsize(fmt[j])
                    // # print "reading ", f , i, j,  fmtsize, len(tmpdata)
                    if (tmpdata.Count < fmtsize)
                        throw new ParsingException("Error, length of data smaller than advertized: " + tmpdata.Count, fmtsize, "for names ", names, f, i, j);
                    // d[names[i]] = struct.unpack("!" + f, tmpdata[0:fmtsize])[0]
                    //# print names[i], d[names[i]]
                    tmpdata = tmpdata.GetRange(fmtsize, tmpdata.Count);
                    j++;
                    j++;
                }
            }
            return d;
        }

        public Tuple<int, int> get_header(List<byte> data)
        {
            //        return struct.unpack("!iB", data[0:5])
        }

        public Tuple<int, int, byte[], byte[]> analyze_header(List<byte> data)
        {
            if (data.Count < 5)
                throw new ParsingException(string.Format("Packet size %s smaller than header size (5 bytes)", data.Length));
            else
            {
                var p = get_header(data);
                var psize = p.Item1;
                var ptype = p.Item2;
                if (psize < 5)
                    throw new ParsingException(string.Format("Error, declared length of data smaller than its own header(5): " + psize));
                else if(psize > data.Count)
                    throw new ParsingException(string.Format("Error, length of data smaller ({0}) than declared ({1})", data.Count, psize));
                byte[] temp1 = new byte[psize];
                Array.Copy(data.ToArray(), 0, temp1, 0, psize);
                byte[] temp2 = new byte[data.Count - psize];
                Array.Copy(data.ToArray(), 0, temp2, 0, data.Count - psize);
                return Tuple.Create(psize, ptype, temp1, temp2);
            }   
        }

        public Tuple<List<byte>, List<byte>> find_first_packet(List<byte> data)
        {
            int counter = 0;
            int limit = 10;
            List<Byte> first, second;
            while(true)
            {
                if(data.Count >= 5)
                {
                    var p = get_header(data);
                    var psize = p.Item1;
                    var ptype = p.Item2;
                    if (psize < 5 || psize > 200 || ptype != 16)
                    {
                        data.RemoveAt(0);
                        counter++;
                        if(counter > limit)
                        {
                            logger.warning(string.Format("tried {0} times to find a packet in data, advertised packet size: {1}, type: {2}", counter, psize, ptype));
                            logger.warning(string.Format("Data length: {0}", data.Count));
                            limit *= 10;
                        }
                    }
                    else if(data.Count >= psize)
                    {
                        logger.debug(string.Format("Got packet with size {0} and type {1}", psize, ptype));
                        if (counter > 0)
                            logger.info(string.Format("Remove {0} bytes of garbage at begining of packet", counter));

                        first = new List<byte>();
                        second = new List<byte>();
                        foreach (var i in data)
                        {
                            if (i < psize)
                                first.Add(i);
                            else
                                second.Add(i);
                        }
                        return Tuple.Create(first, second);
                    }
                    else
                    {
                        // packet is not complete
                        logger.debug(string.Format("Packet is not complete, advertised size is {0}, received size is {1}, type is {2}", psize, data.Count, ptype));
                        return Tuple.Create<List<byte>, List<byte>>(null, null);
                    }
                }
                else
                {
                    logger.debug("data smaller than 5 bytes");
                    return Tuple.Create<List<byte>, List<byte>>(null, null);
                }
            }
           
        }
    }

    public class SecondaryMonitor
    {
        private ParserUtils parser;
        private Dictionary<string, string> dict;
        public string host;
        private bool trystop = false;
        public bool running = false;
        private Logger logger;
        public int lastpacket_timestamp = 0;
        private List<byte> dataqueue;

        public SecondaryMonitor(string host)
        {
            //    Thread.__init__(self)
            logger = new Logger("ursecmon");
            parser = new ParserUtils();

            //    self._dictLock = Lock()
            this.host = host;
            int secondary_port = 30002;
            //self._s_secondary = socket.create_connection((self.host, secondary_port), timeout = 0.5)
            //self._prog_queue = []
            //self._prog_queue_lock = Lock()
            dataqueue = new List<byte>();
            //self._dataEvent = Condition()

            start();
            wait();  
        }

        public void send_program(string prog)
        {
        //    prog.strip()
        //self.logger.debug("Enqueueing program: %s", prog)
        //if not isinstance(prog, bytes):
        //    prog = prog.encode()

        var data = Program(prog + "\n");
        //with data.condition:
        //    with self._prog_queue_lock:
        //    self._prog_queue.append(data)
        //    data.condition.wait()
        //    self.logger.debug("program sendt: %s", data)
        }

        public void run()
        {
            //            while not self._trystop:
            //            with self._prog_queue_lock:
            //            if len(self._prog_queue) > 0:
            //                    data = self._prog_queue.pop(0)
            //                    self._s_secondary.send(data.program)
            //                    with data.condition:
            //            data.condition.notify_all()

            //            data = self._get_data()
            //            try:
            //                tmpdict = self._parser.parse(data)
            //                with self._dictLock:
            //            self._dict = tmpdict
            //            except ParsingException as ex:
            //                self.logger.warning("Error parsing one packet from urrobot: %s", ex)
            //                continue

            //            if "RobotModeData" not in self._dict:
            //                self.logger.warning("Got a packet from robot without RobotModeData, strange ...")
            //                continue

            //            self.lastpacket_timestamp = time.time()

            //            rmode = 0
            //            if self._parser.version >= (3, 0):
            //                rmode = 7

            //            if self._dict["RobotModeData"]["robotMode"] == rmode \
            //                    and self._dict["RobotModeData"]["isRealRobotEnabled"] is True \
            //                    and self._dict["RobotModeData"]["isEmergencyStopped"] is False \
            //                    and self._dict["RobotModeData"]["isSecurityStopped"] is False \
            //                    and self._dict["RobotModeData"]["isRobotConnected"] is True \
            //                    and self._dict["RobotModeData"]["isPowerOnRobot"] is True:
            //                self.running = True
            //            else:
            {
                if (running)
                    logger.error("Robot not running: " + self._dict["RobotModeData"]);
                running = false;
            }
//            with self._dataEvent:
//# print("X: new data")
//            self._dataEvent.notifyAll()
        }

        private List<byte> get_data()
        {
            while(true)
            {
                //logger.debug(string.Format("data queue size is: {0}"),dataqueue.Length);

                var ans = parser.find_first_packet(dataqueue);

                if(ans.Item1 != null && ans.Item2 != null)
                {
                    dataqueue = ans.Item2;
                    //logger.debug(string.Format("found packet of size {0}", ans.Item1));
                    return ans.Item1;
                }
                else
                {
                    logger.debug("Could not find packet in received data");
                    var tmp = s_secondary.recv(1024);
                    dataqueue.Add(tmp);
                }
            }
        }

        public void wait(double timeout = 0.5)
        {
            var tstamp = lastpacket_timestamp;
        //with self._dataEvent:
        //    self._dataEvent.wait(timeout)
        //    if tstamp == self.lastpacket_timestamp:
        //        raise TimeoutException("Did not receive a valid data packet from robot in {}".format(timeout))
        }

        public Dictionary<string, int> get_cartesian_info(bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //    if "CartesianInfo" in self._dict:
            //        return self._dict["CartesianInfo"]
            //    else:
            //        return None
            return null;
        }

        public void get_all_data(bool wait = false)
        {
            if (wait)
                this.wait();
        //with self._dictLock:
            //return self._dict.copy()
        }

        public void get_joint_data(bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //if "JointData" in self._dict:
            //    return self._dict["JointData"]
            //else:
            //    return None
        }

        public void get_digital_out(int nb, bool wait = false)
        {
            if (wait)
                this.wait();
        //    with self._dictLock:
        //    output = self._dict["MasterBoardData"]["digitalOutputBits"]
        //mask = 1 << nb
        //if output & mask:
        //    return 1
        //else:
        //    return 0
        }

        public void get_digital_out_bits(bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //return self._dict["MasterBoardData"]["digitalOutputBits"]
        }

        public void get_digital_in(int nb, bool wait = false)
        {
            if (wait)
                this.wait();
            //    with self._dictLock:
            //    output = self._dict["MasterBoardData"]["digitalInputBits"]
            //mask = 1 << nb
            //if output & mask:
            //    return 1
            //else:
            //    return 0
        }

        public void get_digital_in_bits(bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //return self._dict["MasterBoardData"]["digitalInputBits"]
        }

        public void get_analog_in(int nb, bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //return self._dict["MasterBoardData"]["analogInput" + str(nb)]
        }

        public void get_analog_inputs(bool wait = false)
        {
            if (wait)
                this.wait();
            //with self._dictLock:
            //return self._dict["MasterBoardData"]["analogInput0"], self._dict["MasterBoardData"]["analogInput1"]
        }

        public bool is_program_running(bool wait = false)
        {
            return true;
            if (wait)
                this.wait();
            //with self._dictLock:
            //return self._dict["RobotModeData"]["isProgramRunning"]
        }

        public void close()
        {
            trystop = true;
            join();
            //if self._s_secondary:
            //with self._prog_queue_lock:
            //self._s_secondary.close()
        }
    }
}
