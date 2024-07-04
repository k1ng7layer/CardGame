namespace Services.Level
{
    public interface ILevelService
    {
        string CurrentLevel { get; }
        string GetNextLevelName();
        bool IsLastLevel(string levelName);
        void UpdateNextLevel();
        void ResetProgress();
    }
}