using System.Globalization;
using System.Threading;
using System.Windows;

namespace BongoPawClicker
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (CultureInfo.CurrentUICulture.Name == "zh-CN")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }
        }
    }
}
