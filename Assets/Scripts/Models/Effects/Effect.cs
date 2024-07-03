using Helpers.Tasks;
using Models.Units;
using Settings.Effects;

namespace Models.Effects
{
    public class Effect
    {
        protected readonly ICoroutineDispatcher CoroutineDispatcher;
        protected EffectSettings Settings { get; }

        public Effect(
            EffectSettings settings, 
            ICoroutineDispatcher coroutineDispatcher
        )
        {
            CoroutineDispatcher = coroutineDispatcher;
            Settings = settings;
            ApplicationType = settings.ApplicationType;
        }
        
        public ApplicationType ApplicationType { get;}

        public virtual void Apply(BattleUnit user, BattleUnit target)
        { }
    }
}