# WorkweekChecker WPF (MVVM)

> 基于 WPF + MVVM 架构的单双周 / 节假日查询桌面应用

## 📌 简介
**WorkweekChecker** 是一个用于 **单双周工作制** 与 **法定节假日** 查询的桌面工具，采用 **WPF + MVVM** 架构开发，支持浅色/深色主题切换、节假日数据导入、基准周智能设置等功能。适合 **大小周制公司员工** 快速查询休息安排，也可作日常节假日参考。

---

## ✨ 功能特性
- **单双周自动判定**  
  基于用户设置的“双休周周一”自动推算任意日期的单双休状态。
- **任意日期查询**  
  选择任意日期，立即显示该周是单休还是双休，并高亮休息日。
- **法定节假日支持**  
  - 导入 JSON 节假日文件
  - 一键写入 **2025 国务院节假日安排**（内置模板）
- **节假日调整（调休）**
  - 自动将调休日期视为工作日
  - 自动将节假日视为休息日
- **主题切换**  
  浅色 / 深色主题，支持全局控件（按钮、日期选择器、输入框、卡片、设置窗口）同步切换。
- **周历徽标条**  
  使用红色/绿色徽标表示一周内工作日和休息日。
  - 红色（#DC2626）：工作日
  - 绿色（#16A34A）：休息日
- **MVVM 架构**  
  视图与业务逻辑解耦，方便后续功能扩展与维护。

---

## 🖥️ 界面示例

### 主界面

<img src="C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\image-20250811173227982.png" alt="image-20250811173227982" style="zoom:67%;" />



- 显示今天日期及单双休状态
- 查询指定日期
- 周历徽标条显示休息与工作日
- 配置按钮进入设置界面

### 设置界面

<img src="C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\image-20250811173252631.png" alt="image-20250811173252631" style="zoom:67%;" />

- **基准周配置**  
  选择任意属于“双休周”的日期，系统自动计算该周周一作为基准周。
- **法定节假日配置**  
  - 导入 JSON 文件（标准格式）
  - 一键导入 2025 官方节假日安排

---

## 📂 项目结构

```
WorkweekChecker/
 ├── Views/           # 界面 XAML 文件（MainWindow、SettingsWindow、HolidayImportWindow）
 ├── ViewModels/      # 视图模型（MainViewModel、SettingsViewModel、RelayCommand、ThemeManager 等）
 ├── Services/        # 核心业务逻辑（WorkweekService、HolidayService、OfficialHolidayProvider）
 ├── Controls/        # 自定义控件（Badge、ModernDatePicker）
 ├── Converters/      # 数据绑定转换器（BoolToBrushConverter、RestWorkToBgConverter 等）
 ├── Themes/          # 主题资源字典（Light.xaml、Dark.xaml、Base.xaml）
 ├── holidays.json    # 节假日数据文件（自动生成或导入）
 └── Readme.md        # 使用说明
```

---

## ⚙️ 安装与运行

### 运行环境
- **Windows 10 / 11**
- **.NET 8 SDK**  
- **Visual Studio 2022+**（建议安装 WPF 工作负载）

### 编译与启动
1. 克隆或下载本项目。
2. 使用 Visual Studio 打开 `WorkweekChecker.sln`。
3. 设置启动项目为 `WorkweekCheckerWPF`。
4. 按 `F5` 运行。

---

## 📄 数据文件说明

节假日数据存储在：

`%AppData%\WorkweekChecker\holidays.json`

### holidays.json 格式

```json
{
  "2025-01-01": "Holiday",
  "2025-02-10": "Workday"
}
```

- `Holiday` 表示节假日
- `Workday` 表示调休上班日
- 其余日期系统自动按单双周规则判断

------

## 📅 内置节假日

本程序内置：

- **2025 国务院法定节假日安排**

2026 年数据将在官方发布后可通过“导入节假日”功能加载。

------

## 🚀 后续可扩展功能

- 自动跟随系统主题切换
- 云端节假日数据同步
- 多语言支持（中文/英文）
- 桌面悬浮窗模式
- 任务栏日历插件

------

## 📝 授权协议

本项目以 MIT 协议开源，可自由修改、分发、二次开发。

------

**作者**: *CJN*
**版本**: v6.1
**更新时间**: 2025-08-11

---

