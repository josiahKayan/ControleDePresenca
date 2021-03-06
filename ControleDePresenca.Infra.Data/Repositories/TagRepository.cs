﻿using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {

        public List<Tag> BuscaTag(int id)
        {
            return context.Set<Tag>().Where(i => i.TagId == id).ToList();
        }


        public Tag SearchTagByCode(string code)
        {
            return context.Tags.Where(c => c.Code == code).FirstOrDefault();
            //return context.Set<TEntity>().Find(id);

        }

        public List<Tag> ListaTagNaoArmazenada()
        {
            return context.Set<Tag>().Where(st => st.Status != 1).ToList();
        }


        public void EditarStatusTag(Tag tag)
        {
            context.Entry(tag).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Tag> BuscaTag(string code)
        {
            throw new NotImplementedException();
        }
    }
}


