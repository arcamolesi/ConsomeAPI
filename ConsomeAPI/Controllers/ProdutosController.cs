using ConsomeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsomeAPI.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Produto> produtos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7074/api/Produtos");

                //HTTP GET
                var responseTask = client.GetAsync("produtos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Produto>>();
                    readTask.Wait();
                    produtos = readTask.Result;
                }
                else
                {
                    produtos = Enumerable.Empty<Produto>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(produtos);
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,descricao,quantidade,valor")] Produto produto)
        {

            if (produto != null)
            {

                using (var client = new HttpClient())
                {
                   // client.BaseAddress = new Uri("https://localhost:7074/api/Produtos");
                    //HTTP POST
                    string url = "https://localhost:7074/api/Produtos";
                    produto.id = 0; 
                    var dataAsString = JsonConvert.SerializeObject(produto);
                    var content = new StringContent(dataAsString);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                    var postTask = client.PostAsync(url, content) ;
                                       
                    postTask.Wait();
                    
                    
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                
            }
            return View(produto);

        }
    }
}
