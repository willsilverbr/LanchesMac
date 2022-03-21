using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _Context;
        public CategoriaRepository(AppDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<Categoria> Categorias => _Context.Categorias;
    }
}
