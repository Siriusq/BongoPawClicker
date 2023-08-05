using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BongoPawClicker
{
    /// <summary>
    /// SettingPanel.xaml
    /// </summary>
    public partial class SettingPanel : Window
    {
        private Key hotKey;
        private ModifierKeys hotKeyModifiers;

        public SettingPanel(Key key, ModifierKeys modifiers)
        {
            InitializeComponent();
            hotKey = key;
            hotKeyModifiers = modifiers;
            UpdateHotKeyText();
        }

        private void SettingPanelDragZone(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SettingPanel_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SettingPanel_Loaded(object sender, RoutedEventArgs e)
        {
            //Read Main Windows's Current Status
            if (Owner is MainWindow mainWindow)
            {
                AlwaysOnTopToggleButton.IsChecked = mainWindow.topmost;
                MeowButton.IsChecked = mainWindow.meow;
                DarkModeToggleButton.IsChecked = mainWindow.darkMode;
            }
        }

        #region HotKey
        private void UpdateHotKeyText()
        {
            HotKeyTextBox.Text = $"{hotKeyModifiers} + {hotKey}";
        }

        private void HotKeyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.UpdateHotKey(hotKey, hotKeyModifiers);
            }
        }

        private void HotKeyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Verify if the Key is valid
                if (IsForbiddenKey(e.Key))
                {
                    e.Handled = true;
                    return;
                }
                hotKey = e.Key;
                hotKeyModifiers = Keyboard.Modifiers;
                UpdateHotKeyText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private bool IsForbiddenKey(Key key)
        {
            return Enum.IsDefined(typeof(ForbiddenKeys), key.ToString());
        }

        public enum ForbiddenKeys
        {
            Back,
            Capital, CapsLock,
            Delete, Down,
            End, Enter, Escape,
            Home,
            Insert,
            Left, LeftAlt, LeftCtrl, LeftShift, LWin,
            Next, NumLock,
            PageDown, PageUp, Pause, PrintScreen,
            Return, Right, RightAlt, RightCtrl, RightShift, RWin,
            System,
            Tab,
            Up,
            VolumeDown, VolumeMute, VolumeUp
        };
        #endregion

        #region Always On Top
        private void AlwaysOnTopToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                //Make the main window visible again
                mainWindow.topmost = true;
                mainWindow.Topmost = true;
            }
        }

        private void AlwaysOnTopToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                //Make the main window visible again
                mainWindow.topmost = false;
                mainWindow.Topmost = false;
            }
        }
        #endregion

        #region Meow
        private void MeowButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.meow = true;
            }
        }

        private void MeowButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.meow = false;
            }
        }
        #endregion

        #region Dark Mode
        private void DarkModeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.SwitchToDarkMode();
            }
            //Force the current window to refresh dynamic resources
            Resources["CardBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"));
            Resources["IndicatorColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffb330"));
        }

        private void DarkModeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.SwitchToLightMode();
            }
            //Force the current window to refresh dynamic resources
            Resources["CardBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8f8ff"));
            Resources["IndicatorColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b39ddb"));
        }
        #endregion
    }
}
