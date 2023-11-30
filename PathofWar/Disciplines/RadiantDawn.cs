using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using PathofWar.Common;
using PathofWar.Components;
using PathofWar.Components.Common;
using PathofWar.Components.RadiantDawn;
using PathofWar.Patches;

namespace BasicTemplate.Disciplines
{
    internal class RadiantDawn
    {
        #region CONSTANTS  
        private const string RadiantDawnProgressionName = "RadiantDawn";
        private const string RadiantDawnProgressionGuid = "ABA5785C-977C-42E7-A13F-57E90159471F";
        private const string RadiantDawnProgressionDisplayName = "RadiantDawn.Name";
        private const string RadiantDawnProgressionDescription = "RadiantDawn.Description";

        private const string BolsterName = "RadiantDawn.Bolster";
        private const string BolsterGuid = "BCB5D2FA-34FA-409E-A646-5D43AD39909F";
        private const string BolsterDisplayName = "RadiantDawn.Bolster.Name";
        private const string BolsterDescription = "RadiantDawn.Bolster.Description";

        private const string BolsterBuffName = "RadiantDawn.Bolster.Buff";
        private const string BolsterBuffGuid = "695A6EB3-847E-4AFF-838A-EEE05C17C3B1";
        private const string BolsterBuffDisplayName = "RadiantDawn.Bolster.Buff.Name";
        private const string BolsterBuffDescription = "RadiantDawn.Bolster.Buff.Description";

        private const string DecreeOfMercyName = "RadiantDawn.DecreeOfMercy";
        private const string DecreeOfMercyGuid = "44B2B138-2B83-4AD3-9F51-874D0BBD1D7F";
        private const string DecreeOfMercyDisplayName = "RadiantDawn.DecreeOfMercy.Name";
        private const string DecreeOfMercyDescription = "RadiantDawn.DecreeOfMercy.Description";

        private const string DecreeOfMercyBuffName = "RadiantDawn.DecreeOfMercy.Buff";
        private const string DecreeOfMercyBuffGuid = "54891945-A174-43F8-AA79-426B95A62FAF";
        private const string DecreeOfMercyBuffDisplayName = "RadiantDawn.DecreeOfMercy.Buff.Name";
        private const string DecreeOfMercyBuffDescription = "RadiantDawn.DecreeOfMercy.Buff.Description";

        private const string DismissName = "RadiantDawn.Dismiss";
        private const string DismissGuid = "CC39C51A-B7B0-4E6E-9F90-9101EF17A22F";
        private const string DismissDisplayName = "RadiantDawn.Dismiss.Name";
        private const string DismissDescription = "RadiantDawn.Dismiss.Description";

        private const string LifeburstStrikeName = "RadiantDawn.LifeburstStrike";
        private const string LifeburstStrikeGuid = "8BB8B201-7339-4B09-9FAE-7EA4F4941FC7";
        private const string LifeburstStrikeDisplayName = "RadiantDawn.LifeburstStrike.Name";
        private const string LifeburstStrikeDescription = "RadiantDawn.LifeburstStrike.Description";

        private const string DecreeOfDeathName = "RadiantDawn.DecreeOfDeath";
        private const string DecreeOfDeathGuid = "0C349A0E-890E-458C-ACEE-1BE7D68C30FA";
        private const string DecreeOfDeathDisplayName = "RadiantDawn.DecreeOfDeath.Name";
        private const string DecreeOfDeathDescription = "RadiantDawn.DecreeOfDeath.Description";

        private const string DecreeOfDeathBuffName = "RadiantDawn.DecreeOfDeath.Buff";
        private const string DecreeOfDeathBuffGuid = "433BC57F-0A31-473F-8318-F96EA2524BD1";
        private const string DecreeOfDeathBuffDisplayName = "RadiantDawn.DecreeOfDeath.Buff.Name";
        private const string DecreeOfDeathBuffDescription = "RadiantDawn.DecreeOfDeath.Buff.Description";

        private const string ShatterSpellName = "RadiantDawn.ShatterSpell";
        private const string ShatterSpellGuid = "B463AAAB-781D-4CAF-A812-C72EA041E809";
        private const string ShatterSpellDisplayName = "RadiantDawn.ShatterSpell.Name";
        private const string ShatterSpellDescription = "RadiantDawn.ShatterSpell.Description";

        private const string DecreeOfPurityName = "RadiantDawn.DecreeOfPurity";
        private const string DecreeOfPurityGuid = "7DD4226C-90C4-4B80-9385-ACC83792F5F5";
        private const string DecreeOfPurityDisplayName = "RadiantDawn.DecreeOfPurity.Name";
        private const string DecreeOfPurityDescription = "RadiantDawn.DecreeOfPurity.Description";

