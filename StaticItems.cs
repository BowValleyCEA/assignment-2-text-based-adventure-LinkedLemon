using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    public class StaticItems
    {
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Smell { get; set; }
        public string Taste { get; set; }
        public string _Type
        {            
            set
            {
                switch (value)
                {
                    case "Static":
                        objectType = Types.Static;
                        break;
                    case "Pickup":
                        objectType = Types.Pickup;
                        break;
                    case "Toggle":
                        objectType = Types.Toggle;
                        break;
                    case "Interaction":
                        objectType = Types.Interaction;
                        break;
                    default:
                        break;
                }
            }
        }
        private string id { get; set; }

        public Types objectType;
            
        public enum Types {
            Static,
            Pickup,
            Toggle,
            Interaction
        }
    }
}
