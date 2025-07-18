using System.IO;

namespace YooAsset.Editor
{
    /// <summary>
    /// 以收集器路径作为资源包名
    /// 注意：收集的所有文件打进一个资源包
    /// </summary>
    [DisplayName("收集YIUI资源")]
    public class YIUIFilterRule : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            // 收集在Prefabs路径下的资源
            if (Path.GetExtension(data.AssetPath) == ".prefab")
            {
                return data.AssetPath.Contains("/Prefabs/");
            }

            // 忽略掉图集
            if (Path.GetExtension(data.AssetPath) == ".spriteatlas" || Path.GetExtension(data.AssetPath) == ".spriteatlasv2")
            {
                return false;
            }

            return true;
        }
    }
    
    /// <summary>
    /// 以收集器路径作为资源包名
    /// 注意：收集的所有文件打进一个资源包
    /// </summary>
    [DisplayName("收集YIUI图集")]
    public class YIUIFilterRule_Atlas : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            if (Path.GetExtension(data.AssetPath) == ".spriteatlas" || Path.GetExtension(data.AssetPath) == ".spriteatlasv2")
            {
                return true;
            }

            return false;
        }
    }
}
