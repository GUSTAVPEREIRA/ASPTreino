using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        //Customizar rotas
        [Route("produtos", Name = "ListaProdutos")]
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            ViewBag.Produto = produtos;
            return View(produtos);
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {

            //Isto é apenas para erros
            int idDaInformatica = 1;
            if (produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("Produto.Invalido", "Informatica com o preço abaixo de 100 R$");                
            }

            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index", "Produto");
            }

            CategoriasDAO categoriasDAO = new CategoriasDAO();
            ViewBag.Categorias = categoriasDAO.Lista();
            ViewBag.Produto = produto;
            return View("Form");

        }

        [Route("produtos/Form")]
        public ActionResult Form()
        {

            CategoriasDAO categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
            Produto produto = new Produto();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = produto;

            return View("Form");
        }

        [Route("produtos/{id}", Name = "VisualizaProduto")]
        public ActionResult Visualiza(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();
        }

        [Route("produtos/decrementa", Name = "decrementa")]
        public ActionResult DescrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            return Json(produto);
        }

    }
}