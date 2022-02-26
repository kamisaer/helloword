using System;
using System.Collections;
using System.Collections.Generic;
using EH.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EH
{
    /// <summary>
    ///     框架入口,继承MonoBehaviour
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("95eh/FW")]
    public class FW : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> Modules = new Dictionary<Type, object>();

        private static readonly Counter fpsCounter = new Counter();

        private IHelper helper;

        /// <summary>
        ///     多线程模块
        /// </summary>
        public static IMThread Thread { get; private set; }

        /// <summary>
        ///     资源模块
        /// </summary>
        public static IMAsset Asset { get; private set; }

        /// <summary>
        ///     ECS实体组件模块
        /// </summary>
        public static IMComponent Component { get; private set; }

        /// <summary>
        ///     ECS实体模块
        /// </summary>
        public static IMEntity Entity { get; private set; }

        /// <summary>
        ///     ECS系统模块
        /// </summary>
        public static IMSystem System { get; private set; }

        /// <summary>
        ///     计时器模块
        /// </summary>
        public static IMTimer Timer { get; private set; }

        /// <summary>
        ///     日志模块
        /// </summary>
        public static IMLog Log { get; private set; }

        /// <summary>
        ///     消息派发模块
        /// </summary>
        public static IMNotice Notice { get; private set; }

        /// <summary>
        ///     网络长连接模块
        /// </summary>
        public static IMConn Conn { get; private set; }

        /// <summary>
        ///     多语言模块
        /// </summary>
        public static IMI18N I18N { get; private set; }

        /// <summary>
        ///     事件模块
        /// </summary>
        public static IMEvent Event { get; private set; }

        /// <summary>
        ///     Http模块
        /// </summary>
        public static IMHttp Http { get; private set; }

        /// <summary>
        ///     下载模块
        /// </summary>
        public static IMDownload Download { get; private set; }

        /// <summary>
        ///     资源更新模块
        /// </summary>
        public static IMVer Ver { get; private set; }

        /// <summary>
        ///     有限状态机模块
        /// </summary>
        public static IMFsm Fsm { get; private set; }

        /// <summary>
        ///     对象池模块
        /// </summary>
        public static IMPool Pool { get; private set; }

        /// <summary>
        ///     命令记录模块
        /// </summary>
        public static IMRecorder Recorder { get; private set; }

        /// <summary>
        ///     场景模块
        /// </summary>
        public static IMScene Scene { get; private set; }

        /// <summary>
        ///     演员模块
        /// </summary>
        public static IMActor Actor { get; private set; }

        /// <summary>
        ///     演员组件模块
        /// </summary>
        public static IMActorComponent ActorComponent { get; private set; }

        /// <summary>
        ///     UI模块
        /// </summary>
        public static IMUI UI { get; private set; }

        /// <summary>
        ///     音频模块
        /// </summary>
        public static IMAudio Audio { get; private set; }

        /// <summary>
        ///     数据模块
        /// </summary>
        public static IMData Data { get; private set; }

        /// <summary>
        ///     命令模块
        /// </summary>
        public static IMCmd Cmd { get; private set; }

        /// <summary>
        ///     FW单例
        /// </summary>
        public static FW Instance { get; private set; }

        /// <summary>
        ///     行为委托,可赋值为IMRecorder.Record,IMCmd.Execute或者其他自定义的方法
        /// </summary>
        public static Action<string, object> Action { get; set; }

        /// <summary>
        ///     FPS
        /// </summary>
        public static event Action<int> OnFPS;

        /// <summary>
        ///     每帧调用的事件，FW.Instance.Update触发
        /// </summary>
        public static event Action OnUpdate;

        /// <summary>
        ///     每帧调用的事件，FW.Instance.LateUpdate触发
        /// </summary>
        public static event Action OnLateUpdate;

        /// <summary>
        ///     固定更新事件，FW.Instance.FixedUpdate触发
        /// </summary>
        public static event Action OnFixedUpdate;

        /// <summary>
        ///     释放事件，FW.Instance.OnDestroy触发, 如果是程序退出后调用,使用OnQuit
        /// </summary>
        public static event Action OnDispose;

        /// <summary>
        ///     下一帧调用一次的事件,调用后被清空，FW.Instance.Update触发
        /// </summary>
        public static event Action OnOnceUpdate;

        /// <summary>
        ///     程序退出事件，FW.Instance.OnApplicationQuit触发
        /// </summary>
        public static event Action OnQuit;

        /// <summary>
        ///     获取新的Guid
        /// </summary>
        /// <returns></returns>
        public static string GetId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     通过类型获取模块实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Get(Type type)
        {
            return Modules.ContainsKey(type) ? Modules[type] : null;
        }

        /// <summary>
        ///     通过泛型获取模块实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            var type = typeof(T);
            return Modules.ContainsKey(type) ? (T) Modules[type] : default;
        }

        /// <summary>
        ///     通过泛型获取使用接口注册的实现类模块或使用父类注册的子类模块实例
        /// </summary>
        /// <typeparam name="T1">标记的类型或接口模块</typeparam>
        /// <typeparam name="T2">实际添加的子类型或者实现类型模块</typeparam>
        /// <returns></returns>
        public static T2 Get<T1, T2>() where T2 : T1
        {
            var type = typeof(T1);
            return Modules.ContainsKey(type) ? (T2) Modules[type] : default;
        }

        /// <summary>
        ///     是否添加过模块
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool Has(Type type)
        {
            return Modules.ContainsKey(type);
        }

        /// <summary>
        ///     是否添加过模块
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns></returns>
        public static bool Has<T>()
        {
            return Modules.ContainsKey(typeof(T));
        }

        /// <summary>
        ///     使用接口注册实现类模块或使用父类注册子类模块
        /// </summary>
        /// <typeparam name="T1">父类或接口类型</typeparam>
        /// <typeparam name="T2">子类或实现类型</typeparam>
        /// <returns></returns>
        public static T2 Register<T1, T2>() where T2 : T1, new()
        {
            var module = new T2();
            Modules.Add(typeof(T1), module);
            return module;
        }

        /// <summary>
        ///     泛型注册模块实例
        /// </summary>
        /// <param name="module">模块实例</param>
        /// <typeparam name="T">模块泛型</typeparam>
        public static void Register<T>(T module)
        {
            Modules.Add(typeof(T), module);
        }

        /// <summary>
        ///     通过可实例化的泛型类型添加模块
        /// </summary>
        /// <typeparam name="T">泛型模块</typeparam>
        /// <returns></returns>
        public static T Register<T>() where T : new()
        {
            var module = new T();
            Modules.Add(typeof(T), module);
            return module;
        }

        /// <summary>
        ///     通过类型添加模块
        /// </summary>
        /// <param name="type"></param>
        public static void Register(Type type)
        {
            Modules.Add(type, Activator.CreateInstance(type));
        }

        /// <summary>
        ///     通过类型移除模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Remove(Type type)
        {
            if (Modules.ContainsKey(type)) return Modules.Remove(type);

            Log.Warn($"not exist type {type}");
            return null;
        }

        /// <summary>
        ///     通过泛型类型移除模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Remove<T>()
        {
            return (T) Remove(typeof(T));
        }

        /// <summary>
        ///     通过实例移除模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static object Remove(object module)
        {
            var type = module.GetType();
            if (Modules.ContainsKey(type)) return Modules.Remove(type);

            Log.Warn($"not exist type {type}");
            return null;
        }

        /// <summary>
        ///     开启协程
        /// </summary>
        /// <param name="coroutine"></param>
        /// <returns></returns>
        public static Coroutine BeginCoroutine(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        /// <summary>
        ///     结束协程
        /// </summary>
        /// <param name="coroutine"></param>
        /// <returns></returns>
        public static void EndCoroutine(IEnumerator coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }

        /// <summary>
        ///     结束所有协程
        /// </summary>
        /// <param name="coroutine"></param>
        /// <returns></returns>
        public static void EndAllCoroutine()
        {
            Instance.StopAllCoroutines();
        }

        /// <summary>
        ///     延迟回调
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="action"></param>
        public static Coroutine Delay(float seconds, Action action)
        {
            return Instance.StartCoroutine(DelayCall(seconds, action));
        }

        private static IEnumerator DelayCall(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            action();
        }

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(this);

            //获取辅助类
            helper = GetComponent<IHelper>();
            if (helper == null) throw new Exception("not exist IMode component");

            //辅助类初始化,并赋值常用模块
            helper.Init();

            Thread = Get<IMThread>();
            Asset = Get<IMAsset>();
            Component = Get<IMComponent>();
            Entity = Get<IMEntity>();
            Timer = Get<IMTimer>();
            Log = Get<IMLog>();
            Notice = Get<IMNotice>();
            Conn = Get<IMConn>();
            I18N = Get<IMI18N>();
            Event = Get<IMEvent>();
            Http = Get<IMHttp>();
            Ver = Get<IMVer>();
            Download = Get<IMDownload>();
            Fsm = Get<IMFsm>();
            Pool = Get<IMPool>();
            Recorder = Get<IMRecorder>();
            Scene = Get<IMScene>();
            Actor = Get<IMActor>();
            ActorComponent = Get<IMActorComponent>();
            UI = Get<IMUI>();
            System = Get<IMSystem>();
            Audio = Get<IMAudio>();
            Data = Get<IMData>();
            Cmd = Get<IMCmd>();

            Log.Info("EH v0.3.3 \nDoc:https://blog.95eh.com/ehfw");

            helper.Begin();
        }

        public static void FPS()
        {
            fpsCounter.onCount += val => OnFPS?.Invoke((int) val);
            fpsCounter.Start(1, true);
        }

        private void Update()
        {
            fpsCounter.Add();

            if (OnOnceUpdate != null)
            {
                OnOnceUpdate();
                OnOnceUpdate = null;
            }

            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnDispose?.Invoke();
            fpsCounter.Dispose();
            OnUpdate = null;
            OnLateUpdate = null;
            OnFixedUpdate = null;
            OnDispose = null;
        }

        private void OnApplicationQuit()
        {
            OnQuit?.Invoke();
        }
    }
}