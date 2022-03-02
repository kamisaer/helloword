# 配置表加载和读取

> ## [1.配置表](#配置表)
> ## [GroupData.xlsx结构](#groupdataxlsx结构)
> ## [DeviceClassification.xlsx结构](#deviceclassificationxlsx结构)
> ## [设备端子.xlsx文件结构](#portconfig-ustringu-设备安装配置表)
> ## [工具.xlsx文件结构](#portconfig-ustringu-设备安装配置表)
> ## [LineData.xlsx文件结构](#linedataxlsx文件结构)


> ## [.xlsx文件转json](#linedataxlsx文件结构)



## 1.配置表
> 使用Excel存储静态数据  配置表存放路径 Assets同级 ExcelTables/

> ExcelTables/Device:  存放设备资源库数据
  - GroupData.xlsx:  所有设备数据  
  - 
  - DeviceClassification.xlsx: 设备的类型分配


## GroupData.xlsx结构

| DeviceId  | cDeviceName | Enable |configName |installConfig |logicConfig |portConfig |icon |legend |model |SystemType |cSystemName |ItemOneType |cItemOneName |ItemTwoType |cItemTwoName |describe |param |
| --- | --- | --- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |
| 设备编号  | 设备名称  | 主页是否显示  |设备通用配置表  |安装通用配置表  |设备逻辑配置表  |设备端口配置表  |设备图标名  |设备图例名  |设备模型名  |系统配置名  |系统名  |子类型配置名  |子类型名  |二级类型  |二级名  |设备描述  |设备参数  |
| 1004  | 单元梯口机  | TRUE  |Device1004  |Device1004  |Device1004  |Device1004  |Device1004  |Legend1004  |Device1004  |1  |安全技术防范系统  |3  |可视对讲系统  |2  |控制设备  | 可视室内机对讲系统的终端设备，具有可视对讲、安防监控、信息接受和查阅等功能 |传输方式：TCP/IP 输入电源：12-24V DC 工作温度：-10℃-55℃ 安装方式：壁挂式  |

## 字段解释:
### **DeviceId <u>int</u> 设备唯一id**

### cDeviceName <u>string</u>  设备中文名称

### Enable <u>bool</u>  控制是否在设备库界面上显示

