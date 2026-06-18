using System;
using UnityObject = UnityEngine.Object;

namespace ET.Client
{
    /// <summary>
    /// YooAsset扩展  因为他不需要pkgName
    /// </summary>
    [FriendOf(typeof(YIUILoadComponent))]
    public static class YIUILoadComponentSystem_YooAsset
    {
        internal static async ETTask<T> LoadAssetAsync<T>(this YIUILoadComponent self, string resName) where T : UnityObject
        {
            return await self.LoadAssetAsync<T>("", resName);
        }

        internal static bool VerifyAssetValidity(this YIUILoadComponent self, string resName)
        {
            return self.VerifyAssetValidity("", resName);
        }

        #region 非泛型

        internal static async ETTask<UnityObject> LoadAssetAsync(this YIUILoadComponent self, string resName, Type assetType)
        {
            return await self.LoadAssetAsync("", resName, assetType);
        }

        #endregion
    }
}