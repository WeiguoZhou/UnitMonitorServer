using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace UnitMonitorCommon
{
   public  class CommUtil
    {/// <summary>
    /// 根据时间计算值次
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
        public static string ZhiC(DateTime d)

        {
            DateTime beginTime = new DateTime(2013, 1, 1, 0, 0, 0);
            if (d < beginTime)
                return "";
            int[] tempZhCNew = { 4, 2, 1, 0, 3, 2, 1, 4, 3, 2, 0, 4, 3, 1, 0 };
            // int[] tempZhCOld = { 4, 0, 1, 2, 3, 3, 4, 0, 1, 2, 0, 1, 2, 3, 4, 1, 2, 3, 4, 0, 2, 3, 4, 0, 1 };

            double f = (new TimeSpan(d.Ticks - beginTime.Ticks)).TotalHours ;
            int banc =Convert.ToInt32 (Math.Floor(f/8))%15;
            string zhc="";
            switch (tempZhCNew[banc])
            {
                case 0:
                    zhc= "甲";
                    break;
                case 1:
                    zhc = "乙";
                    break;
                case 2:
                    zhc = "丙";
                    break;
                case 3:
                    zhc = "丁";
                    break;
                case 4:
                    zhc = "戊";
                    break;
            }
            return zhc;
        }
        /// <summary>
        /// 根据时间计算班次
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string BanC(DateTime d)
        {
            if (d.Hour < 8)
                return "夜";
            if (d.Hour < 16)
                return "早";
            return "中";
        }
        /// <summary>
        /// 转义逗号
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string Transfer(string instr)
        {
            if (instr == null) return "";
            return instr.Replace(",", "&comma;");
        }
        /// <summary>
        /// 还原转义逗号
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string DeTransfer(string instr)
        {
            if (instr == null) return "";
            return instr.Replace("&comma;", ",");
        }
        /// <summary>
        /// 返回两个时间间隔的秒数
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static double TimeSecondSpan( DateTime time1,DateTime time2 )
        {
            return (new TimeSpan(time2.Ticks-time1.Ticks)).TotalSeconds;

        }
        /// <summary>
        /// 返回两个时间间隔的毫秒数
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static double TimeMillisecondSpan(DateTime time1, DateTime time2)
        {
            return (new TimeSpan(time2.Ticks - time1.Ticks)).TotalMilliseconds;

        }
        public static string GetHostName(string ip)
        {
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                IPHostEntry hostEntry = Dns.GetHostEntry(address);
                return hostEntry.HostName;
            }
            catch { }
            return "";
        }
        public static string GetLocalIp()
        {
            foreach (IPAddress item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return item.ToString();
            }
            return "127.0.0.1";
        }
    }
}
