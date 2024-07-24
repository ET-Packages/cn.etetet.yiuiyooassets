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
}