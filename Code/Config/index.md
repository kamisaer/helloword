# 配置表加载和读取

> ## [1.配置表生成](#配置表加载和读取)


## 1.配置表生成
> 使用Excel存储静态数据  配置表存放路径 Assets同级 ExcelTables/

> ExcelTables/Device:  存放设备资源库数据
  - GroupData.xlsx:  所有设备数据
  - DeviceClassification.xlsx: 设备的类型分配


## GroupData.xlsx结构

| DeviceId  | cDeviceName | Enable |configName |installConfig |logicConfig |portConfig |icon |legend |model |SystemType |cSystemName |ItemOneType |cItemOneName |ItemTwoType |cItemTwoName |describe |param |
| --- | --- | --- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |
| 设备编号  | 设备名称  | 主页是否显示  |设备通用配置表  |安装通用配置表  |设备逻辑配置表  |设备端口配置表  |设备图标名  |设备图例名  |设备模型名  |系统配置名  |系统名  |子类型配置名  |子类型名  |二级类型  |二级名  |设备描述  |设备参数  |
| 1004  | 单元梯口机  | TRUE  |Device1004  |Device1004  |Device1004  |Device1004  |Device1004  |Legend1004  |Device1004  |1  |安全技术防范系统  |3  |可视对讲系统  |2  |控制设备  | 可视室内机对讲系统的终端设备，具有可视对讲、安防监控、信息接受和查阅等功能 |传输方式：TCP/IP 输入电源：12-24V DC 工作温度：-10℃-55℃ 安装方式：壁挂式  |

## 字段解释:
> ### **DeviceId <u>int</u> 设备唯一id**

> ### cDeviceName <u>string</u>  设备中文名称

> ### Enable <u>bool</u>  控制是否在设备库界面上显示

> ### configName <u>string</u> 设备通用配置表名
  - 为当前设备创建一个通用配置json文件，configName为json文件名称，通常命名规则是 $"Device{DeviceId}"  
  
  - 通过编辑器工具 [根据模板生成json文件](https://kamisaer.github.io/helloword/Tool/#根据模板生成json文件) 生成一个通用配置json文件
  
  - 通用配置模板项目路径 <u>Assets/ResourcePersistant/ConfigTempalte/DeviceTemp.json</u>
  
  - 已经创建的同名文件不会被覆盖
  
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

  -  "- -"后跟字符串会被自动替换成设备id
  
  -  生成的json文件路径 <u>Assets/ResourceLoad/Configs/Element/Device(设备id)/Device(设备id).json</u>
  
  -  该json文件主要用于配置设备动态加载时候需要挂载的脚本名称，也可以手动修改脚步名称，在设备实例化时候会反射加载脚步


> ### installConfig **string** 设备安装配置表

  - 根据模板创建xlsx文件 模板路径<u> Assets/ResourcePersistant/ConfigTempalte/InstallTemp.json</u>

| bodyId  | bodyName | available |alias | connectType |correstConnect |
| ---  | --- | --- |--- | --- |--- |
| 1  | 底座 | TRUE | MenQJ01 | 1 | 0 |

  - 通过编辑器工具 [根据模板生成设备安装xlsx文件](https://kamisaer.github.io/helloword/Tool/#根据模板生成设备安装xlsx文件) 生成设备安装xlsx文件文件
  
  -  生成的xlsx路径 Assets同级下<u>ExcelTables/Install/Device(设备id).xlsx</u>
  
  -  已经创建的同名文件不会被覆盖  