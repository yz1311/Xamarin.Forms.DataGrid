using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using System.Collections;

namespace Kowalski.DataGrid
{
    public class DataGrid : StackLayout
    {

        #region binding properties
        public static readonly BindableProperty HeaderBackgroundProperty =
                             BindableProperty.Create<DataGrid, Color>((p) => p.HeaderBackground, Color.Aqua);

        public static readonly BindableProperty HeaderTextColorProperty =
                            BindableProperty.Create<DataGrid, Color>((p) => p.HeaderTextColor, Color.Black);

        public static readonly BindableProperty EvenRowBackgroundProperty =
                            BindableProperty.Create<DataGrid, Color>((p) => p.EvenRowBackground, Color.White);

        public static readonly BindableProperty OddRowBackgroundProperty =
                             BindableProperty.Create<DataGrid, Color>((p) => p.OddRowBackground, Color.White);

        public static readonly BindableProperty EvenRowForegroundProperty =
                            BindableProperty.Create<DataGrid, Color>((p) => p.EvenRowForeground, Color.Black);

        public static readonly BindableProperty OddRowForegroundProperty =
                            BindableProperty.Create<DataGrid, Color>((p) => p.OddRowForeground, Color.Black);

        public static readonly BindableProperty ColumnsProperty =
                            BindableProperty.Create<DataGrid, ColumnCollection>((p) => p.Columns, null, BindingMode.TwoWay, null, UpdateColumns);

        public static readonly BindableProperty ItemsSourceProperty =
                            BindableProperty.Create<DataGrid, IEnumerable>((p) => p.ItemsSource, null, BindingMode.Default, null, DrawDataGrid);

        public static readonly BindableProperty RowHeightProperty =
                            BindableProperty.Create<DataGrid, double>((p) => p.RowHeight, 40);

        public static readonly BindableProperty HeaderHeightProperty =
                            BindableProperty.Create<DataGrid, double>((p) => p.HeaderHeight, 40);

        public static readonly BindableProperty IsSortableProperty =
                            BindableProperty.Create<DataGrid, bool>((p) => p.IsSortable, true);

        public static readonly BindableProperty HeaderFontSizeProperty =
                            BindableProperty.Create<DataGrid, double>((p)=>p.HeaderFontSize,13);

        private static void DrawDataGrid(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            if ((bindable as DataGrid).Columns.Count > 0 && !(bindable as DataGrid).hasInitialized)
                (bindable as DataGrid).InitilizeUI();
        }

        private static void UpdateColumns(BindableObject bindable, ColumnCollection oldValue, ColumnCollection newValue)
        {
            if (!(bindable as DataGrid).hasInitialized && newValue.Count > 0)
                (bindable as DataGrid).InitilizeUI();
        }

        #endregion

        #region properties
        public Color HeaderBackground
        {
            get { return (Color)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public Color HeaderTextColor
        {
            get { return (Color)GetValue(HeaderTextColorProperty); }
            set { SetValue(HeaderTextColorProperty, value); }
        }

        public Color EvenRowBackground
        {
            get { return (Color)GetValue(EvenRowBackgroundProperty); }
            set { SetValue(EvenRowBackgroundProperty, value); }
        }

        public Color OddRowBackground
        {
            get { return (Color)GetValue(OddRowBackgroundProperty); }
            set { SetValue(OddRowBackgroundProperty, value); }
        }

        public Color EvenRowForeground
        {
            get { return (Color)GetValue(EvenRowForegroundProperty); }
            set { SetValue(EvenRowForegroundProperty, value); }
        }

        public Color OddRowForeground
        {
            get { return (Color)GetValue(OddRowForegroundProperty); }
            set { SetValue(OddRowForegroundProperty, value); }
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ColumnCollection Columns
        {
            get { return (ColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty,value); }
        }

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        public bool IsSortable
        {
            get { return (bool)GetValue(IsSortableProperty); }
            set { SetValue(IsSortableProperty, value); }
        }

        #endregion

        #region fields

        bool hasInitialized = false;
        Dictionary<int, SortingOrder> SortingOrders;
        ListView listView;
        #endregion

        #region ctor
        public DataGrid()
        {
            SortingOrders = new Dictionary<int, SortingOrder>();

            this.Columns = new ColumnCollection();
            this.Padding = 5;
            this.Spacing = 0;
            this.BackgroundColor = Color.White;
            this.VerticalOptions = LayoutOptions.Fill;

            listView = new ListView()
            {
                SeparatorVisibility = SeparatorVisibility.None,
                ItemTemplate = new DataTemplate(GetRowTemplate),
            };

            listView.ItemSelected += (s, e) =>
            {
                listView.SelectedItem = null;
            };
        }
        #endregion

        public void InitilizeUI()
        {
            hasInitialized = true;

            if (IsSortable)
            {
                for (int i = 0; i < Columns.Count; i++ )
                    SortingOrders.Add(i, SortingOrder.NotDetermined);
            }

            listView.ItemsSource = ItemsSource;
            listView.RowHeight = (int)RowHeight;
            this.Children.Add(GetHeader());

            if (Device.OS == TargetPlatform.Android)
            {
                ScrollView sv = new ScrollView();
                sv.Content = listView;
                this.Children.Add(sv);
            }
            else
                this.Children.Add(listView);

        }

        protected virtual Grid GetHeader()
        {
            Grid header = new Grid()
            {
                HeightRequest = HeaderHeight,
                Padding = new Thickness(1),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                ColumnSpacing = 1,
                BackgroundColor = Color.Black,
            };

            header.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) });

            foreach (var col in Columns)
            {
                header.ColumnDefinitions.Add(new ColumnDefinition() { Width = col.Width });
            }

            for (int i = 0; i < Columns.Count; i++)
            {
                Label lb = new Label()
                {
                    Text = Columns[i].Title,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = HeaderTextColor,
                    FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center,
                    LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap,
                    FontSize=HeaderFontSize,
                };

                StackLayout sl = new StackLayout()
                {
                    BackgroundColor = HeaderBackground,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children = { lb }
                };

                if (IsSortable)
                {
                    var orderingIcon = new Label()
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Text = " ",
                    };

                    Columns[i].Params = orderingIcon;
                    sl.Children.Add(orderingIcon);
                    
                    string property = Columns[i].DataProperty;
                    string title = Columns[i].Title;
                    var tgr = new TapGestureRecognizer();
                    tgr.Tapped += (s, e) => {
                        var col = Columns.FirstOrDefault(x=> x.DataProperty == property && x.Title == title);
                        SortItems(Columns.IndexOf(col)); 
                    };
                    sl.GestureRecognizers.Add(tgr);
                }
                header.Children.Add(sl);

                Grid.SetColumn(sl, i);
            }
            return header;
        }

