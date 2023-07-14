using System.Threading.Tasks;
using Merge2D.Source.Data;
using Merge2D.Source.Data.Constant;
using Merge2D.Source.Services;

namespace Merge2D.Source.Game
{
    public class BootState : GameState
    {
        private readonly IMainCameraProvider _mainCameraProvider;
        private readonly ICameraFactory _cameraFactory;
        private readonly IConfigsProvider _configsProvider;
        private readonly IAssetProvider _assetProvider;

        public BootState(IGameProvider gameProvider, IMainCameraProvider mainCameraProvider,
            ICameraFactory cameraFactory, IAssetProvider assetProvider, IConfigsProvider configsProvider) : base(gameProvider)
        {
            _mainCameraProvider = mainCameraProvider;
            _cameraFactory = cameraFactory;
            _assetProvider = assetProvider;
            _configsProvider = configsProvider;
        }

        public override async void Enter()
        {
            base.Enter();

            await LoadAssets();
            await CacheMainCamera();

            GameToGameLoopState();
        }

        private async Task LoadAssets()
        {
            _configsProvider.MergeConfig = await _assetProvider.LoadAssetAsync<IMergeConfig>(AddressableNames.MergeConfig);
        }

        private async Task CacheMainCamera()
        {
            _mainCameraProvider.Value = await _cameraFactory.Create(AddressableNames.Camera);
        }

        private void GameToGameLoopState()
        {
            Game.SwitchState<GameLoopState>();
        }
    }
}
