<h1 align="center">BongoPawClicker</h1>

<p align="center">
An auto clicker with bongo cat integrated
</p>

<p align="center">
  <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/Siriusq/BongoPawClicker?style=for-the-badge&logo=c">  
  <img alt="GitHub Release Date" src="https://img.shields.io/github/release-date/Siriusq/BongoPawClicker?style=for-the-badge&logo=github">
  <img alt="GitHub License" src="https://img.shields.io/github/license/Siriusq/BongoPawClicker?style=for-the-badge&logo=git">
  <img alt="NET Framework" src="https://img.shields.io/badge/Framework%204.8-lightgrey.svg?style=for-the-badge&label=.NET&labelColor=%23555555&color=%23512BD4">
</p>

<p align="center">
  <a href="https://github.com/Siriusq/BongoPawClicker/blob/master/README/README-CN.md">🔗 中文文档</a>
  <a href="https://github.com/Siriusq/BongoPawClicker/releases/download/v1.1/BongoPawClicker.exe">🔗 Download</a>
  <a href="https://siriusq.top/en/bongo-paw-blicker.html">🔗 Development Summary</a>
  <a href="https://www.virustotal.com/gui/file/987c32823ee9e1a2da8869eab6798c5c17bc5de52e01b2e40686d2cdb2284297">🔗 VirusTotal Scan Report</a>
</p>

# 🎬 Preview
![preview](./README/en-preview.png)

# ✨ Features
- ⏱ Random click interval
- 🎯 Random click area, the program will click at random positions within the area.
- 🖱 Real-time mouse-following auto-click.
- 🔁 Unlimited continuous clicks
- 🌗 Light/Dark theme switching
- 🌐 Automatically switch between English and Chinese according to the system language settings
- ⌨️ Modify your own hotkeys
- 🐾 Bongo cats react differently depending on the click type (cat's paw on the table)
- 🔔 Catcall alert when clicking done

# How to Use
## 📘 Usage Guide
### ⌨️ Hotkey
- The default hotkey is **`Alt + Ctrl + P`**.
- You can trigger auto-clicking even when the program is minimized.
- Press the hotkey again during execution to immediately stop all subsequent clicks.
### 🎚️ Custom Hotkey
- In the settings panel, left-click the hotkey preview textbox.
- Press your desired key combination to record a new hotkey.
- Click **“OK”** to save it once you're done.
### 🖱️ Real-Time Mouse-Following Click
- Turn **off** the **Random Area Enable** option.
- **Clear** the **X** and **Y** coordinates of the click position.
- The auto-clicker will then click exactly where your mouse currently is, in real time.
### 🎯 Click Position Selection
- When **Random Area Enable** is **off**:
  - Click anywhere on the selection overlay to choose the exact click point.
- When **Random Area Enable** is **on**:
  - Hold the **left mouse button** and drag to select a rectangular click area.
### ⏱️ Random Click Interval
- Enable **Random Delay Enable** to enter a delay value in the textbox.
- Example: If the base interval is **200 ms** and the delay is **100 ms**, the final interval will be randomly chosen between **200–300 ms**.
- When random delay is enabled, the double-click interval will also be randomly chosen between **50–300 ms**.

# ⚠️ Known Issues
- 🛡 **Permission Issue**: Some applications run with higher privileges. If the auto-clicker does not work correctly in these programs, try running the auto-clicker as an administrator.
- ⏱ **Click Interval**: Due to timer precision and computer performance, the actual click interval may be slightly longer than the interval set.

# 💻 Development
If you want to modify the program’s code, please open **FodyWeavers.xml** and comment out **Costura**, otherwise VS won't be able to display the window preview correctly. Because **Fody.Costura** is used in the project to package the program into a single exe, DLL and other files will be embedded in the exe when packaging, which will cause VS to report an error due to inability to find the required file.

For example, if the path to a required DLL is `. /bin/Release/xxx.dll`, Fody.Costura will embed this file in BongoPawClicker.exe, the `xxx.dll` will not exist in the `bin/Release` directory, and VS will still look for the DLL according to the previous path, which leads to an error.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <!--<Costura />-->
</Weavers>
```

# 🐾 Murmur
My motivation for developing this clicker is that the plot of a certain open-world game that starts with G is getting more and more naive and the scriptwriters are bringing in their crap, and I couldn't stand it. Yet there is no option to skip the plot, so I decided to create a mouse clicker to help me automatically click to play the episode quickly without the need for me to watch it myself, and I can also learn WPF along the way. As for the bongo cat, it's just for fun! 🐱

# 📦 Package
### 🎲 NuGet
- [Material Design In XAML](http://materialdesigninxaml.net/)
- [Costura](https://github.com/Fody/Costura) 
- [Resource.Embedder](https://www.nuget.org/packages/Resource.Embedder/)

### 🙏 Special thanks
- [ChatGPT](https://chat.openai.com/)
