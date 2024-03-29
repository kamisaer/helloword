# 保存系统

> ## [1.保存格式](#1保存格式)
> ## [2.保存策略](#2保存策略)
> ## [3.问题记录](#3问题记录)
> ## [4.优化方向](#4优化方向)


## 1.保存格式
> 保存的数据需要存入后台，所以采用JSON结构进行存储，然后直接发送给后台服务器


## 2.保存策略

```Markdown
### 保存数据
    1. 创建一个Object对象，将所有数据都保存到这个对象中
```

```Markdown
### 保存行为
    1. 只记录用户操作，然后在加载时候复现
```
## 3.问题记录

> 数据嵌套过深 项目几个模块功能操作数据有直接依赖，类型和数据量很大，处理每个数据之间的依赖也很麻烦
> 加载保存数据过程，没有处理每个数据加载过程，无法获悉加载完成时间，可能在某些低配置电脑会出现加载错误问题
> 数据保存存在问题，无法二次编辑（主要是桥架绘制模块数据这块,前同事做的，目前没有时间处理），目前只能保存第一次编辑数据，保存后继续修改会导致再次保存数据c出现错误

## 4.优化方向

> 模块解耦，需要从设计和代码上对每个模块进行重构，减少各个模块直接的依赖
> 可使用ScriptableObject ，将数据存储在资源文件中，优点是可以肉眼可见的修改数据，方便管理它，也可以编辑器上进行数据修改，最后也能序列化成json
> 缺点是 项目后期才考虑保存问题，这坨屎主程大口吃🤢