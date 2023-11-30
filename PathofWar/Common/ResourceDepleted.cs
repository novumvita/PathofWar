using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using UnityEngine;

namespace PathofWar.Components
{
    public class AddAbilityResourceDepletedTrigger : UnitFactComponentDelegate, IUnitAbilityResourceHandler, IGlobalSubscriber, ISubscriber
    {
        [SerializeField]
        public BlueprintAbilityResource Resource;

        public ActionList Action;

        public int Cost;

        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            BlueprintAbilityResourceReference m_Resource = Resource.ToReference<BlueprintAbilityResourceReference>();

            if (oldAmount > resource.Amount && !(unit != base.Fact.Owner.Unit) && m_Resource.Is(resource.Blueprint as BlueprintAbilityResource) && resource.Amount < Cost)
            {
                (base.Fact as IFactContextOwner)?.RunActionInContext(Action);
            }
        }
    }
}