using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form单位对攻测试 {
    public class Army {
        public string Name { get; set; }
        public int HP { get; set; }
        // 组织度
        public int Organisation { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }
        public int MinPierce { get; set; }
        public int MaxPierce { get; set; }
        //最小组织度击损
        public int MinLostOrganisation { get; set; }
        public int MaxLostOrganisation { get; set; }
        public int Miss { get; set; }
        public int Armor { get; set; }
        public bool IsTank { get; set; }
        public int Move { get; set; }
        public int Rout { get; set; }
        public int MaxHP { get; set; }
        public Army() {

        }
        public Army(Army army) {
            HP = army.HP;
            Name = army.Name;
            MinAttack = army.MinAttack;
            MaxAttack = army.MaxAttack;
            MinLostOrganisation = army.MinLostOrganisation;
            MaxLostOrganisation = army.MaxLostOrganisation;
            MinPierce = army.MinPierce;
            MaxPierce = army.MaxPierce;
            Miss = army.Miss;
            Armor = army.Armor;
            IsTank = army.IsTank;
            Move = army.Move;
            Rout = army.Rout;
            MaxHP = army.MaxHP;
        }

    }

}
