using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form单位对攻测试 {
    public partial class Form2 : Form {
        private Army _army;
        public Form2(Country country,  Army army) {
            InitializeComponent();
            Init(country, army);
            _army = army;
        }

        private void Form2_Load(object sender, EventArgs e) {
          
        }
        private void Init(Country country, Army army) {
            tbxHP.Text = army.HP.ToString();
            tbxArmyName.Text = army.Name;
            tbxMinAtk.Text = army.MinAttack.ToString();
            tbxMaxAtk.Text = army.MaxAttack.ToString();
            tbxMinPierce.Text = army.MinPierce.ToString();
            tbxMaxPierce.Text = army.MaxPierce.ToString();
            tbxMinLostO.Text = army.MinLostOrganisation.ToString();
            tbxMaxLostO.Text = army.MaxLostOrganisation.ToString();
            tbxArmorNum.Text = army.Armor.ToString();
            tbxMiss.Text = army.Miss.ToString();
            tbxCountryName.Text = country.Name;
            tbxOrganisation.Text = army.Organisation.ToString();
            if (army.IsTank) {
                cbxIsTank.SelectedIndex = 0;
            } else {
                cbxIsTank.SelectedIndex = 1;
            }
            tbxMove.Text = army.Move.ToString();
            tbxRout.Text = army.Rout.ToString();

        }

        private void button1_Click(object sender, EventArgs e) {
            _army.Name = tbxArmyName.Text;
            _army.HP = Convert.ToInt32(tbxHP.Text);
            _army.Organisation = Convert.ToInt32(tbxOrganisation.Text);
            _army.MinAttack = Convert.ToInt32(tbxMinAtk.Text);
            _army.MaxAttack = Convert.ToInt32(tbxMaxAtk.Text);
            _army.MinPierce = Convert.ToInt32(tbxMinPierce.Text);
            _army.MaxPierce = Convert.ToInt32(tbxMaxPierce.Text);
            _army.MinLostOrganisation = Convert.ToInt32(tbxMinLostO.Text);
            _army.MaxLostOrganisation = Convert.ToInt32(tbxMaxLostO.Text);
            _army.Miss = Convert.ToInt32(tbxMiss.Text);
            _army.Armor = Convert.ToInt32(tbxArmorNum.Text);
            if (cbxIsTank.SelectedIndex == 1) {
                _army.IsTank = false;
            } else {
                _army.IsTank = true;
            }
            _army.Move = Convert.ToInt32(tbxMove.Text);
            _army.Rout = Convert.ToInt32(tbxRout.Text);
            _army.MaxHP = _army.HP;
            MessageBox.Show("已保存");
        }
    }
}
