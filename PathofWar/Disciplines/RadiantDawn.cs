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
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using PathofWar.Common;
using PathofWar.Components;
using PathofWar.Components.Common;
using PathofWar.Components.RadiantDawn;
using PathofWar.Patches;
using System.Linq;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace BasicTemplate.Disciplines
{
    internal class RadiantDawn
    {
        #region CONSTANTS  
        private const string RadiantDawnProgressionName = "RadiantDawn";
        private const string RadiantDawnProgressionDisplayName = "RadiantDawn.Name";
        private const string RadiantDawnProgressionDescription = "RadiantDawn.Description";

        private const string RadiantDawnManeuverSelectionName = "RadiantDawn.ManeuverSelection";

        private const string RadiantDawnStanceSelectionName = "RadiantDawn.StanceSelection";

        private const string RadiantDawnStanceDescription = "RadiantDawn.Stance.Description";

        private const string BolsterName = "RadiantDawn.Bolster";
        private const string BolsterDisplayName = "RadiantDawn.Bolster.Name";
        private const string BolsterDescription = "RadiantDawn.Bolster.Description";

        private const string BolsterBuffName = "RadiantDawn.Bolster.Buff";
        private const string BolsterBuffDisplayName = "RadiantDawn.Bolster.Buff.Name";
        private const string BolsterBuffDescription = "RadiantDawn.Bolster.Buff.Description";

        private const string BolsterAreaEffectName = "RadiantDawn.Bolster.AreaEffect";

        private const string BolsterBaseBuffName = "RadiantDawn.Bolster.BaseBuff";
        private const string BolsterBaseBuffDisplayName = "RadiantDawn.Bolster.BaseBuff.Name";
        private const string BolsterBaseBuffDescription = "RadiantDawn.Bolster.BaseBuff.Description";

        private const string DecreeOfMercyName = "RadiantDawn.DecreeOfMercy";
        private const string DecreeOfMercyDisplayName = "RadiantDawn.DecreeOfMercy.Name";
        private const string DecreeOfMercyDescription = "RadiantDawn.DecreeOfMercy.Description";

        private const string DecreeOfMercyAreaEffectName = "RadiantDawn.DecreeOfMercy.AreaEffect";
        private const string DecreeOfMercyAreaEffectGuid = "EB7384B4-AE1A-4A51-93AE-9BC247A47D08";

        private const string DecreeOfMercyBaseBuffName = "RadiantDawn.DecreeOfMercy.BaseBuff";
        private const string DecreeOfMercyBaseBuffDisplayName = "RadiantDawn.DecreeOfMercy.BaseBuff.Name";
        private const string DecreeOfMercyBaseBuffDescription = "RadiantDawn.DecreeOfMercy.BaseBuff.Description";

        private const string DecreeOfMercyBuffName = "RadiantDawn.DecreeOfMercy.Buff";
        private const string DecreeOfMercyBuffDisplayName = "RadiantDawn.DecreeOfMercy.Buff.Name";
        private const string DecreeOfMercyBuffDescription = "RadiantDawn.DecreeOfMercy.Buff.Description";

        private const string DismissName = "RadiantDawn.Dismiss";
        private const string DismissDisplayName = "RadiantDawn.Dismiss.Name";
        private const string DismissDescription = "RadiantDawn.Dismiss.Description";

        private const string LifeburstStrikeName = "RadiantDawn.LifeburstStrike";
        private const string LifeburstStrikeDisplayName = "RadiantDawn.LifeburstStrike.Name";
        private const string LifeburstStrikeDescription = "RadiantDawn.LifeburstStrike.Description";

        private const string DecreeOfDeathName = "RadiantDawn.DecreeOfDeath";
        private const string DecreeOfDeathDisplayName = "RadiantDawn.DecreeOfDeath.Name";
        private const string DecreeOfDeathDescription = "RadiantDawn.DecreeOfDeath.Description";

        private const string DecreeOfDeathBuffName = "RadiantDawn.DecreeOfDeath.Buff";
        private const string DecreeOfDeathBuffDisplayName = "RadiantDawn.DecreeOfDeath.Buff.Name";
        private const string DecreeOfDeathBuffDescription = "RadiantDawn.DecreeOfDeath.Buff.Description";

        private const string TyrantsEndName = "RadiantDawn.TyrantsEnd";
        private const string TyrantsEndDisplayName = "RadiantDawn.TyrantsEnd.Name";
        private const string TyrantsEndDescription = "RadiantDawn.TyrantsEnd.Description";

        private const string TyrantsEndBuffName = "RadiantDawn.TyrantsEnd.Buff";
        private const string TyrantsEndBuffDisplayName = "RadiantDawn.TyrantsEnd.Buff.Name";
        private const string TyrantsEndBuffDescription = "RadiantDawn.TyrantsEnd.Buff.Description";

        private const string ShatterSpellName = "RadiantDawn.ShatterSpell";
        private const string ShatterSpellDisplayName = "RadiantDawn.ShatterSpell.Name";
        private const string ShatterSpellDescription = "RadiantDawn.ShatterSpell.Description";

        private const string DecreeOfPurityName = "RadiantDawn.DecreeOfPurity";
        private const string DecreeOfPurityDisplayName = "RadiantDawn.DecreeOfPurity.Name";
        private const string DecreeOfPurityDescription = "RadiantDawn.DecreeOfPurity.Description";

        private const string JudgementDayName = "RadiantDawn.JudgementDay";
        private const string JudgementDayDisplayName = "RadiantDawn.JudgementDay.Name";
        private const string JudgementDayDescription = "RadiantDawn.JudgementDay.Description";

        private const string PushTheAdvantageName = "RadiantDawn.PushTheAdvantage";
        private const string PushTheAdvantageDisplayName = "RadiantDawn.PushTheAdvantage.Name";
        private const string PushTheAdvantageDescription = "RadiantDawn.PushTheAdvantage.Description";

        private const string PushTheAdvantageBuffName = "RadiantDawn.PushTheAdvantage.Buff";
        private const string PushTheAdvantageBuffDisplayName = "RadiantDawn.PushTheAdvantage.Buff.Name";
        private const string PushTheAdvantageBuffDescription = "RadiantDawn.PushTheAdvantage.Buff.Description";

        private const string NoblesseObligeName = "RadiantDawn.NoblesseOblige";
        private const string NoblesseObligeDisplayName = "RadiantDawn.NoblesseOblige.Name";
        private const string NoblesseObligeDescription = "RadiantDawn.NoblesseOblige.Description";

        private const string NoblesseObligeBuffName = "RadiantDawn.NoblesseOblige.Buff";
        private const string NoblesseObligeBuffDisplayName = "RadiantDawn.NoblesseOblige.Buff.Name";
        private const string NoblesseObligeBuffDescription = "RadiantDawn.NoblesseOblige.Buff.Description";

        private const string SpoilsOfWarName = "RadiantDawn.SpoilsOfWar";
        private const string SpoilsOfWarDisplayName = "RadiantDawn.SpoilsOfWar.Name";
        private const string SpoilsOfWarDescription = "RadiantDawn.SpoilsOfWar.Description";

        private const string SpoilsOfWarBuffName = "RadiantDawn.SpoilsOfWar.Buff";
        private const string SpoilsOfWarBuffDisplayName = "RadiantDawn.SpoilsOfWar.Buff.Name";
        private const string SpoilsOfWarBuffDescription = "RadiantDawn.SpoilsOfWar.Buff.Description";

        private const string SpoilsOfWarAreaEffectName = "RadiantDawn.SpoilsOfWar.AreaEffect";

        private const string SpoilsOfWarBuffEffectName = "RadiantDawn.SpoilsOfWar.BuffEffect";
        private const string SpoilsOfWarBuffEffectDisplayName = "RadiantDawn.SpoilsOfWar.BuffEffect.Name";
        private const string SpoilsOfWarBuffEffectDescription = "RadiantDawn.SpoilsOfWar.BuffEffect.Description";

        private const string ArmamentsOfTheEmpireName = "RadiantDawn.ArmamentsOfTheEmpire";
        private const string ArmamentsOfTheEmpireDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.Name";
        private const string ArmamentsOfTheEmpireDescription = "RadiantDawn.ArmamentsOfTheEmpire.Description";

        private const string ArmamentsOfTheEmpireBuffName = "RadiantDawn.ArmamentsOfTheEmpire.Buff";
        private const string ArmamentsOfTheEmpireBuffDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.Buff.Name";
        private const string ArmamentsOfTheEmpireBuffDescription = "RadiantDawn.ArmamentsOfTheEmpire.Buff.Description";

        private const string ArmamentsOfTheEmpireAreaEffectName = "RadiantDawn.ArmamentsOfTheEmpire.AreaEffect";

        private const string ArmamentsOfTheEmpireBuffEffectName = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect";
        private const string ArmamentsOfTheEmpireBuffEffectDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect.Name";
        private const string ArmamentsOfTheEmpireBuffEffectDescription = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect.Description";

        private const string TheCagedSunName = "RadiantDawn.TheCagedSun";
        private const string TheCagedSunDisplayName = "RadiantDawn.TheCagedSun.Name";
        private const string TheCagedSunDescription = "RadiantDawn.TheCagedSun.Description";

        private const string TheCagedSunBuffName = "RadiantDawn.TheCagedSun.Buff";
        private const string TheCagedSunBuffDisplayName = "RadiantDawn.TheCagedSun.Buff.Name";
        private const string TheCagedSunBuffDescription = "RadiantDawn.TheCagedSun.Buff.Description";

        private const string TheCagedSunHPBuffName = "RadiantDawn.TheCagedSun.HPBuff";
        private const string TheCagedSunHPBuffDisplayName = "RadiantDawn.TheCagedSun.HPBuff.Name";
        private const string TheCagedSunHPBuffDescription = "RadiantDawn.TheCagedSun.HPBuff.Description";

        private const string BattleAgainstTheSunName = "RadiantDawn.BattleAgainstTheSun";
        private const string BattleAgainstTheSunDisplayName = "RadiantDawn.BattleAgainstTheSun.Name";
        private const string BattleAgainstTheSunDescription = "RadiantDawn.BattleAgainstTheSun.Description";

        private const string BattleAgainstTheSunBuffName = "RadiantDawn.BattleAgainstTheSun.Buff";
        private const string BattleAgainstTheSunBuffDisplayName = "RadiantDawn.BattleAgainstTheSun.Buff.Name";
        private const string BattleAgainstTheSunBuffDescription = "RadiantDawn.BattleAgainstTheSun.Buff.Description";
        #endregion

        static readonly UnityEngine.Sprite icon = AbilityRefs.AngelStormOfJusticeAbility.Reference.Get().Icon;

        internal static Discipline Configure()
        {
            var discipline_feat = FeatureConfigurator.New(RadiantDawnProgressionName, GuidStore.ReserveDynamic())
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon).Configure();

            var maneuver_selection = FeatureSelectionConfigurator.New(RadiantDawnManeuverSelectionName, GuidStore.ReserveDynamic())
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon)
                .Configure();

            var stance_selection = FeatureSelectionConfigurator.New(RadiantDawnStanceSelectionName, GuidStore.ReserveDynamic())
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon)
                .Configure();

            /*MANEUVERS*/
            var dismiss = FeatureGen.FeatureFromFact(Dismiss(), discipline_feat, maneuver_selection, 1);
            var lifeburst_strike = FeatureGen.FeatureFromFact(LifeburstStrike(), discipline_feat, maneuver_selection, 1);
            var decree_of_death = FeatureGen.FeatureFromFact(DecreeOfDeath(), discipline_feat, maneuver_selection, 1);
            var shatter_spell = FeatureGen.FeatureFromFact(ShatterSpell(), discipline_feat, maneuver_selection, 4);
            var decree_of_purity = FeatureGen.FeatureFromFact(DecreeOfPurity(), discipline_feat, maneuver_selection, 7);
            var judgement_day = FeatureGen.FeatureFromFact(JudgementDay(), discipline_feat, maneuver_selection, 16);
            var tyrants_end = FeatureGen.FeatureFromFact(TyrantsEnd(), discipline_feat, maneuver_selection, 16);

            /*STANCES*/
            var bolster = FeatureGen.FeatureFromFact(Bolster(), discipline_feat, stance_selection, 1);
            var decree_of_mercy = FeatureGen.FeatureFromFact(DecreeOfMercy(), discipline_feat, stance_selection, 1);
            var spoils_of_war = FeatureGen.FeatureFromFact(SpoilsOfWar(), discipline_feat, stance_selection, 1);
            var the_caged_sun = FeatureGen.FeatureFromFact(TheCagedSun(), discipline_feat, stance_selection, 7);
            var armaments_of_the_empire = FeatureGen.FeatureFromFact(ArmamentsOfTheEmpire(), discipline_feat, stance_selection, 7);
            var battle_against_the_sun = FeatureGen.FeatureFromFact(BattleAgainstTheSun(), discipline_feat, stance_selection, 7);
            var push_the_advantage = FeatureGen.FeatureFromFact(PushTheAdvantage(), discipline_feat, stance_selection, 13);
            var noblesse_oblige = FeatureGen.FeatureFromFact(NoblesseOblige(), discipline_feat, stance_selection, 13);

            Discipline discipline = new Discipline()
            {
                discipline = discipline_feat,
                maneuver_selection = maneuver_selection,
                stance_selection = stance_selection
            };

            return discipline;
        }

        internal static BlueprintActivatableAbility Bolster()
        {
            var bolster_effect_buff = BuffConfigurator.New(BolsterBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(BolsterBuffDisplayName)
                .SetDescription(BolsterBuffDescription)
                .AddDamageResistancePhysical(value: ContextValues.Rank())
                .AddTargetAttackWithWeaponTrigger(ActionsBuilder.New().RemoveSelf().Build(), onlyHit: true)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetIcon(icon).Configure();

            var bolster_area_effect = AbilityAreaEffectConfigurator.New(BolsterAreaEffectName, GuidStore.ReserveDynamic())
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(bolster_effect_buff, condition: ConditionsBuilder.New().IsAlly().TargetIsYourself(negate: true).Build())
                .Configure();

            var bolster_buff = BuffConfigurator.New(BolsterBaseBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(BolsterBaseBuffDisplayName)
                .SetDescription(BolsterBaseBuffDescription)
                .AddAreaEffect(bolster_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(BolsterName, GuidStore.ReserveDynamic())
                .SetDisplayName(BolsterDisplayName)
                .SetDescription(BolsterDescription)
                .SetBuff(bolster_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility DecreeOfMercy()
        {
            var decree_of_mercy_effect_buff = BuffConfigurator.New(DecreeOfMercyBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfMercyBuffDisplayName)
                .SetDescription(DecreeOfMercyBuffDescription)
                .AddComponent<DecreeOfMercy>()
                .SetIcon(icon).Configure();

            var decree_of_mercy_area_effect = AbilityAreaEffectConfigurator.New(DecreeOfMercyAreaEffectName, DecreeOfMercyAreaEffectGuid)
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(decree_of_mercy_effect_buff, condition: ConditionsBuilder.New().IsEnemy().Build())
                .Configure();

            var decree_of_mercy_buff = BuffConfigurator.New(DecreeOfMercyBaseBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfMercyBaseBuffDisplayName)
                .SetDescription(DecreeOfMercyBaseBuffDescription)
                .AddAreaEffect(decree_of_mercy_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(DecreeOfMercyName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfMercyDisplayName)
                .SetDescription(DecreeOfMercyDescription)
                .SetBuff(decree_of_mercy_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility Dismiss()
        {
            return AbilityConfigurator.New(DismissName, GuidStore.ReserveDynamic())
                .SetDisplayName(DismissDisplayName)
                .SetDescription(DismissDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().CombatManeuver(ActionsBuilder.New().Push(ContextValues.Constant(10), false), CombatManeuver.Trip, newStat: StatType.Charisma, useCasterLevelAsBaseAttack: true).Build())
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility LifeburstStrike()
        {
            return AbilityConfigurator.New(LifeburstStrikeName, GuidStore.ReserveDynamic())
                .SetDisplayName(LifeburstStrikeDisplayName)
                .SetDescription(LifeburstStrikeDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<RadiantHeal>().Build())
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfDeath()
        {
            var decree_of_death_buff = BuffConfigurator.New(DecreeOfDeathBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfDeathBuffDisplayName)
                .SetDescription(DecreeOfDeathBuffDescription)
                .AddComponent<DecreeOfDeath>()
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(DecreeOfDeathName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfDeathDisplayName)
                .SetDescription(DecreeOfDeathDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(decree_of_death_buff, ContextDuration.Fixed(1)).Build())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility TyrantsEnd()
        {
            var features = FeatureRefs.All.Select(c => c.Reference.Get()).Where(c => FeatureChecker.IsFeatureImmunityOrResistance(c));

            var tyrants_end_buff = BuffConfigurator.New(TyrantsEndBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(TyrantsEndBuffDisplayName)
                .SetDescription(TyrantsEndBuffDescription)
                .AddComponent<SuppressFeatures>(c => c.features = features.ToList())
                .AddComponent<TyrantsEnd>()
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(TyrantsEndName, GuidStore.ReserveDynamic())
                .SetDisplayName(TyrantsEndDisplayName)
                .SetDescription(TyrantsEndDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); c.ability_for_fx_target = AbilityRefs.SmiteEvilAbility.Reference.Get(); c.bonus_dmg_desc = [DamageTypes.Energy(DamageEnergyType.Holy).GetDamageDescriptor(new DiceFormula(6, DiceType.D6))]; })
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(tyrants_end_buff, ContextDuration.Fixed(1)).Build())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility ShatterSpell()
        {
            return AbilityConfigurator.New(ShatterSpellName, GuidStore.ReserveDynamic())
                .SetDisplayName(ShatterSpellDisplayName)
                .SetDescription(ShatterSpellDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); c.bonus_dmg_desc = [DamageTypes.Energy(DamageEnergyType.Holy).GetDamageDescriptor(new DiceFormula(2, DiceType.D6))]; })
                .AddAbilityEffectRunAction(ActionsBuilder.New().DispelMagic(ContextActionDispelMagic.BuffType.FromSpells, RuleDispelMagic.CheckType.CasterLevel, 9).Build())
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfPurity()
        {
            return AbilityConfigurator.New(DecreeOfPurityName, GuidStore.ReserveDynamic())
                .SetDisplayName(DecreeOfPurityDisplayName)
                .SetDescription(DecreeOfPurityDescription)
                .SetActionType(UnitCommand.CommandType.Swift)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .SetRange(AbilityRange.Close)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<RadiantHeal>().Build())
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility JudgementDay()
        {
            return AbilityConfigurator.New(JudgementDayName, GuidStore.ReserveDynamic())
                .SetDisplayName(JudgementDayDisplayName)
                .SetDescription(JudgementDayDescription)
                .SetIsFullRoundAction()
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityTargetsAround(radius: 100.Feet(), targetType: TargetType.Any, includeDead: true)
                .SetAnimation(CastAnimationStyle.Omni)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New()
                    .Conditional(ConditionsBuilder.New().IsAlly().Build(),
                    ActionsBuilder.New().BreathOfLife(ContextDice.Value(DiceType.Zero, 0, 50)).Build(),
                    ActionsBuilder.New().DealDamage(DamageTypes.Energy(DamageEnergyType.Holy), ContextDice.Value(DiceType.Zero, 0, 50)).Build()).Build())
                .AddAbilitySpawnFx(prefabLink: AbilityRefs.AngelEyeOfTheSun.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink)
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility PushTheAdvantage()
        {
            var push_the_advantage_buff = BuffConfigurator.New(PushTheAdvantageBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(PushTheAdvantageBuffDisplayName)
                .SetDescription(PushTheAdvantageBuffDescription)
                .AddComponent<PushTheAdvantage>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(PushTheAdvantageName, GuidStore.ReserveDynamic())
                .SetDisplayName(PushTheAdvantageDisplayName)
                .SetDescription(PushTheAdvantageDescription)
                .SetBuff(push_the_advantage_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility NoblesseOblige()
        {
            var noblesse_oblige_buff = BuffConfigurator.New(NoblesseObligeBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(NoblesseObligeBuffDisplayName)
                .SetDescription(NoblesseObligeBuffDescription)
                .AddComponent<NoblesseOblige>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(NoblesseObligeName, GuidStore.ReserveDynamic())
                .SetDisplayName(NoblesseObligeDisplayName)
                .SetDescription(NoblesseObligeDescription)
                .SetBuff(noblesse_oblige_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility SpoilsOfWar()
        {

            var spoils_of_war_area_buff = BuffConfigurator.New(SpoilsOfWarBuffEffectName, GuidStore.ReserveDynamic())
                .SetDisplayName(SpoilsOfWarBuffEffectDisplayName)
                .SetDescription(SpoilsOfWarBuffEffectDescription)
                .AddComponent<SpoilsOfWar>()
                .SetIcon(icon).Configure();

            var spoils_of_war_area_effect = AbilityAreaEffectConfigurator.New(SpoilsOfWarAreaEffectName, GuidStore.ReserveDynamic())
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(spoils_of_war_area_buff, condition: ConditionsBuilder.New().IsAlly().Build())
                .Configure();

            var spoils_of_war_buff = BuffConfigurator.New(SpoilsOfWarBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(SpoilsOfWarBuffDisplayName)
                .SetDescription(SpoilsOfWarBuffDescription)
                .AddAreaEffect(spoils_of_war_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(SpoilsOfWarName, GuidStore.ReserveDynamic())
                .SetDisplayName(SpoilsOfWarDisplayName)
                .SetDescription(SpoilsOfWarDescription)
                .SetBuff(spoils_of_war_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility ArmamentsOfTheEmpire()
        {

            var spoils_of_war_area_buff = BuffConfigurator.New(ArmamentsOfTheEmpireBuffEffectName, GuidStore.ReserveDynamic())
                .SetDisplayName(ArmamentsOfTheEmpireBuffEffectDisplayName)
                .SetDescription(ArmamentsOfTheEmpireBuffEffectDescription)
                .AddBuffEnchantAnyWeapon(WeaponEnchantmentRefs.Holy.Reference.Get())
                .AddBuffEnchantAnyWeapon(WeaponEnchantmentRefs.TemporaryEnhancement3.Reference.Get())
                .AddBuffEnchantArmor([ArmorEnchantmentRefs.TemporaryArmorEnhancementBonus3.Reference.Get()])
                .SetIcon(icon).Configure();

            var spoils_of_war_area_effect = AbilityAreaEffectConfigurator.New(ArmamentsOfTheEmpireAreaEffectName, GuidStore.ReserveDynamic())
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(spoils_of_war_area_buff, condition: ConditionsBuilder.New().IsAlly().Build())
                .Configure();

            var spoils_of_war_buff = BuffConfigurator.New(ArmamentsOfTheEmpireBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(ArmamentsOfTheEmpireBuffDisplayName)
                .SetDescription(ArmamentsOfTheEmpireBuffDescription)
                .AddAreaEffect(spoils_of_war_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(ArmamentsOfTheEmpireName, GuidStore.ReserveDynamic())
                .SetDisplayName(ArmamentsOfTheEmpireDisplayName)
                .SetDescription(ArmamentsOfTheEmpireDescription)
                .SetBuff(spoils_of_war_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility TheCagedSun()
        {
            var the_caged_sun_temp_hp_buff = BuffConfigurator.New(TheCagedSunHPBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(TheCagedSunHPBuffDisplayName)
                .SetDescription(TheCagedSunHPBuffDescription)
                .AddTemporaryHitPointsFromAbilityValue(value: ContextValues.Shared(AbilitySharedValue.Heal), removeWhenHitPointsEnd: true)
                .SetIcon(icon).Configure();

            var the_caged_sun_buff = BuffConfigurator.New(TheCagedSunBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(TheCagedSunBuffDisplayName)
                .SetDescription(TheCagedSunBuffDescription)
                .AddComponent<TheCagedSun>(c => { c.buff = the_caged_sun_temp_hp_buff; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(TheCagedSunName, GuidStore.ReserveDynamic())
                .SetDisplayName(TheCagedSunDisplayName)
                .SetDescription(TheCagedSunDescription)
                .SetBuff(the_caged_sun_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility BattleAgainstTheSun()
        {
            var the_caged_sun_buff = BuffConfigurator.New(BattleAgainstTheSunBuffName, GuidStore.ReserveDynamic())
                .SetDisplayName(BattleAgainstTheSunBuffDisplayName)
                .SetDescription(BattleAgainstTheSunBuffDescription)
                .AddComponent<BattleAgainstTheSun>()
                .SetFxOnStart(BuffRefs.DayLightBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.DayLightBuff.Reference.Get().FxOnRemove)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(BattleAgainstTheSunName, GuidStore.ReserveDynamic())
                .SetDisplayName(BattleAgainstTheSunDisplayName)
                .SetDescription(BattleAgainstTheSunDescription)
                .SetBuff(the_caged_sun_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }
    }
}
