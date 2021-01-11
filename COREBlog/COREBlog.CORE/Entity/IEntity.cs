using System;
using System.Collections.Generic;
using System.Text;

namespace COREBlog.CORE.Entity
{
    public interface IEntity<T>    {
        T ID { get; set; }        
    }
}

