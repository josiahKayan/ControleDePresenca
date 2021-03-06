﻿using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{

    //: RepositoryBase<Tag>, ITagRepository
    public class ListaPresencaRepository : RepositoryBase<ListaPresenca>
    {

        public List<ListaPresenca> GetListaPresencaByIdTurma(int id)
        {
            return context.Set<ListaPresenca>().Include("Turma").Where( t => t.Turma.TurmaId == id    ).OrderByDescending(a => a.Ano).ThenByDescending( o => o.Mes  ).ThenByDescending( p => p.Dia ).ToList();
        }


        //BuscaListaPrensecaPorData

        public ListaPresenca BuscaListaPrensecaPorData(DateTime tempo)
        {
            return context.Set<ListaPresenca>().Include("Turma").Where(lp => lp.Ativo == true && lp.Dia == tempo.Day && lp.Mes == tempo.Month && lp.Ano == tempo.Year  && lp.HoraEntrada <= tempo  ).FirstOrDefault();
        }


    }
}
