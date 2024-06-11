using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falures
{
    public class Failure
    {
        public FailureType Type { get; set; }
        public DateTime Time { get; set; }
        public int DeviceID { get; set; }

        public bool IsSerious()
        {
            return Type == FailureType.UnexpectedShutdown || Type == FailureType.HardwareFailure;
        }
    }
}
