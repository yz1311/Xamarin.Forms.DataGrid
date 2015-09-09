using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kowalski.DataGrid
{
    public sealed class DataGridColumn : BindableObject, IDefinition
    {
        #region bindable properties
        public static readonly BindableProperty WidthProperty =
                                BindableProperty.Create<DataGridColumn, GridLength>((p) => p.Width, 1);

        public static readonly BindableProperty TitleProperty =
                                BindableProperty.Create<DataGridColumn, string>((p) => p.Title, string.Empty);

        public static readonly BindableProperty DataProperty_Property =
                                BindableProperty.Create<DataGridColumn, string>((p) => p.DataProperty, string.Empty);

        public static readonly BindableProperty CellTemplateProperty =
                                BindableProperty.Create<DataGridColumn, DataTemplate>((p) => p.CellTemplate, null);

        public static readonly BindableProperty HorizontalContentAlignmentProperty =
                               BindableProperty.Create<DataGridColumn, LayoutOptions>((p) => p.HorizontalContentAlignment, LayoutOptions.Center);


        #endregion

        #region properties

        public GridLength Width
        {
            get { return (GridLength)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string DataProperty
        {
            get { return (string)GetValue(DataProperty_Property); }
            set { SetValue(DataProperty_Property, value); }
        }

        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty,value); }
        }

        public object Params{get;set;}

        public LayoutOptions HorizontalContentAlignment
        {
            get { return (LayoutOptions)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }
        #endregion

        public DataGridColumn()
        {
        }

        public event EventHandler SizeChanged;

    }

}
