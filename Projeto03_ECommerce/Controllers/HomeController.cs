using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Projeto03_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto03_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Logout();
            return View();
        }

        //PARA GESTÃO DE USUÁRIOS

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(NovoUsuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var usuarioStore = new UserStore<IdentityUser>();
                var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

                //CRIAR A IDENTIDADE DO USUÁRIO
                var usuarioInfo = new IdentityUser()
                {
                    UserName = usuario.Nome,
                };

                //CRIAR O USUARIO NO BANCO DE DADOS
                IdentityResult resultado = usuarioManager
                    .Create(usuarioInfo, usuario.Senha);

                //SE USUARIO CRIADO ENTÃO AUTENTICA

                if (resultado.Succeeded)
                {
                    //AUTENTICA E VOLTA PARA A PÁGINA INICIAL
                    var autManager = System.Web
                        .HttpContext
                        .Current
                        .GetOwinContext().Authentication;

                    var identidadeUsuario = usuarioManager
                        .CreateIdentity(
                        usuarioInfo, 
                        DefaultAuthenticationTypes
                        .ApplicationCookie);

                    autManager.SignIn(
                        new AuthenticationProperties(){IsPersistent = true,},
                        identidadeUsuario);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MensagemErro = resultado.Errors.FirstOrDefault();
                    return View("Erro");
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUsuario usuario, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var usuarioStore = new UserStore<IdentityUser>();
                var usuarioManager = new UserManager<IdentityUser>(usuarioStore);
                                
                var usuarioInfo = usuarioManager.Find(usuario.Nome, usuario.Senha);                

                if (usuarioInfo != null)
                {
                    //AUTENTICA E VOLTA PARA A PÁGINA INICIAL
                    var autManager = System.Web
                        .HttpContext
                        .Current
                        .GetOwinContext().Authentication;

                    var identidadeUsuario = usuarioManager
                        .CreateIdentity(
                        usuarioInfo,
                        DefaultAuthenticationTypes
                        .ApplicationCookie);

                    autManager.SignIn(
                        new AuthenticationProperties() { IsPersistent = true, },
                        identidadeUsuario);

                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MensagemErro = "Usuário ou senha inválidos!";
                    return View("Erro");
                }
            }
        }

        public ActionResult Logout()
        {
            var autManager = System.Web
                .HttpContext
                .Current
                .GetOwinContext().Authentication;
            
            autManager.SignOut();

            return RedirectToAction("Index");
        }
    }
}