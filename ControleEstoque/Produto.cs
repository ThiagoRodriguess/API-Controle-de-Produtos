namespace ControleEstoque
{
    public class Produto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
