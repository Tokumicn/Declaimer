using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using DeclaimerCommon;
using Symbol.RFID3;
using System.Data;


namespace WaitingGodotDeclaimer
{
    public class ZebraRFIDFixedReader : IDeclaimer
    {
        /// <summary>
        /// 读取器对象
        /// </summary>
        private RFIDReader m_RFIDReader = null;
        /// <summary>
        /// 读取电子标签事件 返回值：Object[]
        /// </summary>
        public event TagObjEventHandler GetRFIDTagObj;

        /// <summary>
        /// 读取电子标签事件 返回值：字符串
        /// </summary>
        public event TagStrEventHandler GetRFIDTagStr;
        /// <summary>
        /// 允许建立连接
        /// </summary>
        public bool IsAllowConnect
        {
            get;
            set;
        }
        /// <summary>
        /// 允许开启读取
        /// </summary>
        public bool IsAllowStartRead
        {
            get;
            set;
        }
        /// <summary>
        /// 连接状态
        /// </summary>
        private bool m_IsConnected = false;
        /// <summary>
        /// 是否已经开始读取
        /// </summary>
        public bool m_IsReading = false;
        /// <summary>
        /// 是否连续读取
        /// </summary>
        public bool IsContinuousRead = false;
        /// <summary>
        /// 以读取标签存储区  防止重复读取
        /// </summary>
        public Hashtable m_TagTable;
        /// <summary>
        /// 目前还没搞明白
        /// </summary>
        private bool m_ReaderInitiatedDisconnectionReceived = false;

        /// <summary>
        /// 读取器IP地址
        /// </summary>
        private string ReaderIP { get; set; }
        /// <summary>
        /// 读取器端口号
        /// </summary>
        private string ReaderPort { get; set; }
        /// <summary>
        /// 读取器连接超时时间
        /// </summary>
        private string ConnectTimeOut { get; set; }
        /// <summary>
        /// 读取器功率设置  0对应1号天线强度
        /// </summary>
        private List<string> ReaderPowerList { get; set; }

        /// <summary>
        /// 读取器开启哪些天线  1,2,3,4
        /// </summary>
        private List<string> ReaderAntennasList { get; set; }

        private PostFilter m_PostFilter = null;
        /// <summary>
        /// Get PostFilter
        /// </summary>
        /// <returns></returns>
        public Symbol.RFID3.PostFilter GetPostFilter()
        {
            return m_PostFilter;
        }

        /// <summary>
        /// 是否使用过滤
        /// </summary>
        private Boolean m_UseAccessFilter = false;
        private AccessFilter m_AccessFilter = null;
        /// <summary>
        /// Get AccessFilter
        /// </summary>
        private Symbol.RFID3.AccessFilter GetAccessFilter()
        {
            return m_UseAccessFilter == true ? m_AccessFilter : null;
        }

        private RssiRangeFilter rssiRangeFilter;
        /// <summary>
        /// 设置RSSI范围
        /// </summary>
        /// <param name="lowerRSSI"></param>
        /// <param name="upperRSSI"></param>
        /// <returns></returns>
        public void SetRssiRangeFilter(SByte lowerRSSI, SByte upperRSSI)
        {
            rssiRangeFilter = new RssiRangeFilter()
            {
                MatchRange = MATCH_RANGE.WITHIN_RANGE,
                PeakRSSILowerLimit = lowerRSSI,
                PeakRSSIUpperLimit = upperRSSI
            };
            //启用过滤器
            m_UseAccessFilter = true;
            m_AccessFilter.RssiRangeFilter = rssiRangeFilter;
        }

        private Symbol.RFID3.TriggerInfo m_TriggerInfo = null;
        /// <summary>
        /// Get Trigger
        /// </summary>
        /// <returns></returns>
        public Symbol.RFID3.TriggerInfo GetTriggerInfo()
        {
            if (null == m_TriggerInfo)
            {
                m_TriggerInfo = new TriggerInfo();
            }
            m_TriggerInfo.EnableTagEventReport = false;
            return m_TriggerInfo;
        }

        private Symbol.RFID3.AntennaInfo m_AntennaList = null;
        /// <summary>
        /// Get Antenna
        /// </summary>
        /// <returns></returns>
        public Symbol.RFID3.AntennaInfo GetAntennaInfo()
        {
            return m_AntennaList;
        }

        /// <summary>
        /// 初始化读取器
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="strPort"></param>
        /// <param name="connTimeOut"></param>
        /// <param name="readerPowerList"></param>
        /// <param name="readerAntennasList"></param>
        /// <returns></returns>
        public ReturnMessage InitReader(string strIP, string strPort, string connTimeOut, List<string> readerPowerList, List<string> readerAntennasList)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            //初始化   Fixed Reader日志记录对象
            DeclaimerReaderLog.InstanceLogger(LoggerClassType.DeclaimerWaitingGodotRollingFileAppender);

