using CRUDWebApi.Models;
using CRUDWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _repository.BuscaUsuarios();
            return usuarios.Any()
                ? Ok(usuarios)
                : NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repository.BuscaUsuario(id);
            return usuario != null
                ? Ok(usuario)
                : NotFound("Usuario não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            _repository.AdicionaUsuario(usuario);
            return await _repository.SaveChangesAsync()
                ? Ok("usuario add com sucesso")
                : BadRequest("Erro ao add usuario");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            var usuarioBd = await _repository.BuscaUsuario(id);
            if(usuarioBd == null)
                NotFound("Usuario não encontrado");

            usuarioBd.Nome = usuario.Nome ?? usuarioBd.Nome;
            usuarioBd.DataNascimento = usuario.DataNascimento != new DateTime()
                ? usuario.DataNascimento : usuarioBd.DataNascimento;

            _repository.AtualizaUsuario(usuarioBd);

            return await _repository.SaveChangesAsync()
                ? Ok("usuario att com sucesso")
                : BadRequest("Erro ao att usuario");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioDel = await _repository.BuscaUsuario(id);
            if(usuarioDel == null)
                NotFound("Usuario não encontrado");

            _repository.DeletaUsuario(usuarioDel);

            return await _repository.SaveChangesAsync()
                ? Ok("usuario del com sucesso")
                : BadRequest("Erro ao del usuario");
        }
    }
}
