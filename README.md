# How to create custom text image column in .NET MAUI DataGrid (SfDataGrid)?
In this article, we will show you how to create custom text image column in [.NET MAUI DataGrid](https://www.syncfusion.com/maui-controls/maui-datagrid).

## Xaml
```
<ContentPage.BindingContext>
    <local:EmployeeViewModel x:Name="viewModel" />
</ContentPage.BindingContext>

<syncfusion:SfDataGrid x:Name="sfGrid" 
                        GridLinesVisibility="Both"
                        HeaderGridLinesVisibility="Both"
                        ColumnWidthMode="Auto"
                        AutoGenerateColumnsMode="None"
                        ItemsSource="{Binding Employees}">

    <syncfusion:SfDataGrid.Columns>
        <syncfusion:DataGridTextColumn MappingName="Name"
                                        HeaderText="Employee Name" />
        <syncfusion:DataGridTextColumn MappingName="Title"
                                        HeaderText="Designation" />
        <syncfusion:DataGridTextColumn MappingName="LoginID"
                                        HeaderText="Login ID" />
        <syncfusion:DataGridTextColumn MappingName="Gender"
                                        HeaderText="Gender" />

    </syncfusion:SfDataGrid.Columns>

</syncfusion:SfDataGrid>
```

## Xaml.cs
```
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        sfGrid.CellRenderers.Add("TextImage", new TextImageColumnRenderer());
    }
}
```

## TextImageColumn.cs

The code below demonstrates how to create custom text image column in SfDataGrid.
```
public class TextImageColumn : DataGridColumn
{
    public TextImageColumn()
    {
        var cellType = typeof(DataGridColumn).GetRuntimeProperties().FirstOrDefault(property => property.Name == "CellType");
        cellType?.SetValue(this, "TextImage");
    }

    protected override void SetConverterForDisplayBinding()
    {
        base.SetConverterForDisplayBinding();

        if (DisplayBinding is Binding binding)
        {
            binding.Converter = new CustomTextConverter();
        }
    }

    public class CustomTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;

            return $"{value}"; // Modify text as needed
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().Replace(" ", ""); // Reverse conversion
        }
    }
}

public class TextImageColumnRenderer : DataGridCellRenderer<Grid, Grid>
{
    protected override Grid OnCreateDisplayUIView()
    {
        var grid = new Grid
        {
            ColumnDefinitions =
        {
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(30) }
        }
        };

        var textLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start
        };
        Grid.SetColumn(textLabel, 0);

        var iconLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            FontSize = 20,
            FontFamily = "Segoe UI Emoji"
        };
        Grid.SetColumn(iconLabel, 1);

        grid.Children.Add(textLabel);
        grid.Children.Add(iconLabel);

        return grid;
    }

    protected override void OnInitializeDisplayView(DataColumnBase dataColumn, Grid view)
    {
        base.OnInitializeDisplayView(dataColumn, view);


        if (view.Children[0] is Label textLabel)
        {
            var binding = new Binding
            {
                Path = dataColumn.DataGridColumn.MappingName,
                Mode = BindingMode.TwoWay,
                Converter = new CustomTextConverter()
            };

            textLabel.SetBinding(Label.TextProperty, binding);
        }

        if (view.Children[1] is Label iconLabel )
        {
            if (dataColumn.DataGridColumn.MappingName == "Title")
            {
                iconLabel.Text = "📄";
            }
            else if (dataColumn.DataGridColumn.MappingName == "BirthDate")
            {
                iconLabel.Text = "\u23F1";
            }
        }
    }
}
```

<img src="https://support.syncfusion.com/kb/agent/attachment/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM3NzUxIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.ZnFgFGZmVJwFODONHsFluc6nRCE14qdX5VWc336S9oE" width=800/>


[View sample in GitHub](https://github.com/SyncfusionExamples/How-to-create-custom-text-image-column-in-.NET-MAUI-DataGrid-SfDataGrid)

Take a moment to explore this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more information about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples. Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid (SfDataGrid).
 
##### Conclusion
 
I hope you enjoyed learning about how to create custom text image column in SfDataGrid.
 
You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.
 
If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!