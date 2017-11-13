using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeclaimerCommon;
using Intermec.DataCollection.RFID;
using System.Windows.Forms;

namespace WaitingGodotDeclaimer
{
    public class IntermecRFIDFixedReader : IDeclaimer
    {
        /// <summary>
        /// 读取器对象
        /// </summary>
        private BRIReader m_RFIDReader = null;
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
        /// 是否使用红外 （GPIO外界红外设备）
        /// </summary>
        public bool m_UsedInfrared { get; set; }
        /// <summary>
        /// 红外状态  被触发时设置为true  长时间未触发设置为false(业务层控制)
        /// </summary>
        public bool InfraredStatus { get; set; }

        /// <summary>
        /// Poll模式 获取已读标签轮询Timer
        /// </summary>
        private Timer m_PollTimer = null;
        /// <summary>
        /// 连续读取持续时长Timer
        /// </summary>
        private Timer m_StopReadTimer = null;
        /// <summary>
        /// 是否已经开启连续读取标识
        /// </summary>
        private bool m_IsContinuousReadStarted = false;
        /// <summary>
        /// 事件模式  tag回调处理事件委托
        /// </summary>
        private Tag_EventHandlerAdv m_TagEventHandler = null;
        /// <summary>
        /// 外界GPIO设备 回调处理事件委托
        /// </summary>
        private GPIO_EventHandlerAdv m_GPIEventHandler = null;
        /// <summary>
        /// 读取器IP地址
        /// </summary>
        private string ReaderIP { get; set; }
        /// <summary>
        /// 读取器端口号
        /// </summary>
        private string ReaderPort { get; set; }
        /// <summary>
        /// 读取器功率设置  0对应1号天线强度
        /// </summary>
        private string ReaderPowerString { get; set; }
        /// <summary>
        /// 读取器开启哪些天线  1,2,3,4
        /// </summary>
        private List<int> ReaderAntennasList { get; set; }
        #region 检查强度
        private double _RSSIMax = 62;
        /// <summary>
        /// RSSI范围上限
        /// </summary>
        public double RSSIMax
        {
            get { return _RSSIMax; }
            set { _RSSIMax = value; }
        }


        private static double _RSSIMin = 40;
        /// <summary>
        /// RSSI范围下限
        /// </summary>
        public static double RSSIMin
        {
            get { return _RSSIMin; }
            set { _RSSIMin = value; }
        }

        private bool CheckMaxMin(double rssi)
        {
            if (rssi >= RSSIMin && rssi <= RSSIMax)
            {
                return true;
            }
            return false;
        }
        #endregion
        /// <summary>
        /// Intermec Fiexd读取器 构造函数  
        /// </summary>
        public IntermecRFIDFixedReader()
        {
            InfraredStatus = false;
        }

        /// <summary>
        /// Intermec Fiexd读取器 构造函数  
        /// </summary>
        /// <param name="usedInfrared">是否启动红外</param>
        public IntermecRFIDFixedReader(bool usedInfrared)
        {
            InfraredStatus = usedInfrared;
        }

        /// <summary>
        /// 初始化读取器
        /// </summary>
        /// <param name="strIP">strIP有两种情况：1.URL模式： "TCP://127.0.0.1";2.COM模式： "SERIAL://COM4";</param>
        /// <param name="strPort"></param>
        /// <param name="connTimeOut"></param>
        /// <param name="readerPowerList"></param>
        /// <param name="readerAntennasList"></param>
        /// <returns></returns>
        public ReturnMessage InitReader(string strIP, string strPort, string connTimeOut, List<string> readerPowerList, List<string> readerAntennasList)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            if (string.IsNullOrEmpty(strPort))
            {
                strPort = "2189";
            }
            if (readerPowerList == null)
            {
                readerPowerList = new List<string>() { "30", "30", "30", "30" };
            }
            if (readerAntennasList == null)
            {
                readerAntennasList = new List<string>() { "1", "2", "3", "4" };
            }
            //strIP有两种情况：
            //URL模式： "TCP://127.0.0.1";
            //COM模式： "SERIAL://COM4";
            this.ReaderIP = strIP;
            this.ReaderPort = strPort;
            //功率
            string strAntennasPower = string.Empty;
            foreach (var power in readerPowerList)
            {
                strAntennasPower += power + "dB,";
            }
            strAntennasPower = strAntennasPower.Substring(0, strAntennasPower.Length - 1);
            this.ReaderPowerString = strAntennasPower;

