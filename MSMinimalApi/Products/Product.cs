namespace MyApp.Products;

public class Product
{
    public required ChiaveComplessa Id { get; set; }
    //public int Id { get; set; }
    public required string Nome { get;  set; } //per risolvere problema dei nullable
    public string Categoria { get; set; } = String.Empty;//oppure assegno il valore
}

public class ChiaveComplessa
{
    public int Id1 { get; set; }
    public int Id2 { get; set; }
    public int Id3 { get; set; }
}
