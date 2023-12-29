using JetBrains.Annotations;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IoCConfig
{
    public static class GameConfigHelper
    {
        private const string GAME_CONFIG_PATH = "Assets/GameConfig.asset";
        [CanBeNull]
        public static GameConfig GameConfig { get; private set; }
        public static bool IsGameConfigLoaded => GameConfig != null;
        [CanBeNull]
        public static IoCConfig IoCConfig => GameConfig?.IoCConfig;

        public async static void GetGameConfigAsync(Action<GameConfig> OnGameConfigLoaded, Action OnGameConfigLoadFailed)
        {
            if (GameConfig == null)
            {
                AsyncOperationHandle<GameConfig> handle = Addressables.LoadAssetAsync<GameConfig>(GAME_CONFIG_PATH);
                await handle.Task;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameConfig = handle.Result;
                    OnGameConfigLoaded?.Invoke(GameConfig);
                    return;
                }

                OnGameConfigLoadFailed?.Invoke();
            }
            else
            {
                OnGameConfigLoaded?.Invoke(GameConfig);
            }
        }
    }
}