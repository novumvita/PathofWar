using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;

namespace PathofWar.Common
{
    internal class FeatureGen
    {
        internal static BlueprintFeature FeatureFromFact(BlueprintUnitFact fact, BlueprintFeature prereq, bool type, int char_level)
        {
            return FeatureConfigurator.New(fact.name + ".Feature", GuidStore.ReserveDynamic())
                .SetDisplayName(fact.m_DisplayName)
                .SetDescription(fact.m_Description)
                .SetIcon(fact.m_Icon)
                .AddFacts(new() { fact })
                .AddPrerequisiteFeature(prereq)
                .AddPrerequisiteCharacterLevel(char_level)
                .AddToFeatureSelection(type ? MainProgression.maneuver_selection : MainProgression.stance_selection)
                .Configure();
        }
    }
}
