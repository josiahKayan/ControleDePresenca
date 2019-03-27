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
                l =  context.Set<Presenca>().Include("Aluno").Where( p => p.ListaPresencaId == id && p.AlunoId == aluno ).ToList()   ;

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
                return context.Set<Presenca>().Include("Aluno").Where(p => p.ListaPresencaId == id).ToList();

            }
        }


       

        public int GetTotalPresenca( int idTurma )
        {
            List<ListaPresenca> l = new List<ListaPresenca>();

            l = context.Set<ListaPresenca>().Include("Presenca").Where(p => p.TurmaId == idTurma  ).ToList();

            int totalDiass = 0;

            totalDiass = l.Count;

            return totalDiass;
            
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

        public List<FrequenciaAlunos> GetFrequenciaAlunos(IEnumerable<Aluno> lAlunos, int idTurma, int totalDias)
        {
            var fa = new List<FrequenciaAlunos>();
            FrequenciaAlunos freq = null;

            var p = new List<Presenca>();
            p = context.Set<Presenca>().Where(t => t.ListaPresenca.TurmaId == idTurma).ToList();

            var lp = new List<ListaPresenca>();
            lp = context.Set<ListaPresenca>().Where(t => t.TurmaId == idTurma).ToList();

            foreach (var aluno in lAlunos)
            {
                freq = new FrequenciaAlunos();
                freq.IdAluno = aluno.AlunoId;
                freq.Nome = aluno.Nome;
                freq.NomeCompleto = aluno.NomeCompleto;
                freq.PresencasTotal = p.Count( x => x.AlunoId == aluno.AlunoId  )  ;
                freq.FaltasTotal = totalDias - freq.PresencasTotal;

                //List<ListaPresenca> g = lp.Select(x => x.Presenca.Where(z => z.AlunoId == aluno.AlunoId)).ToList();

                freq.ListaPresencaDia = new List<PresencaDia>();

                for (int i = 0; i < lp.Count; i++)
                {

                    PresencaDia presencaDia = new PresencaDia();

                    try
                    {
                        presencaDia.AlunoId = aluno.AlunoId;


                        presencaDia.Presente =  lp[i].Presenca.Select(x => x).Any(x => x.AlunoId == aluno.AlunoId) == true ? 1: 0;

                        Presenca presenca = lp[i].Presenca.Select(x => x).Where(x => x.AlunoId == aluno.AlunoId).FirstOrDefault();

                        presencaDia.PresencaId = i;

                        presencaDia.Dia = "" + presenca.ListaPresenca.Dia.ToString() + "-" + presenca.ListaPresenca.Mes + "-" + presenca.ListaPresenca.Ano;


                    }
                    catch (Exception e)
                    {
                        presencaDia.Dia = "";
                    }

                  
                    freq.ListaPresencaDia.Add(presencaDia);

                }



                //freq.ListaData.Add();

                //freq.ListaData = 

                fa.Add(freq);

                //var playersPerTeam =
                //    from player in players
                //    group player by player.Team into playerGroup
                //    select new
                //    {
                //        Team = playerGroup.Key,
                //        Count = playerGroup.Count(),
                //    };

                //var t = l.Select(x => x.Presenca.Select(a => a.AlunoId == aluno.AlunoId)).ToList();

            }

            return fa;
        }


        public List<string> GetListaDatas(IEnumerable<Aluno> lAlunos, int idTurma, int totalDias)
        {
            var listaDatas = new List<string>();

            var lp = new List<ListaPresenca>();
            lp = context.Set<ListaPresenca>().Where(t => t.TurmaId == idTurma).ToList();

            foreach (var item in lp)
            {
                listaDatas.Add(item.Dia .ToString() + "-" + item.Mes + "-" + item.Ano);
            }

            return listaDatas;
        }


    }
}
