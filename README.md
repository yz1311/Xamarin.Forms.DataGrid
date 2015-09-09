# Xamarin.Forms.DataGrid
DataGrid component for Xamarin.Forms with MVVM. It supports bindings, templates etc.

#Usage With Xaml:
```  xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
              xmlns:dg="clr-namespace:Kowalski.DataGrid;assembly=Kowalski.DataGrid"
              xmlns:local="clr-namespace:DataGridSample;assembly=DataGridSample"
             x:Class="DataGridSample.MainPage">
 
  <ContentPage.Resources>
    <ResourceDictionary>
      <local:RateToIconConverter x:Key="arrowConverter" />
      <local:RateToColorConverter x:Key="colorConverter" />
    </ResourceDictionary>
  </ContentPage.Resources>
  
  <dg:DataGrid HeaderBackground="#BDBDBD" 
               HeaderTextColor="White" 
               OddRowBackground="#FFFFFF" 
               EvenRowBackground="#F2F2F2" 
               ItemsSource="{Binding Stocks}" 
               VerticalOptions="Fill"
               HeaderFontSize="12">
    <dg:DataGrid.Columns>
      <dg:DataGridColumn Title="Company Code" Width="100" DataProperty="Name"/>
      <dg:DataGridColumn Title="Status" Width="60" DataProperty="Change" HorizontalContentAlignment="Center">
        <dg:DataGridColumn.CellTemplate>
          <DataTemplate>
            <ContentView Padding="5">
              <Image Source="{Binding Change,Converter={StaticResource arrowConverter}}"/>
            </ContentView>
          </DataTemplate>
        </dg:DataGridColumn.CellTemplate>
      </dg:DataGridColumn>
      <dg:DataGridColumn Title="Change %" Width="*" DataProperty="Change"/>
      <dg:DataGridColumn Title="Higest" Width="*" DataProperty="Highest"/>
      <dg:DataGridColumn Title="Lowest" Width="*" DataProperty="Lowest"/>
      <dg:DataGridColumn Title="Mounthly Change" Width="*" DataProperty="MounthlyChange"/>
      <dg:DataGridColumn Title="Yearly Change" Width="*" DataProperty="YearlyChange" HorizontalContentAlignment="Fill">
        <dg:DataGridColumn.CellTemplate>
          <DataTemplate>
            <ContentView HorizontalOptions="FillAndExpand" 
                         VerticalOptions="Fill" 
                         BackgroundColor="{Binding YearlyChange,Converter={StaticResource colorConverter}}">
              <Label Text="{Binding YearlyChange}" TextColor="Black" HorizontalOptions="Center" VerticalOptions="Center"/>
            </ContentView>
          </DataTemplate>
        </dg:DataGridColumn.CellTemplate>
      </dg:DataGridColumn>
    </dg:DataGrid.Columns>
  </dg:DataGrid>

 </ContentPage>

```

#Usage With C#

```  xaml

    DataGrid dg = new DataGrid(){
        HeaderBackground = Color.FromHex("#BDBDBD"),
        VerticalOptions = LayoutOptions.Fill
    };

    ColumnCollection colums = new ColumnCollection();
    colums.Add(new DataGridColumn() {
        Title = "Company Code",
        Width = new GridLength(100,GridUnitType.Absolute),
        DataProperty = "Name",
        HorizontalContentAlignment = LayoutOptions.Center
    });

    colums.Add(new DataGridColumn(){
        Title = "Change %",
        Width = new GridLength(1,GridUnitType.Star),
        DataProperty = "Change"
    });


    ObservableCollection<StockItem> stocks=new ObservableCollection<StockItem>();
    //add items to list

    dg.SetBinding(DataGrid.ItemsSourceProperty, new Binding("ItemsSource", BindingMode.OneWay));

```
