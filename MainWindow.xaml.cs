using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static BongoPawClicker.User32API;

namespace BongoPawClicker
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public bool meow;
        public bool darkMode;
        public bool topmost;
        //Default Hot Key is Alt,Control + P
        private Key hotKey;
        private ModifierKeys hotKeyModifiers;
        System.Media.SoundPlayer meowAudio;

        public MainWindow()
        {
            InitializeComponent();

            //Restore HotKey
            hotKey = (Key)Enum.Parse(typeof(Key), Properties.Settings.Default.HotKey);
            hotKeyModifiers = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), Properties.Settings.Default.Modifiers);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Read User's Configuration
            meow = Properties.Settings.Default.Meow;
            darkMode = Properties.Settings.Default.DarkMode;
            if (darkMode)
            {
                SwitchToDarkMode();
            }
            topmost = Properties.Settings.Default.Topmost;
            this.Topmost = topmost;

            //Read User's Last Input
            ClickTypeSelection.SelectedIndex = Properties.Settings.Default.ClickType;
            MinsTextBox.Text = Properties.Settings.Default.ClickIntervalMins;
            SecsTextBox.Text = Properties.Settings.Default.ClickIntervalSecs;
            MsTextBox.Text = Properties.Settings.Default.ClickIntervalms;
            RandomDelayEnabledToggleButton.IsChecked = Properties.Settings.Default.RandomDelayEnable;
            RandomDelaySecsTextBox.Text = Properties.Settings.Default.RandomDelaySecs;
            RandomDelayMsTextBox.Text = Properties.Settings.Default.RandomDelayms;
            PositionXTextBox.Text = Properties.Settings.Default.XPos;
            PositionYTextBox.Text = Properties.Settings.Default.YPos;
            RandomAreaEnabledToggleButton.IsChecked = Properties.Settings.Default.RandomAreaEnabled;
            RandomAreaWidth.Text = Properties.Settings.Default.RandomAreaWidth;
            RandomAreaHeight.Text = Properties.Settings.Default.RandomAreaHeight;
            InfinityClickEnabledToggleButton.IsChecked = Properties.Settings.Default.InfinityClickEnabled;
            ClickTimesTextBox.Text = Properties.Settings.Default.ClickTimes;

            meowAudio = new System.Media.SoundPlayer(Properties.Resources.meow);
        }

        #region HotKey
        //Register HotKey on program start
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Regist(this, hotKeyModifiers, hotKey, () =>
            {
                if (StopButton.IsEnabled) { StopButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent)); }
                else { StartButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent)); }
            });
        }

        //Update HotKey
        public void UpdateHotKey(Key key, ModifierKeys modifiers)
        {
            hotKey = key;
            hotKeyModifiers = modifiers;
            UpdateHintBox(0);
            ModifyGlobalHotKey(new WindowInteropHelper(this), key, modifiers);
        }
        #endregion

        #region UI
        //Update Hint TextBox
        public void UpdateHintBox(int signal)
        {
            if (CultureInfo.CurrentUICulture.Name == "zh-CN")
            {
                switch (signal)
                {
                    case 0:
                        HintTextBox.Text = $"当前快捷键为 {hotKeyModifiers} + {hotKey}";
                        break;
                    case 1:
                        HintTextBox.Text = "点击完成了喵~";
                        break;
                    case 2:
                        HintTextBox.Text = $"点击开始！按下 {hotKeyModifiers} + {hotKey} 来停止喵~";
                        break;
                    case 3:
                        HintTextBox.Text = $"无限点击开启, 记得用 {hotKeyModifiers} + {hotKey} 来退出喵~";
                        break;
                    case 4:
                        HintTextBox.Text = "无限点击已关闭喵~";
                        break;
                }
            }
            else
            {
                switch (signal)
                {
                    case 0:
                        HintTextBox.Text = $"Current hotKey is {hotKeyModifiers} + {hotKey}";
                        break;
                    case 1:
                        HintTextBox.Text = "Click Completed! Meow~";
                        break;
                    case 2:
                        HintTextBox.Text = $"Click started! Press {hotKeyModifiers} + {hotKey} to stop!";
                        break;
                    case 3:
                        HintTextBox.Text = $"Infinite clicks enabled, remember to use {hotKeyModifiers} + {hotKey} to stop it!";
                        break;
                    case 4:
                        HintTextBox.Text = "Infinite clicks disabled";
                        break;
                }
            }

        }

        private void WindowsDragZone(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //Load Setting Panel
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingPanel settingPanel = new SettingPanel(hotKey, hotKeyModifiers);
            settingPanel.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settingPanel.Owner = this;
            settingPanel.ShowDialog();
        }

        //Theme Settings
        public void SwitchToDarkMode()
        {
            var paletteHelper = new PaletteHelper();
            PrimaryColor primary = PrimaryColor.Amber;
            Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)primary];
            Resources["CardBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"));
            Resources["IndicatorColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffb330"));
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Dark);
            theme.SetPrimaryColor(primaryColor);
            paletteHelper.SetTheme(theme);
            darkMode = true;
        }

        public void SwitchToLightMode()
        {
            var paletteHelper = new PaletteHelper();
            PrimaryColor primary = PrimaryColor.DeepPurple;
            Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)primary];
            Resources["CardBackgroundColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8f8ff"));
            Resources["IndicatorColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b39ddb"));
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            theme.SetPrimaryColor(primaryColor);
            paletteHelper.SetTheme(theme);
            darkMode = false;
        }

        //Load Random Area Selector
        private void PositionSelectButton_Click(object sender, RoutedEventArgs e)
        {
            //Minimising the main window will cause the child window to lose focus
            //So here the main window is set to transparent to achieve the same visual effect
            this.Opacity = 0;
            //this.WindowState = WindowState.Minimized;
            PositionSelector positionSelector = new PositionSelector(RandomAreaEnabledToggleButton.IsChecked.Value);
            positionSelector.Owner = this;
            positionSelector.ShowDialog();
        }

        public void UpdateClickPositionTextBox(int x, int y, int w, int h)
        {
            try
            {
                PositionXTextBox.Text = x.ToString();
                PositionYTextBox.Text = y.ToString();
                RandomAreaWidth.Text = w.ToString();
                RandomAreaHeight.Text = h.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessageOutput(ex.ToString());
                return;
            }
        }

        //Animation: Minimize Window
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            var story = (Storyboard)this.Resources["MinimizeWindow"];
            if (story != null)
            {
                story.Completed += delegate { this.WindowState = WindowState.Minimized; };
                story.Begin(this);
            }
            //this.WindowState = WindowState.Minimized;
        }

        //Animation: Restore Window From TaskBar
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal && this.Opacity==0)
            {
                var story = (Storyboard)this.Resources["MaximizeWindow"];
                story.Begin(this);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Save User's Options
            Properties.Settings.Default.Meow = meow;
            Properties.Settings.Default.DarkMode = darkMode;
            Properties.Settings.Default.Topmost = topmost;
            Properties.Settings.Default.HotKey = hotKey.ToString();
            Properties.Settings.Default.Modifiers = hotKeyModifiers.ToString();

            //Save User's Input
            Properties.Settings.Default.ClickType = ClickTypeSelection.SelectedIndex;
            Properties.Settings.Default.ClickIntervalMins = MinsTextBox.Text;
            Properties.Settings.Default.ClickIntervalSecs = SecsTextBox.Text;
            Properties.Settings.Default.ClickIntervalms = MsTextBox.Text;
            Properties.Settings.Default.RandomDelayEnable = RandomDelayEnabledToggleButton.IsChecked.Value;
            Properties.Settings.Default.RandomDelaySecs = RandomDelaySecsTextBox.Text;
            Properties.Settings.Default.RandomDelayms = RandomDelayMsTextBox.Text;
            Properties.Settings.Default.XPos = PositionXTextBox.Text;
            Properties.Settings.Default.YPos = PositionYTextBox.Text;
            Properties.Settings.Default.RandomAreaEnabled = RandomAreaEnabledToggleButton.IsChecked.Value;
            Properties.Settings.Default.RandomAreaWidth = RandomAreaWidth.Text;
            Properties.Settings.Default.RandomAreaHeight = RandomAreaHeight.Text;
            Properties.Settings.Default.InfinityClickEnabled = InfinityClickEnabledToggleButton.IsChecked.Value;
            Properties.Settings.Default.ClickTimes = ClickTimesTextBox.Text;

            Properties.Settings.Default.Save();

            //Close Animation
            var story = (Storyboard)this.Resources["HideWindow"];
            if (story != null)
            {
                story.Completed += delegate { Close(); };
                story.Begin(this);
            }

            //Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnRegist(new WindowInteropHelper(this));
            Environment.Exit(0);
        }

        private void InfinityClickEnabledToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateHintBox(3);
        }

        private void InfinityClickEnabledToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateHintBox(4);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;

            UpdateHintBox(2);

            Thread clickLuncher = new Thread(ClickLuncher);
            clickLuncher.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        }
        #endregion

        #region Exception Handle
        private void ErrorMessageOutput(string message)
        {
            MessageBox.Show(message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Limit the text box to numbers only
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Verify if the input value is a number
        private int ValidIntConvertor(string text)
        {
            int res;
            try
            {
                res = Int32.Parse((text == "") ? "0" : text);
                return res;
            }
            catch (Exception ex)
            {
                ErrorMessageOutput(ex.ToString());
                return 0;
            }
        }
        #endregion

        //Read in the relevant parameters and implement mouse clicks
        private void ClickLuncher()
        {
            Random random = new Random();

            //Click Type Parameters
            int clickType = -1;

            //Click Interval Parameters
            bool randomDelayEnabled = false;
            int clickInterval = 0;
            int randomDelay = 0;

            //Click Position Parameters
            bool randomAreaEnabled = false;
            int xPos = 0, yPos = 0;
            int randomAreaWidth = 0, randomAreaHeight = 0;

            //Click Times Parameters
            bool infinityClickEnabled = false;
            int clickTimes = 0;

            Dispatcher.Invoke((Action)(() =>
            {
                //Get Click Type
                clickType = ClickTypeSelection.SelectedIndex;

                //Get Click Interval
                try
                {
                    randomDelayEnabled = RandomDelayEnabledToggleButton.IsChecked.Value;

                    clickInterval = (ValidIntConvertor(MinsTextBox.Text) * 60
                    + ValidIntConvertor(SecsTextBox.Text)) * 1000
                    + ValidIntConvertor(MsTextBox.Text);
                    clickInterval = (clickInterval == 0) ? 1 : clickInterval;

                    if (randomDelayEnabled)
                    {
                        randomDelay = ValidIntConvertor(RandomDelaySecsTextBox.Text) * 1000
                            + ValidIntConvertor(RandomDelayMsTextBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessageOutput(ex.ToString());
                    return;
                }

                //Get Click Position
                try
                {
                    randomAreaEnabled = RandomAreaEnabledToggleButton.IsChecked.Value;

                    xPos = ValidIntConvertor(PositionXTextBox.Text);
                    yPos = ValidIntConvertor(PositionYTextBox.Text);

                    if (randomAreaEnabled)
                    {
                        randomAreaWidth = ValidIntConvertor(RandomAreaWidth.Text);
                        randomAreaHeight = ValidIntConvertor(RandomAreaHeight.Text);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessageOutput(ex.ToString());
                    return;
                }

                //Get Click Times
                if (!infinityClickEnabled)
                {
                    try
                    {
                        infinityClickEnabled = InfinityClickEnabledToggleButton.IsChecked.Value;
                        clickTimes = ValidIntConvertor(ClickTimesTextBox.Text);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessageOutput(ex.ToString());
                        return;
                    }
                }
            }));

            //Do Clicks
            int xCoord = xPos, yCoord = yPos; //Click Position Coords
            bool manualStop = false;
            while (infinityClickEnabled || clickTimes > 0)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    manualStop = !StopButton.IsEnabled;
                    NonePaw.Visibility = Visibility.Collapsed;
                }));

                if (manualStop) { break; }

                //Calculate random click position if random click areas are enabled
                if (randomAreaEnabled)
                {
                    xCoord = (randomAreaWidth == 0) ? xPos : random.Next(xPos, xPos + randomAreaWidth + 1);
                    yCoord = (randomAreaHeight == 0) ? yPos : random.Next(yPos, yPos + randomAreaHeight + 1);
                }

                //Click Type
                if (clickType == 0)//Left Single Click
                {
                    Dispatcher.InvokeAsync((Action)(async () =>
                    {
                        LeftPaw.Visibility = Visibility.Visible;
                        NonePaw.Visibility = Visibility.Collapsed;
                        await Task.Delay(100);
                        LeftPaw.Visibility = Visibility.Collapsed;
                        NonePaw.Visibility = Visibility.Visible;
                    }));
                    User32API.ClickOnScreen(0, xCoord, yCoord);
                }
                else if (clickType == 1)//Left Double Click
                {
                    //Cat Paw Down
                    Dispatcher.InvokeAsync((Action)(async () =>
                    {
                        BothPaw.Visibility = Visibility.Visible;
                        NonePaw.Visibility = Visibility.Collapsed;
                        await Task.Delay(100);
                        BothPaw.Visibility = Visibility.Collapsed;
                        NonePaw.Visibility = Visibility.Visible;
                    }));

                    User32API.ClickOnScreen(0, xCoord, yCoord);//First Left Click

                    //Double Click Interval
                    if (randomDelayEnabled)
                    {
                        Thread.Sleep(random.Next(50, 300));//Simulate manual double-click delay
                    }
                    else
                    {
                        Thread.Sleep(200);
                    }

                    //Cat Paw Up
                    Dispatcher.InvokeAsync((Action)(async () =>
                    {
                        BothPaw.Visibility = Visibility.Visible;
                        NonePaw.Visibility = Visibility.Collapsed;
                        await Task.Delay(100);
                        BothPaw.Visibility = Visibility.Collapsed;
                        NonePaw.Visibility = Visibility.Visible;
                    }));

                    User32API.ClickOnScreen(0, xCoord, yCoord);//Second Left Click
                }
                else//Right Single Click
                {
                    Dispatcher.InvokeAsync((Action)(async () =>
                    {
                        RightPaw.Visibility = Visibility.Visible;
                        NonePaw.Visibility = Visibility.Collapsed;
                        await Task.Delay(100);
                        RightPaw.Visibility = Visibility.Collapsed;
                        NonePaw.Visibility = Visibility.Visible;
                    }));

                    User32API.ClickOnScreen(2, xCoord, yCoord);
                }

                //Click Interval
                if (randomDelayEnabled)
                {
                    Thread.Sleep(clickInterval + random.Next(0, randomDelay));
                }
                else
                {
                    Thread.Sleep(clickInterval);
                }

                if (!infinityClickEnabled) { clickTimes--; }
            }


            Dispatcher.InvokeAsync((Action)(() =>
            {
                if (StopButton.IsEnabled)
                {
                    StopButton.IsEnabled = false;
                    StartButton.IsEnabled = true;
                }
                NonePaw.Visibility = Visibility.Visible;
                LeftPaw.Visibility = Visibility.Collapsed;
                RightPaw.Visibility = Visibility.Collapsed;
                BothPaw.Visibility = Visibility.Collapsed;

                if (meow)
                {
                    meowAudio.Play();
                }

                UpdateHintBox(1);
            }));
        }

    }
}
