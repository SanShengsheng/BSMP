using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Reports.Dtos
{
    public class GetHomeReportResponse
    {
        public HomeReportData YesterDay { get; set; }
        public List<HomeReportHistory> Histories { get; set; }
        public ChartLine Charts { get; set; }
    }

    public class HomeReportData
    {
        public int UserTotal { get; set; }
        public double RechargeTotal { get; set; }
        public int FamilyTotal { get; set; }
        public int BabyTotal { get; set; }
    }

    public class HomeReportHistory : HomeReportData
    {
        public DateTime Day { get; set; }
    }

    public class ChartLine
    {
        public List<string> Labels { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }

    public class ChartDataset
    {
        public string  Label { get; set; }
        public List<double> Data { get; set; }
    }
}
