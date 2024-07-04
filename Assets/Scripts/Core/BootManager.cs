using Services.Level.Impl;
using Services.SceneLoading;
using Settings.GameLevels;
using UnityEngine;

namespace Core
{
    public class BootManager : MonoBehaviour
    {
        [SerializeField] private GameLevelsSettingsBase _gameLevelsSettings;
        
        private void Start()
        {
            var levelService = new LevelService(_gameLevelsSettings);
            levelService.Initialize();
            var sceneLoadingManager = new SceneLoadingManager();
            
            levelService.ResetProgress();
            sceneLoadingManager.LoadLevel(levelService.CurrentLevel);
        }
    }
}