using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemConstrutor
{

    //usando Theory para testar o mesmo metodo com varios parametros diferentes, em um unico método
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
    [InlineData(null, "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
    public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao) 
    {

        //cenário - arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));

        //ação - act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //validação - assert
        Assert.Equal(validacao, oferta.EhValido);
    }


    //usando Fact para testar o método com um unico parametro
    //[Fact]
    //public void RetornaOfertaValidaQuandoDadosValidos() // A A A -> (Arrange, Act, Assert) \\
    //{

    //    //cenário - arrange
    //    Rota rota = new Rota("OrigemTeste", "DestinoTeste");
    //    Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
    //    double preco = 100.0;
    //    var validacao = true;

    //    //ação - act
    //    OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

    //    //validação - assert
    //    Assert.Equal(validacao, oferta.EhValido);
    //}

    [Fact]
    public void RetornaMensagemDeErroQuandoRotaNula()
    {
        //cenário
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;

        //ação
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //validação
        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroQuandoPeriodoInvalido()
    {
        //arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 10, 10), new DateTime(2024, 5, 5));
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroQuandoPrecoMenorQueZero()
    {
        //arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 10, 10), new DateTime(2024, 10, 15));
        double preco = -100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }

    [Fact]
    public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoInvalidos()
    {
        //arrange
        int quantidadeEsperada = 3;
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 6, 1), new DateTime(2024, 5, 10));
        double preco = -100;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
    }


}