        bool isEven = false;
        bool first = true;
        object GetRowTemplate()
        {
            KowalskiViewCell row = new KowalskiViewCell();
            Grid rowGrid = new Grid()
            {
                BackgroundColor = Color.Black,
                RowSpacing = 0,
                ColumnSpacing = 1,
                Padding = new Thickness(1, first ? 1 : 0, 1, 1)
            };

            first = false;
            for (int i = 0; i < this.Columns.Count; i++)
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = Columns[i].Width });

            for (int i = 0; i < this.Columns.Count; i++)
            {
                ContentView cell = new ContentView()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    BackgroundColor = (isEven) ? EvenRowBackground : OddRowBackground
                };
                 
                if(Columns[i].CellTemplate!= null)
                {
                    View v = Columns[i].CellTemplate.CreateContent() as View;
                    v.HorizontalOptions = Columns[i].HorizontalContentAlignment;
                    cell.Content = v;
                }

                else if (Columns[i].DataProperty != null)
                {
                    Label text = new Label()
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = Columns[i].HorizontalContentAlignment,
                        TextColor = (isEven) ? OddRowForeground : EvenRowForeground,
                        FontSize = 12,
                    };
                    text.SetBinding(Label.TextProperty, new Binding(Columns[i].DataProperty, BindingMode.OneWay));
                    cell.Content = text;
                }

                rowGrid.Children.Add(cell);
                Grid.SetColumn(cell, i);
            }

            isEven = !isEven;
            row.View = rowGrid;
            return row;
        }

        private void SortItems(int propertyIndex)
        {
            if (ItemsSource == null || ItemsSource.Count <= 1)
                return;

            List<Object> item = new List<Object>();
            foreach (var itm in ItemsSource)
                item.Add(itm);

            List<Object> sortedItems;


            if (!IsSortable)
                throw new InvalidOperationException("This DataGrid is not sortable");
            if (Columns[propertyIndex].DataProperty == null)

                throw new InvalidOperationException("Please set the DataProperty property of Column");
            if (SortingOrders[propertyIndex] != SortingOrder.Descendant)
            {
                int i = 0;
                var itm = item[0].GetType().GetRuntimeProperty(Columns[propertyIndex].DataProperty).GetValue(item[0]);
                if (itm is decimal)
                    i++;
                if (itm is double)
                    i++;
                sortedItems = item.OrderByDescending((x) => x.GetType().GetRuntimeProperty(Columns[propertyIndex].DataProperty).GetValue(x)).ToList();
                SortingOrders[propertyIndex] = SortingOrder.Descendant;
                (Columns[propertyIndex].Params as Label).Text = "▼";
            }
            else
            {
                sortedItems = item.OrderBy((x) => x.GetType().GetRuntimeProperty(Columns[propertyIndex].DataProperty).GetValue(x)).ToList();
                SortingOrders[propertyIndex] = SortingOrder.Ascendant;
                (Columns[propertyIndex].Params as Label).Text = "▲";
            }

            foreach (var column in Columns)
            {
                if ((column.Params as Label).Text!= null && Columns[propertyIndex] != column )
                    (column.Params as Label).Text = " ";
            }

            isEven = false;
            listView.ItemsSource = sortedItems;
        }
    }

}
