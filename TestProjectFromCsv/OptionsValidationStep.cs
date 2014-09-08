using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public abstract class OptionsValidationStep : ValidationStep
    {
        private Options _options;

        public OptionsValidationStep(Options options)
        {
            _options = options;
        }

        public Options Options
        {
            get
            {
                return _options;
            }
        }
    }
}
