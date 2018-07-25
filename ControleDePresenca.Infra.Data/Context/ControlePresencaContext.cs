using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Infra.Data.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Context
{
    public class ControlePresencaContext : DbContext
    {
        public ControlePresencaContext():base("ControlePresencaContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        public void SetLazyLoading(bool check)
        {
            if (check)
            {
                Configuration.LazyLoadingEnabled = true;
                Configuration.ProxyCreationEnabled = true;
            }
            else
            {
                Configuration.LazyLoadingEnabled = false;
                Configuration.ProxyCreationEnabled = false;
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new AlunoMap());
            modelBuilder.Configurations.Add(new CursoMap());
            modelBuilder.Configurations.Add(new PresencaMap());
            modelBuilder.Configurations.Add(new ProfessorMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new TurmaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());


            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure( p => p.HasColumnType("varchar") );



        }

        public DbSet<Presenca> Presencas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Professor> Professor { get; set; }

        public DbSet<Aluno> Aluno { get; set; }

    }
}
