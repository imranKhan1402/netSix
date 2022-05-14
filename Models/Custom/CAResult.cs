using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Custom
{
public class CAResult
    {
        public bool SUCCESS { get; set; }
        public string MESSAGES { get; set; }
        public int ROWS { get; set; }
        public CAResult()
        {
            SUCCESS = true;
            MESSAGES = CAMessage.SUCCESS_MESSAGES;
            ROWS = 0;
        }
    }
}
