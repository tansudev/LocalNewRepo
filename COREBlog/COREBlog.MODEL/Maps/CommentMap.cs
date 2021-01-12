using COREBlog.CORE.Map;
using COREBlog.MODEL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace COREBlog.MODEL.Maps
{
    class CommentMap: CoreMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.Property(x => x.CommenText).HasMaxLength(255).IsRequired(true);

            base.Configure(builder);
        }
    }
}
