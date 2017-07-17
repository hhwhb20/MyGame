using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;
using Common;
using MyGameServer.Handle;
using MyGameServer.Model;

namespace MyGameServer
{
    //所有的server端 主类都要集成自applicationbase
    class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public MapServer map;



        public static MyGameServer Instance
        {
            get;
            private set;
        }

        public Dictionary<OperationCode, BaseHandle> HandlerDict = new Dictionary<OperationCode, BaseHandle>();
        public List<ClientPeer> peerList = new List<ClientPeer>();//通过这个集合可以访问到所有客户端的peer，从而向任何一个客户端发送数据 
        //当一个客户端请求链接的时候
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接进来了");
            ClientPeer peer = new ClientPeer(initRequest);
            peerList.Add(peer);
            return peer;
        }

        //初始化
        protected override void Setup()
        {
            Instance = this;
            //日志的初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            FileInfo configFileInfo = new FileInfo( Path.Combine(this.BinaryPath, "log4net.config"));
            if(configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); //让photon知道使用哪个插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo); //让log4net这个插件读取配置文件
            }

            log.Info("SetUp Completed!");

            //InitHandler();
            InitMap();
            InitHandler();
        }

        public void InitHandler()
        {
            LoginHandle loginHandler = new LoginHandle();
            HandlerDict.Add(loginHandler.opCode, loginHandler);
            DefaultHandle defaultHandle = new DefaultHandle();
            HandlerDict.Add(defaultHandle.opCode, defaultHandle);
            RegisterHandle regiseterhandler = new RegisterHandle();
            HandlerDict.Add(regiseterhandler.opCode, regiseterhandler);
            MatchHandle matchHandler = new MatchHandle();
            HandlerDict.Add(matchHandler.opCode, matchHandler);
        }

        public void InitMap()
        {
            log.Info("MapInit Start!");
            map = new MapServer();
            string path = @"bin/Map/map.xml";
            map.Init(path);
        }

        //Server端关闭的时候
        protected override void TearDown()
        {
        }

        public void PrintLog(string tip)
        {
            log.Info(tip);
        }

        public long GenerateIntID()
        {

            byte[] buffer = Guid.NewGuid().ToByteArray();

            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
