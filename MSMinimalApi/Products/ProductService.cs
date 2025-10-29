

namespace MyApp.Products;


public class ProductService : IProduct
{
    public IEnumerable<Product> GetAll()
    {
        return new List<Product>{
            new Product() { Id = new ChiaveComplessa(){Id1=1,Id2=2,Id3=3 }, Nome = "Test", Categoria = "pippo" },
            new Product() { Id = new ChiaveComplessa(){Id1=1,Id2=2,Id3=4 }, Nome = "Test senza categoria" }
        };    
    }
}
