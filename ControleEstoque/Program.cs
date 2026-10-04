using System.Text.Json;

public class Produto
{
    public int codigoProduto { get; set; }
    public string descricaoProduto { get; set; } = string.Empty;
    public int estoque { get; set; }
}

public class Movimentacao
{
    public int numero { get; set; }
    public string descricao { get; set; } = string.Empty;
    public int codigoProduto { get; set; }
    public int quantidade { get; set; }
}

class Program
{
    static void Main()
    {
        string json = """
        {
            "estoque": [
                {
                    "codigoProduto": 101,
                    "descricaoProduto": "Caneta Azul",
                    "estoque": 150
                },
                {
                    "codigoProduto": 102,
                    "descricaoProduto": "Caderno Universitário",
                    "estoque": 75
                },
                {
                    "codigoProduto": 103,
                    "descricaoProduto": "Borracha Branca",
                    "estoque": 200
                },
                {
                    "codigoProduto": 104,
                    "descricaoProduto": "Lápis Preto HB",
                    "estoque": 320
                },
                {
                    "codigoProduto": 105,
                    "descricaoProduto": "Marcador de Texto Amarelo",
                    "estoque": 90
                }
            ]
        }
        """;

        // Lê os produtos do JSON
        var dados = JsonSerializer.Deserialize<Dictionary<string, List<Produto>>>(json)!;
        var produtos = dados["estoque"];

        Console.WriteLine("=== PRODUTOS ===");

        foreach (var produto in produtos)
        {
            Console.WriteLine(
                $"Código: {produto.codigoProduto} | " +
                $"Produto: {produto.descricaoProduto} | " +
                $"Estoque: {produto.estoque}"
            );
        }

        Console.WriteLine("\nDigite o código do produto:");
        int codigo = int.Parse(Console.ReadLine()!);

        Produto? produtoSelecionado = produtos.Find(
            produto => produto.codigoProduto == codigo
        );

        if (produtoSelecionado == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine("\nDigite o tipo de movimentação:");
        Console.WriteLine("1 - Entrada");
        Console.WriteLine("2 - Saída");

        int tipo = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine()!);

        if (quantidade <= 0)
        {
            Console.WriteLine("A quantidade deve ser maior que zero.");
            return;
        }

        var movimentacao = new Movimentacao
        {
            numero = 1,
            codigoProduto = codigo,
            quantidade = quantidade
        };

        if (tipo == 1)
        {
            movimentacao.descricao = "Entrada de mercadoria";
            produtoSelecionado.estoque += quantidade;
        }
        else if (tipo == 2)
        {
            if (quantidade > produtoSelecionado.estoque)
            {
                Console.WriteLine(
                    "Quantidade de saída maior que o estoque disponível."
                );
                return;
            }

            movimentacao.descricao = "Saída de mercadoria";
            produtoSelecionado.estoque -= quantidade;
        }
        else
        {
            Console.WriteLine("Tipo de movimentação inválido.");
            return;
        }

        Console.WriteLine("\n=== MOVIMENTAÇÃO REALIZADA ===");
        Console.WriteLine($"Número: {movimentacao.numero}");
        Console.WriteLine($"Descrição: {movimentacao.descricao}");
        Console.WriteLine($"Produto: {produtoSelecionado.descricaoProduto}");
        Console.WriteLine($"Quantidade movimentada: {movimentacao.quantidade}");
        Console.WriteLine($"Estoque final: {produtoSelecionado.estoque}");
    }
}