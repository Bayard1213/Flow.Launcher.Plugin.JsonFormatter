using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Flow.Launcher.Plugin.JsonFormatter.Views;
/// <summary>
/// Логика взаимодействия для JsonWindow.xaml
/// </summary>
public partial class JsonWindow : Window
{
    public JsonWindow(string formattedJson)
    {
        InitializeComponent();

        JsonEditor.Text = formattedJson;
    }

    private void Copy_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(JsonEditor.Text);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
