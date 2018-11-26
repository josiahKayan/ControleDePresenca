using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class PresencaRepository : RepositoryBase<ListaPresenca> , IPresencaRepository
    {


        public List<Presenca> GetResumoListaPresencaByIdPresencalista(int id, int aluno)
        {
            if (aluno > 0) {
                List<Presenca> l = new List<Presenca>();
                l =  context.Set<Presenca>().Include("Aluno").Where( p => p.PresencaId == id ).ToList()   ;

                if(l.Count == 0 )
                {

                    Presenca p = new Presenca();
                    p.Aluno = new Aluno() { NomeCompleto = "" };
                    l.Add(p);

                }

                return l;
            }
            else
            {
                return context.Set<Presenca>().Include("Aluno").Where(p => p.PresencaId == id).ToList();

            }
        }

        public IEnumerable<ListaPresenca> BuscaPorMesEAno(int mes, int ano)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaPresenca> GetListaPresenca(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertPresenca(int idPresenca, int idTurma, int idUser)
        {
            var arrayWeek = new string[] { "domingo", "segunda-feira", "terça-feira", "quarta-feira", "quinta-feira", "sexta-feira", "sábado" };
            var arrayMonth = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

            var dia = (int) DateTime.Now.DayOfWeek;

            string nomeDia = arrayWeek[     dia  ];
            string nomeMes = arrayMonth[   DateTime.Now.Month -1   ];

            Presenca p = new Presenca()
            {
                AlunoId = idUser ,
                HoraChegada = DateTime.Now,
                NomeDia = nomeDia,
                NomeMes = nomeMes,
                ListaPresencaId = idPresenca
                
            };

            

            context.Presenca.Add(p);
            context.SaveChanges();

            //var professor = context.Professor.Find(turma.ProfessorId);
            //var curso = context.Cursos.Find(turma.CursoId);

            //turma.Professor = professor;
            //turma.Curso = curso;

            //context.Professor.Attach(turma.Professor);
            //context.Cursos.Attach(turma.Curso);

            //context.Turmas.Add(turma);

            //context.SaveChanges();

        }

            //using (var context = new ControlePresencaContext())
            //{
            //    aluno.Tag.Status = 1;
            //    context.Tags.Attach(aluno.Tag);
            //    context.Aluno.Add(aluno);
            //    //context.Usuarios.Attach(aluno.Usuario);
            //    //context.Aluno.Add(aluno);
            //    context.SaveChanges();
            //}


        //public IEnumerable<Presenca> GetListaPresenca(int id)
        //{
        //    //return context.Set<Presenca>().Include("Alunos").Where(x => x. == id).ToList();

        //}


    }
}