            //开启的天线
            foreach (var antenna in readerAntennasList)
            {
                ReaderAntennasList.Add(Convert.ToInt32(antenna));
            }

            try
            {
                m_RFIDReader = new BRIReader(null, this.ReaderIP);
                if (m_RFIDReader.IsConnected)
                {
                    IsAllowConnect = true;
                    returnMsg.CallMessage = "读取器连接成功.";
                    returnMsg.CallStatus = true;
                }
                else
                {
                    IsAllowConnect = false;
                    returnMsg.CallMessage = "读取器连接失败.";
                    returnMsg.CallStatus = false;
                }
            }
            catch (BasicReaderException bre)
            {
                IsAllowConnect = false;
                returnMsg.CallMessage = "读取器连接失败 :" + bre.Message;
                returnMsg.CallStatus = false;
            }
            return returnMsg;
        }

        /// <summary>
        /// 开启读取器连接
        /// </summary>
        /// <returns></returns>
        public ReturnMessage OpenReaderConnection()
        {
            ReturnMessage returnMsg = new ReturnMessage();

            if (m_RFIDReader == null)
            {
                IsAllowStartRead = false;
                returnMsg.CallMessage = "读取器开启失败 : 读取器对象为空.";
                returnMsg.CallStatus = false;
            }

            try
            {
                if (!m_RFIDReader.IsConnected)
                {
                    if (m_UsedInfrared)
                    {
                        //增加GPIO设备响应事件
                        m_GPIEventHandler = new GPIO_EventHandlerAdv(BRIReaderEventHandler_GPITrigger);
                        m_RFIDReader.EventHandlerGPIO += m_GPIEventHandler;
                    }
                    //m_RFIDReader.SetAutoPollTriggerEvents(false);
                    m_RFIDReader.AutoPollTriggerEvents = false;
                    SetReaderAttributes();
                    SetTriggers();

                    IsAllowStartRead = true;
                    returnMsg.CallMessage = "读取器开启成功.";
                    returnMsg.CallStatus = true;
                }
            }
            catch (BasicReaderException brex)
            {
                IsAllowStartRead = false;
                returnMsg.CallMessage = "读取器属性设置失败 : 异常信息 === " + brex.Message;
                returnMsg.CallStatus = false;
            }

            return returnMsg;
        }

