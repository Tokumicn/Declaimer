using DeclaimerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WaitingGodotDeclaimer
{
    /// 读取标签事件
    /// </summary>
    /// <param name="tagContent"></param>
    public delegate void TagStrEventHandler(string tagData);

    /// 读取标签事件委托
    /// </summary>
    /// <param name="tagContent"></param>
    public delegate void TagObjEventHandler(object[] tagData);

    public interface IDeclaimer
    {
        /// <summary>
        /// 读取电子标签事件 返回值：Object[]
        /// </summary>
        event TagObjEventHandler GetRFIDTagObj;

        /// <summary>
        /// 读取电子标签事件 返回值：字符串
        /// </summary>
        event TagStrEventHandler GetRFIDTagStr;

        /// <summary>
        /// 允许建立连接
        /// </summary>
        bool IsAllowConnect { get; set; }

        /// <summary>
        /// 允许开始读取
        /// </summary>
        bool IsAllowStartRead { get; set; }

        /// <summary>
        /// 初始化读取器
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="strPort"></param>
        /// <param name="connTimeOut"></param>
        /// <param name="readerPowerList"></param>
        /// <param name="readerAntennasList"></param>
        /// <returns></returns>
        ReturnMessage InitReader(string strIP, string strPort, string connTimeOut, List<string> readerPowerList, List<string> readerAntennasList);

        /// <summary>
        /// 开启读取器连接
        /// </summary>
        /// <returns></returns>
        ReturnMessage OpenReaderConnection();

        /// <summary>
        /// 关闭读取器连接
        /// </summary>
        /// <returns></returns>
        ReturnMessage CloseReaderConnection();

        /// <summary>
        /// 开启读取
        /// </summary>
        /// <param name="readingType">读取类型：1.连续读取;2.一次读取</param>
        /// <returns></returns>
        ReturnMessage StartReading(ReadingType readingType, int aPollInterval, int aReadInterval);

        /// <summary>
        /// 暂停读取
        /// </summary>
        /// <returns></returns>
        ReturnMessage StopReading();

        /// <summary>
        /// 显示声音与报警灯
        /// </summary>
        /// <param name="GPOIndex"></param>
        void ShowAlarmLamp_Sound(int[] GPOIndex);
    }
}
