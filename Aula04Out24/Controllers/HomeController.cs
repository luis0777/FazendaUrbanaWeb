using Aula04Out24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace Aula04Out24.Controllers
{
    public class HomeController : Controller
    {

        private FazendaUrbanaWebEntities2 db = new FazendaUrbanaWebEntities2();
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Gerenciamento()
        {
            var clientes = db.Cliente.ToList(); // Carregar todos os clientes
            return View(clientes); // Passar a lista de clientes para a view
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Adm adm)
        {
            // Verifica se o modelo é válido
            if (ModelState.IsValid)
            {
                using (var db = new FazendaUrbanaWebEntities2())
                {
                    var admin = db.Adm.SingleOrDefault(a => a.Usuario == adm.Usuario && a.Senha == adm.Senha);
                    if (admin != null)
                    {
                        TempData["ToastrMessage"] = "Login efetuado com sucesso!";
                        TempData["ToastrType"] = "success";
                        return RedirectToAction("Gerenciamento");
                    }
                    else
                    {
                        TempData["ToastrMessage"] = "Usuário ou senha incorretos.";
                        TempData["ToastrType"] = "error";
                    }
                }
            }
            else
            {
                // Se o modelo não for válido, as mensagens de erro serão exibidas
                TempData["ToastrMessage"] = "Por favor, preencha todos os campos obrigatórios.";
                TempData["ToastrType"] = "error";
            }

            // Retorna a View com o modelo de volta para a tela, incluindo mensagens de erro
            return View(adm);
        }

        public ActionResult QuemSomos()
        {
            return View();
        }
        public ActionResult Toastr()
        {
            return View();
        }
        public ActionResult TenhoInteresse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TenhoInteresse(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente); // Adiciona o cliente ao banco de dados
                db.SaveChanges(); // Salva as alterações no banco
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success"; // Tipo da mensagem: success, info, warning, error


                return RedirectToAction("TenhoInteresse");
            }
            else
            {
                TempData["ToastrMessage"] = "Por favor, preencha todos os campos corretamente.";
                TempData["ToastrType"] = "error"; // Define o tipo como "error"
            }
            return View(cliente);

            //return View(cliente); // Retorna o formulário com mensagens de erro, se houver
        }

    }
}