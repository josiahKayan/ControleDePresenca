﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class PresencaDia
    {

        public int PresencaId { get; set; }
        public int AlunoId { get; set; }
        public string Dia { get; set; }
        public int Presente { get; set; }
        public string HoraChegada { get; set; }



    }
}
