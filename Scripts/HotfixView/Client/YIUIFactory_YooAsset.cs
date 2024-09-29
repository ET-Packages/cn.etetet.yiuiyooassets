using UnityEngine;

namespace ET.Client
{
    public static partial class YIUIFactory
    {
        public static async ETTask<GameObject> InstantiateGameObjectAsync(string resName)
        {
            return await InstantiateGameObjectAsync("", resName);
        }
    }
}