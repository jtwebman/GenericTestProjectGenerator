using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public abstract class ValidationStep
    {
        public abstract string FailedStepText { get; }
        public abstract bool IsValid();
    }
}
