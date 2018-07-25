using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {

        public UsuarioMap()
        {
            //ToTable("Usuario");

            //HasKey( x => x.UsuarioId );

            //Property(x => x.Email).IsRequired();

            //Property(x => x.Senha).IsRequired();

        }

    }
}
