using System;
using System.Collections.Generic;
using System.Text;

namespace DoToo.Models {
    class TodoItem {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime Due { get; set; }
    }
}
