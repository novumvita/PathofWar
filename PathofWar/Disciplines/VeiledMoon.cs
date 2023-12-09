using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
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

        private const string FlashingEtherTouchName = "VeiledMoon.FlashingEtherTouch";
        private const string FlashingEtherTouchGuid = "5B9EE7EC-63A0-4E7D-9EAB-C87DFE2BBE35";
        private const string FlashingEtherTouchDisplayName = "VeiledMoon.FlashingEtherTouch.Name";
        private const string FlashingEtherTouchDescription = "VeiledMoon.FlashingEtherTouch.Description";

        private const string FadeThroughName = "VeiledMoon.FadeThrough";
        private const string FadeThroughGuid = "9C232BDF-2CA9-4274-AAA1-6BD531159F93";
        private const string FadeThroughDisplayName = "VeiledMoon.FadeThrough.Name";
        private const string FadeThroughDescription = "VeiledMoon.FadeThrough.Description";

        private const string TwistingEtherName = "VeiledMoon.TwistingEther";
        private const string TwistingEtherGuid = "D94CD772-B4DD-4BFB-9DEF-44385835F148";
        private const string TwistingEtherDisplayName = "VeiledMoon.TwistingEther.Name";
        private const string TwistingEtherDescription = "VeiledMoon.TwistingEther.Description";

        private const string AnchoringSpiritName = "VeiledMoon.AnchoringSpirit";
        private const string AnchoringSpiritGuid = "FA07E4EB-31D8-4C30-BF7A-857DB3C0FEF8";
        private const string AnchoringSpiritDisplayName = "VeiledMoon.AnchoringSpirit.Name";
        private const string AnchoringSpiritDescription = "VeiledMoon.AnchoringSpirit.Description";

        private const string AnchoringSpiritBuffName = "VeiledMoon.AnchoringSpirit.Buff";
        private const string AnchoringSpiritBuffGuid = "1943CE8C-0DC3-4796-9255-C181A3189803";
        private const string AnchoringSpiritBuffDisplayName = "VeiledMoon.AnchoringSpirit.Buff.Name";
        private const string AnchoringSpiritBuffDescription = "VeiledMoon.AnchoringSpirit.Buff.Description";

        private const string AnchoringSpiritAreaEffectName = "VeiledMoon.AnchoringSpirit.AreaEffect";
        private const string AnchoringSpiritAreaEffectGuid = "6590E4C0-DF62-489A-823C-14EB91FAD906";

        private const string AnchoringSpiritBuffEffectName = "VeiledMoon.AnchoringSpirit.BuffEffect";
        private const string AnchoringSpiritBuffEffectGuid = "1141C4E0-DDF6-48E7-B47A-22EA01BE9B84";
        private const string AnchoringSpiritBuffEffectDisplayName = "VeiledMoon.AnchoringSpirit.BuffEffect.Name";
        private const string AnchoringSpiritBuffEffectDescription = "VeiledMoon.AnchoringSpirit.BuffEffect.Description";
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
            var flashing_ether_touch = FeatureGen.FeatureFromFact(FlashingEtherTouch(), discipline_feat, maneuver_selection, 1);
            var twisting_ether = FeatureGen.FeatureFromFact(TwistingEther(), discipline_feat, maneuver_selection, 7);
            var fade_through = FeatureGen.FeatureFromFact(FadeThrough(), discipline_feat, maneuver_selection, 7);
            var eclipsing_moon = FeatureGen.FeatureFromFact(EclipsingMoon(), discipline_feat, maneuver_selection, 16);

            /*STANCES*/
            var stance_of_the_ether_gate = FeatureGen.FeatureFromFact(StanceOfTheEtherGate(), discipline_feat, stance_selection, 7);
            var anchoring_spirit = FeatureGen.FeatureFromFact(AnchoringSpirit(), discipline_feat, stance_selection, 13);
            var lunar_penumbra = FeatureGen.FeatureFromFact(LunarPenumbra(), discipline_feat, stance_selection, 16);

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

        internal static BlueprintActivatableAbility AnchoringSpirit()
        {
            var anchoring_spirit_area_buff = BuffConfigurator.New(AnchoringSpiritBuffEffectName, AnchoringSpiritBuffEffectGuid)
                .SetDisplayName(AnchoringSpiritBuffEffectDisplayName)
                .SetDescription(AnchoringSpiritBuffEffectDescription)
                .CopyFrom(BuffRefs.DimensionalAnchorBuff, [typeof(ForbidSpecificSpellsCast)])
                .SetIcon(icon).Configure();

            var anchoring_spirit_area_effect = AbilityAreaEffectConfigurator.New(AnchoringSpiritAreaEffectName, AnchoringSpiritAreaEffectGuid)
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(anchoring_spirit_area_buff, condition: ConditionsBuilder.New().IsEnemy().Build())
                .Configure();

            var anchoring_spirit_buff = BuffConfigurator.New(AnchoringSpiritBuffName, AnchoringSpiritBuffGuid)
                .SetDisplayName(AnchoringSpiritBuffDisplayName)
                .SetDescription(AnchoringSpiritBuffDescription)
                .AddAreaEffect(anchoring_spirit_area_effect)
                .AddFacts([FeatureRefs.Incorporeal.Reference.Get()])
                .SetFxOnStart(BuffRefs.BlinkBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.BlinkBuff.Reference.Get().FxOnRemove)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(AnchoringSpiritName, AnchoringSpiritGuid)
                .SetDisplayName(AnchoringSpiritDisplayName)
                .SetDescription(AnchoringSpiritDescription)
                .SetBuff(anchoring_spirit_buff)
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
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
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
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityExecuteActionOnCast(ActionsBuilder.New().ApplyBuff(eclipsing_moon_buff, ContextDuration.Fixed(1)).Build())
                .SetIcon(icon).Configure();

            eclipsing_moon_buff = BuffConfigurator.For(eclipsing_moon_buff)
                .AddComponent<EclipsingMoon>(c => c.ability = eclipsing_moon_ability)
                .Configure();

            return eclipsing_moon_ability;
        }

        internal static BlueprintAbility FlashingEtherTouch()
        {
            return AbilityConfigurator.New(FlashingEtherTouchName, FlashingEtherTouchGuid)
                .SetDisplayName(FlashingEtherTouchDisplayName)
                .SetDescription(FlashingEtherTouchDescription)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Touch)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetAnimation(CastAnimationStyle.Touch)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<FlashingEtherTouch>().Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility FadeThrough()
        {
            return AbilityConfigurator.New(FadeThroughName, FadeThroughGuid)
                .SetDisplayName(FadeThroughDisplayName)
                .SetDescription(FadeThroughDescription)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.DoubleMove)
                .SetActionType(UnitCommand.CommandType.Swift)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AllowTargeting(point: true)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<FadeThrough>().Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility TwistingEther()
        {
            return AbilityConfigurator.New(TwistingEtherName, TwistingEtherGuid)
                .SetDisplayName(TwistingEtherDisplayName)
                .SetDescription(TwistingEtherDescription)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Close)
                .SetActionType(UnitCommand.CommandType.Swift)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AllowTargeting(friends: true)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<TwistingEther>().Build())
                .SetIcon(icon).Configure();
        }
    }
}
