﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de cliente benefiário que representa o registro na tabela Cliente do Banco de Dados
    /// </summary>
    public class ClienteBeneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Id fk Cliente
        /// </summary>
        public string IdCliente { get; set; }
    }    
}
