using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class CursoMap : EntityTypeConfiguration<Curso>
    {

        public CursoMap()
        {
            ToTable("Curso");

            HasKey( x => x.CursoId );

            Property(x => x.Nome).IsRequired();


            //Exemplo de N:N
            HasMany(x => x.ProfessorLista)
                .WithMany(x => x.CursoLista)
                .Map(m =>
               {
                   m.MapLeftKey("CursoId");
                   m.MapRightKey("ProfessorId");
                   m.ToTable("CursoProfessor");

               });

        }

    }
}
