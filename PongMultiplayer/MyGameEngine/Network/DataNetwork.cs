using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    [System.Serializable]
    public class DataNetwork
    {
        public int id;
        public int senderID;
        public object obj;

        public DataNetwork(int networkID, int senderID, object target)
        {
            this.id = networkID;
            this.senderID = senderID;
            this.obj = target;
        }
    }
}
