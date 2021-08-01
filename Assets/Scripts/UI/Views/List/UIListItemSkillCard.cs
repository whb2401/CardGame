using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace FLH.UI.Views.List
{
    public class UIListItemSkillCard : BaseMonoBehaviour<UIListItemSkillCard>
    {
        public Image imgCard;
        public Text textDesc;
        public Button btnEquip;

        public Action actEquip;

        const string unequipped = "Equip";
        const string equipped = "Equipped";

        private bool isShowText;
        private CardInfo _data;

        public void Bind(CardInfo data, bool isEquipped)
        {
            Debug.Assert(data != null);

            _data = data;
            AssetManager.SetImageTexture(imgCard, GameSetting.ABPTEXCARD, data.AssetName);
            textDesc.SetTextValue(data.Description.FLHReplaceChangeLine());
            btnEquip.enabled = !isEquipped;
            btnEquip.interactable = !isEquipped;
            btnEquip.GetComponentInChildren<Text>().text = isEquipped ? equipped : unequipped;
        }

        public void OnClicked()
        {
            isShowText = !isShowText;
            imgCard.gameObject.SetVisibility(!isShowText);
        }

        public void OnChangeButtonStatus(int id, bool isEnabled)
        {
            if (_data.Id != id) return;
            btnEquip.enabled = isEnabled;
            btnEquip.interactable = isEnabled;
            btnEquip.GetComponentInChildren<Text>().text = !isEnabled ? equipped : unequipped;
        }

        public void OnEquipClicked()
        {
            if (Singleton<Me>.Instance.EquipCard.Count(x => x.Id == _data.Id) > 0)
            {
                // TODO: alert msg
                // Equipped
                return;
            }
            btnEquip.enabled = false;
            btnEquip.interactable = false;
            btnEquip.GetComponentInChildren<Text>().text = equipped;

            Singleton<Me>.Instance.AddSkillCard(_data.Id);

            if (actEquip != null)
            {
                actEquip.Invoke();
            }
        }
    }
}
