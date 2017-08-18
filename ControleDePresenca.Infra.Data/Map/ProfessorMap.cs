using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class ProfessorMap : EntityTypeConfiguration<Professor>
    {

        public ProfessorMap()
        {
            //ToTable("Professor");

            //HasKey(x => x.ProfessorId);

            //Exemplo de 1:1
            //HasRequired(x => x.Usuario)
            // .WithRequiredPrincipal();
        }

    }
}
