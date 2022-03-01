# 程序启动

> ## [1.程序启动](#1程序启动)
> ## [2.内部模块](#2内部模块)


## 1.程序启动

### 启动场景路径
> Assets/App.unity
> 启动脚本AppMode

![img](图1.png)

### 选择场景模式
> 主工程 字面意思
> 设备认知 单独打开设备资源库UI，点击Item后展示设备的模型，设备参数描述，拆解等
> 接线预览 用于设备接线单独测试，后期更新改动多大暂时放弃
```csharp
public enum SceneMode
{
    主工程 = 0,
    设备认知,
    接线预览
}
```
### 选择设备系统
> 只有在场景模式选择设备认知才生效
> 根据选择类型对加载的设备资源库进行筛选，只会加载选择类型设备
```csharp
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

### 开始运行
> 从Application.streamingAssetsPath中获取服务器url并保存
> WebGLUtil.baseUrl作为后续和后台请求url
> WebGLUtil.loginUrl用于编辑模式下进行本地账号登陆，方便开发测试
```csharp
IEnumerator ParseBackUrl() {
    UnityWebRequest request = UnityWebRequest.Get($"{Application.streamingAssetsPath}/url.json");
    yield return request.SendWebRequest();
    string str = request.downloadHandler.text;
    var content = JsonConvert.DeserializeObject<JObject>(str);
    WebGLUtil.baseUrl = content["baseUrl"].ToString();
    WebGLUtil.loginUrl = content["loginUrl"].ToString();
    Debug.Log($"请求地址  {WebGLUtil.baseUrl.ToString()}");
    Debug.Log($"测试登录地址  {WebGLUtil.loginUrl.ToString()}");
    OnStart();
}
```
### 截取前端url地址参数
> webgl版本 web前端通过url地址后缀添加参数返回到unity
> 解析方法 [WebGLUtil.instance.ParseUrlMsg("params string[] args)](http://ddd)
```csharp
public void OnStart() {
    WebGLUtil.instance.ParseUrlMsg(params string[] args);
    Global.defaultSystemIndex = ((int)defaultLoadSystem).ToString();
    string getParamsSceneMode = WebGLUtil.instance.GetParamsDic("SceneMode");
    string toInt = string.IsNullOrEmpty(getParamsSceneMode) ? ((int)sceneMode).ToString() :getParamsSceneMode;
    SceneMode nowMode = (SceneMode)int.Parse(toInt);
    ConText conText = new ConText(nowMode);
    conText.Launch();
    MainProcess.Instance.OnInitialize();
}
```


## 2.内部模块

### UI系统
> ui模块:UGUI  
> EH框架内置
> 继承 UIBase
> ui和代码分离,通过反射动态绑定ui
```csharp
var types = Assembly.GetExecutingAssembly().GetTypes();
foreach (var item in types)
{
    if (item.Namespace == Constant.FWNamespace.ViewNamespace && item.GetInterface(nameof(IUI)) != null)
    {
        FW.UI.BindUI(item, PathUtil.GetViewPath(item.Name));
    }
}
```
 [创建UI预制体和代码模板快捷工具 编辑器工具uigenarator](https://kamisaer.github.io/helloword/Tool/#编辑器工具uigenarator)

### 状态机
> EH框架内置
>状态继承接口 IState
>反射加载每个状态，使用FW.Fsm进行状态切换
>部分接口方法
```csharp
public interface IFsm{
T ChangeState<T>(bool forcibly = false) where T : IState;
T GetState<T>() where T : IState;
T RegisterState<T>() where T : IState, new();
T RemoveState<T>() where T : IState;
}
```

### 消息派发和事件
> EH框架内置
> FW.Notice调用
> 接口方法
```csharp
 public interface IMNotice
 {
  void BindNotice(string name, Action<object> action);
  void ClearNotice();
  void DispatchNotice(string name, object data = null);
  void UnbindNotice(string name, Action<object> action);
}
```
>事件的监听与注销  
>FW.Event调用

```csharp
 public interface IMEvent
 {
     //
     // 摘要:
     //     绑定事件
     //
     // 参数:
     //   name:
     //     名称
     //
     //   condition:
     //     触发条件
     //
     //   action:
     //     触发回调
     //
     //   priority:
     //     优先级
     void BindEvent(string name, Func<bool> condition, Action action, int priority = 0);
     //
     // 摘要:
     //     绑定事件行为,不设置条件
     //
     // 参数:
     //   name:
     //
     //   action:
     //
     //   priority:
     void BindEvent(string name, Action action, int priority = 0);
     //
     // 摘要:
     //     绑定事件,返回随机事件名
     //
     // 参数:
     //   condition:
     //
     //   action:
     //
     //   priority:
     string BindEvent(Func<bool> condition, Action action, int priority = 0);
     //
     // 摘要:
     //     绑定一次性事件,触发后移除掉
     //
     // 参数:
     //   condition:
     //
     //   action:
     //
     //   priority:
     void BindOnce(Func<bool> condition, Action action = null, int priority = 0);
     void ClearEvents();
     //
     // 摘要:
     //     重设触发条件
     //
     // 参数:
     //   name:
     //
     //   condition:
     void SetCondition(string name, Func<bool> condition, int priority = 0);
     //
     // 摘要:
     //     解绑事件
     //
     // 参数:
     //   name:
     void UnbindEvent(string name);
 }
```


### HTTP
>EH框架内置
>FW.Http调用
```csharp
 //
 // 摘要:
 //     http请求模块
 public interface IMHttp
 {
     void Delete(string uri, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Get(string uri, Action<byte[], IError> onResponse, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Post(string uri, string data, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Post(string uri, WWWForm form, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Post(string uri, Dictionary<string, string> fields, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Put(string uri, string data, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     void Put(string uri, byte[] data, Action<byte[], IError> onResponse = null, Dictionary<string, string> header = null, Action<float> onProgress = null);
     //
     // 摘要:
     //     设置默认请求头
     //
     // 参数:
     //   header:
     void SetHeader(Dictionary<string, string> header);
 }
```




### ECS
>EH内置
>只是参考了ECS模式的实现，并未带给项目性能提升
>暂弃



### 自定义模块
> 可以创建一个新的模块
> 比如 FW.Register<IDataManager, DataManager>();
> FW.Get<DataManager>() 调用