        /// <summary>
        /// 设置读写器参数
        /// </summary>
        private void SetReaderAttributes()
        {
            string sRsp = null;
            try
            {
                m_RFIDReader.Attributes.SetIDTRIES(1);
                m_RFIDReader.Attributes.SetANTTRIES(1);
                m_RFIDReader.Attributes.SetWRTRIES(3);
                m_RFIDReader.Attributes.SetTAGTYPE("EPCC1G2");
                m_RFIDReader.Attributes.SetANTS(this.ReaderAntennasList.ToArray());
                sRsp = m_RFIDReader.Execute("ATTRIB");
                if (!string.IsNullOrEmpty(ReaderPowerString))
                {
                    sRsp = m_RFIDReader.Execute("ATTRIBUTE FIELDSTRENGTH=" + this.ReaderPowerString);
                }
                else
                {
                    sRsp = m_RFIDReader.Execute("ATTRIBUTE FIELDSTRENGTH=30dB, 30dB, 30dB, 30dB");
                }
            }
            catch (BasicReaderException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置触发器  红外用
        /// </summary>
        private void SetTriggers()
        {
            //Optional Code
            //You need to delete the triggers when you exit your app otherwise they will 
            //still be running after you disconnect.

            string sMsg = null;

            //delete any existing trigger definitions
            DeleteTriggers();

            //CREATE TRIGGERS
            #region 1接口 遮挡返回14、离开返回15
            //sMsg = brdr.Execute("TRIGGER \"Sensor 1 ON\" GPIOEDGE 1 0 FILTER 0");//14
            //sMsg = brdr.Execute("TRIGGER \"Sensor 1 OFF\" GPIOEDGE 1 1 FILTER 0");//15 

            //Sensor 1 ON=====14
            //Sensor 1 OFF=====15
            #endregion

            #region 2接口 遮挡返回13、离开（没有触发事件）无返回值
            //sMsg = brdr.Execute("TRIGGER \"Sensor 2 ON\" GPIOEDGE 2 0 FILTER 0");//13
            //sMsg = brdr.Execute("TRIGGER \"Sensor 2 OFF\" GPIOEDGE 2 1 FILTER 0");//无返回值

            //Sensor 2 ON=====13            
            //Sensor 2 OFF
            #endregion

            #region 1、2接口 两边同时遮挡返回12 左边离开无返回值、右面离开返回13
            sMsg = m_RFIDReader.Execute("TRIGGER \"Sensor 1 ON\" GPIOEDGE 1 0 FILTER 0");//14
            sMsg = m_RFIDReader.Execute("TRIGGER \"Sensor 1 OFF\" GPIOEDGE 1 1 FILTER 0");//15 
            sMsg = m_RFIDReader.Execute("TRIGGER \"Sensor 2 ON\" GPIOEDGE 2 0 FILTER 0");//13
            sMsg = m_RFIDReader.Execute("TRIGGER \"Sensor 2 OFF\" GPIOEDGE 2 1 FILTER 0");//无返回值

            //Sensor 1 ON=====14
            //Sensor 1 OFF=====15

            //Sensor 2 ON=====13            
            //Sensor 1 ON=====12

            //Sensor 1 OFF=====13
            //Sensor 1 ON=====12

            //Sensor 2 OFF
            //Sensor 2 ON=====12

            //Sensor 2 OFF
            //Sensor 1 OFF=====13

            //Sensor 1 ON=====14
            //Sensor 1 OFF=====15
            #endregion
        }

        /// <summary>
        /// 删除触发器
        /// </summary>
        private void DeleteTriggers()
        {
            //Optional Code
            //You need to delete the triggers when you exit your app otherwise they will 
            //still be running after you disconnect.

            string sMsg = null;

            //CLEAR ALL MACROS AND TRIGGERS
            sMsg = m_RFIDReader.Execute("TRIGGER RESET");
        }

        /// <summary>
        /// 关闭读取器连接
        /// </summary>
        /// <returns></returns>
        public ReturnMessage CloseReaderConnection()
        {
            ReturnMessage returnMsg = new ReturnMessage();

            DeleteTriggers();

            if (m_RFIDReader != null)
            {
                m_RFIDReader.Dispose();
                m_RFIDReader = null;
            }

            returnMsg.CallStatus = true;
            returnMsg.CallMessage = "关闭成功";
            return returnMsg;
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        /// <param name="readingType"></param>
        /// <returns></returns>
        public ReturnMessage StartReading(ReadingType readingType, int aPollInterval, int aReadInterval)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            switch (readingType)
            {
                case ReadingType.OnceReading:
                    SingleShotRead();
                    break;
                case ReadingType.PollReading:
                    PollRead(aPollInterval, aReadInterval);
                    break;
                case ReadingType.EventReading:
                    EventRead(aReadInterval);
                    break;
                default:
                    //log [TODO]
                    break;
            }
            return returnMsg;
        }

        /// <summary>
        /// 单次读取  命令：RSSI ANT
        /// </summary>
        private ReturnMessage SingleShotRead()
        {
            ReturnMessage returnMsg = new ReturnMessage();
            try
            {
                m_RFIDReader.Read(null, "RSSI ANT");

                // Other read example:
                // Besides the tag key (EPCID) which is read by default, also
                // read the	antenna number (where read occurs) and 4 bytes of
                // data in memory bank 2 starting from address 0.
                // m_RFIDReader.Read(null, "ANT, HEX(2:0,4)");

                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "单次读取";
                //解析标签
                AnalysisTags(m_RFIDReader.Tags);
            }
            catch (Exception ex)
            {
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "异常：读取失败.错误信息：" + ex.Message;
            }
            return returnMsg;
        }


        /// <summary>
        /// 连续读取模式下，设置轮询时间间隔，每次到达时间间隔时，统一获取一次标签
        /// </summary>
        /// <param name="aPollInterval">Poll轮询获取已读取标签的时间 默认：5秒</param>
        /// <param name="aReadInterval">连续读取 所持续时间[注意：如果持续时长参数为0或者小于0的数值,则表示不通过定时器关闭,而由使用者手动调用关闭方法]</param>
        private void PollRead(int aPollInterval, int aReadInterval)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            //*****
            //* 初始化Poll轮序 定时器
            //*****
            if (null == m_PollTimer)
            {
                m_PollTimer = new Timer();
                m_PollTimer.Tick += new EventHandler(TimerTick_PollTags);
            }
            m_PollTimer.Enabled = false;
            if (aPollInterval > 0)
            {
                m_PollTimer.Interval = aPollInterval;
            }
            else
            {
                //默认值  5秒
                m_PollTimer.Interval = 5000;
            }

            //*****
            //* 初始化连续读取持续时长  定时器
            //*****
            if (null == m_StopReadTimer)
            {
                m_StopReadTimer = new Timer();
                m_StopReadTimer.Tick += new EventHandler(TimerTick_StopRead);
            }
            m_StopReadTimer.Enabled = false;
            m_StopReadTimer.Interval = aReadInterval;

            try
            {
                if (m_RFIDReader.StartReadingTags(null, "ANT RSSI", BRIReader.TagReportOptions.POLL))
                {
                    returnMsg.CallStatus = true;
                    returnMsg.CallMessage = "Poll模式 连续读取 启动.";

                    m_IsContinuousReadStarted = true;
                    m_PollTimer.Enabled = true; // 开启Poll轮询Timer
                    if (aReadInterval > 0)
                    {
                        m_StopReadTimer.Enabled = true; // 开启连续读取持续时长Timer
                    }
                    else
                    {
                        m_StopReadTimer.Enabled = false;
                    }
                }
                else
                {
                    returnMsg.CallStatus = false;
                    returnMsg.CallMessage = "异常：启动Poll模式连续读取 失败.";
                }
            }
            catch (Exception ex)
            {
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "异常：启动Poll模式 连续读取 失败.异常信息：" + ex.Message;
            }
        }

        /// <summary>
        /// 连续读取Poll模式下 已读到标签轮询Timer执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerTick_PollTags(object sender, EventArgs e)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            try
            {
                if (m_RFIDReader.PollTags())
                {
                    //解析标签
                    AnalysisTags(m_RFIDReader.Tags);
                }
                else
                {
                    returnMsg.CallStatus = false;
                    returnMsg.CallMessage = "异常：Poll模式  获取标签失败.";
                }
            }
            catch (Exception ex)
            {
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "异常：Poll模式  获取标签失败.异常信息：" + ex.Message;
            }
        }

