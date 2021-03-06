﻿using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 只在编辑器下出现，分别对应一个Loader~生成一个GameObject对象，为了方便调试！
    /// </summary>
    public class BaseLoaderDebugger : MonoBehaviour
    {
        BaseLoader loader;
        public int refCount;
        public float finishUsedTime; // 参考，完成所需时间

        public static bool IsApplicationQuit = false;
        const string bigType = "BaseLoaderDebuger";

        public static BaseLoaderDebugger Create(string type, string uniqueKey, BaseLoader loader)
        {
            if (IsApplicationQuit) return null;

            //simplified uniqueKey
            uniqueKey = uniqueKey.Replace(AssetConfig.GetWritablePath(), "").Replace(AssetConfig.GetStreamingAssetsPath(), "").Replace(AssetConfig.GameAssetsFolder, "");

            // create a BaseLoaderDebugger
            GameObject newHelpGameObject = new GameObject(uniqueKey);
            var newHelp = newHelpGameObject.AddComponent<BaseLoaderDebugger>();
            newHelp.loader = loader;

            loader.DisposeEvent += () =>
            {
                DebuggerObjectTool.RemoveFromParent(bigType, type, newHelpGameObject);
            };

            // add to hierarchy
            DebuggerObjectTool.SetParent(bigType, type, newHelpGameObject);

            return newHelp;
        }

        private void Update()
        {
            refCount = loader.RefCount;
            finishUsedTime = loader.CostTime;
        }

        private void OnApplicationQuit()
        {
            IsApplicationQuit = true;
        }

    }

}
