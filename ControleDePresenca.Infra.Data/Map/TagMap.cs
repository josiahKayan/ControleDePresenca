using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {

        public TagMap()
        {

            ToTable("Tag");

            HasKey(x => x.TagId);

            

        }

    }
}
