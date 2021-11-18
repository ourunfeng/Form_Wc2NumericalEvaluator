using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form单位对攻测试 {
   public  class Country {
        public string Name { get; set; }
        public List<Army> ListArmys { get; set; } = new List<Army>();
    }
}
