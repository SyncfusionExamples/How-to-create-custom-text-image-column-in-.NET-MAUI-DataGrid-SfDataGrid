﻿using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SfDataGridSample.TextImageColumn;

namespace SfDataGridSample
{
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
}
