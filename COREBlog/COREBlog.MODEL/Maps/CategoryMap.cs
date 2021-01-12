using COREBlog.CORE.Map;
using COREBlog.MODEL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COREBlog.MODEL.Maps
{
    class CategoryMap: CoreMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(50).IsRequired(true);
            
            // silmiyoruz...
            base.Configure(builder);
        }
    }
}
