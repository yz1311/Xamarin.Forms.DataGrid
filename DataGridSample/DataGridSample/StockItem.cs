using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridSample
{
    class StockItem
    {
        public string Name { get; set; }
        public double Highest { get; set; }
        public double Lowest { get; set; }
        public double Change { get; set; }
        public double WeeklyChange { get; set; }
        public double MounthlyChange { get; set; }
        public double YearlyChange { get; set; }
    }
}
