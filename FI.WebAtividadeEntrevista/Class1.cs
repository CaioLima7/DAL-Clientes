using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebAtividadeEntrevista
{
    public class Class1
    {
        public static string body()
        {
            Cliente cliente = new Cliente();
            JavaScriptSerializer json = new JavaScriptSerializer();
            string ser = json.Serialize(cliente);
            return "Ok";
        }
    }
}