using DesafioIbge.Data;
using DesafioIbge.Models;
using DesafioIbge.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioIbge.Controllers
{
    public class IbgeController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromServices] DataContext context, IbgeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dataIbge = new Ibge
                {
                    Id = model.Id,
                    State = model.State,
                    City = model.City
                };
                await context.Ibges.AddAsync(dataIbge);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar.");
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> ReadById([FromServices] DataContext context, [FromRoute] int id)
        {
            try
            {
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return (dataIbge is null) ? NotFound("Dados não encontrados.") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter detalhes.");
            }
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromServices] DataContext context, [FromRoute] int? id)
        {
            if (id is null || id == 0)
                return BadRequest("Por favor, informe um código.");

            try
            {
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                return (dataIbge is null) ? NotFound("Não foi possível encontrar os dados") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter os dados.");
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromServices] DataContext context, IbgeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);

                if (dataIbge is null)
                    return NotFound("Não foi possível encontrar os dados.");

                dataIbge.State = model.State;
                dataIbge.City = model.City;

                context.Ibges.Update(dataIbge);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar os dados.");
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromServices] DataContext context, [FromRoute] int id)
        {
            try
            {
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return (dataIbge is null) ? NotFound("Não foi possível encontrar os dados.") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter os dados.");
            }            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromServices] DataContext context, IbgeViewModel model)
        {
            try
            {
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);

                if (dataIbge is null)
                    return NotFound("Não foi possível encontrar os dados.");

                context.Ibges.Remove(dataIbge);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao deletar os dados.");
            }            
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult SearchCity()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult SearchId()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult SearchState()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> SearchByCity([FromServices] DataContext context, string city)
        {
            if (city is null)
                return BadRequest("Por favor, informe uma cidade para a busca.");

            try
            {
                ViewData["SearchedWord"] = city;
                var dataIbge = await context.Ibges.AsNoTracking().Where(x => x.City.Contains(city)).ToListAsync();

                return (dataIbge is null) ? NotFound($"Não foi possível encontrar dados com este nome '{city}'") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter a cidade");
            }
        }

        [HttpGet]
        [Route("Ibge/SearchByState/{model:alpha}/{page:int}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> SearchByState([FromServices] DataContext context, [FromRoute] string model, [FromRoute] int page = 0, int pageSize = 25)
        {
            if (model is null)
                return BadRequest("Por favor, informe uma sigla de estado para a busca.");
            try
            {
                var count = await context.Ibges.AsNoTracking().Where(x => x.State == model).CountAsync();

                var dataIbge = await context.Ibges.AsNoTracking().Where(x => x.State == model).OrderBy(x => x.City).Skip(page * pageSize).Take(pageSize).ToListAsync();

                ViewData["SearchedWord"] = model;
                ViewData["TotalPages"] = count / pageSize;

                return (dataIbge is null) ? NotFound($"Não foi possível encontrar dados com esta sigla '{model}'") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter os dados.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAllOfState([FromServices] DataContext context, Ibge model, int page = 0, int pageSize = 25)
        {
            if (model is null)
                return View(model);
            try
            {
                var count = await context.Ibges.AsNoTracking().Where(x => x.State == model.State).CountAsync();
                var dataIbge = await context.Ibges.AsNoTracking().Where(x => x.State == model.State).OrderBy(x => x.City).Skip(page * pageSize).Take(pageSize).ToListAsync();
                ViewData["SearchedWord"] = model.State;
                ViewData["TotalPages"] = count / pageSize;
                return (dataIbge is null) ? NotFound($"Não foi possível encontrar dados com esta sigla '{model}'") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter os dados.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> SearchById([FromServices] DataContext context, int? id)
        {
            if (id is null)
                return BadRequest("Por favor, informe um código para a busca.");

            try
            {
                ViewData["SearchedWord"] = id;
                var dataIbge = await context.Ibges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                return (dataIbge is null) ? NotFound($"Não foi possível encontrar dados com este código'{id}'") : View(dataIbge);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao obter o dado.");
            }

        }

    }
}