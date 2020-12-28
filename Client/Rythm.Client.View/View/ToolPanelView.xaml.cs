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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace Rythm.Client.View
{
    /// <summary>
    /// Interaction logic for UsersListController.xaml
    /// </summary>
    public partial class ToolPanelView : UserControl
    {
        private bool lightTheme = true;
        public ToolPanelView()
        {
            InitializeComponent();

            List<string> styles = new List<string> { "ButtonStyle", "ButtonStyleDark" };

            themeButton.Click += ClickButton;
        }

		private void ClickButton(object sender, RoutedEventArgs e)
		{
            lightTheme = !lightTheme;
			string _theme = string.Empty;

			switch (lightTheme)
			{
                case true:
                    _theme = "ButtonStyle";
                    break;

                case false:
                    _theme = "ButtonStyleDark";
                    break;
            }

            // определяем путь к файлу ресурсов
            var uri = new Uri(@"../../Resources/" + _theme + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

        }
    }
}
