using Settings.Battle;
using Settings.Effects;
using Settings.GameLevels;
using Settings.Prefab;
using Settings.Units.Enemy;
using Settings.Units.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/GameSettingsBase", fileName = "GameSettingsBase")]
    public class GameSettingsBase : ScriptableObject
    {
        [SerializeField] private BattleSettingsBase battleBattleSettingsBase;
        [SerializeField] private EffectSettingsBase _effectSettingsBase;
        [SerializeField] private PrefabBase _prefabBase;
        [SerializeField] private EnemySettingsBase _enemySettingsBase;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private GameLevelsSettingsBase _levelsSettings;

        public BattleSettingsBase BattleSettingsBase => battleBattleSettingsBase;

        public EffectSettingsBase EffectSettingsBase => _effectSettingsBase;

        public PrefabBase PrefabBase => _prefabBase;

        public EnemySettingsBase EnemySettingsBase => _enemySettingsBase;

        public PlayerSettings PlayerSettings => _playerSettings;

        public GameLevelsSettingsBase LevelsSettings => _levelsSettings;
    }
}