        /// <summary>
        /// 连续读取模式： 每当读取到标签将触发事件处理方法
        /// </summary>
        /// <param name="aReadInterval">连续读取 所持续时间[注意：如果持续时长参数为0或者小于0的数值,则表示不通过定时器关闭,而由使用者手动调用关闭方法]</param>
        private void EventRead(int aReadInterval)
        {
            //*****
            //* 设置标签读到后触发的事件处理
            //*****
            if (null == m_TagEventHandler)
            {
                m_TagEventHandler = new Tag_EventHandlerAdv(BRIReaderEventHandler_Tag);
                m_RFIDReader.EventHandlerTag += m_TagEventHandler;
            }

            //*****
            //* 初始化连续读取持续时间Timer
            //*****
            if (null == m_StopReadTimer)
            {
                m_StopReadTimer = new Timer();
                m_StopReadTimer.Tick += new EventHandler(TimerTick_StopRead);
            }
            m_StopReadTimer.Enabled = false;
            m_StopReadTimer.Interval = aReadInterval;

            try
            {
                //该模式下读取到的标签不会去重
                if (m_RFIDReader.StartReadingTags(null, "RSSI ANT", BRIReader.TagReportOptions.EVENTALL))
                //if (m_RFIDReader.StartReadingTags(BRIReader.TagReportOptions.EVENT))//该模式下将不会重复提交已经提交过的标签（去重效果）
                {
                    //[TODO] 记录日志
                    //DisplayMessage("Continuous read with event reporting is started.");
                    m_IsContinuousReadStarted = true;
                    if (aReadInterval > 0)
                    {
                        //开启连续读取时长 Timer
                        m_StopReadTimer.Enabled = true;
                    }
                    else
                    {
                        //如果持续时长参数为0或者小于0的数值  则表示不通过定时器关闭  而由使用者手动调用关闭方法
                        m_StopReadTimer.Enabled = false;
                    }
                }
                else
                {
                    //[TODO] 记录日志
                    //DisplayMessage("Error: Failed to start continuous read with event reporting.");
                }
            }
            catch (Exception ex)
            {
                //[TODO] 记录日志
                //DisplayMessage("Error: Failed to start continuous read with event reporting, exception="+ ex.Message);
            }
        }

