namespace MyApp.Products;

public interface IProduct
{
    IEnumerable<Product> GetAll(); //posso rimanere generico nel tipo di ritorno come IEnumerable. posso concretizzarlo poi in vari modi
}
