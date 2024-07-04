using Services.SceneLoading;
using Settings.GameLevels;
using UnityEngine;

namespace Services.Level.Impl
{
    public class LevelService : ILevelService
    {
        private readonly GameLevelsSettingsBase _gameLevelsSettings;
        private readonly SceneLoadingManager _sceneLoadingManager;
        private const string LevelIdKey = "LevelId";
        
        public LevelService(
            GameLevelsSettingsBase gameLevelsSettings)
        {
            _gameLevelsSettings = gameLevelsSettings;
        }

        public string CurrentLevel { get; private set; }

        public void Initialize()
        {
            var currentLevelId = PlayerPrefs.GetInt(LevelIdKey, 0);
            
            CurrentLevel = _gameLevelsSettings.GetLevelByIndex(currentLevelId);
        }

        public string GetNextLevelName()
        {
            var currentLevelId = PlayerPrefs.GetInt(LevelIdKey, 0);
            
            var levelName = currentLevelId + 1 > _gameLevelsSettings.LevelNames.Count - 1 ? 
                _gameLevelsSettings.GetLevelByIndex(currentLevelId) :
                _gameLevelsSettings.GetLevelByIndex(++currentLevelId);
            
            return levelName;
        }

        public bool IsLastLevel(string levelName)
        {
            var levelId = _gameLevelsSettings.GetLevelIndexByName(levelName);
            
            return levelId + 1 > _gameLevelsSettings.LevelNames.Count - 1;
        }

        public void UpdateNextLevel()
        {
            var nextLevelName = GetNextLevelName();
            var levelId = _gameLevelsSettings.GetLevelIndexByName(nextLevelName);
            PlayerPrefs.SetInt(LevelIdKey, levelId);
            PlayerPrefs.Save();
        }

        public void ResetProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(LevelIdKey, 0);
            PlayerPrefs.Save();
            
            CurrentLevel = _gameLevelsSettings.GetLevelByIndex(0);
        }
    }
}