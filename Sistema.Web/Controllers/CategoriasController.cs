using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.almacen;
using Sistema.Web.models.almacen.categoria;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CategoriasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Categorias/Listar
        [HttpGet("[action]")]  //el action es listar
        public async Task <IEnumerable<CategoriaViewModel>> Listar()
        {
            var categoria = await _context.Categorias.ToListAsync();

            return categoria.Select(c => new CategoriaViewModel {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });
        }

        // GET: api/Categorias/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(new CategoriaViewModel
            {
                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Condicion = categoria.Condicion
            });
            
                    
        }

        // PUT: api/Categorias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel actualizarModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (actualizarModel.IdCategoria < 1)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == actualizarModel.IdCategoria);

            if(categoria == null)
            {
                return BadRequest();
            }
            else
            {
                categoria.Nombre = actualizarModel.Nombre;
                categoria.Descripcion = actualizarModel.Descripcion;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }

            return Ok();
        }

        // POST: api/Categorias/Insertar
        [HttpPost("[action]")]
        public async Task<IActionResult> Insertar([FromBody] InsertarViewModel insertarModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categoria newCategoria = new Categoria()
            {
                Nombre = insertarModel.Nombre,
                Descripcion = insertarModel.Descripcion,
                Condicion = true
            };
           
            _context.Categorias.Add(newCategoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                return BadRequest();
            }

            // return CreatedAtAction("Mostrar", new { id = newCategoria.IdCategoria }, newCategoria );
            return Ok();
        }

        // DELETE: api/Categorias/Delete/2
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                BadRequest();
            }

            return Ok(categoria);
        }

        // PUT: api/Categorias/Activar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
          
            if (id < 1)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
            {
                return BadRequest();
            }
            else
            {
                categoria.Condicion = true;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }

            return Ok();
        }

        // PUT: api/Categorias/Desactivar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id < 1)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
            {
                return BadRequest();
            }
            else
            {
                categoria.Condicion = false;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }

            return Ok();
        }

    }
}