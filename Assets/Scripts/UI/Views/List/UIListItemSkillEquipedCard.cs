using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace FLH.UI.Views.List
{
    public class UIListItemSkillEquipedCard : BaseMonoBehaviour<UIListItemSkillEquipedCard>
    {
        public Text textEquiped;

        public Action<int> actUnEquip;

        const string master = "Master.";
        const string slave = "Slave.";

        private CardInfo _data;

        public void Bind(CardInfo data)
        {
            Debug.Assert(data != null);

            _data = data;
            var ex = data.EquippedSlot == 3 ? slave : master;
            textEquiped.SetTextValue(ex + " " + data.Name);
        }

        public void OnRemoveEquippedCard()
        {
            Singleton<Me>.Instance.RemoveSkillCard(_data.Id);

            if (actUnEquip != null)
            {
                actUnEquip.Invoke(_data.Id);
            }
        }
    }
}
