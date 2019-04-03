using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class PublicacaoRepository : RepositoryBase<MensagemRapida>, IPublicacaoRepository
    {
        public List<MensagemRapida> GetAllIncludes(int idUser)
        {
            return context.Set<MensagemRapida>().Where(t => t.Responsavel == idUser).OrderByDescending(a => a.Ano).ThenByDescending(o => o.Mes).ThenByDescending(p => p.Dia).ThenByDescending(h => h.HoraPublicacao.Hour).ThenByDescending(m => m.HoraPublicacao.Minute).ThenByDescending(s => s.HoraPublicacao.Second).ToList();
        }

        public List<MensagemRapida> GetAllIncludesPaginacaoProfessor(int idUser, int quantidadeRegistros, int pagina)
        {
            var l = context.Set<MensagemRapida>().Where(t => t.Responsavel == idUser).OrderByDescending(a => a.Ano).ThenByDescending(o => o.Mes).ThenByDescending(p => p.Dia).ThenByDescending(h => h.HoraPublicacao.Hour).ThenByDescending(m => m.HoraPublicacao.Minute).ThenByDescending(s => s.HoraPublicacao.Second).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros).ToList();
            return l;
        }

        public List<MensagemRapida> GetAllIncludesAlunos(int idUser)
        {
            var l = context.Set<MensagemRapida>().Where(t => t.PessoaDestino == idUser).OrderByDescending(a => a.Ano).ThenByDescending(o => o.Mes).ThenByDescending(p => p.Dia).ThenByDescending( h => h.HoraPublicacao.Hour).ThenByDescending(m => m.HoraPublicacao.Minute).ThenByDescending(s => s.HoraPublicacao.Second).ToList();
            return l;
        }

        public List<MensagemRapida> GetAllIncludesPaginacaoAlunos(int idUser, int quantidadeRegistros, int pagina)
        {
            //Paginação
            var l = context.Set<MensagemRapida>().Where(t => t.PessoaDestino == idUser).OrderByDescending(a => a.Ano).ThenByDescending(o => o.Mes).ThenByDescending(p => p.Dia).ThenByDescending(h => h.HoraPublicacao.Hour).ThenByDescending(m => m.HoraPublicacao.Minute).ThenByDescending(s => s.HoraPublicacao.Second).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros).ToList();
            return l;
        }

        public void AddPublicacao(MensagemRapida p, int idTurma)
        {

            List<Usuario> u = null;

            using (var context = new ControlePresencaContext())
            {

                var l = context.Aluno.Where(x => x.Turma.Select(t => t.TurmaId == idTurma).FirstOrDefault()).ToList();

                var g = l.Select(x => x.UsuarioId).ToList();

                u = context.Usuario.Where(r => g.Contains(r.UsuarioId)).ToList();

            }

            using (var context = new ControlePresencaContext())
            {

                var pub = new List<MensagemRapida>();

                foreach (var item in u)
                {
                    p.PessoaDestino = item.UsuarioId;

                    context.MensagemRapida.Add(p);

                    context.SaveChanges();

                }
            }



            
        }


    }
}
