using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using IResult = Microsoft.AspNetCore.Http.IResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApplication2.Controllers;

public class Controller
{
    public Controller(WebApplication app)
    {
        var dataStore = app.MapGroup("/storage");
        dataStore.MapGet("/", GetAll);
        dataStore.MapGet("/{id}", GetOne);
        dataStore.MapPost("/", Create);
        dataStore.MapPut("/{id}", Update);
        dataStore.MapDelete("/{id}", Delete);
        
        static async Task<IResult> GetAll(Storage st)
        {
            return TypedResults.Ok(await st.Store.ToArrayAsync());
        }
        
        static async Task<IResult> GetOne(int id, Storage db)
        {
            return await db.Store.FindAsync(id)
                is Model todo
                ? TypedResults.Ok(todo)
                : TypedResults.NotFound();
        }
        
        static async Task<IResult> Create( Model model, Storage db)
        {
            db.Store.Add(model);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/storage/{model.id}", model);
        }
        
        static async Task<IResult> Update(int id, Model model, Storage db)
        {
            var newmodel = await db.Store.FindAsync(id);

            if (newmodel is null) return TypedResults.NotFound();

            newmodel.information = model.information;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        static async Task<IResult> Delete(int id, Storage db)
        {
            if (await db.Store.FindAsync(id) is Model model)
            {
                db.Store.Remove(model);
                await db.SaveChangesAsync();
                return TypedResults.Ok(model);
            }

            return TypedResults.NotFound();
        }
    }
   
}