# 配置表加载和读取

> ## [1.配置表生成](#配置表加载和读取)


## 1.配置表生成
> 使用Excel存储静态数据  配置表存放路径 Assets同级 ExcelTables/
> ExcelTables/Device:  存放设备资源库数据
  - GroupData.xlsx:  所有设备数据
  - DeviceClassification.xlsx: 设备的类型分配


### GroupData.xlsx结构

| DeviceId  | cDeviceName | Enable |configName |installConfig |logicConfig |portConfig |icon |legend |model |SystemType |cSystemName |ItemOneType |cItemOneName |ItemTwoType |cItemTwoName |describe |param |
| --- | --- | --- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |--- |
| 设备编号  | 设备名称  | 主页是否显示  |设备通用配置表  |安装通用配置表  |设备逻辑配置表  |设备端口配置表  |设备图标名  |设备图例名  |设备模型名  |系统配置名  |系统名  |子类型配置名  |子类型名  |二级类型  |二级名  |设备描述  |设备参数  |
| 1004  | 单元梯口机  | TRUE  |Device1004  |Device1004  |Device1004  |Device1004  |Device1004  |Legend1004  |Device1004  |1  |安全技术防范系统  |3  |可视对讲系统  |2  |控制设备  | fd |
  