        private const string PushTheAdvantageName = "RadiantDawn.PushTheAdvantage";
        private const string PushTheAdvantageGuid = "A6CC09D4-7B03-4253-B8DB-64835C369603";
        private const string PushTheAdvantageDisplayName = "RadiantDawn.PushTheAdvantage.Name";
        private const string PushTheAdvantageDescription = "RadiantDawn.PushTheAdvantage.Description";

        private const string PushTheAdvantageBuffName = "RadiantDawn.PushTheAdvantage.Buff";
        private const string PushTheAdvantageBuffGuid = "DDC1E618-D62A-4CBC-AE6B-9656C965F9BF";
        private const string PushTheAdvantageBuffDisplayName = "RadiantDawn.PushTheAdvantage.Buff.Name";
        private const string PushTheAdvantageBuffDescription = "RadiantDawn.PushTheAdvantage.Buff.Description";

        private const string NoblesseObligeName = "RadiantDawn.NoblesseOblige";
        private const string NoblesseObligeGuid = "495D1CB5-1E55-4F47-A68B-1C9D17C0EB9F";
        private const string NoblesseObligeDisplayName = "RadiantDawn.NoblesseOblige.Name";
        private const string NoblesseObligeDescription = "RadiantDawn.NoblesseOblige.Description";

        private const string NoblesseObligeBuffName = "RadiantDawn.NoblesseOblige.Buff";
        private const string NoblesseObligeBuffGuid = "EF9B3E3D-64E4-471D-8CDD-08E5AD792892";
        private const string NoblesseObligeBuffDisplayName = "RadiantDawn.NoblesseOblige.Buff.Name";
        private const string NoblesseObligeBuffDescription = "RadiantDawn.NoblesseOblige.Buff.Description";

        private const string SpoilsOfWarName = "RadiantDawn.SpoilsOfWar";
        private const string SpoilsOfWarGuid = "0EF8ED97-4E3F-4AF8-802D-709CBF296376";
        private const string SpoilsOfWarDisplayName = "RadiantDawn.SpoilsOfWar.Name";
        private const string SpoilsOfWarDescription = "RadiantDawn.SpoilsOfWar.Description";

        private const string SpoilsOfWarBuffName = "RadiantDawn.SpoilsOfWar.Buff";
        private const string SpoilsOfWarBuffGuid = "75D3FD9A-8FCF-4272-8984-25A6EC43DA04";
        private const string SpoilsOfWarBuffDisplayName = "RadiantDawn.SpoilsOfWar.Buff.Name";
        private const string SpoilsOfWarBuffDescription = "RadiantDawn.SpoilsOfWar.Buff.Description";

        private const string SpoilsOfWarAreaEffectName = "RadiantDawn.SpoilsOfWar.AreaEffect";
        private const string SpoilsOfWarAreaEffectGuid = "61E59355-CD5B-4B64-8FEB-AB131B7F25F3";

        private const string SpoilsOfWarBuffEffectName = "RadiantDawn.SpoilsOfWar.BuffEffect";
        private const string SpoilsOfWarBuffEffectGuid = "1BE21C8F-9C8B-42F9-9107-CF7DFD6A9539";
        private const string SpoilsOfWarBuffEffectDisplayName = "RadiantDawn.SpoilsOfWar.BuffEffect.Name";
        private const string SpoilsOfWarBuffEffectDescription = "RadiantDawn.SpoilsOfWar.BuffEffect.Description";

        private const string TheCagedSunName = "RadiantDawn.TheCagedSun";
        private const string TheCagedSunGuid = "9E126087-4FF1-4CC7-A18D-C382CE10837E";
        private const string TheCagedSunDisplayName = "RadiantDawn.TheCagedSun.Name";
        private const string TheCagedSunDescription = "RadiantDawn.TheCagedSun.Description";

        private const string TheCagedSunBuffName = "RadiantDawn.TheCagedSun.Buff";
        private const string TheCagedSunBuffGuid = "52977995-64E2-4479-89CB-0C9D1C57D57C";
        private const string TheCagedSunBuffDisplayName = "RadiantDawn.TheCagedSun.Buff.Name";
        private const string TheCagedSunBuffDescription = "RadiantDawn.TheCagedSun.Buff.Description";

        private const string TheCagedSunHPBuffName = "RadiantDawn.TheCagedSun.HPBuff";
        private const string TheCagedSunHPBuffGuid = "4B8B4E85-8CC8-4583-B426-9A1D5B3F011D";
        private const string TheCagedSunHPBuffDisplayName = "RadiantDawn.TheCagedSun.HPBuff.Name";
        private const string TheCagedSunHPBuffDescription = "RadiantDawn.TheCagedSun.HPBuff.Description";
        #endregion

