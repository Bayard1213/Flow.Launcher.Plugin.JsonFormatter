using System;
using System.Collections.Generic;
using System.Text.Json;
using Flow.Launcher.Plugin;
using System.Windows;
using Flow.Launcher.Plugin.JsonFormatter.Views;

namespace Flow.Launcher.Plugin.JsonFormatter;
public class JsonFormatter : IPlugin
{
    private PluginInitContext _context;

    public void Init(PluginInitContext context)
    {
        _context = context;
    }

    public List<Result> Query(Query query)
    {
        var results = new List<Result>();
        var input = query.Search;

        if (string.IsNullOrWhiteSpace(input))
        {
            results.Add(new Result
            {
                Title = "Введи JSON после ключевого слова",
                SubTitle = "Например: json {\"a\":1,\"b\":[1,2,3]}",
                IcoPath = "Images\\json_formatter_icon.png"
            });
            return results;
        }

        string formatted;

        try
        {
            using var doc = JsonDocument.Parse(input);
            formatted = JsonSerializer.Serialize(
                doc.RootElement,
                new JsonSerializerOptions { WriteIndented = true });
        }
        catch (JsonException ex)
        {
            results.Add(new Result
            {
                Title = "Ошибка разбора JSON",
                SubTitle = ex.Message,
                IcoPath = "Images\\json_formatter_icon.png"
            });
            return results;
        }

        results.Add(new Result
        {
            Title = "Скопировать отформатированный JSON",
            SubTitle = "Копирование в буфер без отдельного окна",
            IcoPath = "Images\\json_formatter_icon.png",
            Action = _ =>
            {
                Clipboard.SetText(formatted);
                return true;
            }
        });

        results.Add(new Result
        {
            Title = "Открыть в отдельном окне",
            SubTitle = "Просмотр и копирование отформатированного JSON",
            IcoPath = "Images\\json_formatter_icon.png",
            Action = _ =>
            {
                _context.API.HideMainWindow();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var win = new JsonWindow(formatted);
                    win.Show();  
                });

                return false;
            }
        });

        return results;
    }
}
