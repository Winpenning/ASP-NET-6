using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categorias")]
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return StatusCode(200,new ResultViewModel<List<Category>>(categories));
        }
        catch
        {
            return StatusCode
                (500,new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
        }
        
    }
    
    [HttpGet("v1/categorias/{id:int}")]
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync
        ([FromServices] BlogDataContext context,
         [FromRoute] int id)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            return category == null ? 
                StatusCode
                    (404,new ResultViewModel<Category>("04X04 - Conteudo nao encontrado")) 
                : StatusCode(200, new ResultViewModel<Category>(category));
        }
        catch
        {
            return StatusCode
                (500, new ResultViewModel<Category>("05X05 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/categorias")]
    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return StatusCode(400,new ResultViewModel<Category>(ModelState.GetErrors()));
        try
        {
            var category = new Category()
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500,new ResultViewModel<Category>("05X06 - Nao foi possivel criar a categoria"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05X07 - Falha interna no servidor"));
        }
    }
    
    [HttpPut("v1/categorias/{id:int}")]
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return StatusCode((404), new ResultViewModel<Category>("04X05 - Categoria nao encontrada"));
            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return StatusCode(200,new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500,new ResultViewModel<Category>("05X08 - Nao foi possivel atualizar a categoria"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05X09 - Falha interna no servidor"));
        }
    }
    
    [HttpDelete("v1/categorias/{id:int}")]
    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return StatusCode(404, new ResultViewModel<Category>("Categoria nao encontrada"));

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return StatusCode(200,new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500,new ResultViewModel<Category>("05X10 - Nao foi possivel excluir a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna no servidor"));
        }
    }

    
}