### configName <u>string</u> 设备通用配置表名
  - 为当前设备创建一个通用配置json文件，configName为json文件名称，通常命名规则是 $"Device{DeviceId}"  
  - 
  - 通过编辑器工具 [根据模板生成json文件](https://kamisaer.github.io/helloword/Tool/#根据模板生成json文件) 生成一个通用配置json文件
  - 
  - 通用配置模板项目路径 <u>Assets/ResourcePersistant/ConfigTempalte/DeviceTemp.json</u>
  - 
  - 已经创建的同名文件不会被覆盖  
  - 
  -  json中"- -"后跟字符串会被自动替换成设备id  
  -  
  -  生成的json文件路径 <u>Assets/ResourceLoad/Configs/Element/Device(设备id)/Device(设备id).json</u>  
  -  
  -  该json文件主要用于配置设备动态加载时候需要挂载的脚本名称，也可以手动修改脚步名称，在设备实例化时候会反射加载脚步  


```json

    {
    "DeviceId": --DeviceId,
    "attachType":"Devices",
    "entity2dType": "Element2d",
    "entity3dType": "Element",
    "visible": "true",
    "install":1,
    "components2d": [
	    "ECElement2dDrag"
    ] ,
    "components3d": [
        "ECElementDrag","ECDevice--DeviceId"
 
    ],
   "componentsPort": [
      "ECElement2dDrag","DeviceId--DeviceIdPort"
      ]
    }

```


### installConfig **string** 设备安装配置表
  - 根据模板创建xlsx文件 模板路径<u> Assets/ResourcePersistant/ConfigTempalte/InstallTemp.json</u>  
  - 
  - 通过编辑器工具 [根据模板生成设备安装xlsx文件](https://kamisaer.github.io/helloword/Tool/#根据模板生成设备安装xlsx文件) 生成设备安装xlsx文件文件  
  - 
  -  生成的xlsx路径 Assets同级下<u>ExcelTables/Install/Device(设备id).xlsx</u>  
  -  
  -  已经创建的同名文件不会被覆盖 
  -  
  -   | bodyId  | bodyName | available |alias | connectType |correstConnect |
| ---  | --- | --- |--- | --- |--- |
| 1  | 底座 | TRUE | MenQJ01 | 1 | 0 |

### installConfig <u>string</u> 设备安装配置表 
  - 设备逻辑配置表，用于自定义设备仿真配置

  
### portConfig <u>string</u> 设备安装配置表 
  - 根据模板创建xlsx文件 模板路径<u> Assets/ResourcePersistant/ConfigTempalte/PortTemp.xlsx</u> 
  - 
  - 通过编辑器工具 [根据模板生成设备端子xlsx文件](https://kamisaer.github.io/helloword/Tool/#根据模板生成设备端子xlsx文件) 生成设备端子xlsx文件文件  
  - 
  -  生成的xlsx路径 Assets同级下<u>ExcelTables/Port/Device(设备id).xlsx</u>  
  -  
  -  已经创建的同名文件不会被覆盖 
  -  | portInfo  | available | alias |name | portType |attribute | value | loop | features |
| ---  | --- | --- |--- | --- |--- | --- | --- | --- |
| 端口信息 | 是否可用 | 別名 | 名稱 | 類型 |屬性（0SM/1M/2S） | 值(电压) | 回路 | 特性匹配 |
| 电源输入 | TRUE | DC+ | DC+ | 1 | 2 | 16.5 | 1 | qwer
| 

> portType字段对应类型

```csharp
public enum PortTypes
{
    Undefined =0,
    /// <summary>
    /// 导线跳线接口
    /// </summary>
    Wire = 1,
   
    /// <summary>
    /// 网线接口
    /// </summary>
    LAN = 2,
    /// <summary>
    /// 视频线接口
    /// </summary>
    Video = 3,
}
```
> attribute属性 ： 0既是输入和输出，1是输出，2是输入
> --------------------------------------------------------------------------------------------------
> ### SystemType **SystemType** 系统类型

```csharp
/// <summary>
/// 系统
/// </summary>
public enum DeviceSystemType
{

    全部 = 0,
    安全技术防范系统 = 1,
    消防自动化系统 = 3,
    综合布线系统 = 4,
    IBMS系统 = 5,
    会议系统 = 6,
    广播系统 = 7,
    LED显示系统 = 8,
    信息发布系统 = 9,
    网络电视系统 = 10,
    供配电系统 = 11,
    机房工程 = 12,
    周界安防系统 = 13,
    楼宇自控系统 = 14,
    其他 = 99 

}
```

> ### ItemOneType **ItemOneType** 子类型配置名

```csharp
public enum DeviceOneType
{
    全部 = 0,
    视频监控系统 = 1,
    入侵报警系统 = 2,
    可视对讲系统 = 3,
    门禁系统 = 4,
    停车场管理系统 = 5,
    防火卷帘门系统 = 6,
    防排烟系统 = 7,
    火灾自动报警系统 = 8,
    气体灭火系统 = 9,
    消防应急照明及疏散指示系统 = 10,
    消火栓系统 = 11,
    自动喷淋灭火系统 = 12,
    空调系统 = 13,
    未知 = 14,
    给排水系统 = 15,
    供配电系统 = 16,
    照明系统 = 17,
    电梯系统 = 18,
    水平子系统 = 19,
    垂直子系统 = 20,
    管理间子系统 = 21,
    工作区子系统 = 22,
    设备间子系统 = 23,
    有线 = 24,
    其他 =99,
}
}
```

### ItemTwoType **ItemTwoType** 二级类型配置名

```csharp
public enum DeviceTwoType
{
    全部 = 0,
    存储设备 = 1,
    控制设备 = 2,
    解码器 = 3,
    通信设备 = 4,
    摄像机 = 5,
    显示设备 = 6,
    扩展设备 = 7,
    前端探测器 = 8,
    报警装置 = 9,
    电源 = 10,
    传感器 = 11,
    火灾报警装置 = 12,
    火灾显示盘 = 13,
    火灾探测器 = 14,
    消防模块 = 15,
    消防电话 = 16,
    消火栓按钮 = 17,
    手动报警按钮 = 18,
    消防电源 = 19,
    广播设备 = 20,
    附件 = 21,
    消防联动控制器 = 22,
    闭式喷头 = 23,
    消防风机 = 24,
    消防水泵接合器 = 25,
    稳压泵 = 26,
    消防水泵 = 27,
    阀门 = 28,
    气压水罐 = 29,
    沟槽连接件 = 30,
    执行器 = 31,
    接触器 = 32,
    开关 = 33,
    空调机组 = 34,
    压缩式冷水机组 = 35,
    变送器 = 36,
    其他 = 37,
    输入设备 = 38,
    防火卷帘门设备 = 39,
    扩音设备 = 40,
    中控设备 = 41,
    电脑主机 = 42,
    服务器 = 43,
        灯具 =44,
    断路器 = 45,
    插座 =47,
    电能表 = 48,
    配电配件 =49,
}
```

> 三个设备类型用于设备分类

## DeviceClassification.xlsx结构

-------------------------------
 | secondId  | firstId |
  | ---  | --- |
 | 二级分类  | 一级分类 |
 | secondId  | firstId |
 | 1  | 1 |

 > 定义了三个设备类型从属关系,<u>DeviceOneType.视频监控系统</u> 包含于 <u>DeviceSystemType.安全技术防范系统</u>

 ## 工具.xlsx文件结构

 | Id  | configName | cName | type | icon | model | Visible |
 | 序号 | 配置表名称 | 名称 | 类型 | 图标 | 模型 | 是否可用 |
  | --- | --- | --- | --- | --- | --- | --- |
  | 10008 | -- |卷尺 | 0 | JC | JuanChi | 模型 | TRUE |

type :工具类型 
```csharp
  public enum ToolEnumType {
	测量 ,      
	登高,	
	夹持,
	紧固, 
	敲击,
	切割,
	修整,
	其他,
	门禁
}
```

## LineData.xlsx文件结构
>参考 GroupData .xlsx结构

## .xlsx文件转json

> 使用工具[to json](https://kamisaer.github.io/helloword/Tool/#根据模板生成json文件)
> 工具路径 Asset同级 excel2json
> .bat命令


<details><summary>excel2json2cs.bat</summary>
<p>

:excel表格路径
@SET EXCEL_DEVICE_FOLDER=.\ExcelTables\Device
@SET EXCEL_PORT_FOLDER=.\ExcelTables\Port
@SET EXCEL_TOOL_FOLDER=.\ExcelTables\Tool
@SET EXCEL_LINE_FOLDER=.\ExcelTables\Line
@SET EXCEL_INSTALL_FOLDER=.\ExcelTables\Install


:项目json路径
@SET JSON_FOLDER=.\Assets\ResourceLoad\Configs
@SET JSON_PORT_FOLDER=.\Assets\ResourceLoad\Configs\Port

@SET JSON_TOOL_FOLDER=.\Assets\ResourceLoad\Configs\Tool
@SET JSON_LINE_FOLDER=.\Assets\ResourceLoad\Configs\Line
@SET JSON_INSTALL_FOLDER=.\Assets\ResourceLoad\Configs\Install


@SET CS_FOLDER=.\Assets\App\Definition\Object
@SET EXE=.\excel2json\excel2json.exe

@ECHO Converting excel files in folder %EXCEL_DEVICE_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_DEVICE_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_DEVICE_FOLDER%\%%~nxi --json %JSON_FOLDER%\%%~ni.json --csharp %CS_FOLDER%\%%~ni.cs --header 3 --a True
)
@ECHO Converting excel files in folder %EXCEL_TOOL_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_TOOL_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_TOOL_FOLDER%\%%~nxi --json %JSON_FOLDER%\%%~ni.json --csharp %CS_FOLDER%\%%~ni.cs --header 3 --a True
)

@ECHO Converting excel files in folder %EXCEL_PORT_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_PORT_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_PORT_FOLDER%\%%~nxi --json %JSON_PORT_FOLDER%\%%~ni.json  --header 3 --a True
)

@ECHO Converting excel files in folder %EXCEL_LINE_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_LINE_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_LINE_FOLDER%\%%~nxi --json %JSON_LINE_FOLDER%\%%~ni.json --csharp %CS_FOLDER%\%%~ni.cs --header 3 --a True

)

@ECHO Converting excel files in folder %EXCEL_INSTALL_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_INSTALL_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_INSTALL_FOLDER%\%%~nxi --json %JSON_INSTALL_FOLDER%\%%~ni.json  --header 3 --a True
)
pause

</p>
</details>

