using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace BongoPawClicker
{
    internal class User32API
    {
        #region Click

        [DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        private const int MOUSEEVENT_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        /// <summary>
        /// Execute the click event
        /// </summary>
        /// <param name="clickType">Left Single Click / Right Single Click / Left Double Click</param>
        /// <param name="x">x Coords</param>
        /// <param name="y">y Coords</param>
        public static void ClickOnScreen(int clickType, int x, int y)
        {
            SetCursorPos(x, y);
            if (clickType == 2) { mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0); }
            else { mouse_event(MOUSEEVENT_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); }
        }

        #endregion


        #region HotKey

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifiers, uint vk);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int WM_HOTKEY = 0x312;
        public delegate void HotKeyCallBackHanlder();
        public const int HOTKEY_ID = 9000;
        //Commands that need to be executed by the hot key
        public static HotKeyCallBackHanlder hotKeyCallBackHanlder = null;

        /// <summary>
        /// Register a new hotkey
        /// </summary>
        /// <param name="window">The window that holds the shortcut keys</param>
        /// <param name="fsModifiers">Modifier Keys like Control, Alt and Shift</param>
        /// <param name="key">Key like ABCDEFG</param>
        /// <param name="callBack">callback function</param>
        public static void Regist(Window window, ModifierKeys fsModifiers, Key key, HotKeyCallBackHanlder callBack)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            var _hwndSource = HwndSource.FromHwnd(hwnd);
            _hwndSource.AddHook(WndProc);

            var vk = KeyInterop.VirtualKeyFromKey(key);
            if (!RegisterHotKey(hwnd, HOTKEY_ID, fsModifiers, (uint)vk))
                MessageBox.Show("Failed to register global hotkey!");
            hotKeyCallBackHanlder = callBack;
        }

        /// <summary> 
        /// Hot Key Message Handling 
        /// </summary> 
        static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {
                hotKeyCallBackHanlder();
            }
            return IntPtr.Zero;
        }

        /// <summary> 
        /// Unregister a exist hot key
        /// </summary> 
        public static void UnRegist(WindowInteropHelper windowInteropHelper)
        {
            UnregisterHotKey(windowInteropHelper.Handle, HOTKEY_ID);
        }

        /// <summary>
        /// Modify a exist hotkey, including unregister the old one and register a new one
        /// </summary>
        /// <param name="windowInteropHelper">Handle of the window holding the hotkey</param>
        /// <param name="key">Key like ABCDEFG</param>
        /// <param name="modifiers">Modifier Keys like Control, Alt and Shift</param>
        public static void ModifyGlobalHotKey(WindowInteropHelper windowInteropHelper, Key key, ModifierKeys modifiers)
        {
            //Unregister the previous global hotkey
            UnregisterHotKey(windowInteropHelper.Handle, HOTKEY_ID);
            //Registering new hotkey and handling possible exceptions
            if (!RegisterHotKey(windowInteropHelper.Handle, HOTKEY_ID, modifiers, (uint)KeyInterop.VirtualKeyFromKey(key)))
                MessageBox.Show("Failed to register global hotkey!");
        }

        #endregion
    }
}
