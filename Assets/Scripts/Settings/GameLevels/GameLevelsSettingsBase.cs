using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.GameLevels
{
    [CreateAssetMenu(menuName = "Settings/GameLevelsSettingBase", fileName = "GameLevelsSettingBase")]
    public class GameLevelsSettingsBase : ScriptableObject
    {
        [SerializeField] private List<string> _levelNames;
        
        public List<string> LevelNames => _levelNames;

        public string GetLevelByIndex(int index)
        {
            return _levelNames[index];
        }

        public int GetLevelIndexByName(string levelName)
        {
            for (var i = 0; i < LevelNames.Count; i++)
            {
                var level = LevelNames[i];
                if (levelName == level)
                    return i;
            }

            throw new Exception($"[{nameof(GameLevelsSettingsBase)}] cant find level with name {levelName}");
        }
    }
}