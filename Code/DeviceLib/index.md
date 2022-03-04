# 设备资源库

![img](图1.png)

> UI数据来源 GroupData.json
> 对所有设备数据进行3级筛选分类 类型从大到小分别为
> [一](https://kamisaer.github.io/helloword/Code/Config/#systemtype-systemtype-系统类型)
> [二](https://kamisaer.github.io/helloword/Code/Config/#itemonetype-itemonetype-子类型配置名)
> [三](https://kamisaer.github.io/helloword/Code/Config/#itemtwotype-itemtwotype-二级类型配置名)

## 脚步参考: ElementLib.cs

脚步注释:
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

## 图集使用

> [doc](图集技术文档.docx)