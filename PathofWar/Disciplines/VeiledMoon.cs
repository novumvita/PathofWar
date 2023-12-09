using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Utility;
using PathofWar.Common;
using PathofWar.Components;
using PathofWar.Components.VeiledMoon;
using PathofWar.Patches;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

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

        private const string LunarPenumbraName = "VeiledMoon.LunarPenumbra";
        private const string LunarPenumbraGuid = "B03E2F0C-6499-48D2-A86B-2FA73E5FB18E";
        private const string LunarPenumbraDisplayName = "VeiledMoon.LunarPenumbra.Name";
        private const string LunarPenumbraDescription = "VeiledMoon.LunarPenumbra.Description";

        private const string LunarPenumbraBuffName = "VeiledMoon.LunarPenumbra.Buff";
        private const string LunarPenumbraBuffGuid = "04E8480D-480C-4959-A798-7BF1D6C3EF27";
        private const string LunarPenumbraBuffDisplayName = "VeiledMoon.LunarPenumbra.Buff.Name";
        private const string LunarPenumbraBuffDescription = "VeiledMoon.LunarPenumbra.Buff.Description";

        private const string EclipsingMoonName = "VeiledMoon.EclipsingMoon";
        private const string EclipsingMoonGuid = "F2D14B08-F979-44D0-A0F5-CC703F5CB874";
        private const string EclipsingMoonDisplayName = "VeiledMoon.EclipsingMoon.Name";
        private const string EclipsingMoonDescription = "VeiledMoon.EclipsingMoon.Description";

        private const string EclipsingMoonBuffName = "VeiledMoon.EclipsingMoon.Buff";
        private const string EclipsingMoonBuffGuid = "FCC597BC-3C50-4593-8BB2-8254520120CB";
        private const string EclipsingMoonBuffDisplayName = "VeiledMoon.EclipsingMoon.Buff.Name";
        private const string EclipsingMoonBuffDescription = "VeiledMoon.EclipsingMoon.Buff.Description";
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
            var eclipsing_moon = FeatureGen.FeatureFromFact(EclipsingMoon(), discipline_feat, maneuver_selection, 1);
            var lunar_penumbra = FeatureGen.FeatureFromFact(LunarPenumbra(), discipline_feat, maneuver_selection, 1);

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

        internal static BlueprintActivatableAbility LunarPenumbra()
        {
            var lunar_penumbra_buff = BuffConfigurator.New(LunarPenumbraBuffName, LunarPenumbraBuffGuid)
                .SetDisplayName(LunarPenumbraBuffDisplayName)
                .SetDescription(LunarPenumbraBuffDescription)
                .AddComponent<LunarPenumbra>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(LunarPenumbraName, LunarPenumbraGuid)
                .SetDisplayName(LunarPenumbraDisplayName)
                .SetDescription(LunarPenumbraDescription)
                .SetBuff(lunar_penumbra_buff)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility EclipsingMoon()
        {
            var eclipsing_moon_buff = BuffConfigurator.New(EclipsingMoonBuffName, EclipsingMoonBuffGuid)
                .SetDisplayName(EclipsingMoonBuffDisplayName)
                .SetDescription(EclipsingMoonBuffDescription)
                .SetIcon(icon).Configure();

            var eclipsing_moon_ability = AbilityConfigurator.New(EclipsingMoonName, EclipsingMoonGuid)
                .SetDisplayName(EclipsingMoonDisplayName)
                .SetDescription(EclipsingMoonDescription)
                .SetIsFullRoundAction()
                .SetType(AbilityType.Extraordinary)
                .SetAnimation(CastAnimationStyle.EnchantWeapon)
                .SetRange(AbilityRange.Personal)
                .AddAbilityExecuteActionOnCast(ActionsBuilder.New().ApplyBuff(eclipsing_moon_buff, ContextDuration.Fixed(1)).Build())
                .SetIcon(icon).Configure();

            eclipsing_moon_buff = BuffConfigurator.For(eclipsing_moon_buff)
                .AddComponent<EclipsingMoon>(c => c.ability = eclipsing_moon_ability)
                .Configure();

            return eclipsing_moon_ability;
        }
    }
}
