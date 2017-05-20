using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class TurmaMap : EntityTypeConfiguration<Turma>
    {

        public TurmaMap()
        {
            ToTable("Turma");

            HasKey( x => x.TurmaId);

            HasRequired(x => x.Curso)
                .WithMany()
                .Map(m => m.MapKey("CursoId"));//Chave estrangeira em Turmas

            HasRequired(x => x.Professor)
                .WithMany(x => x.TurmaLista)
                .Map(m => m.MapKey("ProfessorId"));
        }

    }
}
