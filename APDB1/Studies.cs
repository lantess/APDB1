using System;
using System.Collections.Generic;
using System.Text;

namespace APDB1
{
    [Serializable]
    public class Studies
    {
        public String name,
                    mode;
        public Studies(String name, String mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public Studies()
        {

        }
    }
}
