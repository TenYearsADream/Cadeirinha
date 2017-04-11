using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCadeirinhaIoT.Parameters
{
    public class ParametersTest
    {
        public string[] TestParameters { get; private set; }

        public ParametersTest()
        {
            TestParameters = new string[11];
            TestParameters[0] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[1] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[2] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[3] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[4] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[5] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[6] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[7] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[8] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[9] = "000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
            TestParameters[10] ="000000;VHLD30;VHLD200;VHLD400;VHLD800;XHLE100;XHLE500;XHLE900;XHLE1400;XHLE1650";
        }

        public string GetRandom()
        {
            int rnd = new Random().Next(0, 10);
            return TestParameters[rnd];
        }
    }
}
