using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemConstrutor
{

    //usando Theory para testar o mesmo metodo com varios parametros diferentes, em um unico m�todo
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
    [InlineData(null, "S�o Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Vit�ria", "S�o Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Rio de Janeiro", "S�o Paulo", "2024-01-01", "2024-01-02", -500, false)]
    public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao) 
    {

        //cen�rio - arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));

        //a��o - act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //valida��o - assert
        Assert.Equal(validacao, oferta.EhValido);
    }


    //usando Fact para testar o m�todo com um unico parametro
    //[Fact]
    //public void RetornaOfertaValidaQuandoDadosValidos() // A A A -> (Arrange, Act, Assert) \\
    //{

    //    //cen�rio - arrange
    //    Rota rota = new Rota("OrigemTeste", "DestinoTeste");
    //    Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
    //    double preco = 100.0;
    //    var validacao = true;

    //    //a��o - act
    //    OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

    //    //valida��o - assert
    //    Assert.Equal(validacao, oferta.EhValido);
    //}

    [Fact]
    public void RetornaMensagemDeErroQuandoRotaNula()
    {
        //cen�rio
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;

        //a��o
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //valida��o
        Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
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
        Assert.Contains("Erro: Data de ida n�o pode ser maior que a data de volta.", oferta.Erros.Sumario);
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
        Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
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