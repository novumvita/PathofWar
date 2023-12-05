using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.ActivatableAbilities;
using PathofWar.Common;
using PathofWar.Components.VeiledMoon;
using PathofWar.Patches;

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

        private const string StanceOfTheEtherGateName = "VeiledMoon.StanceOfTheEtherGate";
        private const string StanceOfTheEtherGateGuid = "22F9650A-C4F9-4A8C-832F-B2ED542CBD5E";
        private const string StanceOfTheEtherGateDisplayName = "VeiledMoon.StanceOfTheEtherGate.Name";
        private const string StanceOfTheEtherGateDescription = "VeiledMoon.StanceOfTheEtherGate.Description";

        private const string StanceOfTheEtherGateBuffName = "VeiledMoon.StanceOfTheEtherGate.Buff";
        private const string StanceOfTheEtherGateBuffGuid = "0F7CF970-CAB0-4EB1-8718-3C7B94F0C134";
        private const string StanceOfTheEtherGateBuffDisplayName = "VeiledMoon.StanceOfTheEtherGate.Buff.Name";
        private const string StanceOfTheEtherGateBuffDescription = "VeiledMoon.StanceOfTheEtherGate.Buff.Description";
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
            var stance_of_the_ether_gate = FeatureGen.FeatureFromFact(StanceOfTheEtherGate(), discipline_feat, stance_selection, 1);

            Discipline discipline = new Discipline()
            {
                discipline = discipline_feat,
                maneuver_selection = maneuver_selection,
                stance_selection = stance_selection
            };

            return discipline;
        }

        internal static BlueprintActivatableAbility StanceOfTheEtherGate()
        {
            var ether_gate_buff = BuffConfigurator.New(StanceOfTheEtherGateBuffName, StanceOfTheEtherGateBuffGuid)
                .SetDisplayName(StanceOfTheEtherGateBuffDisplayName)
                .SetDescription(StanceOfTheEtherGateBuffDescription)
                .AddComponent<EtherGate>()
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(StanceOfTheEtherGateName, StanceOfTheEtherGateGuid)
                .SetDisplayName(StanceOfTheEtherGateDisplayName)
                .SetDescription(StanceOfTheEtherGateDescription)
                .SetBuff(ether_gate_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }
    }
}
