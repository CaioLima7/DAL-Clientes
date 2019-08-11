using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Extensoes;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            bool ValidarCPF = Validacoes.ValidaCpf(model.CPF);

            if (ValidarCPF)
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        CPF = model.CPF,
                        Telefone = model.Telefone
                    });
                    return Json("Cadastro efetuado com sucesso");
                }
            }
            else
            {
                return Json("CPF Inválido!");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });
                               
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListarBeneficiarios(string id)
        {
            List<ClienteBeneficiario> clientes = new BoCliente().ListarBeneficiarios();
            clientes.RemoveAll(Id => Id == null);
            clientes.RemoveAll(IdCliente => IdCliente == null);
            List<Row> teste = new List<Row>();


            for (int i = 0; i < clientes.Count(); i++)
            {
                teste.Add(new Row()
                {
                    CPF = clientes[i].CPF,
                    Nome = clientes[i].Nome,
                });
            }
            RootObject Escopo = new RootObject()
            { rowCount = clientes.Count(), rows = teste, current = clientes.Count(), total = clientes.Count()};
            return Json(Escopo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Exclusão na tabela direto, não é exclusão lógica
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoverBeneficiario(string CPF)
        {
            var teste = new BoCliente();

            var id = teste.BuscarId(CPF);
            teste.ExcluirBeneficiario(id);
            return Json("ok");
        }

        public JsonResult IncluirBeneficiario(ClienteBeneficiarioModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                var buscarId = bo.BuscarId(model.CPF);
                return Json("CPF já existente");
            }
            catch (Exception ex)
            {

            }


            //if (buscarId > 1)
            //{
            //    return Json("CPF já existente");
            //}

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bool ValidarCPF = Validacoes.ValidaCpf(model.CPF);

                if (ValidarCPF)
                {
                    var IdCliente = bo.ConsultarCPF(model.CPF).ToString();

                    if (IdCliente != "CPF Não encontrado")
                    {
                        model.Id = bo.IncluirBeneficiario(new ClienteBeneficiario()
                        {
                            Nome = model.Nome,
                            CPF = model.CPF,
                            IdCliente = IdCliente
                        });

                        return Json("Cadastro efetuado com sucesso");
                    }
                    else
                    {
                        return Json(IdCliente);
                    }
                }
                else
                {
                    return Json("CPF Inválido!");
                }
            }
        }
        
    }
}