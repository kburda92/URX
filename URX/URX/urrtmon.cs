using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URX
{
    //rtstruct692 = struct.Struct('>d6d6d6d6d6d6d6d6d18d6d6d6dQ')
    //rtstruct540 = struct.Struct('>d6d6d6d6d6d6d6d6d18d')

    public class URRTMonitor
    {
        public bool daemon;
        private bool stop_event, buffering;
        string urHost;
        private Transform csys;
        public URRTMonitor(string urHost)
        {
            //threading.Thread.__init__(self)
            //self.logger = logging.getLogger(self.__class__.__name__)
            daemon = true;
            stop_event = true;
            //self._dataEvent = threading.Condition()
            //self._dataAccess = threading.Lock()
            //self._rtSock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            //self._rtSock.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
            this.urHost = urHost;
            //# Package data variables
            //self._timestamp = None
            //self._ctrlTimestamp = None
            //self._qActual = None
            //self._qTarget = None
            //self._tcp = None
            //self._tcp_force = None
            //self.__recvTime = 0
            //self._last_ctrl_ts = 0
            buffering = false;
            //self._buffer_lock = threading.Lock()
            //self._buffer = []
            //self._csys_lock = threading.Lock()
        }

        public void set_csys(Transform csys)
        {
            //    with self._csys_lock:
            this.csys = csys;
        }


        //def __recv_bytes(self, nBytes):
        private void recv_bytes(int nBytes)
        {
            //    recvTime = 0
            //    pkg = b''
            //    while len(pkg) < nBytes:
            //        pkg += self._rtSock.recv(nBytes - len(pkg))
            //        if recvTime == 0:
            //            recvTime = time.time()
            //    self.__recvTime = recvTime
            //    return pkg
        }

        public void wait()
        {
            //    with self._dataEvent:
            //        self._dataEvent.wait()
        }

        public void q_actual(bool wait = false, bool timestamp = false)
        {
            if (wait)
                wait();
            //    with self._dataAccess:
            //        if timestamp:
            //            return self._timestamp, self._qActual
            //        else:
            //            return self._qActual
        }
        //getActual = q_actual

        public void q_target(bool wait = false, bool timestamp = false)
        {
            if (wait)
                wait();
            //    with self._dataAccess:
            //        if timestamp:
            //            return self._timestamp, self._qTarget
            //        else:
            //            return self._qTarget
        }
        //getTarget = q_target

        public void tcf_pose(bool wait = false, bool timestamp = false, bool ctrlTimestamp = false)
        {
            if (wait)
                wait();
            //    with self._dataAccess:
            //        tcf = self._tcp
            //        if ctrlTimestamp or timestamp:
            //            ret = [tcf]
            //            if timestamp:
            //                ret.insert(-1, self._timestamp)
            //            if ctrlTimestamp:
            //                ret.insert(-1, self._ctrlTimestamp)
            //            return ret
            //        else:
            //            return tcf
        }
        //getTCF = tcf_pose

        public void tcf_force(bool wait = false, bool timestamp = false)
        {
            if (wait)
                wait();
            //    with self._dataAccess:
            //        # tcf = self._fwkin(self._qActual)
            //        tcf_force = self._tcp_force
            //        if timestamp:
            //            return self._timestamp, tcf_force
            //        else:
            //            return tcf_force
        }
        //getTCFForce = tcf_force

        //def __recv_rt_data(self):
        private void recv_rt_data()
        {
            //    head = self.__recv_bytes(4)
            //    # Record the timestamp for this logical package
            //    timestamp = self.__recvTime
            //    pkgsize = struct.unpack('>i', head)[0]
            //    self.logger.debug(
            //        'Received header telling that package is %s bytes long',
            //        pkgsize)
            //    payload = self.__recv_bytes(pkgsize - 4)
            //    if pkgsize >= 692:
            //        unp = self.rtstruct692.unpack(payload[:self.rtstruct692.size])
            //    elif pkgsize >= 540:
            //        unp = self.rtstruct540.unpack(payload[:self.rtstruct540.size])
            //    else:
            //        self.logger.warning(
            //            'Error, Received packet of length smaller than 540: %s ',
            //            pkgsize)
            //        return

            //    with self._dataAccess:
            //        self._timestamp = timestamp
            //        # it seems that packet often arrives packed as two... maybe TCP_NODELAY is not set on UR controller??
            //        # if (self._timestamp - self._last_ts) > 0.010:
            //        # self.logger.warning("Error the we did not receive a packet for {}s ".format( self._timestamp - self._last_ts))
            //        # self._last_ts = self._timestamp
            //        self._ctrlTimestamp = np.array(unp[0])
            //        if self._last_ctrl_ts != 0 and (
            //                self._ctrlTimestamp -
            //                self._last_ctrl_ts) > 0.010:
            //            self.logger.warning(
            //                "Error the controller failed to send us a packet: time since last packet %s s ",
            //                self._ctrlTimestamp - self._last_ctrl_ts)
            //        self._last_ctrl_ts = self._ctrlTimestamp
            //        self._qActual = np.array(unp[31:37])
            //        self._qTarget = np.array(unp[1:7])
            //        self._tcp_force = np.array(unp[67:73])
            //        self._tcp = np.array(unp[73:79])

            //        if self._csys:
            //            with self._csys_lock:
            //                # might be a godd idea to remove dependancy on m3d
            //                tcp = self._csys.inverse * m3d.Transform(self._tcp)
            //            self._tcp = tcp.pose_vector
            //    if self._buffering:
            //        with self._buffer_lock:
            //            self._buffer.append(
            //                (self._timestamp,
            //                 self._ctrlTimestamp,
            //                 self._tcp,
            //                 self._qActual))  # FIXME use named arrays of allow to configure what data to buffer

            //    with self._dataEvent:
            //        self._dataEvent.notifyAll()
        }

        public void start_buffering()
        {
            //self._buffer = []
            buffering = true;
        }

        public void stop_buffering()
        {
            buffering = false;
        }

        public void try_pop_buffer()
        {
            //    with self._buffer_lock:
            //        if len(self._buffer) > 0:
            //            return self._buffer.pop(0)
            //        else:
            //            return None
        }

        public void pop_buffer()
        {
            //    while True:
            //        with self._buffer_lock:
            //            if len(self._buffer) > 0:
            //                return self._buffer.pop(0)
            //        time.sleep(0.001)
        }

        public void get_buffer()
        {
            //    with self._buffer_lock:
            //        return deepcopy(self._buffer)
        }

        public void get_all_data(bool wait = true)
        {
            if (wait)
                wait();
            //    with self._dataAccess:
            //        return dict(
            //            timestamp=self._timestamp,
            //            ctrltimestamp=self._ctrlTimestamp,
            //            qActual=self._qActual,
            //            qTarget=self._qTarget,
            //            tcp=self._tcp,
            //            tcp_force=self._tcp_force)
        }

        public void stop()
        {
            stop_event = true;
        }

        public void close()
        {
            stop();
            join();
        }

        public void run()
        {
            stop_event = false;
            //    self._rtSock.connect((self._urHost, 30003))
            //    while not self._stop_event:
            //        self.__recv_rt_data()
            //    self._rtSock.close()
        }
    }
}
