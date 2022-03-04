# 设备资源库

![img](图1.png)

> UI数据来源 GroupData.json
> 对所有设备数据进行3级筛选分类 类型从大到小分别为  
> ---
> [大系统分类](https://kamisaer.github.io/helloword/Code/Config/#systemtype-systemtype-系统类型)
> [二级分类](https://kamisaer.github.io/helloword/Code/Config/#itemonetype-itemonetype-子类型配置名)
> [三级分类](https://kamisaer.github.io/helloword/Code/Config/#itemtwotype-itemtwotype-二级类型配置名)

## 脚步参考: ElementLib.cs

脚步注释:
## 设备数据的分类,查询,筛选逻辑 SElementConf.cs

```csharp
/// <summary>
/// 获取系统下所有子类型
/// </summary>
/// <param name="deviceSystemType"></param>
/// <returns></returns>
public GroupData[] GetGroupSystemType(DeviceSystemType deviceSystemType){}

/// <summary>
/// 获取one类型获得列表
/// </summary>
/// <param name="deviceItemOneTypes"></param>
/// <param name="deviceOneType"></param>
/// <returns></returns>
public GroupData[] GetGroupByOneType(GroupData[] deviceItemOneTypes, DeviceOneType deviceOneType){}

 /// <summary>
 /// 获取two类型下列表
 /// </summary>
 /// <returns></returns>
 public List<ElementData> GetElementDataByTwoType(GroupData[] deviceItemTwoTypes, DeviceTwoType deviceTwoType){}

 /// <summary>
/// 设备id查询
/// </summary>
/// <param name="deviceId"></param>
/// <returns></returns>
public ElementData SearchDeviceById(int deviceId){}

/// <summary>
/// 设备名称查询
/// </summary>
/// <param name="deviceName"></param>
/// <returns></returns>
public List<ElementData>  SearchDeviceByFuzzyName(string deviceName){}

```
## 对象池使用
```csharp
    ### 通过name注册 item
    BindPool("element_tpl1");

    ------------------------

    ### 对象实例化
    var item = SpawnTransform("element_tpl1").gameObject;

    ### 元素创建和销毁系统 SSystemElement.cs

    public void CreateToolElement(object args, Action<IActor> OnComplete = null)
     {
        var data = (Data<string, ToolData, Vector3, Transform, DragStates, FloorMapIndex>)args;
        var entity = FW.Entity.SpawnEntity(data.data0, Constant.EntityType.CommonTool, data);
        FW.Actor.SpawnActor(entity, actor =>
        {
            OnComplete?.Invoke(actor);
        });

    }
     public void DeleteToolElement(object args)
     {
        var id = (string)args;
        FW.Actor.RecycleActor(id);
        FW.Entity.RemoveEntity(id);
     }

```
## 设备创建逻辑

> [doc 文档](设备创建.docx)

## 使用UIGUI图集优化性能 (同事注)

> [doc](图集技术文档.docx)

> 目前项目中图集使用嵌套太多层了，感觉没必要，,影响加载效率,这里做个优化
> --

```csharp
## 加载图集

string path = "SpriteAtlas图集地址";
string spriteName = "SpriteAtlas图集地址里面单张Sprite name"

### 这里只需要根据icon名字加载图集里面的单个Sprite就好

Addressables.LoadAssetAsync<Sprite>($"{path}[{spriteName}]").Completed += AtlasNameSubDone;

### 加载完成回调
 private void AtlasNameSubDone(AsyncOperationHandle<Sprite> obj)
  {
      if (obj.Result == null)
      {
          Debug.LogError("no sprite in atlas here.");
          return;
      }
      item.transform.FindComp<Image>("img_icon").sprite = obj.Result;
  }
```

