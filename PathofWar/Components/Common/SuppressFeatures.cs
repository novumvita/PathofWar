using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using System.Collections.Generic;

namespace PathofWar.Components.Common
{
    internal class SuppressFeatures : UnitFactComponentDelegate
    {
        public List<BlueprintFeature> features;
        private List<EntityFact> removed_facts = [];
        public override void OnPostLoad()
        {
            OnActivate();
        }

        public override void OnActivate()
        {
            foreach (var feature in features)
            {
                if (Owner.HasFact(feature))
                {
                    removed_facts.Add(Owner.GetFact(feature));
                    Owner.RemoveFact(feature);
                }
            }
        }

        public override void OnDeactivate()
        {
            foreach (var feature in removed_facts)
                Owner.AddFact(feature.Blueprint as BlueprintUnitFact, feature.MaybeContext);
            removed_facts = [];
        }
    }
}