        static readonly UnityEngine.Sprite icon = AbilityRefs.AngelStormOfJusticeAbility.Reference.Get().Icon;

        internal static BlueprintFeature Configure()
        {
            var discipline_feat = FeatureConfigurator.New(RadiantDawnProgressionName, RadiantDawnProgressionGuid)
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon).Configure();

            /*MANEUVERS*/
            var bolster = FeatureGen.FeatureFromFact(Bolster(), discipline_feat, true, 1);
            var decree_of_mercy = FeatureGen.FeatureFromFact(DecreeOfMercy(), discipline_feat, true, 1);
            var dismiss = FeatureGen.FeatureFromFact(Dismiss(), discipline_feat, true, 1);
            var staunching_strike = FeatureGen.FeatureFromFact(LifeburstStrike(), discipline_feat, true, 1);
            var decree_of_death = FeatureGen.FeatureFromFact(DecreeOfDeath(), discipline_feat, true, 7);
            var shatter_spell = FeatureGen.FeatureFromFact(ShatterSpell(), discipline_feat, true, 4);
            var decree_of_purity = FeatureGen.FeatureFromFact(DecreeOfPurity(), discipline_feat, true, 7);
            var push_the_advantage = FeatureGen.FeatureFromFact(PushTheAdvantage(), discipline_feat, true, 13);
            var noblesse_oblige = FeatureGen.FeatureFromFact(NoblesseOblige(), discipline_feat, true, 13);

            /*STANCES*/
            var spoils_of_war = FeatureGen.FeatureFromFact(SpoilsOfWar(), discipline_feat, false, 1);
            var the_caged_sun = FeatureGen.FeatureFromFact(TheCagedSun(), discipline_feat, false, 7);

            return discipline_feat;
        }

