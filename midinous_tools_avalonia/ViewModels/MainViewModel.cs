using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using midinous_tools;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows.Input;
namespace midinous_tools_avalonia.ViewModels;

/// <summary>
/// This converter can calculate any number of values.
/// </summary>
public class MathMultiConverter : IValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        // We need to validate if the provided values are valid. We need at least 3 values.
        // The first value is the operator and the other two values should be a decimal.
        if (values.Count != 3)
        {
            // We can write a message into the Trace if we want to inform the developer.
            Trace.WriteLine("Exactly three values expected");

            // return "BindingOperations.DoNothing" instead of throwing an Exception.
            // If you want, you can also return a BindingNotification with an Exception
            return BindingOperations.DoNothing;
        }

        // The first item of values is the operation.
        // The operation to use is stored as a string.
        string operation = values[0] as string ?? "+";

        // Create a variable result and assign the first value we have to if
        decimal value1 = values[1] as decimal? ?? 0;
        decimal value2 = values[2] as decimal? ?? 0;


        // depending on the operator calculate the result.
        switch (operation)
        {
            case "+":
                return value1 + value2;

            case "-":
                return value1 - value2;

            case "*":
                return value1 * value2;

            case "/":
                // We cannot divide by zero. If value2 is '0', return an error.
                if (value2 == 0)
                {
                    return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
                }

                return value1 / value2;
        }

        // If we reach this line, something was wrong. So we return an error notification
        return new BindingNotification(new InvalidOperationException("Something went wrong"), BindingErrorType.Error);
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var p = parameter as decimal? ?? 0;
        if (p == 0)
        {
            return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
        }
        return (decimal?)value / p;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (decimal?)value * (decimal?)parameter;
    }
}
public class MainViewModel : ViewModelBase
{
    public ICommand Cmd_LoadJson_OnClick { get; }
    public ICommand Cmd_SaveJson_OnClick { get; }
    public ICommand Cmd_FixIds_OnClick { get; }
    public ICommand Cmd_Merge_OnClick { get; }
    public ICommand Cmd_Offset_OnClick { get; }
    public ICommand Cmd_GenerateRegularPoly_OnClick { get; }


    public double Offset_x { get; set; }
    public double Offset_y { get; set; }
    public double scale { get; set; } = 36;
    public int pointType { get; set; }
    public int sides { get; set; } = 3;
    public bool Gen_CW { get; set; } = true;
    public bool Gen_CCW { get; set; }

    public mn_json file { get; set; } = new mn_json();


    public MainViewModel()
    {
        Cmd_LoadJson_OnClick = ReactiveCommand.Create(LoadJson_OnClick);
        Cmd_SaveJson_OnClick = ReactiveCommand.Create(SaveJson_OnClick);
        Cmd_FixIds_OnClick = ReactiveCommand.Create(FixIds_OnClick);
        Cmd_Merge_OnClick = ReactiveCommand.Create(Merge_OnClick);
        Cmd_Offset_OnClick = ReactiveCommand.Create(Offset_OnClick);
        Cmd_GenerateRegularPoly_OnClick = ReactiveCommand.Create(GenerateRegularPoly_OnClick);
    }

    private async void LoadJson_OnClick()
    {
        string[] json = (await Utils.OpenTextFile()).Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries);
        file = new mn_json();
        file.LoadJson(json);
    }
    private async void SaveJson_OnClick()
    {
       if(file.lines_json.Count == 0) { 
            file.CreateDefaultLines(); 
        }
       await Task.Run(()=>Utils.SaveTextFile(file.SaveJson()));
       
    }
    private void FixIds_OnClick()
    {
        file.reset_Ids();
    }
    private async void Merge_OnClick()
    {
        string[] json = (await Utils.OpenTextFile()).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (json.Length>0)
        {
            var tmp = new mn_json();
            tmp.LoadJson(json);
            file.Add(tmp, (int)Offset_x, (int)Offset_y);
        }
    }
    private void Offset_OnClick()
    {
        file.offsetPoints((int)Offset_x, (int)Offset_y);
    }
    private void GenerateRegularPoly_OnClick()
    {
        file.generate_poly_points(scale, sides, (Offset_x, Offset_y), pointType+1, Gen_CW, Gen_CCW);
    }
}