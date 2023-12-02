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
        private const string RadiantDawnProgressionGuid = "ABA5785C-977C-42E7-A13F-57E90159471F";
        private const string RadiantDawnProgressionDisplayName = "RadiantDawn.Name";
        private const string RadiantDawnProgressionDescription = "RadiantDawn.Description";

        private const string RadiantDawnManeuverSelectionName = "RadiantDawn.ManeuverSelection";
        private const string RadiantDawnManeuverSelectionGuid = "4A32371B-DC1C-463F-901A-5F0C66BA17E2";

        private const string RadiantDawnStanceSelectionName = "RadiantDawn.StanceSelection";
        private const string RadiantDawnStanceSelectionGuid = "8DF93188-5C8A-46F1-BBEA-749BE4514465";

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

        private const string TyrantsEndName = "RadiantDawn.TyrantsEnd";
        private const string TyrantsEndGuid = "6FFA1BCF-5DA2-4135-8D7A-CDA5C05E7EED";
        private const string TyrantsEndDisplayName = "RadiantDawn.TyrantsEnd.Name";
        private const string TyrantsEndDescription = "RadiantDawn.TyrantsEnd.Description";

        private const string TyrantsEndBuffName = "RadiantDawn.TyrantsEnd.Buff";
        private const string TyrantsEndBuffGuid = "45B194A9-343E-4361-9BD3-29DF6C6E1586";
        private const string TyrantsEndBuffDisplayName = "RadiantDawn.TyrantsEnd.Buff.Name";
        private const string TyrantsEndBuffDescription = "RadiantDawn.TyrantsEnd.Buff.Description";

        private const string ShatterSpellName = "RadiantDawn.ShatterSpell";
        private const string ShatterSpellGuid = "B463AAAB-781D-4CAF-A812-C72EA041E809";
        private const string ShatterSpellDisplayName = "RadiantDawn.ShatterSpell.Name";
        private const string ShatterSpellDescription = "RadiantDawn.ShatterSpell.Description";

        private const string DecreeOfPurityName = "RadiantDawn.DecreeOfPurity";
        private const string DecreeOfPurityGuid = "7DD4226C-90C4-4B80-9385-ACC83792F5F5";
        private const string DecreeOfPurityDisplayName = "RadiantDawn.DecreeOfPurity.Name";
        private const string DecreeOfPurityDescription = "RadiantDawn.DecreeOfPurity.Description";

        private const string JudgementDayName = "RadiantDawn.JudgementDay";
        private const string JudgementDayGuid = "B309DF44-0E88-4D4B-9FED-199B9DFD43C8";
        private const string JudgementDayDisplayName = "RadiantDawn.JudgementDay.Name";
        private const string JudgementDayDescription = "RadiantDawn.JudgementDay.Description";

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

        private const string ArmamentsOfTheEmpireName = "RadiantDawn.ArmamentsOfTheEmpire";
        private const string ArmamentsOfTheEmpireGuid = "DB5349DB-966B-45D0-AAE1-7D4AB944E9AB";
        private const string ArmamentsOfTheEmpireDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.Name";
        private const string ArmamentsOfTheEmpireDescription = "RadiantDawn.ArmamentsOfTheEmpire.Description";

        private const string ArmamentsOfTheEmpireBuffName = "RadiantDawn.ArmamentsOfTheEmpire.Buff";
        private const string ArmamentsOfTheEmpireBuffGuid = "455DF34D-8498-419B-824D-E33CB6D8E300";
        private const string ArmamentsOfTheEmpireBuffDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.Buff.Name";
        private const string ArmamentsOfTheEmpireBuffDescription = "RadiantDawn.ArmamentsOfTheEmpire.Buff.Description";

        private const string ArmamentsOfTheEmpireAreaEffectName = "RadiantDawn.ArmamentsOfTheEmpire.AreaEffect";
        private const string ArmamentsOfTheEmpireAreaEffectGuid = "ACBEDDAB-89B9-4E09-8855-03D45EDB0E34";

        private const string ArmamentsOfTheEmpireBuffEffectName = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect";
        private const string ArmamentsOfTheEmpireBuffEffectGuid = "1162309E-EFA6-4300-987E-F2875BC55395";
        private const string ArmamentsOfTheEmpireBuffEffectDisplayName = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect.Name";
        private const string ArmamentsOfTheEmpireBuffEffectDescription = "RadiantDawn.ArmamentsOfTheEmpire.BuffEffect.Description";

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

        private const string BattleAgainstTheSunName = "RadiantDawn.BattleAgainstTheSun";
        private const string BattleAgainstTheSunGuid = "D1A50532-1A12-45B8-86B7-9B6A4A495C93";
        private const string BattleAgainstTheSunDisplayName = "RadiantDawn.BattleAgainstTheSun.Name";
        private const string BattleAgainstTheSunDescription = "RadiantDawn.BattleAgainstTheSun.Description";

        private const string BattleAgainstTheSunBuffName = "RadiantDawn.BattleAgainstTheSun.Buff";
        private const string BattleAgainstTheSunBuffGuid = "83B0726D-F4F6-4FDC-A182-EB01AA1708E6";
        private const string BattleAgainstTheSunBuffDisplayName = "RadiantDawn.BattleAgainstTheSun.Buff.Name";
        private const string BattleAgainstTheSunBuffDescription = "RadiantDawn.BattleAgainstTheSun.Buff.Description";
        #endregion

        static readonly UnityEngine.Sprite icon = AbilityRefs.AngelStormOfJusticeAbility.Reference.Get().Icon;

        internal static Discipline Configure()
        {
            var discipline_feat = FeatureConfigurator.New(RadiantDawnProgressionName, RadiantDawnProgressionGuid)
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon).Configure();

            var maneuver_selection = FeatureSelectionConfigurator.New(RadiantDawnManeuverSelectionName, RadiantDawnManeuverSelectionGuid)
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon)
                .Configure();

            var stance_selection = FeatureSelectionConfigurator.New(RadiantDawnStanceSelectionName, RadiantDawnStanceSelectionGuid)
                .SetDisplayName(RadiantDawnProgressionDisplayName)
                .SetDescription(RadiantDawnProgressionDescription)
                .SetIcon(icon)
                .Configure();

            /*MANEUVERS*/
            var bolster = FeatureGen.FeatureFromFact(Bolster(), discipline_feat, maneuver_selection, 1);
            var decree_of_mercy = FeatureGen.FeatureFromFact(DecreeOfMercy(), discipline_feat, maneuver_selection, 1);
            var dismiss = FeatureGen.FeatureFromFact(Dismiss(), discipline_feat, maneuver_selection, 1);
            var lifeburst_strike = FeatureGen.FeatureFromFact(LifeburstStrike(), discipline_feat, maneuver_selection, 1);
            var decree_of_death = FeatureGen.FeatureFromFact(DecreeOfDeath(), discipline_feat, maneuver_selection, 7);
            var shatter_spell = FeatureGen.FeatureFromFact(ShatterSpell(), discipline_feat, maneuver_selection, 4);
            var decree_of_purity = FeatureGen.FeatureFromFact(DecreeOfPurity(), discipline_feat, maneuver_selection, 7);
            var push_the_advantage = FeatureGen.FeatureFromFact(PushTheAdvantage(), discipline_feat, maneuver_selection, 13);
            var noblesse_oblige = FeatureGen.FeatureFromFact(NoblesseOblige(), discipline_feat, maneuver_selection, 13);
            var judgement_day = FeatureGen.FeatureFromFact(JudgementDay(), discipline_feat, maneuver_selection, 16);
            var tyrants_end = FeatureGen.FeatureFromFact(TyrantsEnd(), discipline_feat, maneuver_selection, 16);

            /*STANCES*/
            var spoils_of_war = FeatureGen.FeatureFromFact(SpoilsOfWar(), discipline_feat, stance_selection, 1);
            var the_caged_sun = FeatureGen.FeatureFromFact(TheCagedSun(), discipline_feat, stance_selection, 7);
            var armaments_of_the_empire = FeatureGen.FeatureFromFact(ArmamentsOfTheEmpire(), discipline_feat, stance_selection, 7);
            var battle_against_the_sun = FeatureGen.FeatureFromFact(BattleAgainstTheSun(), discipline_feat, stance_selection, 7);

            Discipline discipline = new Discipline()
            {
                discipline = discipline_feat,
                maneuver_selection = maneuver_selection,
                stance_selection = stance_selection
            };

            return discipline;
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(bolster_buff).Build())
                .SetType(AbilityType.Extraordinary)
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(decree_of_mercy_buff).Build())
                .SetType(AbilityType.Extraordinary)
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().CombatManeuver(ActionsBuilder.New().Push(ContextValues.Constant(10), false), CombatManeuver.Trip, newStat: StatType.Charisma, useCasterLevelAsBaseAttack: true).Build())
                .SetType(AbilityType.Extraordinary)
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<RadiantHeal>().Build())
                .SetType(AbilityType.Extraordinary)
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(decree_of_death_buff, ContextDuration.Fixed(1)).Build())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility TyrantsEnd()
        {
            var features = FeatureRefs.All.Select(c => c.Reference.Get()).Where(c => FeatureChecker.IsFeatureImmunityOrResistance(c));

            var tyrants_end_buff = BuffConfigurator.New(TyrantsEndBuffName, TyrantsEndBuffGuid)
                .SetDisplayName(TyrantsEndBuffDisplayName)
                .SetDescription(TyrantsEndBuffDescription)
                .AddComponent<SuppressFeatures>(c => c.features = features.ToList())
                .AddComponent<TyrantsEnd>()
                .SetIcon(icon).Configure();

            return AbilityConfigurator.New(TyrantsEndName, TyrantsEndGuid)
                .SetDisplayName(TyrantsEndDisplayName)
                .SetDescription(TyrantsEndDescription)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityResourceLogic(requiredResource: MainProgression.maneuver_count, isSpendResource: true, amount: 1)
                .AllowTargeting(enemies: true)
                .SetRange(AbilityRange.Weapon)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); c.ability_for_fx_target = AbilityRefs.SmiteEvilAbility.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().DealDamage(DamageTypes.Energy(DamageEnergyType.Holy), ContextDice.Value(DiceType.D6, 6)).ApplyBuff(tyrants_end_buff, ContextDuration.Fixed(1)).Build())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .SetType(AbilityType.Extraordinary)
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
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>(c => { c.ability_for_fx_self = AbilityRefs.CureLightWounds.Reference.Get(); })
                .AddAbilityEffectRunAction(ActionsBuilder.New().DealDamage(DamageTypes.Energy(DamageEnergyType.Holy), ContextDice.Value(DiceType.D6, 2)).DispelMagic(ContextActionDispelMagic.BuffType.FromSpells, RuleDispelMagic.CheckType.CasterLevel, 9).Build())
                .SetType(AbilityType.Extraordinary)
                .SetIcon(icon).Configure();
        }

        internal static BlueprintAbility DecreeOfPurity()
        {
            return AbilityConfigurator.New(DecreeOfPurityName, DecreeOfPurityGuid)
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
            return AbilityConfigurator.New(JudgementDayName, JudgementDayGuid)
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

        internal static BlueprintActivatableAbility ArmamentsOfTheEmpire()
        {

            var spoils_of_war_area_buff = BuffConfigurator.New(ArmamentsOfTheEmpireBuffEffectName, ArmamentsOfTheEmpireBuffEffectGuid)
                .SetDisplayName(ArmamentsOfTheEmpireBuffEffectDisplayName)
                .SetDescription(ArmamentsOfTheEmpireBuffEffectDescription)
                .AddBuffEnchantAnyWeapon(WeaponEnchantmentRefs.Holy.Reference.Get())
                .AddBuffEnchantAnyWeapon(WeaponEnchantmentRefs.TemporaryEnhancement3.Reference.Get())
                .AddBuffEnchantArmor([ArmorEnchantmentRefs.TemporaryArmorEnhancementBonus3.Reference.Get()])
                .SetIcon(icon).Configure();

            var spoils_of_war_area_effect = AbilityAreaEffectConfigurator.New(ArmamentsOfTheEmpireAreaEffectName, ArmamentsOfTheEmpireAreaEffectGuid)
                .SetShape(AreaEffectShape.Cylinder)
                .SetSize(30.Feet())
                .AddAbilityAreaEffectBuff(spoils_of_war_area_buff, condition: ConditionsBuilder.New().IsAlly().Build())
                .Configure();

            var spoils_of_war_buff = BuffConfigurator.New(ArmamentsOfTheEmpireBuffName, ArmamentsOfTheEmpireBuffGuid)
                .SetDisplayName(ArmamentsOfTheEmpireBuffDisplayName)
                .SetDescription(ArmamentsOfTheEmpireBuffDescription)
                .AddAreaEffect(spoils_of_war_area_effect)
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(ArmamentsOfTheEmpireName, ArmamentsOfTheEmpireGuid)
                .SetDisplayName(ArmamentsOfTheEmpireDisplayName)
                .SetDescription(ArmamentsOfTheEmpireDescription)
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

        internal static BlueprintActivatableAbility BattleAgainstTheSun()
        {
            var the_caged_sun_buff = BuffConfigurator.New(BattleAgainstTheSunBuffName, BattleAgainstTheSunBuffGuid)
                .SetDisplayName(BattleAgainstTheSunBuffDisplayName)
                .SetDescription(BattleAgainstTheSunBuffDescription)
                .AddComponent<BattleAgainstTheSun>()
                .AddFacts([BuffRefs.DayLightBuff.Reference.Get()])
                .SetIcon(icon).Configure();

            return ActivatableAbilityConfigurator.New(BattleAgainstTheSunName, BattleAgainstTheSunGuid)
                .SetDisplayName(BattleAgainstTheSunDisplayName)
                .SetDescription(BattleAgainstTheSunDescription)
                .SetBuff(the_caged_sun_buff)
                .SetGroup(ExpandedActivatableAbilityGroup.MartialStance)
                .SetDeactivateImmediately()
                .SetIcon(icon).Configure();
        }
    }
}
