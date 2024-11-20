using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test;
public class RotaConstrutor
{

    [Fact]
    public void RetornaRotaValidaDeAcordoComDadosDeEntrada()
    {
        //arrange
        var rotaOrigem = "Campo Mourão";
        var rotaDestino = "Curitiba";

        //act
        Rota rota = new Rota(rotaOrigem, rotaDestino);     

        //assert
        Assert.True(rota.EhValido);
    }

    [Fact]
    public void RetornaRotaInvalidaDeAcordoComDadosDeEntradaNulosOuVazios()
    {
        //arrange
        var rotaOrigem = "";
        var rotaDestino = "";

        //act
        Rota rota = new Rota(rotaOrigem, rotaDestino);

        //assert
        Assert.Contains("A rota não pode possuir um destino nulo ou vazio.", rota.Erros.Sumario);
        Assert.False(rota.EhValido);
    }


}
