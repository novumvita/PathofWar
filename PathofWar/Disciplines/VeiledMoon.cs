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
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker;
using PathofWar.Common;
using PathofWar.Components;
using PathofWar.Components.Common;
using PathofWar.Components.VeiledMoon;
using PathofWar.Patches;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace PathofWar.Disciplines
{
    internal class VeiledMoon
    {
        #region CONSTANTS  
        private const string VeiledMoonProgressionName = "VeiledMoon";
        private const string VeiledMoonProgressionDisplayName = "VeiledMoon.Name";
        private const string VeiledMoonProgressionDescription = "VeiledMoon.Description";

        private const string VeiledMoonManeuverSelectionName = "VeiledMoon.ManeuverSelection";

        private const string VeiledMoonStanceSelectionName = "VeiledMoon.StanceSelection";

        private const string StanceOfTheEtherGateName = "VeiledMoon.StanceOfTheEtherGate";
        private const string StanceOfTheEtherGateDisplayName = "VeiledMoon.StanceOfTheEtherGate.Name";
        private const string StanceOfTheEtherGateDescription = "VeiledMoon.StanceOfTheEtherGate.Description";

        private const string StanceOfTheEtherGateBuffName = "VeiledMoon.StanceOfTheEtherGate.Buff";
        private const string StanceOfTheEtherGateBuffDisplayName = "VeiledMoon.StanceOfTheEtherGate.Buff.Name";
        private const string StanceOfTheEtherGateBuffDescription = "VeiledMoon.StanceOfTheEtherGate.Buff.Description";

        private const string FormlessDanceName = "VeiledMoon.FormlessDance";
        private const string FormlessDanceDisplayName = "VeiledMoon.FormlessDance.Name";
        private const string FormlessDanceDescription = "VeiledMoon.FormlessDance.Description";

        private const string FormlessDanceBuffName = "VeiledMoon.FormlessDance.Buff";
        private const string FormlessDanceBuffDisplayName = "VeiledMoon.FormlessDance.Buff.Name";
        private const string FormlessDanceBuffDescription = "VeiledMoon.FormlessDance.Buff.Description";

        private const string SpiritualWeaponStanceName = "VeiledMoon.SpiritualWeaponStance";
        private const string SpiritualWeaponStanceDisplayName = "VeiledMoon.SpiritualWeaponStance.Name";
        private const string SpiritualWeaponStanceDescription = "VeiledMoon.SpiritualWeaponStance.Description";

        private const string SpiritualWeaponStanceBuffName = "VeiledMoon.SpiritualWeaponStance.Buff";
        private const string SpiritualWeaponStanceBuffDisplayName = "VeiledMoon.SpiritualWeaponStance.Buff.Name";
        private const string SpiritualWeaponStanceBuffDescription = "VeiledMoon.SpiritualWeaponStance.Buff.Description";

        private const string LunarPenumbraName = "VeiledMoon.LunarPenumbra";
        private const string LunarPenumbraDisplayName = "VeiledMoon.LunarPenumbra.Name";
        private const string LunarPenumbraDescription = "VeiledMoon.LunarPenumbra.Description";

        private const string LunarPenumbraBuffName = "VeiledMoon.LunarPenumbra.Buff";
        private const string LunarPenumbraBuffDisplayName = "VeiledMoon.LunarPenumbra.Buff.Name";
        private const string LunarPenumbraBuffDescription = "VeiledMoon.LunarPenumbra.Buff.Description";

        private const string EclipsingMoonName = "VeiledMoon.EclipsingMoon";
        private const string EclipsingMoonDisplayName = "VeiledMoon.EclipsingMoon.Name";
        private const string EclipsingMoonDescription = "VeiledMoon.EclipsingMoon.Description";

        private const string EclipsingMoonBuffName = "VeiledMoon.EclipsingMoon.Buff";
        private const string EclipsingMoonBuffDisplayName = "VeiledMoon.EclipsingMoon.Buff.Name";
        private const string EclipsingMoonBuffDescription = "VeiledMoon.EclipsingMoon.Buff.Description";

        private const string FlashingEtherTouchName = "VeiledMoon.FlashingEtherTouch";
        private const string FlashingEtherTouchDisplayName = "VeiledMoon.FlashingEtherTouch.Name";
        private const string FlashingEtherTouchDescription = "VeiledMoon.FlashingEtherTouch.Description";

        private const string FadeThroughName = "VeiledMoon.FadeThrough";
        private const string FadeThroughDisplayName = "VeiledMoon.FadeThrough.Name";
        private const string FadeThroughDescription = "VeiledMoon.FadeThrough.Description";

        private const string TwistingEtherName = "VeiledMoon.TwistingEther";
        private const string TwistingEtherDisplayName = "VeiledMoon.TwistingEther.Name";
        private const string TwistingEtherDescription = "VeiledMoon.TwistingEther.Description";

        private const string AnchoringSpiritName = "VeiledMoon.AnchoringSpirit";
        private const string AnchoringSpiritDisplayName = "VeiledMoon.AnchoringSpirit.Name";
        private const string AnchoringSpiritDescription = "VeiledMoon.AnchoringSpirit.Description";

        private const string AnchoringSpiritBuffName = "VeiledMoon.AnchoringSpirit.Buff";
        private const string AnchoringSpiritBuffDisplayName = "VeiledMoon.AnchoringSpirit.Buff.Name";
        private const string AnchoringSpiritBuffDescription = "VeiledMoon.AnchoringSpirit.Buff.Description";

        private const string AnchoringSpiritAreaEffectName = "VeiledMoon.AnchoringSpirit.AreaEffect";

        private const string AnchoringSpiritBuffEffectName = "VeiledMoon.AnchoringSpirit.BuffEffect";
        private const string AnchoringSpiritBuffEffectDisplayName = "VeiledMoon.AnchoringSpirit.BuffEffect.Name";
        private const string AnchoringSpiritBuffEffectDescription = "VeiledMoon.AnchoringSpirit.BuffEffect.Description";

        private const string WarpWormName = "VeiledMoon.WarpWorm";
        private const string WarpWormDisplayName = "VeiledMoon.WarpWorm.Name";
        private const string WarpWormDescription = "VeiledMoon.WarpWorm.Description";

        private const string GhostwalkName = "VeiledMoon.Ghostwalk";
        private const string GhostwalkDisplayName = "VeiledMoon.Ghostwalk.Name";
        private const string GhostwalkDescription = "VeiledMoon.Ghostwalk.Description";

        private const string GhostwalkBuffName = "VeiledMoon.Ghostwalk.Buff";
        private const string GhostwalkBuffDisplayName = "VeiledMoon.Ghostwalk.Buff.Name";
        private const string GhostwalkBuffDescription = "VeiledMoon.Ghostwalk.Buff.Description";

        private const string DimensionalStrikeName = "VeiledMoon.DimensionalStrike";
        private const string DimensionalStrikeDisplayName = "VeiledMoon.DimensionalStrike.Name";
        private const string DimensionalStrikeDescription = "VeiledMoon.DimensionalStrike.Description";
        #endregion

        static readonly UnityEngine.Sprite icon = AbilityRefs.ProtectionFromAlignmentCommunal.Reference.Get().Icon;

        internal static Discipline Configure()
        {
            var discipline_feat = FeatureConfigurator.New(VeiledMoonProgressionName, GuidStore.ReserveDynamic())
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon).Configure();

            var maneuver_selection = FeatureSelectionConfigurator.New(VeiledMoonManeuverSelectionName, GuidStore.ReserveDynamic())
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon)
                .Configure();

            var stance_selection = FeatureSelectionConfigurator.New(VeiledMoonStanceSelectionName, GuidStore.ReserveDynamic())
                .SetDisplayName(VeiledMoonProgressionDisplayName)
                .SetDescription(VeiledMoonProgressionDescription)
                .SetIcon(icon)
                .Configure();

            /*MANEUVERS*/
            var ghostwalk = FeatureGen.FeatureFromFact(Ghostwalk(), discipline_feat, maneuver_selection, 1);
            var dimensional_strike = FeatureGen.FeatureFromFact(DimensionalStrike(), discipline_feat, maneuver_selection, 1);
            var twisting_ether = FeatureGen.FeatureFromFact(TwistingEther(), discipline_feat, maneuver_selection, 7);
            var fade_through = FeatureGen.FeatureFromFact(FadeThrough(), discipline_feat, maneuver_selection, 7);
            var flashing_ether_touch = FeatureGen.FeatureFromFact(FlashingEtherTouch(), discipline_feat, maneuver_selection, 10);
            var warp_worm = FeatureGen.FeatureFromFact(WarpWorm(), discipline_feat, maneuver_selection, 10);
            var eclipsing_moon = FeatureGen.FeatureFromFact(EclipsingMoon(), discipline_feat, maneuver_selection, 16);

            /*STANCES*/
            var formless_dance = FeatureGen.FeatureFromFact(FormlessDance(), discipline_feat, stance_selection, 4);
            var stance_of_the_ether_gate = FeatureGen.FeatureFromFact(StanceOfTheEtherGate(), discipline_feat, stance_selection, 7);
            var spiritual_weapon_stance = FeatureGen.FeatureFromFact(SpiritualWeaponStance(), discipline_feat, stance_selection, 7);
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
            var ether_gate_buff = BuffConfigurator.New(StanceOfTheEtherGateBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(StanceOfTheEtherGateBuffDisplayName)
                .SetDescription(StanceOfTheEtherGateBuffDescription)
                .AddComponent<EtherGate>()
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(StanceOfTheEtherGateName, GuidStore.ReserveDynamic())
                .SetDisplayName(StanceOfTheEtherGateDisplayName)
                .SetDescription(StanceOfTheEtherGateDescription)
                .SetBuff(ether_gate_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility FormlessDance()
        {
            var formless_dance_buff = BuffConfigurator.New(FormlessDanceBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(FormlessDanceBuffDisplayName)
                .SetDescription(FormlessDanceBuffDescription)
                .AddConcealment(false, false, concealment: Kingmaker.Enums.Concealment.Partial, descriptor: Kingmaker.Enums.ConcealmentDescriptor.Blur)
                .AddCondition(Kingmaker.UnitLogic.UnitCondition.SeeInvisibility)
                .SetFxOnStart(BuffRefs.BlurBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.BlurBuff.Reference.Get().FxOnRemove)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(FormlessDanceName, GuidStore.ReserveDynamic())
                .SetDisplayName(FormlessDanceDisplayName)
                .SetDescription(FormlessDanceDescription)
                .SetBuff(formless_dance_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility SpiritualWeaponStance()
        {
            var spiritual_weapon_stance_buff = BuffConfigurator.New(SpiritualWeaponStanceBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(SpiritualWeaponStanceBuffDisplayName)
                .SetDescription(SpiritualWeaponStanceBuffDescription)
                .AddBuffEnchantAnyWeapon(WeaponEnchantmentRefs.GhostTouch.Reference.Get())
                .AddSpellResistance(true, value: ContextValues.Rank())
                .AddContextRankConfig(ContextRankConfigs.CharacterLevel().WithBonusValueProgression(5))
                .AddComponent<WeaponBonusDamageDice>(c => c.dmg_desc = DamageTypes.Force().GetDamageDescriptor(new DiceFormula(2, DiceType.D6)))
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(SpiritualWeaponStanceName, GuidStore.ReserveDynamic())
                .SetDisplayName(SpiritualWeaponStanceDisplayName)
                .SetDescription(SpiritualWeaponStanceDescription)
                .SetBuff(spiritual_weapon_stance_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility AnchoringSpirit()
        {
            var anchoring_spirit_area_buff = BuffConfigurator.New(AnchoringSpiritBuffEffectName, GuidStore.ReserveDynamic())
                .CopyFrom(BuffRefs.DimensionalAnchorBuff)
                .SetDisplayName(AnchoringSpiritBuffEffectDisplayName)
                .SetDescription(AnchoringSpiritBuffEffectDescription)
                .SetIcon(icon).Configure();

            var anchoring_spirit_area_effect = AbilityAreaEffectConfigurator.New(AnchoringSpiritAreaEffectName, GuidStore.ReserveDynamic())
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(anchoring_spirit_area_buff, condition: ConditionsBuilder.New().IsEnemy().Build())
                .Configure();

            var anchoring_spirit_buff = BuffConfigurator.New(AnchoringSpiritBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(AnchoringSpiritBuffDisplayName)
                .SetDescription(AnchoringSpiritBuffDescription)
                .AddAreaEffect(anchoring_spirit_area_effect)
                .AddFacts([FeatureRefs.Incorporeal.Reference.Get()])
                .SetFxOnStart(BuffRefs.BlinkBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.BlinkBuff.Reference.Get().FxOnRemove)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(AnchoringSpiritName, GuidStore.ReserveDynamic())
                .SetDisplayName(AnchoringSpiritDisplayName)
                .SetDescription(AnchoringSpiritDescription)
                .SetBuff(anchoring_spirit_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility LunarPenumbra()
        {
            var lunar_penumbra_buff = BuffConfigurator.New(LunarPenumbraBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(LunarPenumbraBuffDisplayName)
                .SetDescription(LunarPenumbraBuffDescription)
                .AddComponent<LunarPenumbra>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(LunarPenumbraName, GuidStore.ReserveDynamic())
                .SetDisplayName(LunarPenumbraDisplayName)
                .SetDescription(LunarPenumbraDescription)
                .SetBuff(lunar_penumbra_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility EclipsingMoon()
        {
            var eclipsing_moon_buff = BuffConfigurator.New(EclipsingMoonBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(EclipsingMoonBuffDisplayName)
                .SetDescription(EclipsingMoonBuffDescription)
                .SetIcon(icon).Configure();

            var eclipsing_moon_ability = AbilityConfigurator.New(EclipsingMoonName, GuidStore.ReserveDynamic())
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

        internal static BlueprintAbility Ghostwalk()
        {
            var ghostwalk_buff = BuffConfigurator.New(GhostwalkBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(GhostwalkBuffDisplayName)
                .SetDescription(GhostwalkBuffDescription)
                .AddFacts([FeatureRefs.Incorporeal.Reference.Get()])
                .SetFxOnStart(BuffRefs.BlinkBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.BlinkBuff.Reference.Get().FxOnRemove)
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(GhostwalkName, GuidStore.ReserveDynamic())
                .SetDisplayName(GhostwalkDisplayName)
                .SetDescription(GhostwalkDescription)
                .SetType(AbilityType.Extraordinary)
                .SetAnimation(CastAnimationStyle.Immediate)
                .SetRange(AbilityRange.Personal)
                .SetActionType(UnitCommand.CommandType.Swift)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityExecuteActionOnCast(ActionsBuilder.New().ApplyBuff(ghostwalk_buff, ContextDuration.Fixed(1)).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DimensionalStrike()
        {
            return AbilityConfigurator.New(DimensionalStrikeName, GuidStore.ReserveDynamic())
                .SetDisplayName(DimensionalStrikeDisplayName)
                .SetDescription(DimensionalStrikeDescription)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Weapon)
                .AllowTargeting(enemies: true)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityExecuteActionOnCast(ActionsBuilder.New().ApplyBuffPermanent(BuffRefs.DimensionStrikeBuff.Reference.Get()).MeleeAttack(UnitAnimationType.MainHandAttack).RemoveBuff(BuffRefs.DimensionStrikeBuff.Reference.Get()).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility FlashingEtherTouch()
        {
            return AbilityConfigurator.New(FlashingEtherTouchName, GuidStore.ReserveDynamic())
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
            return AbilityConfigurator.New(FadeThroughName, GuidStore.ReserveDynamic())
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
            return AbilityConfigurator.New(TwistingEtherName, GuidStore.ReserveDynamic())
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

        internal static BlueprintAbility WarpWorm()
        {
            return AbilityConfigurator.New(WarpWormName, GuidStore.ReserveDynamic())
                .SetDisplayName(WarpWormDisplayName)
                .SetDescription(WarpWormDescription)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Touch)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetAnimation(CastAnimationStyle.Touch)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<WarpWorm>().Build())
                .SetIcon(icon).Configure();
        }
    }
}
