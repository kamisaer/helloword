# 项目篇

> ## [1.Webgl踩坑](#1webgl踩坑)
> ## [2.coding管理](#2coding管理)
> ## [3.SourTree使用](#3SourTree使用)


## 1.Webgl踩坑
[Webgl踩坑](WebGl开发.docx);

补充:
ugui InputField:
网页上 InputField 输入无法显示中文

```cs
[AddComponentMenu("WebGL/SearchBox")]

// call js function
 Application.ExternalCall(JsOnShowInputFun, gameObject.name, IF_SearchBox.text);

```
js 方法 在index.html 中调用
```js
  function OnShowHTMLSearchBox(n,textStr)
  {
        UnityFunctionName = n;
        var s = document.getElementById("SearchBoxInputId");
        s.value = textStr;
         s.style.visibility = "visible";  		
         s.style.opacity = 0; //透明化
         s.focus(); //获取焦点
  }
```

## webgl回调
>  WebGLUtil.cs  相关webgl平台才能使用的方法
>  WebGLUtil给出了一些js的解决办法
>  ReceiveWebGLCallBack 中处理js 发过来的消息事件
>  二进制文件保存和加载
>  获取url参数
>  监听网页刷新和关闭
>  js网页弹窗

## 2.coding管理

## 3.SourTree使用