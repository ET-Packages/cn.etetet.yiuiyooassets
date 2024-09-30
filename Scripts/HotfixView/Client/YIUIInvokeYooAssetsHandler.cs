using YIUIFramework;
using YooAsset;

namespace ET.Client
{
    [Invoke]
    public class YIUIInvokeYooAssetsHandler : AInvokeHandler<YIUIInvokeLoadInitialize, ETTask<bool>>
    {
        public override async ETTask<bool> Handle(YIUIInvokeLoadInitialize args)
        {
            YIUILoadComponent loadComponent = args.LoadComponent;
            if (loadComponent == null)
            {
                Log.Error($"LoadComponent is null!");
                return false;
            }

            await ETTask.CompletedTask;

            return loadComponent.AddComponent<YIUIYooAssetsLoadComponent>().Initialize();
        }
    }

    [Invoke(EYIUIInvokeType.Sync)]
    public class YIUIInvokeGetAssetsInfoSyncHandler : AInvokeHandler<YIUIInvokeGetAssetInfo, AssetInfo>
    {
        public override AssetInfo Handle(YIUIInvokeGetAssetInfo args)
        {
            return YIUILoadComponent.Inst?.GetComponent<YIUIYooAssetsLoadComponent>().GetAssetInfo(args.Location, args.AssetType);
        }
    }

    [Invoke(EYIUIInvokeType.Sync)]
    public class YIUIInvokeGetAssetInfoByGUIDSyncHandler : AInvokeHandler<YIUIInvokeGetAssetInfoByGUID, AssetInfo>
    {
        public override AssetInfo Handle(YIUIInvokeGetAssetInfoByGUID args)
        {
            return YIUILoadComponent.Inst?.GetComponent<YIUIYooAssetsLoadComponent>().GetAssetInfoByGUID(args.AssetGUID, args.AssetType);
        }
    }
}