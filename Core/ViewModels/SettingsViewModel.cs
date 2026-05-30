using System.Windows;

namespace BudgetApp.Core.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public IEnumerable<string> Themes { get; } = new List<string> { "DarkLightBlueTheme", "DarkTheme", "LightTheme" };

    private string? _selectedTheme = "";
    public string? SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (value == null) return;
            _selectedTheme = value;

            var Theme = new ResourceDictionary{ Source = new Uri($"Resources/Themes/{_selectedTheme}.xaml", UriKind.Relative) };
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(Theme);
        }
    }

    public SettingsViewModel()
    {
        SelectedTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString().Split("/")[^1].Split(".")[0];
    }
}