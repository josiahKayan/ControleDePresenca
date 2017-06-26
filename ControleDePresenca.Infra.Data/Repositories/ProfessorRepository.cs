﻿using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class ProfessorRepository : RepositoryBase<Professor>, IProfessorRepository
    {

        public IEnumerable<Professor> GetAllProfessors()
        {
            return context.Set<Professor>().Include("Usuario").ToList();
        }

        public Professor GetProfessorByIdIncludes(int id)
        {
            return context.Set<Professor>().Include("Usuario").ToList().Find(x => x.ProfessorId == id);
        }

        public void RemoveComUsuario(Professor obj)
        {

            context.Usuarios.Remove(obj.Usuario);
            context.Set<Professor>().Remove(obj);
            context.SaveChanges();
        }

    }
}
