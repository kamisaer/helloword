# 设备仿真

> ## [1.设备仿真基类](#1设备仿真基类)
> ## [2.设备实现类](#2设备实现类)
> ## [3.设备实现类逻辑](#3设备实现类逻辑)
> ## [4.获取设备端子接线数据](#4获取设备端子接线数据)




## 1.设备仿真基类

设备基类  DeviceLogicBase 继承 ActorComponentBase

设备工作状态

```csharp

# 工作状态

public enum WorkStates {

        Undefined,
        /// <summary>
        /// 等待进行点位判断 传值等
        /// </summary>
        Initializeing,
        /// <summary>
        /// 传值和逻辑判定前的准备阶段
        /// </summary>
        Presetting,
        /// <summary>
        /// 未工作
        /// </summary>
        Stopped,
        /// <summary>
        /// 通电正常工作中
        /// </summary>
        Working,
        /// <summary>
        /// 特性或值不匹配等错误
        /// </summary>
        CommonError,
        /// <summary>
        /// 短路错误
        /// </summary>
        ShortCircuitError,
    }

 # 工作状态调用的虚方法

 /// <summary>
/// 初始化时，只执行一次
/// </summary>
protected virtual void OnInitialize() { }
    /// <summary>
/// 初始化中一直接调用
/// </summary>
protected virtual void ProcessInitializeing() { }
    /// <summary>
/// 判断值或者连接等状态，一直调用
/// </summary>
protected virtual void ProcessPresetting() { }
    /// <summary>
/// 设备工作中一直调用
/// </summary>
protected virtual void ProcessWorking() { }

。。。。。。。

```
## 2.设备实现类

继承于 DeviceLogicBase

实现类名在设备通用配置中进行配置,通过反射进行绑定


```cs
private void CreateElement(GameObject go, IEntity entity, Action<IActor> onComplete)
{
    go.name = $"{entity.type}_{entity.id}";
    var actor = go.GetComp<Actor>();
    actor.InitEntity(entity);
    var types = Assembly.GetExecutingAssembly().GetTypes();
    foreach (var item in types)
    {
        if (item.Namespace != Constant.FWNamespace.ComponentNamespace || item.GetInterface(nameof(IComponent)) == null)
            continue;
        if (entity.GetComponent(item) == null)
            continue;
        //通用配置中配置的是Enity组件，EC替换成AC就是Actor组件名
        var name = $"{Constant.FWNamespace.ActorComponentNamespace}.{item.Name.Replace("EC", "AC")}";
        var type = Type.GetType(name);
        if (type != null && type.Name != "ACElementDrag")
            actor.AddActorComponent(type);
    }
    actor.AddActorComponent<ACElement>();
    onComplete?.Invoke(actor);
}

```

实现类名在通用配置自动创建时候已经默认按照 EC+设备id 格式生成一个唯一的脚本名称
所以我们在开发中可以单独创建一个AC+设备id名就可以创建实现类
这块可以提供一个工具根据代码模板自动创建实现类脚本😜✨  很抱歉 偷懒没做

## 3.设备实现类逻辑

DeviceLogicBase 继承 ActorComponentBase

所以可以重写 InitActor

```cs
public override void InitActor(IActor actor)
```
在这个地方进行一些初始化操作，比如对象实例化，组件获取，事件绑定

重写 DeviceLogicBase 中的虚方法  
> 虚方法模拟了设备工作状态的一个顺序执行过程  
> 通常我们只用选择合适当前设备逻辑的虚方法进行重写  
> ing结尾的虚方法会进行轮询，其他只会在进入当前状态时候执行一次  
> 使用 ChangeState(WorkStates newState)在外部进行状态切换  

> 通常会在 ProcessInitializeing(初始化一直调用)查寻端子的连接情况  
> 如果连接正确,使用ChangeState(WorkStates newState)切换到工作状态
> 或者错误，切换到错误状态
> 然后在所处状态去设置设备的状态现象
> 通常设备仿真本身逻辑复杂，操作逻辑主要体现在UI层，这里不详解



## 4.获取设备端子接线数据

DeviceLogicBase 中提供了一个公开方法

```cs
    /// <summary>
    /// 获取接线信息
    /// </summary>
    /// <returns></returns>
    public DevicePortBase GetDevicePort()
```
这里在设备实体id和设备端子实体id中定义了一种规则,他们拥有相同的guidid，但是前缀不相同
调用静态方法，获取到设备端子实体id
Define.Find2dLine(actor.entity.id);
使用FW.Actor.GetActor获取到演员类


