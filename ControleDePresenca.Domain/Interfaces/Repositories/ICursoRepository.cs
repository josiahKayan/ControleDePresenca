using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface ICursoRepository : IRepositoryBase<Curso>
    {

        //IEnumerable<Curso> BuscaTag(string code);

        Curso GetCursoByIdIncludesTurma(int id);

    }
}
