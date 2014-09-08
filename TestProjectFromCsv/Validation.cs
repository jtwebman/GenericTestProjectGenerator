using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectFromCsv
{
    public class Validation
    {
        private IList<ValidationStep> _steps;
        private IList<string> _issues = new List<string>();

        public IList<string> Issues
        {
            get
            {
                return _issues;
            }
        }

        public Validation( IList<ValidationStep> steps)
        {
            _steps = steps;
        }

        public bool IsValid()
        {
            foreach (ValidationStep step in _steps)
            {
                if (!step.IsValid())
                {
                    _issues.Add(step.FailedStepText);
                }
            }
            return _issues.Count == 0; ;
        }
    }
}
