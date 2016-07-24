using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitMonitorCommon
{
   public static class Dna
    {
 

                   
        [DllImport("EzDnaApi.dll", EntryPoint = "DNAGetRTValue", CharSet = CharSet.Auto)]

        public  static extern int DNAGetRTValue(string point,ref  Double value);

        [DllImport("EzDnaApi.dll", EntryPoint = "DNAGetRTValueList", CharSet = CharSet.Ansi)]

        public static extern int DNAGetRTValueList(UInt16 nCount, string[] point, Double[] value);
        [DllImport("EzDnaApi.dll", EntryPoint = "DnaGetHistSnap", CharSet = CharSet.Auto)]
       private  static extern int DnaGetHistSnap(string point, string szStart, string szEnd, string szPeriod, UInt32 key);
        [DllImport("EzDnaApi.dll", EntryPoint = "DnaGetNextHist", CharSet = CharSet.Auto)]
        private static extern int DnaGetNextHist(UInt32 key, double value, string curTime, UInt16 nTime, string status, UInt16 nStatus);
        public static Queue<double>  DNAGetHistValue(string id,DateTime beginTime, Int32 period)
        {
            DateTime endTime = beginTime.AddHours(1);
            if (endTime > DateTime.Now)
                endTime = DateTime.Now;
            string szBegintTime = string.Format("{0:MM/dd/yy hh:mm:ss}",beginTime);
            string szEndTime = string.Format("{0:MM/dd/yy hh:mm:ss}", endTime);
            UInt32 key=0;
       
            UInt16 nTime=0, nStatus=0;
            string szTime = "";
            string szStatus="";
            double v=0;
            Queue<double> values = new Queue<double>() ;
            int ret = DnaGetHistSnap(id, szBegintTime, szEndTime, period.ToString(), key);
            if (ret != 0)
                throw new Exception(string.Format("读取{0}的历史数据时错误", id));

            ret = DnaGetNextHist(key, v, szTime, nTime, szStatus, nStatus);
            while (ret == 0)
            {
                values.Enqueue(v);
                ret = DnaGetNextHist(key, v, szTime, nTime, szStatus, nStatus);
            }

            return values;
        }

        public static Double[] GetRtValue( List<string> points)
        {
            double[] value=null;
            if ((points!=null) && (points.Count>0))
            {
                string[] point = points.ToArray();
                UInt16 nCount = Convert.ToUInt16( point.Count());
                value=new  double[nCount];
   
                    int ret = DNAGetRTValueList(nCount, point,  value);
                if (ret != 0)
                {
                    string strPoints = "";
                    foreach (string item in points)
                    {
                        if (strPoints == "")
                            strPoints = item;
                        else
                            strPoints += "," + item;
                    } 
                        throw new Exception("读取实时数据时错误，要读取数据的点表为："+ strPoints);                      
                }                 
            }
             return value;
        } 


    }
    public enum DataMode
    {
        RealTime,
        History,
        Debug

    }
}
    

