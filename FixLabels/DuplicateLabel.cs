using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLabels
{
    class DuplicateLabel
    {
        public string labelId { get; set; }
        public string label { get; set; }
        public List<string> labelList { get; } = new List<string>();

        public DuplicateLabel(string _labelId)
        {
            this.labelId = _labelId;
        }

        public void addLabel(string _label)
        {
            labelList.Add(_label);
        }
    }
}
