using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MetroFramework.Forms;
using DeclaimerCommon;

namespace Declaimer
{
    public partial class ManageForm : MetroForm
    {
        WaitingGodotDeclaimer.IDeclaimer declaimer = null;
        DataTable tblDatas = new DataTable("Tags");
        public ManageForm()
        {
            InitializeComponent();

            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            tblDatas.Columns[0].AutoIncrement = true;
            tblDatas.Columns[0].AutoIncrementSeed = 1;
            tblDatas.Columns[0].AutoIncrementStep = 1;

            tblDatas.Columns.Add("EPC", Type.GetType("System.String"));
            tblDatas.Columns.Add("RSSI", Type.GetType("System.String"));
            tblDatas.Columns.Add("ANT", Type.GetType("System.String"));
            metroTagGrid.Font = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
            metroTagGrid.AllowUserToAddRows = false;

            //打开与读取器的连接
            declaimer = DeclaimerFactory.DeclaimerFactory.CreateDeclaimer(DeclaimerCommon.DeclaimerHardwareType.Fixed_ZebraFX9500);

            string strReaderIP = "172.17.22.81";
            string strReaderPort = "5084";
            string strConnTimeOut = "0";
            ReturnMessage result = declaimer.InitReader(strReaderIP, strReaderPort, strConnTimeOut, null, null);
            ShowStatusMessageAndColor(result);
        }

        /// <summary>
        /// 连接读取器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroBtnConnect_Click(object sender, EventArgs e)
        {
            if (declaimer.IsAllowConnect)
            {
                ReturnMessage result = declaimer.OpenReaderConnection();
                ShowStatusMessageAndColor(result);
            }
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroBtnStart_Click(object sender, EventArgs e)
        {
            if (declaimer.IsAllowStartRead)
            {
                //开始读取
                ReturnMessage result = declaimer.StartReading(ReadingType.EventReading, 0, 0);
                ShowStatusMessageAndColor(result);

                //设置回调函数
                //declaimer.GetRFIDTagStr += TagStrCallBack;
                declaimer.GetRFIDTagObj += TagObjCallBack;

            }
        }

        /// <summary>
        /// 读取器回调方法
        /// </summary>
        /// <param name="tagData"></param>
        private void TagStrCallBack(string tagData)
        {
            MessageBox.Show(tagData);
        }



        /// <summary>
        /// 读取器回调方法
        /// </summary>
        /// <param name="tagData"></param>
        private void TagObjCallBack(object[] tagData)
        {
            //_table.ReadXml(Application.StartupPath + @"\Data\Books.xml");

            tblDatas.Rows.Add(new object[] { null, tagData[0].ToString(), tagData[1].ToString(), tagData[2].ToString() });


            if (metroTagGrid.InvokeRequired)
            {
                metroTagGrid.Invoke((Action)(() =>
                {
                    metroTagGrid.DataSource = tblDatas;
                    metroTagGrid.Refresh();
                }));
            }
            else
            {
                metroTagGrid.DataSource = tblDatas;
                metroTagGrid.Refresh();
            }
        }

        /// <summary>
        /// 暂停读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroBtnStop_Click(object sender, EventArgs e)
        {
            ReturnMessage result = declaimer.StopReading();
            ShowStatusMessageAndColor(result);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroBtnDisConn_Click(object sender, EventArgs e)
        {
            ReturnMessage result = declaimer.CloseReaderConnection();
            ShowStatusMessageAndColor(result);
        }

        /// <summary>
        /// 状态面板修改是否被占用标识
        /// </summary>
        bool IsShowMessageBussy = false;
        /// <summary>
        /// 更新 状态信息展示栏和状态颜色 方法
        /// </summary>
        /// <param name="statusColor"></param>
        /// <param name="message"></param>
        private void ShowStatusMessageAndColor(ReturnMessage result)
        {
            Color target;
            //根据调用结果成功与否决定状态信息颜色  红绿两种
            if (result.CallStatus)
            {
                target = Color.YellowGreen;
            }
            else
            {
                target = Color.Maroon;
            }

            if (!IsShowMessageBussy)
            {
                IsShowMessageBussy = true;

                //显示信息
                this.metroLabStatusMsg.Text = result.CallMessage;

                MetroFramework.Animation.ColorBlendAnimation myColorAnim = new MetroFramework.Animation.ColorBlendAnimation();
                myColorAnim.Start(this.panelStatusColor, "BackColor", target, 2);
                myColorAnim.AnimationCompleted += new EventHandler(myColorAnim_AnimationCompleted);
            }
        }
        /// <summary>
        /// 完成变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myColorAnim_AnimationCompleted(object sender, EventArgs e)
        {
            IsShowMessageBussy = false;
        }
    }
}
