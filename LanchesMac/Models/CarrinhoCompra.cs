using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _Context;

        public CarrinhoCompra(AppDbContext context)
        {
            _Context = context;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //Define uma Sessão
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()? .HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto 
            var context = services.GetService<AppDbContext>();

            //Obter ou gera o Id do carrinho
           string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atruibuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };

        }
        
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _Context.carrinhoCompraItens.SingleOrDefault(
                         s => s.Lanche.LancheId == lanche.LancheId &&
                         s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId, 
                    Lanche = lanche, 
                    Quantidade = 1
                };
                _Context.carrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++; 
            }
            _Context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _Context.carrinhoCompraItens.SingleOrDefault(
                         s => s.Lanche.LancheId == lanche.LancheId &&
                         s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--; 
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
            }
            else
            {
                _Context.carrinhoCompraItens.Remove(carrinhoCompraItem);
            }
            _Context.SaveChanges();
            return quantidadeLocal; 
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItens ?? 
                (CarrinhoCompraItens = 
                    _Context.carrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Lanche)
                    .ToList());

        }
        public void LimparCarrinho()
        {
            var carrinhoItens = _Context.carrinhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _Context.carrinhoCompraItens.RemoveRange(carrinhoItens);
            _Context.SaveChanges(); 

        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _Context.carrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;   
        }

    }
}
