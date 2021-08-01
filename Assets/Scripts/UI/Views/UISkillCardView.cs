using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using FLH.UI.Views.List;

namespace FLH.UI.Views
{
    public class UISkillCardView : BaseMonoBehaviour<UISkillCardView>
    {
        public GameObject objCardView;
        public GridLayoutGroup gridCards;

        public GameObject objEquipCardView;
        public GridLayoutGroup gridEquipCards;

        private void Awake()
        {
            if (!GameSetting.DebugMode)
            {
                Initialize();
            }
        }

        private void OnDestroy()
        {
            foreach (var item in gridCards.transform.GetChildList(true))
            {
                item.GetComponent<UIListItemSkillCard>().actEquip -= BindEquippedSkillCardList;
            }

            foreach (var item in gridEquipCards.transform.GetChildList(true))
            {
                item.GetComponent<UIListItemSkillEquipedCard>().actUnEquip -= UnEquippedSkillCard;
            }
        }

        private void Initialize()
        {
            foreach (var item in Singleton<Me>.Instance.SkillCard)
            {
                var isEquipped = Singleton<Me>.Instance.EquipCard.Count(x => x.Id == item.Id) > 0;
                var newObj = Instantiate(objCardView.GetComponent<UIListItemSkillCard>(), gridCards.transform);
                Debug.Assert(newObj != null);
                newObj.Bind(item, isEquipped);
                newObj.actEquip += BindEquippedSkillCardList;
                newObj.gameObject.SetVisibility(true);
            }

            BindEquippedSkillCardList();
        }

        public void BindEquippedSkillCardList()
        {
            var children = gridEquipCards.transform.GetChildList(true);
            while (children.Count > 0)
            {
                var index = children.Count - 1;
                children[index].GetComponent<UIListItemSkillEquipedCard>().actUnEquip -= UnEquippedSkillCard;
                Destroy(children[index].gameObject);
                children.RemoveAt(index);
            }

            foreach (var item in Singleton<Me>.Instance.EquipCard)
            {
                var newObj = Instantiate(objEquipCardView.GetComponent<UIListItemSkillEquipedCard>(), gridEquipCards.transform);
                Debug.Assert(newObj != null);
                newObj.Bind(item);
                newObj.actUnEquip += UnEquippedSkillCard;
                newObj.gameObject.SetVisibility(true);
            }
        }

        public void UnEquippedSkillCard(int id)
        {
            foreach (var item in gridCards.transform.GetChildList(true))
            {
                item.GetComponent<UIListItemSkillCard>().OnChangeButtonStatus(id, true);
            }

            // rebind
            BindEquippedSkillCardList();
        }
    }
}
