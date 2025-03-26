using ProdutosCia.Domain.Entities;
using ProdutosCia.Domain.Interfaces;
using ProdutosCia.Infrastructure.Data.Context;
using ProdutosCia.Infrastructure.Data.Repository.Base;

namespace ProdutosCia.Infrastructure.Data.Repository;

public class ProductRepository(ProdutosCiaContext context) : BaseRepository<Product>(context), IProductRepository
{
    
}