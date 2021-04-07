using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.UWP {
    class Bootstrapper : DoToo.Bootstrapper {
        public static void Init() {
            var instance = new Bootstrapper();
        }
    }
}
