﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumericalSimulation.Scripts.Prefab
{
    public class UIChooseArmy : MonoBehaviour
    {
        public RectTransform chooseNode;
        public GameObject chooseButtonPrefab;
        public Button chooseArmyButton;
        public Text showArmyData;
        private List<GameObject> chooseButtons;
        private Dictionary<int, ArmDataType> armDataTypes;

        public const int CHOOSE_NODE_WIDTH = 960;
        public const int CHOOSE_NODE_HEIGHT = 540;
        public const int BUTTON_WIDTH = 240;
        public const int BUTTON_HEIGHT = 40;

        public void OnInit(Dictionary<int, ArmDataType> armDataType)
        {
            armDataTypes = armDataType;
            chooseButtons = new List<GameObject>();
            chooseArmyButton.onClick.AddListener(ShowAllArmy);
        }

        private void ShowAllArmy()
        {
            chooseNode.gameObject.SetActive(true);
            int totalApace = 100;
            int everySpace = 20;
            int buttonColNum = CHOOSE_NODE_WIDTH / (BUTTON_WIDTH + everySpace);
            int buttonRowNum = CHOOSE_NODE_HEIGHT / (BUTTON_HEIGHT + everySpace);
            List<int> ids = new List<int>(armDataTypes.Keys);
            for (int i = 0; i < ids.Count; i++)
            {
                int id = ids[i];
                ArmDataType type = armDataTypes[id];
                GameObject button;
                if (chooseButtons.Count > i)
                {
                    button = chooseButtons[i];
                }
                else
                {
                    button = Instantiate(chooseButtonPrefab, chooseNode);
                    chooseButtons.Add(button);
                    RectTransform rectTransform = button.GetComponent<RectTransform>();
                    int col = i / buttonRowNum;
                    int row = i % buttonRowNum;
                    rectTransform.anchoredPosition = new Vector2(
                        col * (BUTTON_WIDTH + everySpace) - CHOOSE_NODE_WIDTH / 2f + totalApace,
                        row * (BUTTON_HEIGHT + everySpace) - CHOOSE_NODE_HEIGHT / 2f + totalApace);
                    Button but = button.transform.Find("Button").GetComponent<Button>();
                    but.onClick.AddListener(() => { ChooseArmy(id); });
                    but.transform.Find("Text").GetComponent<Text>().text = type.unitName;
                }
            }
        }

        private void ChooseArmy(int id)
        {
            chooseNode.gameObject.SetActive(false);
            ArmDataType type = armDataTypes[id];
            Dictionary<string, string> attribute = UINumericalSimulation.armyAttribute;
            int maxNameLength = 0;
            foreach (var key in attribute.Keys)
            {
                maxNameLength = Mathf.Max(maxNameLength, attribute[key].Length);
            }

            // Format the attributes
            string lines = "";
            // Define a format string for aligned output
            lines += attribute[nameof(ArmDataType.unitName)] + "：" + type.unitName + "\n";
            lines += attribute[nameof(ArmDataType.totalHP)] + "：" + type.totalHP + "\n";
            lines += attribute[nameof(ArmDataType.totalTroops)] + "：" + type.totalTroops + "\n";
            lines += attribute[nameof(ArmDataType.attack)] + "：" + type.attack + "\n";
            lines += attribute[nameof(ArmDataType.charge)] + "：" + type.charge + "\n";
            lines += attribute[nameof(ArmDataType.defenseMelee)] + "：" + type.defenseMelee + "\n";
            lines += attribute[nameof(ArmDataType.defenseRange)] + "：" + type.defenseRange + "\n";
            lines += attribute[nameof(ArmDataType.meleeNormal)] + "：" + type.meleeNormal + "\n";
            lines += attribute[nameof(ArmDataType.meleeArmor)] + "：" + type.meleeArmor + "\n";
            lines += attribute[nameof(ArmDataType.armor)] + "：" + type.armor + "\n";
            lines += attribute[nameof(ArmDataType.mobility)] + "：" + type.mobility + "\n";
            lines += attribute[nameof(ArmDataType.sight)] + "：" + type.sight + "\n";
            lines += attribute[nameof(ArmDataType.stealth)] + "：" + type.stealth + "\n";
            lines += attribute[nameof(ArmDataType.ammo)] + "：" + type.ammo + "\n";
            lines += attribute[nameof(ArmDataType.range)] + "：" + type.range + "\n";
            lines += attribute[nameof(ArmDataType.reload)] + "：" + type.reload + "\n";
            lines += attribute[nameof(ArmDataType.accuracy)] + "：" + type.accuracy + "\n";
            lines += attribute[nameof(ArmDataType.rangeDamage)] + "：" + type.rangeDamage + "\n";
            lines += attribute[nameof(ArmDataType.morale)] + "：" + type.morale + "\n";
            lines += attribute[nameof(ArmDataType.fatigue)] + "：" + type.fatigue + "\n";
            lines += attribute[nameof(ArmDataType.cost)] + "：" + type.cost + "\n";

            // Join all lines and display in the Text component
            showArmyData.text = lines;
        }
    }
}