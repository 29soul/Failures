using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falures
{
    public enum FailureType
    {
        UnexpectedShutdown,
        ShortNonResponding,
        HardwareFailure,
        ConnectionProblem
    }
}
