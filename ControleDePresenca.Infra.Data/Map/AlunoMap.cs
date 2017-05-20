using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class AlunoMap : EntityTypeConfiguration<Aluno>
    {

        public AlunoMap()
        {
            ToTable("Aluno");
            HasKey( x => x.AlunoId );

            HasRequired(x => x.Turma)
                .WithMany(x => x.AlunoLista)
                .Map(m => m.MapKey("TurmaId"));

            


        }

    }
}
