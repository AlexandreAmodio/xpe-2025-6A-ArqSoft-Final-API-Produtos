namespace ProdutosApi.Models;

/// <summary>
/// Representa a entidade de Produto no sistema.
/// Contém as informações básicas de identificação e descrição de um produto,
/// utilizadas nos processos de cadastro, consulta, atualização e exclusão.
/// </summary>
    public class Produto
    {
        /// <summary>
        /// Identificador único do produto.
        /// Esse campo é usado como chave primária no banco de dados.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descrição detalhada do produto.
        /// Pode conter informações adicionais como características, especificações e observações.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Preço unitário do produto.
        /// Representa o valor monetário aplicado a cada unidade.
        /// </summary>
        public float UnitPrice { get; set; }
    }