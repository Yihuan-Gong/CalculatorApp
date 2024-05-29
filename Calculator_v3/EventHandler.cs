using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class EventHandler
    {
        CentralDataUnit centralDataUnit;

        public EventHandler()
        {
            centralDataUnit = new CentralDataUnit();
        }

        public void AssignButton(string buttonName, ButtonType buttonType)
        {
            centralDataUnit.SetData(buttonName, buttonType);
        }
    }
}
