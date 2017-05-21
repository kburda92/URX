﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    class ParsingException
    {
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

    class ParserUtils
    {
        public ParserUtils()
        {
            //        self.logger = logging.getLogger("ursecmon")
            //        self.version = (0, 0)
        }

        public void parse(int data)
        {
            Dictionary<string, string> allData;

            //        while data:
            //            psize, ptype, pdata, data = self.analyze_header(data)
            //            # print "We got packet with size %i and type %s" % (psize, ptype)
            //            if ptype == 16:
            //                allData["SecondaryClientData"] = self._get_data(pdata, "!iB", ("size", "type"))
            //                data = (pdata + data)[5:]  # This is the total size so we resend data to parser
            //            elif ptype == 0:
            //                # this parses RobotModeData for versions >=3.0 (i.e. 3.0)
            //                if psize == 38:
            //                    self.version = (3, 0)
            //                    allData['RobotModeData'] = self._get_data(pdata, "!IBQ???????BBdd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "controlMode", "speedFraction", "speedScaling"))
            //                elif psize == 46:  # It's 46 bytes in 3.2
            //                    self.version = (3, 2)
            //                    allData['RobotModeData'] = self._get_data(pdata, "!IBQ???????BBdd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "controlMode", "speedFraction", "speedScaling", "speedFractionLimit"))
            //                else:
            //                    allData["RobotModeData"] = self._get_data(pdata, "!iBQ???????Bd", ("size", "type", "timestamp", "isRobotConnected", "isRealRobotEnabled", "isPowerOnRobot", "isEmergencyStopped", "isSecurityStopped", "isProgramRunning", "isProgramPaused", "robotMode", "speedFraction"))
            //            elif ptype == 1:
            //                tmpstr = ["size", "type"]
            //                for i in range(0, 6):
            //                    tmpstr += ["q_actual%s" % i, "q_target%s" % i, "qd_actual%s" % i, "I_actual%s" % i, "V_actual%s" % i, "T_motor%s" % i, "T_micro%s" % i, "jointMode%s" % i]

            //allData["JointData"] = self._get_data(pdata, "!iB dddffffB dddffffB dddffffB dddffffB dddffffB dddffffB", tmpstr)

            //            elif ptype == 4:
            //                if self.version< (3, 2):
            //                    allData["CartesianInfo"] = self._get_data(pdata, "iBdddddd", ("size", "type", "X", "Y", "Z", "Rx", "Ry", "Rz"))
            //                else:
            //                    allData["CartesianInfo"] = self._get_data(pdata, "iBdddddddddddd", ("size", "type", "X", "Y", "Z", "Rx", "Ry", "Rz", "tcpOffsetX", "tcpOffsetY", "tcpOffsetZ", "tcpOffsetRx", "tcpOffsetRy", "tcpOffsetRz"))
            //            elif ptype == 5:
            //                allData["LaserPointer(OBSOLETE)"] = self._get_data(pdata, "iBddd", ("size", "type"))
            //            elif ptype == 3:

            //                if self.version >= (3, 0):
            //                    fmt = "iBiibbddbbddffffBBb"     # firmware >= 3.0
            //                else:
            //                    fmt = "iBhhbbddbbddffffBBb"     # firmware < 3.0

            //                allData["MasterBoardData"] = self._get_data(pdata, fmt, ("size", "type", "digitalInputBits", "digitalOutputBits", "analogInputRange0", "analogInputRange1", "analogInput0", "analogInput1", "analogInputDomain0", "analogInputDomain1", "analogOutput0", "analogOutput1", "masterBoardTemperature", "robotVoltage48V", "robotCurrent", "masterIOCurrent"))  # , "masterSafetyState" ,"masterOnOffState", "euromap67InterfaceInstalled"   ))
            //            elif ptype == 2:
            //                allData["ToolData"] = self._get_data(pdata, "iBbbddfBffB", ("size", "type", "analoginputRange2", "analoginputRange3", "analogInput2", "analogInput3", "toolVoltage48V", "toolOutputVoltage", "toolCurrent", "toolTemperature", "toolMode"))
            //            elif ptype == 9:
            //                continue  # This package has a length of 53 bytes. It is used internally by Universal Robots software only and should be skipped.
            //            elif ptype == 8 and self.version >= (3, 2):
            //                allData["AdditionalInfo"] = self._get_data(pdata, "iB??", ("size", "type", "teachButtonPressed", "teachButtonEnabled"))
            //            elif ptype == 7 and self.version >= (3, 2):
            //                allData["ForceModeData"] = self._get_data(pdata, "iBddddddd", ("size", "type", "x", "y", "z", "rx", "ry", "rz", "robotDexterity"))
            //            # elif ptype == 8:
            //            #     allData["varMessage"] = self._get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))
            //            # elif ptype == 7:
            //            #     allData["keyMessage"] = self._get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))

            //            elif ptype == 20:
            //                tmp = self._get_data(pdata, "!iB Qbb", ("size", "type", "timestamp", "source", "robotMessageType"))
            //                if tmp["robotMessageType"] == 3:
            //                    allData["VersionMessage"] = self._get_data(pdata, "!iBQbb bAbBBiAb", ("size", "type", "timestamp", "source", "robotMessageType", "projectNameSize", "projectName", "majorVersion", "minorVersion", "svnRevision", "buildDate"))
            //                elif tmp["robotMessageType"] == 6:
            //                    allData["robotCommMessage"] = self._get_data(pdata, "!iBQbb iiAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "messageText"))
            //                elif tmp["robotMessageType"] == 1:
            //                    allData["labelMessage"] = self._get_data(pdata, "!iBQbb iAc", ("size", "type", "timestamp", "source", "robotMessageType", "id", "messageText"))
            //                elif tmp["robotMessageType"] == 2:
            //                    allData["popupMessage"] = self._get_data(pdata, "!iBQbb ??BAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "warning", "error", "titleSize", "messageTitle", "messageText"))
            //                elif tmp["robotMessageType"] == 0:
            //                    allData["messageText"] = self._get_data(pdata, "!iBQbb Ac", ("size", "type", "timestamp", "source", "robotMessageType", "messageText"))
            //                elif tmp["robotMessageType"] == 8:
            //                    allData["varMessage"] = self._get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))
            //                elif tmp["robotMessageType"] == 7:
            //                    allData["keyMessage"] = self._get_data(pdata, "!iBQbb iiBAcAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "titleSize", "messageTitle", "messageText"))
            //                elif tmp["robotMessageType"] == 5:
            //                    allData["keyMessage"] = self._get_data(pdata, "!iBQbb iiAc", ("size", "type", "timestamp", "source", "robotMessageType", "code", "argument", "messageText"))
            //                else:
            //                    self.logger.debug("Message type parser not implemented %s", tmp)
            //            else:
            //                self.logger.debug("Unknown packet type %s with size %s", ptype, psize)

            //        return allData

        }

        private void get_data(int data, int fmt, int names)
        {
            //        tmpdata = copy(data)
            //        fmt = fmt.strip()  # space may confuse us
            //        d = dict()
            //        i = 0
            //        j = 0
            //        while j<len(fmt) and i<len(names):
            //            f = fmt[j]
            //            if f in (" ", "!", ">", "<"):
            //                j += 1
            //            elif f == "A":  # we got an array
            //                # first we need to find its size
            //                if j == len(fmt) - 2:  # we are last element, size is the rest of data in packet
            //                    arraysize = len(tmpdata)
            //                else:  # size should be given in last element
            //                    asn = names[i - 1]
            //                    if not asn.endswith("Size"):
            //                        raise ParsingException("Error, array without size ! %s %s" % (asn, i))
            //                    else:
            //                        arraysize = d[asn]
            //                d[names[i]] = tmpdata[0:arraysize]
            //                # print "Array is ", names[i], d[names[i]]
            //                tmpdata = tmpdata[arraysize:]
            //                j += 2
            //                i += 1
            //            else:
            //                fmtsize = struct.calcsize(fmt[j])
            //                # print "reading ", f , i, j,  fmtsize, len(tmpdata)
            //                if len(tmpdata) < fmtsize:  # seems to happen on windows
            //                    raise ParsingException("Error, length of data smaller than advertized: ", len(tmpdata), fmtsize, "for names ", names, f, i, j)
            //                d[names[i]] = struct.unpack("!" + f, tmpdata[0:fmtsize])[0]
            //# print names[i], d[names[i]]
            //tmpdata = tmpdata[fmtsize:]
            //j += 1
            //                i += 1
            //        return d
        }

        public void get_header(int data)
        {
            //        return struct.unpack("!iB", data[0:5])
        }

        public void analyze_header(int data)
        {
            //        if len(data) < 5:
            //            raise ParsingException("Packet size %s smaller than header size (5 bytes)" % len(data))
            //        else:
            //            psize, ptype = self.get_header(data)
            //            if psize< 5:
            //                raise ParsingException("Error, declared length of data smaller than its own header(5): ", psize)
            //            elif psize > len(data):
            //                raise ParsingException("Error, length of data smaller (%s) than declared (%s)" % (len(data), psize))
            //        return psize, ptype, data[:psize], data[psize:]
        }

        public void find_first_packet(int data)
        {
            //        counter = 0
            //        limit = 10
            //        while True:
            //            if len(data) >= 5:
            //                psize, ptype = self.get_header(data)
            //                if psize< 5 or psize> 2000 or ptype != 16:
            //                    data = data[1:]
            //                    counter += 1
            //                    if counter > limit:
            //                        self.logger.warning("tried %s times to find a packet in data, advertised packet size: %s, type: %s", counter, psize, ptype)
            //                        self.logger.warning("Data length: %s", len(data))
            //                        limit = limit* 10
            //                elif len(data) >= psize:
            //                    self.logger.debug("Got packet with size %s and type %s", psize, ptype)
            //                    if counter:
            //                        self.logger.info("Remove %s bytes of garbage at begining of packet", counter)
            //                    # ok we we have somehting which looks like a packet"
            //                    return (data[:psize], data[psize:])
            //                else:
            //                    # packet is not complete
            //                    self.logger.debug("Packet is not complete, advertised size is %s, received size is %s, type is %s", psize, len(data), ptype)
            //                    return None
            //            else:
            //                # self.logger.debug("data smaller than 5 bytes")
            //                return None
        }
    }

    public class SecondaryMonitor
    {
        private ParserUtils parser;
        private Dictionary<string, string> dict;
        public string host;
        private bool trystop = false;
        public bool running = false;
        public int lastpacket_timestamp = 0;

        public SecondaryMonitor(string host)
        {
            //    Thread.__init__(self)
            //self.logger = logging.getLogger("ursecmon")
            parser = new ParserUtils();
            //    self._dictLock = Lock()
            this.host = host;
            int secondary_port = 30002;
            //self._s_secondary = socket.create_connection((self.host, secondary_port), timeout = 0.5)
            //self._prog_queue = []
            //self._prog_queue_lock = Lock()
            //self._dataqueue = bytes()
            //self._dataEvent = Condition()

            start();
            wait();  
        }

        public void send_program(int prog)
        {
        //    prog.strip()
        //self.logger.debug("Enqueueing program: %s", prog)
        //if not isinstance(prog, bytes):
        //    prog = prog.encode()

        //data = Program(prog + b"\n")
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
//                if self.running:
//                    self.logger.error("Robot not running: " + str(self._dict["RobotModeData"]))
//                self.running = False
//            with self._dataEvent:
//# print("X: new data")
//            self._dataEvent.notifyAll()
        }

        private void get_data()
        {
            //while True:
            //# self.logger.debug("data queue size is: {}".format(len(self._dataqueue)))
            //ans = self._parser.find_first_packet(self._dataqueue[:])
            //if ans:
            //    self._dataqueue = ans[1]
            //    # self.logger.debug("found packet of size {}".format(len(ans[0])))
            //    return ans[0]
            //else:
            //    # self.logger.debug("Could not find packet in received data")
            //    tmp = self._s_secondary.recv(1024)
            //    self._dataqueue += tmp
        }

        public void wait(double timeout = 0.5)
        {
        //    tstamp = self.lastpacket_timestamp
        //with self._dataEvent:
        //    self._dataEvent.wait(timeout)
        //    if tstamp == self.lastpacket_timestamp:
        //        raise TimeoutException("Did not receive a valid data packet from robot in {}".format(timeout))
        }

        public Vector get_cartesian_info(bool wait = false)
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
