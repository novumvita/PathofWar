using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathofWar.Components.RadiantDawn
{
    internal class TyrantsEnd : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            foreach (BaseDamage item in evt.DamageBundle)
            {
                item.Vulnerability = 1.5f;
            }
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }
    }
}