        /// <summary>
        /// 连续读取模式下,事件处理模式：每当读到新的标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="EvtArgs"></param>
        void BRIReaderEventHandler_Tag(object sender, EVTADV_Tag_EventArgs EvtArgs)
        {
            //intCOUNt += 1;//[TODO]读到标签总数

            //解析标签
            //Tag[] tagArr = new Tag[] { EvtArgs.Tag };
            //AnalysisTags(tagArr);
            TagConvertToBarCode(EvtArgs.Tag);
        }

        /// <summary>
        /// This method is invoked when a general purpose input trigger event occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="EvtArgs"></param>
        void BRIReaderEventHandler_GPITrigger(object sender, EVTADV_GPIO_EventArgs EvtArgs)
        {
            InfraredStatus = true;
            //红外状态设置为True  
            //记录日志 [TODO]
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="tempTagList"></param>
        private void AnalysisTags(Tag[] tags)
        {
            if (null == tags)
                return;

            for (int i = 0; i < tags.Length; i++)
            {
                TagConvertToBarCode(tags[i]);
            }
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="tempTagList">返回结果列表</param>
        private void TagConvertToBarCode(Tag tag)
        {
            int iNumFields = tag.TagFields.ItemCount;

            if (iNumFields > 0)
            {
                TagField[] tagFieldArray = tag.TagFields.FieldArray;

                string rssiStr = tagFieldArray[0].ToString();
                //将十六进制标签数据  转换为一维码标签值
                string tagString = DeclaimerCommon.DeclaimerCommon.HexStr2String(tag.ToString());
                string tagAnt = string.Empty;//天线号
                //新增
                if (tagFieldArray.Length >= 1)
                {
                    tagAnt = tagFieldArray[1].ToString();
                }
                //string tagString = HexToString(tag);
                //在RSSI范围内的标签将保留下来  否则  忽略
                if (!CheckMaxMin(System.Math.Abs(Convert.ToDouble(rssiStr))))
                {
                    tagString = string.Empty;
                }
                if (!string.IsNullOrEmpty(tagString))
                {
                    object[] tempObjectTag = CheckTagType(tagString, tagAnt);
                    if (Convert.ToBoolean(tempObjectTag[0]))
                    {
                        GetRFIDTagObj(tempObjectTag);
                    }
                }

            } //endif (iNumFields > 0)
        }


        #region 查验标签类型
        /// <summary>
        /// 查验标签类型
        /// 0：true 存在 false 不存在;
        /// 1：标签类型;
        /// 2：标签内容;
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private object[] CheckTagType(string tag, string ant)
        {
            object[] obj = new object[4];
            obj[0] = false;
            obj[1] = "";
            obj[2] = "";
            obj[3] = ant;//天线号

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
        #endregion


        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public ReturnMessage StopReading()
        {
            ReturnMessage returnMsg = new ReturnMessage();
            if (m_PollTimer != null)
            {
                m_PollTimer.Enabled = false;
            }
            m_StopReadTimer.Enabled = false;

            try
            {
                m_RFIDReader.StopReadingTags();
                m_IsContinuousReadStarted = false;
                //"Continuous read stopped."
            }
            catch (Exception ex)
            {
                //异常处理 写入本地Log
                //returnMsg.CallStatus = false;
                //returnMsg.CallMessage = "异常：  停止连续读取失败.异常信息：" + ex.Message;
            }
            return returnMsg;
        }

        /// <summary>
        /// 连续读取时  暂停读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerTick_StopRead(object sender, EventArgs e)
        {
            ReturnMessage returnMsg = new ReturnMessage();
            if (m_PollTimer != null)
            {
                m_PollTimer.Enabled = false;
            }
            m_StopReadTimer.Enabled = false;

            try
            {
                m_RFIDReader.StopReadingTags();
                m_IsContinuousReadStarted = false;
                //"Continuous read stopped."
            }
            catch (Exception ex)
            {
                //异常处理 写入本地Log
                returnMsg.CallStatus = false;
                returnMsg.CallMessage = "异常：  停止连续读取失败.异常信息：" + ex.Message;
            }
        }

        /// <summary>
        /// 显示声音与报警灯
        /// </summary>
        /// <param name="GPOIndex"></param>
        public void ShowAlarmLamp_Sound(int[] GPOIndex)
        {
            throw new NotImplementedException();
        }
    }
}
