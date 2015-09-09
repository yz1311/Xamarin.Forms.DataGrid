using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridSample
{
    class SampleVM: INotifyPropertyChanged
    {
        const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random r;

        public ObservableCollection<StockItem> stocks;
        
        public ObservableCollection<StockItem> Stocks
        {
            get {
                return stocks;
            }
            set
            {
                stocks = value;
                OnPropertyChanged("Products");
            }
        }


        public SampleVM()
        {

            Stocks = new ObservableCollection<StockItem>();

            r = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < 8; i++)
            {
                Stocks.Add(new StockItem() { 
                    Change = Math.Round(r.NextDouble()*20-10,2),
                    Highest = Math.Round(r.NextDouble()*20+10,2),
                    Lowest = Math.Round(r.NextDouble()*10-20,2),
                    WeeklyChange= Math.Round(r.NextDouble() * 26 - 10,2),
                    MounthlyChange = Math.Round(r.NextDouble() * 25,2),
                    YearlyChange = Math.Round(r.NextDouble()*40-25,2),
                    Name = "" + LETTERS[r.Next() % LETTERS.Length] + LETTERS[r.Next() % LETTERS.Length] + LETTERS[r.Next() % LETTERS.Length],
                 });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
