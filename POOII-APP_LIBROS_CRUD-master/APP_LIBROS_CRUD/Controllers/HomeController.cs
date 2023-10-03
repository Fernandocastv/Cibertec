using APP_LIBROS_CRUD.Models;
using APP_LIBROS_CRUD.Repositorios.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APP_LIBROS_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGenericRepository<Autor> _autorRepository;
        private readonly IGenericRepository<Editorial> _editorialRepository;
        private readonly IGenericRepository<Libro> _libroRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Autor> autorRepository,
            IGenericRepository<Editorial> editorialRepository, 
            IGenericRepository<Libro> libroRepository)
        {
            _logger = logger;
            _autorRepository = autorRepository;
            _editorialRepository = editorialRepository;
            _libroRepository = libroRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] public async Task<IActionResult> listarAutores()
        {
            List<Autor> _lista = await _autorRepository.FindAll();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet] public async Task<IActionResult> listarEditoriales()
        {
            List<Editorial> _lista = await _editorialRepository.FindAll();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet] public async Task<IActionResult> listarLibros()
        {
            List<Libro> _lista = await _libroRepository.FindAll();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost] public async Task<IActionResult> guardarLibro([FromBody] Libro libro)
        {
            bool _resultado = await _libroRepository.Save(libro);
            if(_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok"});
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error"});
        }

        [HttpPut]
        public async Task<IActionResult> actualizarLibro([FromBody] Libro libro)
        {
            bool _resultado = await _libroRepository.Update(libro);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "OK" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> eliminarLibro(int id)
        {
            bool _resultado = await _libroRepository.Delete(id);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "OK" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}