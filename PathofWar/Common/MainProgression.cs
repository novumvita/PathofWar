using BasicTemplate.Disciplines;
using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using PathofWar.Components;
using PathofWar.Disciplines;
using System.Collections.Generic;

namespace PathofWar.Common
{
    internal class MainProgression
    {
        #region CONST STRINGS
        private const string MainSelectionName = "PathofWar.Selection";
        private const string MainSelectionGuid = "8A64B550-66D9-4B07-87D0-E04E34333CAB";
        private const string MainSelectionDisplayName = "PathofWar.Selection.Name";
        private const string MainSelectionDescription = "PathofWar.Selection.Description";

        private const string MainProgressionName = "PathofWar.Progression";
        private const string MainProgressionGuid = "61C6A14D-D03C-462A-A6BF-75382A751A42";
        private const string MainProgressionDisplayName = "PathofWar.Progression.Name";
        private const string MainProgressionDescription = "PathofWar.Progression.Description";

        private const string ManeuverSelectionName = "PathofWar.Maneuver";
        private const string ManeuverSelectionGuid = "22413A47-1216-45F4-9980-81A5FBBB6D37";
        private const string ManeuverSelectionDisplayName = "PathofWar.Maneuver.Name";
        private const string ManeuverSelectionDescription = "PathofWar.Maneuver.Description";

        private const string StanceSelectionName = "PathofWar.Stance";
        private const string StanceSelectionGuid = "0653B493-3302-4601-90A2-EB6B3E3E743E";
        private const string StanceSelectionDisplayName = "PathofWar.Stance.Name";
        private const string StanceSelectionDescription = "PathofWar.Stance.Description";

        private const string ResourceName = "PathofWar.Resource";
        private const string ResourceGuid = "C923C566-E04C-4F6A-A973-21AD09DBC726";
        #endregion

        public static BlueprintAbilityResource maneuver_count;

        internal static void Configure()
        {
            maneuver_count = AbilityResourceConfigurator.New(ResourceName, ResourceGuid)
                .SetMaxAmount(ResourceAmountBuilder.New(0).IncreaseByLevelStartPlusDivStep(otherClassLevelsMultiplier: 1, startingLevel: 1, startingBonus: 2, levelsPerStep: 3, bonusPerStep: 1, minBonus: 0))
                .Configure();

            List<Discipline> disciplines = new()
            {
                RadiantDawn.Configure(),
                VeiledMoon.Configure(),
            };

            var maneuver_selection_0 = FeatureSelectionConfigurator.New(ManeuverSelectionName, ManeuverSelectionGuid)
                .SetDisplayName(ManeuverSelectionDisplayName)
                .SetDescription(ManeuverSelectionDescription)
                .AddToAllFeatures(FeatureSelectionRefs.BasicFeatSelection.Reference.Get())
                .SetShowThisSelection(false);

            var stance_selection_0 = FeatureSelectionConfigurator.New(StanceSelectionName, StanceSelectionGuid)
                .SetDisplayName(StanceSelectionDisplayName)
                .SetDescription(StanceSelectionDescription)
                .AddToAllFeatures(FeatureSelectionRefs.BasicFeatSelection.Reference.Get())
                .SetShowThisSelection(false);

            var discipline_selection_0 = FeatureSelectionConfigurator.New(MainSelectionName, MainSelectionGuid)
                .SetDisplayName(MainSelectionDisplayName)
                .SetDescription(MainSelectionDescription)
                .AddAbilityResources(resource: maneuver_count, restoreAmount: true)
                .AddComponent<RestoreResourceOnCombatEnd>(c => c.Resource = maneuver_count);

            foreach (var discipline in disciplines)
            {
                maneuver_selection_0.AddToAllFeatures(discipline.maneuver_selection);
                stance_selection_0.AddToAllFeatures(discipline.stance_selection);
                discipline_selection_0.AddToAllFeatures(discipline.discipline);
            }

            var maneuver_selection = maneuver_selection_0.Configure();
            var stance_selection = stance_selection_0.Configure();
            var discipline_selection = discipline_selection_0.Configure();

            var lb = LevelEntryBuilder.New()
                .AddEntry(1, maneuver_selection, maneuver_selection, stance_selection)
                .AddEntry(4, maneuver_selection, stance_selection)
                .AddEntry(7, maneuver_selection, stance_selection)
                .AddEntry(10, maneuver_selection)
                .AddEntry(13, maneuver_selection, stance_selection)
                .AddEntry(16, maneuver_selection)
                .AddEntry(19, maneuver_selection, stance_selection);

            var discipline_progression = ProgressionConfigurator.New(MainProgressionName, MainProgressionGuid)
                .SetDisplayName(MainProgressionDisplayName)
                .SetDescription(MainProgressionDescription)
                .AddToLevelEntries(lb.GetEntries())
                .Configure();

            var feat_prog = ProgressionConfigurator.For(ProgressionRefs.BasicFeatsProgression)
                .AddToLevelEntry(1, discipline_selection, discipline_progression)
                .Configure();

            RootConfigurator.For(RootRefs.BlueprintRoot)
                .ModifyProgression(c => c.m_FeatsProgression = feat_prog.ToReference<BlueprintProgressionReference>())
                .Configure();
        }
    }
}
