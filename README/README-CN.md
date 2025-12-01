# 邦鼓猫连点器
![GitHub](https://img.shields.io/github/license/Siriusq/BongoPawClicker?style=flat-square)
![GitHub top language](https://img.shields.io/github/languages/top/Siriusq/BongoPawClicker?style=flat-square&logo=csharp&color=%232c8d0f)
![Static Badge](https://img.shields.io/badge/platform-windows-lightgrey.svg?style=flat-square&logo=windows11&label=platform&color=%230078D4)
![GitHub Release Date - Published_At](https://img.shields.io/github/release-date/Siriusq/BongoPawClicker?style=flat-square&logo=github)
![Static Badge](https://img.shields.io/badge/Framework%204.8-lightgrey.svg?style=flat-square&label=.NET&labelColor=%23555555&color=%23512BD4)

一个集成了邦鼓猫 (Bongo Cat) 的自动鼠标连点器

🔗[English Documents](../README.md)
🔗[下载](https://github.com/Siriusq/BongoPawClicker/releases/download/v1.0/BongoPawClicker.exe)
🔗[开发总结](https://siriusq.top/BongoPawClicker.html)

# 预览
![preview](./cn-preview.png)

# 特性
- 可设置随机点击间隔
- 可以设置点击范围，程序将会在范围内的随机位置进行点击
- 支持实时跟随鼠标位置的自动点击功能
- 可以设置无限连续点击
- 支持浅色/深色主题切换
- 支持根据系统语言设置自动切换中英文
- 支持自行修改快捷键
- 程序在执行自动点击操作时，邦鼓猫会根据点击方式做出不同的反应（猫爪拍桌.jpg）
- 可以设置在点击完成后发出猫叫提醒

# 使用指南
- **快捷键**：默认快捷键为 `Alt + Ctrl + P`。即使程序最小化，也可以通过该快捷键触发自动点击。在点击执行过程中再次按下快捷键，可以立即停止所有后续点击。
- **快捷键自定义**：在设置面板中左键单击快捷键预览文本框，然后直接按下想要的键盘组合即可录制新的快捷键，录制完成后点击确定保存。
- **实时跟随鼠标位置点击**：关闭**范围内随机位置点击**开关，并**清空**点击位置的 **X** 和 **Y** 坐标，即可让点击实时跟随鼠标位置。
- **点击位置选择**：
  - 当**范围内随机位置点击**开关未开启时，可在选择 Overlay 中点击任意位置完成选择。
  - 开启**范围内随机位置点击**开关时，通过按住鼠标左键执行框选操作，选择一个点击范围。
- **随机点击延时**：开启**点击间隔内随机延时**的开关即可在后面的文本框中输入延迟时间，例如点击间隔时间设置为**200**毫秒，延时时间设置为**100**毫秒，那么最终的点击间隔时间将会在**200-300**毫秒中随机取值。此外开启随机延时后，执行左键双击时双击的间隔将会在**50-300**毫秒中随机取值
- **点击位置坐标**：可以手动输入屏幕坐标，也可以点击**选择**按钮进入位置选择器中使用左键单击选择。在开启**范围内随机位置点击**开关后，可以在位置选择器中按住鼠标左键来框选范围。

# 开发
如果你想修改程序的代码，请打开**FodyWeavers.xml**，注释掉**Costura**，否则VS将不能正确的显示窗口预览。因为项目中使用了**Fody.Costura**来将程序打包为单个的exe，打包时dll等文件会嵌入exe中，导致VS因找不到需要的文件而报错。

例如某个需要的dll路径为`./bin/Release/xxx.dll`，Fody.Costura会将这个文件嵌入BongoPawClicker.exe中，`./bin/Release`目录下将不会存在`xxx.dll`，而VS仍然会按照先前的路径来寻找dll，导致出错。

```xml
<?xml version="1.0" encoding="utf-8"?>
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <!--<Costura />-->
</Weavers>
```

# 碎碎念
我开发这个连点器的动机是某款开放世界游戏的剧情越来越幼稚且编剧夹带私货，实在看不下去，却又不能跳过剧情，于是决定搞一个鼠标连点器来帮助我点点点播放剧情，顺带学习下WPF。至于邦鼓猫嘛，纯属找个乐子（欸嘿.jpg）

# 软件包
### NuGet
- [Material Design In XAML](http://materialdesigninxaml.net/)
- [Costura](https://github.com/Fody/Costura) 
- [Resource.Embedder](https://www.nuget.org/packages/Resource.Embedder/)

### 特别鸣谢
- [ChatGPT](https://chat.openai.com/)