using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Map
{
    public class PresencaMap : EntityTypeConfiguration<Presenca>
    {

        public PresencaMap()
        {

            //ToTable("Presenca");

            //HasKey(x => x.PresencaId);

            ////Exemplo de N:N
            //HasMany(x => x.TurmaLista)
            //    .WithMany(x => x.PresencaLista)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("PresencaId");
            //        m.MapRightKey("TurmaId");
            //        m.ToTable("PresencaTurma");

            //    });
        }

    }
}
