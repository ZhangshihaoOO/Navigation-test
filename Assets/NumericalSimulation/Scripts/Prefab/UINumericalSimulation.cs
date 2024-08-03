using System.Collections.Generic;
using UnityEngine;
using Utils.Script;

namespace NumericalSimulation.Scripts.Prefab
{
    public class UINumericalSimulation : MonoBehaviour
    {
        public TextAsset data;
        public UIChooseArmy team1Choose;
        public UIChooseArmy team2Choose;

        private Dictionary<int, ArmDataType> armDataTypes;

        public static Dictionary<string, string> armyAttribute;

        private void Awake()
        {
            InitArmyAttribute();
            armDataTypes = new Dictionary<int, ArmDataType>();
            GameUtility.AnalysisJsonConfigurationTable(data, armDataTypes);
            team1Choose.OnInit(armDataTypes);
            team2Choose.OnInit(armDataTypes);
        }

        /// <summary>
        /// 可以通过属性的英文名称找到中文，因为是工具，就直接写死了
        /// </summary>
        private static void InitArmyAttribute()
        {
            armyAttribute = new Dictionary<string, string>
            {
                { nameof(ArmDataType.unitName), "兵种名称" },
                { nameof(ArmDataType.totalHP), "总血量" },
                { nameof(ArmDataType.totalTroops), "总人数" },
                { nameof(ArmDataType.attack), "攻击能力" },
                { nameof(ArmDataType.charge), "冲锋加成" },
                { nameof(ArmDataType.defenseMelee), "防御能力（近战）" },
                { nameof(ArmDataType.defenseRange), "防御能力（远程）" },
                { nameof(ArmDataType.meleeNormal), "近战杀伤（普通）" },
                { nameof(ArmDataType.meleeArmor), "近战杀伤（破甲）" },
                { nameof(ArmDataType.armor), "护甲强度" },
                { nameof(ArmDataType.mobility), "移动能力" },
                { nameof(ArmDataType.sight), "视野" },
                { nameof(ArmDataType.stealth), "隐蔽" },
                { nameof(ArmDataType.ammo), "弹药量" },
                { nameof(ArmDataType.range), "射程" },
                { nameof(ArmDataType.reload), "装填速度" },
                { nameof(ArmDataType.accuracy), "精度" },
                { nameof(ArmDataType.rangeDamage), "远程杀伤" },
                { nameof(ArmDataType.morale), "作战意志" },
                { nameof(ArmDataType.fatigue), "疲劳值" },
                { nameof(ArmDataType.cost), "价格" }
            };
        }
    }
}