using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Form单位对攻测试 {
    public partial class Form1 : Form {
        public static Form1 form1;
        public Dictionary<string, Country> dicCountries = new Dictionary<string, Country>();
        public Form1() {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {
            Init();
        }
        private void Init() {
            form1 = this;
            //默认为：是
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox2.SelectedItem = comboBox2.Items[1];
            comboBox3.SelectedItem = comboBox3.Items[1];
            tbxTimes.Text = "1";
            LoadXml();
            for (int i = 0; i < dicCountries.Count; i++) {
                string CountryName = dicCountries.ElementAt(i).Key;
                cbx1.Items.Add(CountryName);
                cbx2.Items.Add(CountryName);
            }
            InitPnl();
            tbxExtraAtk1.Text = "0";
            tbxExtraAtk2.Text = "0";
            tbxExtraDfs1.Text = "0";
            tbxExtraDfs2.Text = "0";

        }
        private void InitPnl() {
            for (int i = 0; i < panel1.Controls.Count; i++) {
                TextBox textBox = panel1.Controls[i] as TextBox;
                if (textBox != null) {
                    textBox.Text = "0";
                }
            }
            tbxCountryName.Text = "无";
            tbxArmyName.Text = "无";
            cbxIsTank.SelectedItem = cbxIsTank.Items[1];
        }
        private void LoadXml() {
            //string currentDirectory = System.Environment.CurrentDirectory;
            string fileName = "allUnits.xml";
            //如果这个文件不存在
            if (!File.Exists(fileName)) {
                //File.Create(fileName);
            } else {
                XDocument xml = XDocument.Load(fileName);
                //如果根节点读取成功

                XElement xRoot = xml.Root;
                var e_Countries = xRoot.Elements();
                int num = e_Countries.Count();
                for (int i = 0; i < num; i++) {
                    Country country = new Country();
                    //国家名字节点
                    string countryName = e_Countries.ElementAt(i).Attribute("name").Value;
                    country.Name = countryName;
                    //添加国家到字典 
                    dicCountries.Add(countryName, country);
                    //获取当前循环到的国家节点
                    var e_ListArmies = e_Countries.ElementAt(i).Elements();
                    int armiesLimit = e_ListArmies.Count();
                    for (int j = 0; j < armiesLimit; j++) {
                        var e_Army = e_ListArmies.ElementAt(j);
                        //创建军队
                        Army army = new Army();
                        //初始化
                        army.Name = e_Army.Attribute("Name").Value;
                        army.HP = Convert.ToInt32(e_Army.Attribute("HP").Value);
                        army.Organisation = Convert.ToInt32(e_Army.Attribute("Organisation").Value);
                        army.MinAttack = Convert.ToInt32(e_Army.Attribute("MinAttack").Value);
                        army.MaxAttack = Convert.ToInt32(e_Army.Attribute("MaxAttack").Value);
                        army.MinPierce = Convert.ToInt32(e_Army.Attribute("MinPierce").Value);
                        army.MaxPierce = Convert.ToInt32(e_Army.Attribute("MaxPierce").Value);
                        army.MinLostOrganisation = Convert.ToInt32(e_Army.Attribute("MinAtkO").Value);
                        army.MaxLostOrganisation = Convert.ToInt32(e_Army.Attribute("MaxAtkO").Value);
                        army.Armor = Convert.ToInt32(e_Army.Attribute("Armor").Value);
                        army.Miss = Convert.ToInt32(e_Army.Attribute("Miss").Value);
                        army.IsTank = Convert.ToBoolean(e_Army.Attribute("isTank").Value);
                        army.Move = Convert.ToInt32(e_Army.Attribute("move").Value);
                        army.Rout = Convert.ToInt32(e_Army.Attribute("rout").Value);
                        army.MaxHP = army.HP;
                        country.ListArmys.Add(army);
                    }

                }


            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Country country = null;
            string name = tbxCountryName.Text;
            //如果没有这个国家  添加这个国家到国家列表中
            if (!dicCountries.ContainsKey(name)) {
                country = new Country();
                country.Name = tbxCountryName.Text;
                dicCountries.Add(country.Name, country);
            } else {
                country = dicCountries[name];
            }
            Army army = new Army();
            army.Name = tbxArmyName.Text;
            for (int i = 0; i < country.ListArmys.Count; i++) {
                if (country.ListArmys[i].Name == army.Name) {
                    var result = MessageBox.Show("该国已经有同名的兵种，是否覆盖？", "警告", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        country.ListArmys.RemoveAt(i);
                        MessageBox.Show("已覆盖！");
                        continue;
                    } else {
                        return;
                    }
                }

            }
            army.HP = Convert.ToInt32(tbxHP.Text);
            army.Organisation = Convert.ToInt32(tbxOrganisation.Text);
            army.MinAttack = Convert.ToInt32(tbxMinAtk.Text);
            army.MaxAttack = Convert.ToInt32(tbxMaxAtk.Text);
            army.MinPierce = Convert.ToInt32(tbxMinPierce.Text);
            army.MaxPierce = Convert.ToInt32(tbxMaxPierce.Text);
            army.MinLostOrganisation = Convert.ToInt32(tbxMinLostO.Text);
            army.MaxLostOrganisation = Convert.ToInt32(tbxMaxLostO.Text);
            army.Miss = Convert.ToInt32(tbxMiss.Text);
            army.Armor = Convert.ToInt32(tbxArmorNum.Text);
            army.Move = Convert.ToInt32(tbxMove.Text);
            army.MaxHP = army.HP;
            if (cbxIsTank.SelectedIndex == 1) {
                army.IsTank = false;
            } else {
                army.IsTank = true;
            }
            army.Rout = Convert.ToInt32(tbxRout.Text);
            country.ListArmys.Add(army);
            cbx1.Items.Clear();
            cbx2.Items.Clear();
            for (int i = 0; i < dicCountries.Count; i++) {
                cbx1.Items.Add(dicCountries.ElementAt(i).Key);
                cbx2.Items.Add(dicCountries.ElementAt(i).Key);

            }

            MessageBox.Show("已添加");

        }

        private void button2_Click(object sender, EventArgs e) {
            XDocument xml = new XDocument();
            XElement e_Root = new XElement("Root");
            xml.Add(e_Root);
            for (int i = 0; i < dicCountries.Count; i++) {
                Country country = dicCountries.ElementAt(i).Value;
                XElement e_Country = new XElement("Country",
                    new XAttribute("name", country.Name)
                    ); ;
                e_Root.Add(e_Country);
                for (int j = 0; j < country.ListArmys.Count; j++) {
                    Army army = country.ListArmys[j];
                    XElement e_Army = new XElement("Army",
                        new XAttribute("Name", army.Name),
                        new XAttribute("HP", army.HP),
                        new XAttribute("Organisation", army.Organisation),
                        new XAttribute("MinAttack", army.MinAttack),
                         new XAttribute("MaxAttack", army.MaxAttack),
                         new XAttribute("MinPierce", army.MinPierce),
                         new XAttribute("MaxPierce", army.MaxPierce),
                         new XAttribute("MinAtkO", army.MinLostOrganisation),
                         new XAttribute("MaxAtkO", army.MaxLostOrganisation),
                         new XAttribute("Armor", army.Armor),
                         new XAttribute("Miss", army.Miss),
                         new XAttribute("isTank", army.IsTank),
                         new XAttribute("move", army.Move),
                         new XAttribute("rout", army.Rout)
                        ); e_Country.Add(e_Army);

                }

            }
            xml.Save("allUnits.xml");
            MessageBox.Show("已保存");
        }

        private void cbxAtk1_MouseClick(object sender, MouseEventArgs e) {
            if (!dicCountries.ContainsKey(cbx1.Text)) {
                MessageBox.Show("该国家未找到！");
                return;
            }
            Country country = dicCountries[cbx1.Text];
            ComboBox comboBox = sender as ComboBox;
            comboBox.Items.Clear();
            for (int i = 0; i < country.ListArmys.Count; i++) {
                comboBox.Items.Add(country.ListArmys[i].Name);
            }

        }

        private void cbxDfs1_MouseClick(object sender, MouseEventArgs e) {
            if (!dicCountries.ContainsKey(cbx2.Text)) {
                MessageBox.Show("该国家未找到！");
                return;
            }
            Country country = dicCountries[cbx2.Text];
            ComboBox comboBox = sender as ComboBox;
            comboBox.Items.Clear();
            for (int i = 0; i < country.ListArmys.Count; i++) {
                comboBox.Items.Add(country.ListArmys[i].Name);
            }
        }

        private void btnFixAtk1_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx1.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx1.Text];
            OpenForm2(cbxAtk1, country);

        }

        private void OpenForm2(Object sender, Country country) {
            if (sender as ComboBox != null) {
                ComboBox combo = sender as ComboBox;
                Army army = null;
                for (int i = 0; i < country.ListArmys.Count; i++) {
                    if (country.ListArmys[i].Name == combo.Text) {
                        army = country.ListArmys[i];
                    }
                }
                if (army == null) {
                    MessageBox.Show("请选择一个军队");
                    return;
                }
                Form2 form2 = new Form2(country, army);
                form2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            int times = Convert.ToInt32(tbxTimes.Text);
            if (dicCountries.ContainsKey(cbx1.Text) == false || dicCountries.ContainsKey(cbx2.Text) == false) {
                MessageBox.Show("请选择国家");
                return;
            }
            Country startCountry = dicCountries[cbx1.Text];
            Country targetCountry = dicCountries[cbx2.Text];
            int startVictory = 0;
            int targetVitory = 0;
            int totalRounds = 0;
            int allDie = 0;
            //开始循环总次数
            for (int i = 0; i < times; i++) {
                //战斗
                StartBattle(startCountry, targetCountry, ref totalRounds, ref startVictory, ref targetVitory, ref allDie); ;
            }
            MessageBox.Show($"在{times}场战斗中平均每场战斗用了{totalRounds / (float)times}回合。\n在这些战斗中，进攻方胜利{startVictory}次，防守方胜利{targetVitory}次，同归于尽{allDie}次。");
        }

        /// <summary>
        /// 进攻方开始改动攻击 直到防守方或者进攻方被全部消灭
        /// </summary>
        /// <param name="startCountry"></param>
        /// <param name="targetCountry"></param>
        private void StartBattle(Country startCountry, Country targetCountry, ref int totalRounds, ref int startVictory, ref int targetVictory, ref int allDie) {
            //即将战斗的单位组
            List<Army> listStartArmy = new List<Army>(4);
            List<Army> listTargetArmy = new List<Army>(4);
            //初始化即将战斗的单位组
            SetBattleArmys(startCountry, cbxAtk1, listStartArmy);
            SetBattleArmys(startCountry, cbxAtk2, listStartArmy);
            SetBattleArmys(startCountry, cbxAtk3, listStartArmy);
            SetBattleArmys(startCountry, cbxAtk4, listStartArmy);
            SetBattleArmys(targetCountry, cbxDfs1, listTargetArmy);
            SetBattleArmys(targetCountry, cbxDfs2, listTargetArmy);
            SetBattleArmys(targetCountry, cbxDfs3, listTargetArmy);
            SetBattleArmys(targetCountry, cbxDfs4, listTargetArmy);
            int rounds = 0;
            //如果所有参与进攻的所有军队的行动力都杀光了，并且敌军还没死光 ,会一直循环下一回合
            while (listTargetArmy.Count != 0 && listStartArmy.Count != 0) {
                rounds++;
                AllArmyFire(listStartArmy, listTargetArmy);
            }
            totalRounds += rounds;
            //如果目标方单位数为0
            if (listTargetArmy.Count == 0 && listStartArmy.Count == 0) {
                allDie++;

            } else if (listTargetArmy.Count == 0) {
                //进攻方胜利次数+1
                startVictory++;

            } else {
                targetVictory++;
            }

        }
        /// <summary>
        ///循环所有参与进攻的单位
        /// </summary>
        /// <param name="listStartArmy"></param>
        /// <param name="listTargetArmy"></param>
        private void AllArmyFire(List<Army> listStartArmy, List<Army> listTargetArmy) {

            for (int i = 0; i < listStartArmy.Count;) {
                ArmyFire(listStartArmy[i], listTargetArmy);
                //如果敌军已经全部被消灭， 则退出循环
                if (listTargetArmy.Count == 0) {
                    if (listStartArmy[i].HP <= 0) {
                        listStartArmy.RemoveAt(i);
                    }
                    break;
                }
                if (listStartArmy[i].HP <= 0) {
                    listStartArmy.RemoveAt(i);
                    continue;
                }
                i++;
            }
        }


        /// <summary>
        /// 会把参与进攻的单位的移动力全部打光
        /// </summary>
        /// <param name="startArmy"></param>
        /// <param name="listTargetArmy"></param>
        private void ArmyFire(Army startArmy, List<Army> listTargetArmy) {

            //如果开启了硬攻
            if (comboBox3.SelectedIndex == 0) {
                //如果被攻击者装甲硬度大于0
                if (listTargetArmy[0].Armor > 0) {
                    //如果对方有装甲度
                    PierceAttack(startArmy, listTargetArmy);
                } else {
                    //如果对方没有装甲度
                    NormalAttack(startArmy, listTargetArmy);
                }
            } else {
                //如果对方没有装甲度
                NormalAttack(startArmy, listTargetArmy);
            }


        }

        /// <summary>
        /// 负责硬攻
        /// </summary>
        /// <param name="startArmy"></param>
        /// <param name="listTargetArmy"></param>
        private void PierceAttack(Army startArmy, List<Army> listTargetArmy) {
            for (int i = 0; i < startArmy.Move;) {
                //如果目标 没有单位了， 或者进攻单位已经阵亡了，退出
                if (listTargetArmy.Count == 0 || startArmy.HP <= 0) {

                    break;
                }
                Army targetArmy = listTargetArmy[0];
                int startDices = GetDicesNum(startArmy.HP, startArmy.MaxHP);
                int targetDices = GetDicesNum(targetArmy.HP, targetArmy.MaxHP);
                int targetReceiveDamage = GetPierceDamage(startDices, startArmy.MinPierce, startArmy.MaxPierce, targetArmy.Armor, true);
                int startReceiveDamage = GetPierceDamage(targetDices, targetArmy.MinPierce, targetArmy.MaxPierce, startArmy.Armor, false);
                startReceiveDamage -= Convert.ToInt32(tbxExtraDfs1.Text) * startDices;
                targetReceiveDamage -= Convert.ToInt32(tbxExtraDfs2.Text) * targetDices;
                //如果开启了闪避
                if (comboBox2.SelectedIndex == 0) {
                    Random r = new Random(Guid.NewGuid().ToString().GetHashCode());

                    if (r.Next(0, 101) <= targetArmy.Miss) {
                        targetReceiveDamage = 0;
                    }
                    if (r.Next(0, 101) <= startArmy.Miss) {
                        startReceiveDamage = 0;
                    }
                }
                startArmy.HP -= startReceiveDamage;
                //如果敌军顶层单位死亡
                if (targetArmy.HP - targetReceiveDamage <= 0) {
                    listTargetArmy.RemoveAt(0);
                    //如果 进攻方有连击
                    if (startArmy.IsTank) {
                        continue;
                    }

                } else {
                    targetArmy.HP -= targetReceiveDamage;
                }
                if (comboBox1.SelectedIndex == 0) {
                    //  组织度下降在伤害结算之后。如果目标未死，看看它有没有溃败
                    if (IsRout(startArmy, targetArmy)) {
                        listTargetArmy.RemoveAt(0);
                    }
                    //  组织度下降在伤害结算之后。如果目标未死，看看它还能否进攻
                    if (IsRout(targetArmy, startArmy)) {
                        break;
                    }
                }

                //如果对方有战壕
                if (checkBox2.Checked && !startArmy.IsTank) {
                    break;
                }

                i++;
            }
        }
        /// <summary>
        /// 是否溃败
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="o">目标组织度</param>
        /// <param name="rout">目溃败阈值</param>
        /// <returns></returns>
        private bool IsRout(Army startArmy, Army targetArmy) {

            int startDices = GetDicesNum(startArmy.HP, startArmy.MaxHP);
            Random r = new Random(Guid.NewGuid().ToString().GetHashCode());
            int targetLostO = 0;
            for (int i = 0; i < startDices; i++) {
                int temp = r.Next(startArmy.MinLostOrganisation, startArmy.MaxLostOrganisation + 1);
                targetLostO += temp;
            }
            targetArmy.Organisation -= targetLostO;
            //如果组织度小于0，溃败
            if (targetArmy.Organisation <= targetArmy.Rout) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 负责软攻
        /// </summary>
        /// <param name="startArmy"></param>
        /// <param name=""></param>
        private void NormalAttack(Army startArmy, List<Army> listTargetArmy) {

            for (int i = 0; i < startArmy.Move;) {
                //如果目标 没有单位了， 或者进攻单位已经阵亡了，退出
                if (listTargetArmy.Count == 0 || startArmy.HP <= 0) {

                    break;
                }
                Army targetArmy = listTargetArmy[0];
                int startDices = GetDicesNum(startArmy.HP, startArmy.MaxHP);
                int targetDices = GetDicesNum(targetArmy.HP, targetArmy.MaxHP);
                int targetReceiveDamage = GetNormalDamage(startDices, startArmy.MinAttack, startArmy.MaxAttack, true);
                int startReceiveDamage = GetNormalDamage(targetDices, targetArmy.MinAttack, targetArmy.MaxAttack, false);
          
                startReceiveDamage -= Convert.ToInt32(tbxExtraDfs1.Text) * startDices;
                targetReceiveDamage -= Convert.ToInt32(tbxExtraDfs2.Text) * targetDices;
                //如果开启了闪避
                if (comboBox2.SelectedIndex == 0) {
                    Random r = new Random(Guid.NewGuid().ToString().GetHashCode());
                   
                    if (r.Next(0, 101) <= targetArmy.Miss) {
                        targetReceiveDamage = 0;
                    }
                    if (r.Next(0, 101) <= startArmy.Miss) {
                        startReceiveDamage = 0;
                    }
                }
                startArmy.HP -= startReceiveDamage;
                //如果敌军顶层单位死亡
                if (targetArmy.HP - targetReceiveDamage <= 0) {
                    listTargetArmy.RemoveAt(0);
                    //如果 进攻方有连击
                    if (startArmy.IsTank) {
                        continue;
                    }

                } else {
                    targetArmy.HP -= targetReceiveDamage;
                }
                if (comboBox1.SelectedIndex == 0) {
                    //  组织度下降在伤害结算之后。如果目标未死，看看它有没有溃败
                    if (IsRout(startArmy, targetArmy)) {
                        listTargetArmy.RemoveAt(0);
                    }
                    //  组织度下降在伤害结算之后。如果目标未死，看看它还能否进攻
                    if (IsRout(targetArmy, startArmy)) {
                        break;
                    }
                }
              
                //如果对方有战壕
                if (checkBox2.Checked && !startArmy.IsTank) {
                    break;
                }
                i++;
            }
        }
        /// <summary>
        /// 得到参与进攻单位本次行动力打出的伤害
        /// </summary>
        /// <param name="Dices"></param>
        /// <param name="minAttack"></param>
        /// <param name="maxAttack"></param>
        /// <returns></returns>
        private int GetNormalDamage(int Dices, int minAttack, int maxAttack, bool isStart) {
            Random r = new Random(Guid.NewGuid().ToString().GetHashCode());
            int damage = 0;
            for (int i = 0; i < Dices; i++) {
                int extra = GetExtraAttack(isStart);
                damage += r.Next(minAttack + extra, maxAttack + 1 + extra);
            }
            return damage;
        }
        private int GetExtraAttack(bool isStart) {
            int extra = 0;
            if (isStart) {
                extra = Convert.ToInt32(tbxExtraAtk1.Text);
            } else {
                extra = Convert.ToInt32(tbxExtraAtk2.Text);
            }
            return extra;
        }
        /// <summary>
        /// 得到参与进攻单位本次行动力打出的伤害
        /// </summary>
        /// <param name="Dices"></param>
        /// <param name="minPierce"></param>
        /// <param name="maxPierce"></param>
        /// <param name="armor">敌军的装甲值</param>
        /// <returns></returns>
        private int GetPierceDamage(int Dices, int minPierce, int maxPierce, int armor, bool isStrat) {
            Random r = new Random(Guid.NewGuid().ToString().GetHashCode());
            int damage = 0;
            for (int i = 0; i < Dices; i++) {
                int extra = GetExtraAttack(isStrat);
                int tempDamage = r.Next(minPierce + extra, maxPierce + 1 + extra);
                //如果没有破甲，伤害等于1
                if (tempDamage <= armor) {
                    tempDamage = 1;
                }
                damage += tempDamage;
            }
            return damage;
        }
        private int GetDicesNum(int currentHP, int maxHp) {
            int ratio = currentHP * 100 / maxHp;
            if (ratio <= 5) {
                return 1;
            } else if (ratio < 15) {
                return 2;
            } else if (ratio < 25) {
                return 3;
            } else if (ratio < 50) {
                return 4;
            } else {
                return 5;
            }
        }
        /// <summary>
        /// 设置等同参与战斗的独立的army对象 到listArmy中 设置后listArmy的军队数等同于参与战斗的实际数
        /// </summary>
        /// <param name="country"></param>
        /// <param name="sender"></param>
        /// <param name="listArmy"></param>
        private void SetBattleArmys(Country country, Object sender, List<Army> listArmy) {
            ComboBox comboBox = sender as ComboBox;
            Army army = null;
            if (comboBox != null) {
                for (int i = 0; i < country.ListArmys.Count; i++) {
                    if (country.ListArmys[i].Name == comboBox.Text) {
                        army = new Army(country.ListArmys[i]);

                    }
                }
            }
            if (army == null) {
                return;
            }
            listArmy.Add(army);
        }

        private void btnFixAtk2_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx1.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx1.Text];
            OpenForm2(cbxAtk2, country);
        }

        private void btnFixAtk3_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx1.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx1.Text];
            OpenForm2(cbxAtk3, country);
        }

        private void btnFixAtk4_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx1.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx1.Text];
            OpenForm2(cbxAtk4, country);
        }

        private void btnFixDfs1_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx2.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx2.Text];
            OpenForm2(cbxDfs1, country);
        }

        private void btnFixDfs2_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx2.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx2.Text];
            OpenForm2(cbxDfs2, country);
        }

        private void btnFixDfs3_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx2.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx2.Text];
            OpenForm2(cbxDfs3, country);
        }

        private void btnFixDfs4_Click(object sender, EventArgs e) {
            if (!dicCountries.ContainsKey(cbx2.Text)) {
                MessageBox.Show("请选择一个国家");
                return;
            }
            Country country = dicCountries[cbx2.Text];
            OpenForm2(cbxDfs4, country);
        }

        private void button4_Click(object sender, EventArgs e) {
            cbxAtk1.Text = "";
            cbxAtk2.Text = "";
            cbxAtk3.Text = "";
            cbxAtk4.Text = "";

        }

        private void button5_Click(object sender, EventArgs e) {
            cbxDfs1.Text = "";
            cbxDfs2.Text = "";
            cbxDfs3.Text = "";
            cbxDfs4.Text = "";
        }

        private void button6_Click(object sender, EventArgs e) {
            MessageBox.Show("本程序作者：李德邻\n时间：2021年11月17日\n世2中的战斗说明：首先世2中每个单位五个小单位。在战斗是时" +
                "每个小单位独自投出骰子。骰子的最小值和最大值如卡牌界面所示。当单位血量小于等于5%时，小单位仅有1.否则，小于15%时，小单位仅有" +
                "2.否则，小于35%时，小单位仅有3.否则，小于50%时，小单位仅有4.\n伤害减伤算法：该单位小单位数量*指定防御值。\n伤害加成算法：小单位的攻击上下限增加指定的攻击值。\n算法战斗顺序：从上到下以此打光行动力，不会切换单位轮替进攻。\n组织度：当本单位的组织度达到阈值以下，则溃败或无法进攻（都相当于阵亡）。");
        }
    }
}