            //默认值设置
            if (string.IsNullOrEmpty(strPort))
            {
                strPort = "5084";
            }
            if (string.IsNullOrEmpty(connTimeOut))
            {
                connTimeOut = "0";
            }
            if (readerPowerList == null)
            {
                readerPowerList = new List<string>() { "3000", "3000", "3000", "3000" };
            }
            if (readerAntennasList == null)
            {
                readerAntennasList = new List<string>() { "1", "2", "3", "4" };
            }
            this.ReaderIP = strIP;
            this.ReaderPort = strPort;
            this.ConnectTimeOut = connTimeOut;
            this.ReaderPowerList = readerPowerList;
            this.ReaderAntennasList = readerAntennasList;
            m_TagTable = new Hashtable(1023);

            //默认全开 AvailableAntennas
            ushort[] antList = new ushort[] { 1, 2, 3, 4 };
            m_AntennaList = new Symbol.RFID3.AntennaInfo(antList);
            //创建读取器对象
            m_RFIDReader = new RFIDReader(this.ReaderIP, uint.Parse(this.ReaderPort), uint.Parse(this.ConnectTimeOut));

            //设置允许连接
            IsAllowConnect = true;

            returnMsg.CallMessage = "Reader Init Success.";
            returnMsg.CallStatus = true;
            //日志记录
            DeclaimerReaderLog.Info("[Reader Init Success.] ==== [" + DateTime.Now.ToString() + "]");

            return returnMsg;
        }

