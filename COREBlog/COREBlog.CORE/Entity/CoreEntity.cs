using System;
using System.Collections.Generic;
using System.Text;

namespace COREBlog.CORE.Entity
{
    public class CoreEntity : IEntity<Guid>
    {

        public CoreEntity()
        {}

        public Guid ID { get ; set; }
        public Status Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public string  CreateComputerName { get; set; }
        public string  CreateIP { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
    }
}
