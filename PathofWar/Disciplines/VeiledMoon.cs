using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using PathofWar.Common;

namespace PathofWar.Disciplines
{
    internal class VeiledMoon
    {
        #region CONSTANTS  
        private const string VeiledMoonProgressionName = "VeiledMoon";
        private const string VeiledMoonProgressionGuid = "BBC53802-65FA-4802-BEB1-5CE3074E7646";
        private const string VeiledMoonProgressionDisplayName = "VeiledMoon.Name";
        private const string VeiledMoonProgressionDescription = "VeiledMoon.Description";

        private const string VeiledMoonManeuverSelectionName = "VeiledMoon.ManeuverSelection";
        private const string VeiledMoonManeuverSelectionGuid = "D7340657-5C30-4601-9D9B-8E53061D9AF3";

        private const string VeiledMoonStanceSelectionName = "VeiledMoon.StanceSelection";
        private const string VeiledMoonStanceSelectionGuid = "9D175E4D-43EC-45FE-A3F3-96C362C3AE02";
        #endregion

        static readonly UnityEngine.Sprite icon = AbilityRefs.ProtectionFromAlignmentCommunal.Reference.Get().Icon;

        internal static Discipline Configure()
        {
            var discipline_feat = FeatureConfigurator.New(VeiledMoonProgressionName, VeiledMoonProgressionGuid)
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon).Configure();

            var maneuver_selection = FeatureSelectionConfigurator.New(VeiledMoonManeuverSelectionName, VeiledMoonManeuverSelectionGuid)
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon)
                .Configure();

            var stance_selection = FeatureSelectionConfigurator.New(VeiledMoonStanceSelectionName, VeiledMoonStanceSelectionGuid)
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon)
                .Configure();

            /*MANEUVERS*/

            /*STANCES*/

            Discipline discipline = new Discipline()
            {
                discipline = discipline_feat,
                maneuver_selection = maneuver_selection,
                stance_selection = stance_selection
            };

            return discipline;
        }
    }
}
