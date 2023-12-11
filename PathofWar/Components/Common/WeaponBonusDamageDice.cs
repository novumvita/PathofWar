using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathofWar.Components.Common
{
    internal class WeaponBonusDamageDice : UnitFactComponentDelegate,  IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
        public DamageDescription dmg_desc;
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
            evt.DamageDescription.Add(dmg_desc);
        }
    }
}
