using System;
using Utils.Script;

namespace NumericalSimulation.Scripts
{
    /// <summary>
    /// 兵种数据类型
    /// </summary>
    [Serializable]
    public class ArmDataType : BaseJsonData
    {
        /// <summary>
        /// 兵种名称
        /// </summary>
        public string unitName;

        /// <summary>
        /// 总血量
        /// </summary>
        public int totalHP;

        /// <summary>
        /// 总人数
        /// </summary>
        public int totalTroops;

        /// <summary>
        /// 攻击能力
        /// </summary>
        public int attack;

        /// <summary>
        /// 冲锋加成
        /// </summary>
        public int charge;

        /// <summary>
        /// 防御能力（近战）
        /// </summary>
        public int defenseMelee;

        /// <summary>
        /// 防御能力（远程）
        /// </summary>
        public int defenseRange;

        /// <summary>
        /// 近战杀伤（普通）
        /// </summary>
        public int meleeNormal;

        /// <summary>
        /// 近战杀伤（破甲）
        /// </summary>
        public int meleeArmor;

        /// <summary>
        /// 护甲强度
        /// </summary>
        public int armor;

        /// <summary>
        /// 移动能力
        /// </summary>
        public int mobility;

        /// <summary>
        /// 视野
        /// </summary>
        public int sight;

        /// <summary>
        /// 隐蔽
        /// </summary>
        public int stealth;

        /// <summary>
        /// 弹药量
        /// </summary>
        public int ammo;

        /// <summary>
        /// 射程
        /// </summary>
        public int range;

        /// <summary>
        /// 装填速度
        /// </summary>
        public int reload;

        /// <summary>
        /// 精度
        /// </summary>
        public int accuracy;

        /// <summary>
        /// 远程杀伤
        /// </summary>
        public int rangeDamage;

        /// <summary>
        /// 作战意志
        /// </summary>
        public int morale;

        /// <summary>
        /// 疲劳值
        /// </summary>
        public int fatigue;

        /// <summary>
        /// 价格
        /// </summary>
        public int cost;
    }
}