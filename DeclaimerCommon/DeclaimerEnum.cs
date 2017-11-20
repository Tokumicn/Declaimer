using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeclaimerCommon
{
    public class DeclaimerEnum
    {

    }

    /// <summary>
    /// 硬件类型
    /// </summary>
    public enum DeclaimerHardwareType
    {
        /// <summary>
        ///  Zebra (斑马)
        ///  FX9500设备  固定式读取器
        /// </summary>
        Fixed_ZebraFX9500,
        /// <summary>
        ///  Zebra (斑马)
        ///  MC9190-Z设备  手持读取器
        /// </summary>
        Handeld_ZebraMC9190Z,
        /// <summary>
        /// Honeywell(霍尼韦尔)
        /// IV7设备 固定式读取器
        /// </summary>
        Fixed_IntermecIV7,
        /// <summary>
        /// Honeywell(霍尼韦尔)
        /// IF2设备 固定式读取器
        /// </summary>
        Fixed_IntermecIF2,
        /// <summary>
        /// Honeywell(霍尼韦尔)
        /// IP30设备 手持读取器
        /// </summary>
        Handeld_IntermecIP30
    }

    /// <summary>
    /// 读取类型
    /// </summary>
    public enum ReadingType
    {
        /// <summary>
        /// 单次读取
        /// </summary>
        OnceReading,
        /// <summary>
        /// 连续读取：  Poll模式读取
        /// </summary>
        PollReading,
        /// <summary>
        /// 连续读取：  事件回调模式读取
        /// </summary>
        EventReading
    }

    /// <summary>
    /// 日志记录类型
    /// </summary>
    public enum LoggerClassType
    {
        /// <summary>
        /// 操作界面日志记录
        /// </summary>
        DeclaimerRollingFileAppender,
        /// <summary>
        /// Fiex Reader日志记录
        /// </summary>
        DeclaimerWaitingGodotRollingFileAppender,
        /// <summary>
        /// Handheld Reader日志记录
        /// </summary>
        DeclaimerWhiteNightRollingFileAppender
    }
}