        internal static BlueprintAbility Bolster()
        {
            var bolster_buff = BuffConfigurator.New(BolsterBuffName, BolsterBuffGuid)
                .SetDisplayName(BolsterBuffDisplayName)
                .SetDescription(BolsterBuffDescription)
                .AddDamageResistancePhysical(value: ContextValues.Rank())
                .AddTargetAttackWithWeaponTrigger(ActionsBuilder.New().RemoveSelf().Build(), onlyHit: true)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(BolsterName, BolsterGuid)
                .SetDisplayName(BolsterDisplayName)
                .SetDescription(BolsterDescription)
                .SetActionType(UnitCommand.CommandType.Free)
                .AllowTargeting(friends: true)
                .SetRange(AbilityRange.Close)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(bolster_buff).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfMercy()
        {
            var decree_of_mercy_buff = BuffConfigurator.New(DecreeOfMercyBuffName, DecreeOfMercyBuffGuid)
                .SetDisplayName(DecreeOfMercyBuffDisplayName)
                .SetDescription(DecreeOfMercyBuffDescription)
                .AddComponent<DecreeOfMercy>()
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(DecreeOfMercyName, DecreeOfMercyGuid)
                .SetDisplayName(DecreeOfMercyDisplayName)
                .SetDescription(DecreeOfMercyDescription)
                .SetActionType(UnitCommand.CommandType.Free)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Close)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(decree_of_mercy_buff).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility Dismiss()
        {
            return AbilityConfigurator.New(DismissName, DismissGuid)
                .SetDisplayName(DismissDisplayName)
                .SetDescription(DismissDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityEffectRunAction(ActionsBuilder.New().CombatManeuver(ActionsBuilder.New().Push(ContextValues.Constant(10), false), CombatManeuver.Trip, newStat: StatType.Charisma, useCasterLevelAsBaseAttack: true).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility LifeburstStrike()
        {
            return AbilityConfigurator.New(LifeburstStrikeName, LifeburstStrikeGuid)
                .SetDisplayName(LifeburstStrikeDisplayName)
                .SetDescription(LifeburstStrikeDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<RadiantHeal>().Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfDeath()
        {
            var decree_of_death_buff = BuffConfigurator.New(DecreeOfDeathBuffName, DecreeOfDeathBuffGuid)
                .SetDisplayName(DecreeOfDeathBuffDisplayName)
                .SetDescription(DecreeOfDeathBuffDescription)
                .AddComponent<DecreeOfDeath>()
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(DecreeOfDeathName, DecreeOfDeathGuid)
                .SetDisplayName(DecreeOfDeathDisplayName)
                .SetDescription(DecreeOfDeathDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(decree_of_death_buff, ContextDuration.Fixed(1)).Build())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility ShatterSpell()
        {
            return AbilityConfigurator.New(ShatterSpellName, ShatterSpellGuid)
                .SetDisplayName(ShatterSpellDisplayName)
                .SetDescription(ShatterSpellDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityEffectRunAction(ActionsBuilder.New().DealDamage(DamageTypes.Direct(), ContextDice.Value(DiceType.D6, 2)).DispelMagic(ContextActionDispelMagic.BuffType.FromSpells, RuleDispelMagic.CheckType.CasterLevel, 9).Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfPurity()
        {
            return AbilityConfigurator.New(DecreeOfPurityName, DecreeOfPurityGuid)
                .SetDisplayName(DecreeOfPurityDisplayName)
                .SetDescription(DecreeOfPurityDescription)
                .SetActionType(UnitCommand.CommandType.Swift)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityAoERadius(radius: 30.Feet(), targetType: TargetType.Ally)
                .SetRange(AbilityRange.Close)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<RadiantHeal>().Build())
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility PushTheAdvantage()
        {
            var push_the_advantage_buff = BuffConfigurator.New(PushTheAdvantageBuffName, PushTheAdvantageBuffGuid)
                .SetDisplayName(PushTheAdvantageBuffDisplayName)
                .SetDescription(PushTheAdvantageBuffDescription)
                .AddComponent<PushTheAdvantage>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(PushTheAdvantageName, PushTheAdvantageGuid)
                .SetDisplayName(PushTheAdvantageDisplayName)
                .SetDescription(PushTheAdvantageDescription)
                .SetBuff(push_the_advantage_buff)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility NoblesseOblige()
        {
            var noblesse_oblige_buff = BuffConfigurator.New(NoblesseObligeBuffName, NoblesseObligeBuffGuid)
                .SetDisplayName(NoblesseObligeBuffDisplayName)
                .SetDescription(NoblesseObligeBuffDescription)
                .AddComponent<NoblesseOblige>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.Resource = MainProgression.maneuver_count; c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(NoblesseObligeName, NoblesseObligeGuid)
                .SetDisplayName(NoblesseObligeDisplayName)
                .SetDescription(NoblesseObligeDescription)
                .SetBuff(noblesse_oblige_buff)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility SpoilsOfWar()
        {

            var spoils_of_war_area_buff = BuffConfigurator.New(SpoilsOfWarBuffEffectName, SpoilsOfWarBuffEffectGuid)
                .SetDisplayName(SpoilsOfWarBuffEffectDisplayName)
                .SetDescription(SpoilsOfWarBuffEffectDescription)
                .AddComponent<SpoilsOfWar>()
                .SetIcon(icon).Configure();

            var spoils_of_war_area_effect = AbilityAreaEffectConfigurator.New(SpoilsOfWarAreaEffectName, SpoilsOfWarAreaEffectGuid)
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(spoils_of_war_area_buff, condition: ConditionsBuilder.New().IsAlly().Build())
                .Configure();

            var spoils_of_war_buff = BuffConfigurator.New(SpoilsOfWarBuffName, SpoilsOfWarBuffGuid)
                .SetDisplayName(SpoilsOfWarBuffDisplayName)
                .SetDescription(SpoilsOfWarBuffDescription)
                .AddAreaEffect(spoils_of_war_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(SpoilsOfWarName, SpoilsOfWarGuid)
                .SetDisplayName(SpoilsOfWarDisplayName)
                .SetDescription(SpoilsOfWarDescription)
                .SetBuff(spoils_of_war_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }

        internal static BlueprintActivatableAbility TheCagedSun()
        {
            var the_caged_sun_temp_hp_buff = BuffConfigurator.New(TheCagedSunHPBuffName, TheCagedSunHPBuffGuid)
                .SetDisplayName(TheCagedSunHPBuffDisplayName)
                .SetDescription(TheCagedSunHPBuffDescription)
                .AddTemporaryHitPointsFromAbilityValue(value: ContextValues.Shared(AbilitySharedValue.Heal), removeWhenHitPointsEnd: true)
                .SetIcon(icon).Configure();

            var the_caged_sun_buff = BuffConfigurator.New(TheCagedSunBuffName, TheCagedSunBuffGuid)
                .SetDisplayName(TheCagedSunBuffDisplayName)
                .SetDescription(TheCagedSunBuffDescription)
                .AddComponent<TheCagedSun>(c => { c.buff = the_caged_sun_temp_hp_buff; })
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(TheCagedSunName, TheCagedSunGuid)
                .SetDisplayName(TheCagedSunDisplayName)
                .SetDescription(TheCagedSunDescription)
                .SetBuff(the_caged_sun_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }
    }
}
