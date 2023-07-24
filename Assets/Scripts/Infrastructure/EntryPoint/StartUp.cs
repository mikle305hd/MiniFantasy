using Infrastructure.GameStates;
using Infrastructure.Services;
using StaticData.Character;
using UniDependencyInjection.Core;
using UniDependencyInjection.Extensions;
using UnityEngine;

namespace Infrastructure.EntryPoint
{
    public class StartUp : UniDependencyInjection.Unity.StartUp

    {
        private readonly ICoroutineRunner _coroutineRunner;
        private GameStateMachine _stateMachine;


        public StartUp(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        protected override void ConfigureServices(IContainerBuilder containerBuilder)
        {
            RegisterDefaultServices(containerBuilder);
            RegisterPlatformDependentServices(containerBuilder);
            RegisterGameStates(containerBuilder);
        }

        private void RegisterDefaultServices(IContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterSingle(_coroutineRunner)
                .RegisterSingle<ISceneLoader, SceneLoader>()
                .RegisterSingle<IAssetProvider, AssetProvider>()
                .RegisterSingle<IObjectsProvider, ObjectsProvider>()
                .RegisterSingle<IGameFactory, GameFactory>()
                .RegisterSingle<IUiFactory, UiFactory>()
                .RegisterSingle<IEnemyFactory, EnemyFactory>()
                .RegisterSingle<ILootFactory, LootFactory>()
                .RegisterSingle<IEnemyConfigurator, EnemyConfigurator>()
                .RegisterSingle<ICharacterConfigurator, CharacterConfigurator>()
                .RegisterSingle<IUiConfigurator, UiConfigurator>()
                .RegisterSingle<IProgressAccess, ProgressAccess>()
                .RegisterSingle<IProgressWatchers, ProgressWatchers>()
                .RegisterSingle<IStorageService, PlayerPrefsStorageService>()
                .RegisterSingle<IAutoSaver, AutoSaver>()
                .RegisterSingle<IStaticDataService, StaticDataService>()
                .RegisterSingle<IConfigAccess, ConfigAccess>()
                .RegisterSingle<IRandomizer, Randomizer>()
                .RegisterSingle<IFpsService, FpsService>();
        }

        private static void RegisterPlatformDependentServices(IContainerBuilder containerBuilder)
        {
            if (Application.isMobilePlatform)
            {
                containerBuilder.RegisterSingle<IInputService, MobileInputService>();
            }
            else
            {
                containerBuilder.RegisterSingle<IInputService, StandaloneInputService>();
            }
        }

        private static void RegisterGameStates(IContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterSingle<GameStateMachine>()
                .RegisterSingle<BootstrapState>()
                .RegisterSingle<StaticDataLoadingState>()
                .RegisterSingle<SettingsLoadingState>()
                .RegisterSingle<ProgressLoadingState>()
                .RegisterSingle<LevelLoadingState>()
                .RegisterSingle<GameProcessState>();
        }

        public void Start(IContainer container)
        {
            _stateMachine = container.CreateScope().Resolve<GameStateMachine>();
            _stateMachine.Enter<BootstrapState>();
        }
    }
}