# 工具篇
> ## [1.插件](#1插件)
> ## [2.内部工具](#2内部工具)

## 1.插件

> #### [1. EHFW0.3.0](#EHFW0.3.0)
> #### [2. Newtonsoft.Json.12.0.1](#newtonsoftjson1201)
> #### [3. Demigiant](#demigiant)
> #### [4. HighlightPlus](#highlightplus)
> #### [5. Vectrosity](#vectrosity)
> #### [6. Minikits](#minikits)
> #### [7. AVProVideo](#avprovideo)
> #### [8. NatCorder](#natcorder)
> #### [9. Paroxe](#paroxe)
> #### [10. RuntimeSceneGizmo](#runtimescenegizmo)
> #### [11. VolumetricLightBeam](#volumetriclightbeam)




======================================================================================


#### EHFW0.3.0

> 公司内部框架 集成了一些常用方法
> 需要同时导入 **Newtonsoft.Json.12.0.1** 和 **Demigiant** 插件
> 安装 **Addressables** 寻址

```csharp
> 部分调用举例
  - 加载UI并显示 
    - FW.UI.Load<XXXUI>().Show();
  - 加载资源 
    - FW.Asset.Load<GameObject>("Assets/a.prefab",(e)=> { });
  
```
 [接口调用示例](FW.cs)


#### Newtonsoft.Json.12.0.1
> json序列化和反序列化

```csharp
 try
{
  list = JsonConvert.DeserializeObject<List<GroupData>>(text.text);
}
  catch (Exception e)
{
  FW.Log.Error($"Deserialize To List<GroupData> Error, Msg :{e.Message}");
  return;
}
```

#### Demigiant
> 动画插件 用于摄像机的位移等移动 旋转等动作

#### HighlightPlus
> 3D物体高亮




#### Vectrosity
> 用于设备接线模块 2d接线
> 贝塞尔曲线

#### Minikits
> 轻量型3D具有物理属性的绳索 [商店地址](https://assetstore.unity.com/packages/tools/physics/rope-minikit-154662)  
> unity Project Setting Api等级需要设置.NET 4.X  
> Package安装Burst  

#### AVProVideo
>视频播放  

#### NatCorder
>运行时录屏  

#### Paroxe
> PDF  

#### RuntimeSceneGizmo
>运行时UI小组件，单击不同轴向可触发事件  


#### VolumetricLightBeam
>体积光  
>用于模拟灯光照射  

## 2.内部工具

> ### [1. excel2json](#excel2json)  
> ### [2. 根据模板生成json文件](#根据模板生成json文件)  
> ### [3. 根据模板生成设备安装xlsx文件](#根据模板生成设备安装xlsx文件)  
> ### [4. 根据模板生成设备端子xlsx文件](#根据模板生成设备端子xlsx文件)  
> ### [5. 编辑器UIGenarator](#编辑器工具uigenarator)  
> ### [6. 编辑器WebGLSetting](#编辑器工具webglsetting)




### excel2json
>将excel文件导入后可生成.json和.cs文件

>[MenuItem("CubeSpace/ExcelDataTable/(所有excel文件转json)Excel2Cs2Json")]
  - 调用.bat批处理excel文件转.json文件
  
### 根据模板生成json文件
>[MenuItem("CubeSpace/ConfigGenarator/xlsxtojson")]
  - 根据json模板生成一个新的json文件，并在新文件中对部分字段内容进行替换
  - 
### 根据模板生成设备安装xlsx文件
>[MenuItem("CubeSpace/ConfigGenarator/(创建 Devices Instanll 设备库安装xlsx文件)")]
  - 根据xlsx模板生成一个新的xlsx文件，并在新文件中对部分字段内容进行替换

### 根据模板生成设备端子xlsx文件
>[MenuItem("CubeSpace/ConfigGenarator/(创建 Devices Ports 设备库端子xlsx文件)")]
  - 根据xlsx模板生成一个新的xlsx文件，并在新文件中对部分字段内容进行替换

### 编辑器工具UIGenarator

>[MenuItem("UIGenarator/CreatPrefabs(选中ui)")]
  - 创建UI预制体到指定路径
>[MenuItem("UIGenarator/CreatViewScripts")]
  - 创建UI代码模板.cs文件
>[MenuItem("UIGenarator/BindViewObject")]
  - 自动获取UI组件和绑定按钮事件

### 编辑器工具WebGLSetting
>[MenuItem("WebGLSetting/FontsToMainFont")]
 - 替换UI上默认的字体

>[MenuItem("WebGLSetting/ImportSetting/SetTextureSize（选中Assets下Texture父级文件夹/512X512")]
 - 批量设置Texture分辨率

>[MenuItem("WebGLSetting/ImportSetting/SetModelsDragParams")]
 - 批量设置模型预制体（统一结构和添加碰撞器和便签，便于代码直接调用）

>[MenuItem("WebGLSetting/BuildWebGL")]
 - 打包webgl（打包流程-资源构建--App构建--到指定路径）

>[MenuItem("WebGLSetting/RunWebGLServerCmd")]
 - 运行本地服务器
 - 目前使用nodejs作为服务器启动，需要安装nodejs
  
>[MenuItem("WebGLSetting/PrintWebUrl")]
 - 输出本地服务器的url 
  
>  [MenuItem("CubeSpace/GuideScriptsGenarator/创建新手引导步骤(已经存在的不做处理)")]
 - 为每个引导步骤创建一个脚本文件

