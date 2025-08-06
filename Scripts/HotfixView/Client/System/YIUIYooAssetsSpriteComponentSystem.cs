using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using YooAsset;

namespace ET.Client
{
    [FriendOf(typeof(YIUIYooAssetsSpriteComponent))]
    [EntitySystemOf(typeof(YIUIYooAssetsSpriteComponent))]
    public static partial class YIUIYooAssetsSpriteComponentSystem
    {
        [EntitySystem]
        private static void Awake(this YIUIYooAssetsSpriteComponent self)
        {
            self.m_YIUILoadRef = self.GetParent<YIUILoadComponent>();
            var tAssetHandle = YooAssets.LoadAssetSync<AtlasInfo>("AtlasConfig");
            var tAtlasInfo = tAssetHandle.AssetObject as AtlasInfo;
            if (tAtlasInfo == null) return;
            self.m_SpritePathMap.Clear();
            foreach (var tSpriteInfo in tAtlasInfo.sprites)
            {
                foreach (var tSpriteName in tSpriteInfo.spriteNames)
                {
                    self.m_SpritePathMap.Add(tSpriteName, tSpriteInfo.atlasLocation);
                }
            }

            tAssetHandle.Release();
        }

        public static string GetSpriteAtlasPath(this YIUIYooAssetsSpriteComponent self, string spriteName)
        {
            return self.m_SpritePathMap.GetValueOrDefault(spriteName);
        }

        public static Sprite GetSprite(this YIUIYooAssetsSpriteComponent self, string spriteName)
        {
            Sprite sprite = null;

            if (!self.m_SpritePathMap.TryGetValue(spriteName, out string atlasPath))
            {
                #if UNITY_EDITOR
                if (!self.YIUILoad.VerifyAssetValidity(spriteName))
                {
                    Log.Error($"验证资产有效性 没有这个资源 图片无法加载 请检查 {spriteName}");
                    return null;
                }
                #endif

                sprite = self.YIUILoad.LoadAsset<Sprite>(spriteName);

                if (sprite == null)
                {
                    Log.Error($"加载失败 没有这个资源 图片无法加载 请检查 {spriteName}");
                }
            }
            else
            {
                var spriteAtlas = self.YIUILoad.LoadAsset<SpriteAtlas>(atlasPath);
                sprite = spriteAtlas?.GetSprite(spriteName);
                if (sprite == null)
                {
                    Log.Error($"找到图集{atlasPath},但是没有找不到Sprite：{spriteName}");
                    return null;
                }

                if (!self.m_LoadedSprites.TryAdd(sprite, spriteAtlas))
                {
                    Log.Error($"重复添加Sprite：{spriteName}");
                }
            }

            return sprite;
        }

        public static async ETTask<Sprite> GetSpriteAsync(this YIUIYooAssetsSpriteComponent self, string spriteName)
        {
            Sprite sprite = null;

            if (!self.m_SpritePathMap.TryGetValue(spriteName, out string atlasPath))
            {
                #if UNITY_EDITOR
                if (!self.YIUILoad.VerifyAssetValidity(spriteName))
                {
                    Log.Error($"验证资产有效性 没有这个资源 图片无法加载 请检查 {spriteName}");
                    return null;
                }
                #endif

                sprite = await self.YIUILoad.LoadAssetAsync<Sprite>(spriteName);

                if (sprite == null)
                {
                    Log.Error($"加载失败 没有这个资源 图片无法加载 请检查 {spriteName}");
                }
            }
            else
            {
                var spriteAtlas = await self.YIUILoad.LoadAssetAsync<SpriteAtlas>(atlasPath);
                sprite = spriteAtlas?.GetSprite(spriteName);
                if (sprite == null)
                {
                    Log.Error($"找到图集{atlasPath},但是没有找不到Sprite：{spriteName}");
                    return null;
                }

                if (!self.m_LoadedSprites.TryAdd(sprite, spriteAtlas))
                {
                    Log.Error($"重复添加Sprite：{spriteName}");
                }
            }

            return sprite;
        }

        public static void ReleaseSprite(this YIUIYooAssetsSpriteComponent self, Sprite sprite)
        {
            if (self.m_LoadedSprites.ContainsKey(sprite))
            {
                var spriteAtlas = self.m_LoadedSprites[sprite];
                self.m_LoadedSprites.Remove(sprite);
                self.YIUILoad.Release(spriteAtlas);
            }
            else
            {
                self.YIUILoad.Release(sprite);
            }
        }
    }
}