        /// <summary>
        /// 打开读取器连接
        /// </summary>
        /// <returns></returns>
        public ReturnMessage OpenReaderConnection()
        {
            ReturnMessage returnMsg = new ReturnMessage();

            if (m_RFIDReader == null)
            {
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "读取器开启失败 : 读取器对象为空.";
                //日志记录
                DeclaimerReaderLog.Error("[读取器开启失败 : 读取器对象为空.] ==== [" + DateTime.Now.ToString() + "]");
            }

            try
            {
                if (!m_RFIDReader.IsConnected)
                {
                    //连接操作
                    m_RFIDReader.Connect();
                    //允许开始读取
                    IsAllowStartRead = true;
                    //设置功率
                    returnMsg = SetReaderPower(this.ReaderPowerList);
                    //日志记录
                    DeclaimerReaderLog.Info("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                    SetReaderAntennas(this.ReaderAntennasList);
                }

                m_IsReading = false;

                if (m_RFIDReader != null)
                {
                    m_RFIDReader.Actions.TagAccess.OperationSequence.DeleteAll();
                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                    op.ReadAccessParams.MemoryBank = (MEMORY_BANK)3; // 3 user区
                    op.ReadAccessParams.ByteCount = 0;
                    op.ReadAccessParams.ByteOffset = 0;
                    op.ReadAccessParams.AccessPassword = 0;
                    m_RFIDReader.Actions.TagAccess.OperationSequence.Add(op);
                }

                try
                {
                    TagStorageSettings tagStorageSettings = m_RFIDReader.Config.GetTagStorageSettings();
                    tagStorageSettings.DiscardTagsOnInventoryStop = true;
                    m_RFIDReader.Config.SetTagStorageSettings(tagStorageSettings);
                    /*
                     *  Events Registration
                     */
                    m_RFIDReader.Events.AttachTagDataWithReadEvent = false;
                    //读取回调
                    m_RFIDReader.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
                    //状态回调
                    //m_RFIDReader.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
                    m_RFIDReader.Events.NotifyBufferFullWarningEvent = true;
                    m_RFIDReader.Events.NotifyBufferFullEvent = true;
                    m_RFIDReader.Events.NotifyReaderDisconnectEvent = true;
                    m_RFIDReader.Events.NotifyAccessStartEvent = true;
                    m_RFIDReader.Events.NotifyAccessStopEvent = true;
                    m_RFIDReader.Events.NotifyInventoryStartEvent = true;
                    m_RFIDReader.Events.NotifyInventoryStopEvent = true;
                    m_RFIDReader.Events.NotifyReaderExceptionEvent = true;
                    m_RFIDReader.Events.NotifyHandheldTriggerEvent = false;
                }
                catch (OperationFailureException ofe)
                {
                    returnMsg.CallStatus = false;
                    returnMsg.CallMessage = "Connect Configuration : " + ofe.VendorMessage;
                    //日志记录
                    DeclaimerReaderLog.Error("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
            }
            catch (OperationFailureException ofe)
            {
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "Connect Error : " + ofe.StatusDescription;
                //日志记录
                DeclaimerReaderLog.Error("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
            }
            return returnMsg;
        }

        /// <summary>
        /// 读事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Events_ReadNotify(object sender, Events.ReadEventArgs args)
        {
            myReadNotify(args.ReadEventData);
            //this.Invoke(m_UpdateReadHandler, new object[] { args.ReadEventData.TagData });
        }

        private static DataTable dt = null;//进行排序
        private void myReadNotify(Events.ReadEventData eventData)
        {
            //SampleTagAnalysis();
            RSSI_ANT_TagAnalysis();
        }

        /// <summary>
        /// RSSI ANT 解析  返回Object[]
        /// </summary>
        private void RSSI_ANT_TagAnalysis()
        {
            //是否已经开启读取
            if (m_IsReading)
            {
                object[] objTag = new object[3];
                //Thread.Sleep(500);
                Symbol.RFID3.TagData[] tagDataArray = m_RFIDReader.Actions.GetReadTags(100);
                if (tagDataArray != null)
                {
                    //解析标签
                    if (tagDataArray.Length > 0)
                    {
                        for (int tagi = 0; tagi < tagDataArray.Length; tagi++)
                        {
                            Symbol.RFID3.TagData tag = tagDataArray[tagi];

                            objTag[0] = tag.TagID;
                            objTag[1] = tag.PeakRSSI;
                            objTag[2] = tag.AntennaID;

                            GetRFIDTagObj(objTag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查验标签类型
        /// 0：true 存在 false 不存在;
        /// 1：标签类型;
        /// 2：标签内容;
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private object[] CheckTagType(string tag)
        {
            object[] obj = new object[3];
            obj[0] = false;
            obj[1] = "";
            obj[2] = "";
            if (null != tag)
            {
                int tempTagIndex = -1;
                foreach (string item in DeclaimerCommon.DeclaimerCommon.TagTypeList)
                {
                    tempTagIndex = tag.ToUpper().IndexOf(item);
                    if (tempTagIndex >= 0)
                    {
                        obj[0] = true;
                        obj[1] = item;
                        obj[2] = tag.Substring(0, tempTagIndex + 2);
                        break;
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="powerList"></param>
        /// <returns></returns>
        private ReturnMessage SetReaderPower(List<string> powerList)
        {
            ReturnMessage resultMsg = new ReturnMessage();
            for (int i = 0; i < powerList.Count; i++)
            {
                try
                {
                    if (m_RFIDReader.IsConnected)
                    {
                        //antID[0]天线号码 0对应1天线，  1对应2天线
                        ushort[] antID = m_RFIDReader.Config.Antennas.AvailableAntennas;
                        Antennas.Config antConfig = m_RFIDReader.Config.Antennas[antID[i]].GetConfig();
                        //默认为0
                        antConfig.ReceiveSensitivityIndex = (ushort)0;

                        int index = Convert.ToInt32((Convert.ToInt32(powerList[i]) - 1000) / 10);

                        //功率选项 【索引最小值是0对应1000，索引最大值是200对应3000】 换算功率=(功率-2000)/10
                        antConfig.TransmitPowerIndex = (ushort)index;

                        if (!m_RFIDReader.ReaderCapabilities.IsHoppingEnabled)
                        {
                            antConfig.TransmitFrequencyIndex = (ushort)1;//默认1
                        }
                        else
                        {
                            antConfig.TransmitFrequencyIndex = (ushort)1;//默认1
                        }
                        //antID[0]天线号码 0对应1天线，  1对应2天线
                        m_RFIDReader.Config.Antennas[antID[i]].SetConfig(antConfig);

                        resultMsg.CallStatus = true;
                        resultMsg.CallMessage = "设置天线功率成功!";
                        //"Set Antenna Configuration Successfully";
                    }
                    else
                    {
                        resultMsg.CallStatus = false;
                        resultMsg.CallMessage = "设置功率失败！请连接读取器后再设置功率。";
                        //日志记录
                        DeclaimerReaderLog.Info("[" + resultMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                        //"Please connect to a reader";
                    }
                }
                catch (InvalidUsageException iue)
                {
                    resultMsg.CallStatus = false;
                    resultMsg.CallMessage = "InvalidUsageException" + iue.VendorMessage;
                    //日志记录
                    DeclaimerReaderLog.Error("[" + resultMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
                catch (OperationFailureException ofe)
                {
                    resultMsg.CallStatus = false;
                    resultMsg.CallMessage = "OperationFailureException :" + ofe.StatusDescription;
                    //日志记录
                    DeclaimerReaderLog.Error("[" + resultMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
                catch (Exception ex)
                {
                    resultMsg.CallStatus = false;
                    resultMsg.CallMessage = "OperationFailureException :" + ex.Message;
                    //日志记录
                    DeclaimerReaderLog.Error("[" + resultMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
            }
            return resultMsg;
        }

        /// <summary>
        /// 设置天线开启
        /// </summary>
        /// <param name="powerList"></param>
        /// <returns></returns>
        private void SetReaderAntennas(List<string> ValidAntennasList)
        {
            ushort[] antList = new ushort[ValidAntennasList.Count];
            //为空则默认开启全部天线
            if (ValidAntennasList.Count == 0)
            {
                m_AntennaList.AntennaID = m_RFIDReader.Config.Antennas.AvailableAntennas;
            }

            for (int index = 0; index < ValidAntennasList.Count; index++)
            {
                antList[index] = ushort.Parse(ValidAntennasList[index].ToString());
            }

            if (null == m_AntennaList)
            {
                m_AntennaList = new Symbol.RFID3.AntennaInfo(antList);
            }
            else
            {
                m_AntennaList.AntennaID = antList;
            }
        }

        /// <summary>
        /// 关闭读取器连接
        /// </summary>
        /// <returns></returns>
        public ReturnMessage CloseReaderConnection()
        {
            ReturnMessage returnMsg = new ReturnMessage();
            if (!m_ReaderInitiatedDisconnectionReceived)
            {
                try
                {
                    //读取相关回调删除
                    m_RFIDReader.Events.ReadNotify -= Events_ReadNotify;
                    //m_RFIDReader.Events.StatusNotify -= Events_StatusNotify;
                    m_RFIDReader.Disconnect();

                    returnMsg.CallStatus = true;
                    returnMsg.CallMessage = "Close Connection Success.";
                    //日志记录
                    DeclaimerReaderLog.Info("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
                catch (OperationFailureException ofe)
                {
                    returnMsg.CallStatus = false;
                    returnMsg.CallMessage = "Close Connection Error : " + ofe.VendorMessage;
                    //日志记录
                    DeclaimerReaderLog.Error("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
                }
            }

            return returnMsg;
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        /// <param name="readingType">读取操作类型</param>
        /// <param name="aPollInterval">暂时无用  主要为适配Intermec设备</param>
        /// <param name="aReadInterval">暂时无用  主要为适配Intermec设备 连续读取定时</param>
        /// <returns></returns>
        public ReturnMessage StartReading(ReadingType readingType, int aPollInterval, int aReadInterval)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            //连续读取标识  设置为  True
            if (readingType == ReadingType.EventReading)
            {
                IsContinuousRead = true;
            }

            //是否已经开启连接
            if (m_RFIDReader.IsConnected)
            {
                if (m_RFIDReader.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_RFIDReader.Actions.TagAccess.OperationSequence.PerformSequence(GetAccessFilter(),
                        GetTriggerInfo(), GetAntennaInfo());
                }
                else
                {
                    m_RFIDReader.Actions.Inventory.Perform(GetPostFilter(), GetTriggerInfo(), GetAntennaInfo());
                }
                //标记开始读取
                m_IsReading = true;
                m_TagTable.Clear();//标签去重存储区

                returnMsg.CallMessage = "开启读取。";
                returnMsg.CallStatus = true;
            }
            else
            {
                returnMsg.CallMessage = "请先连接读取器后开始读取。";
                returnMsg.CallStatus = false;
                //日志记录
                DeclaimerReaderLog.Error("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
            }
            return returnMsg;
        }

        /// <summary>
        /// 关闭读取
        /// </summary>
        /// <returns></returns>
        public ReturnMessage StopReading()
        {
            ReturnMessage returnMsg = new ReturnMessage();
            //是否已经开启连接
            if (m_RFIDReader.IsConnected)
            {
                if (m_RFIDReader.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_RFIDReader.Actions.TagAccess.OperationSequence.StopSequence();
                }
                else
                {
                    m_RFIDReader.Actions.Inventory.Stop();
                }
                m_IsReading = false;

                returnMsg.CallMessage = "成功停止读取.";
                returnMsg.CallStatus = true;
            }
            else
            {
                returnMsg.CallMessage = "请先连接读取器后开始读取。";
                returnMsg.CallStatus = false;
                //日志记录
                DeclaimerReaderLog.Error("[" + returnMsg.CallMessage + "] ==== [" + DateTime.Now.ToString() + "]");
            }
            return returnMsg;
        }

        /// <summary>
        /// 1:声音  2：绿灯  3：黄灯  4：红灯
        /// </summary>
        /// <param name="index"></param>
        public void ShowAlarmLamp_Sound(int[] GPOIndex)
        {
            if (m_RFIDReader.Config.GPO.Length > 0)
            {
                for (int i = 0; i < GPOIndex.Length; i++)
                {
                    if (GPOIndex[i] == 1)
                    {
                        m_RFIDReader.Config.GPO[i + 1].PortState = GPOs.GPO_PORT_STATE.TRUE;
                    }
                    else
                    {
                        m_RFIDReader.Config.GPO[i + 1].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    }
                }
            }
        }

    }
}
