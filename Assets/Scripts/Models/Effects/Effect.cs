using Models.Units;
using Settings.Effects;

namespace Models.Effects
{
    public class Effect
    {
        protected readonly BattleUnit Target;

        public Effect(
            BattleUnit target,
            ApplicationType applicationType
        )
        {
            ApplicationType = applicationType;
            Target = target;
        }
        
        public ApplicationType ApplicationType { get; }

        public virtual void Apply()
        { }
    }
}