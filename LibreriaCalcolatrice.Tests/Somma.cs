using Moq;

namespace LibreriaCalcolatrice.Tests;

public class Somma
{
    [Fact]
    public void SommaDueNumeriRestituisceSommaAritmetica()
    {
        //tipo di test con classi mock create nel progetto originario
        //Arrange
        IClock clock = new MockWednesdayClock();
        var calcolatrice = new Calcolatrice(clock);
        int a = 2;
        int b = 3;
        var atteso = 5;

        //Act
        var calcolato = calcolatrice.Somma(a, b);

        //Assert
        Assert.Equal(atteso,calcolato);
    }
    [Fact]
    public void SommaZeroRestituisceNumeroDiPartenza()
    {
        IClock clock = new MockWednesdayClock();
        var calcolatrice = new Calcolatrice(clock);
        //Arrange
        int a = 2;
        int b = 0;
        var atteso = 2;
        //Act
        var calcolato = calcolatrice.Somma(a, b);

        //Assert
        Assert.Equal(atteso, calcolato);
    }
    [Fact]
    public void SommoNegativoRestituisceZero()
    {

        IClock clock = new MockWednesdayClock();
        var calcolatrice = new Calcolatrice(clock);
        //Arrange
        int a = 2;
        int b = -2;
        var atteso = 0;
        //Act
        var calcolato = calcolatrice.Somma(a, b);

        //Assert
        Assert.Equal(atteso, calcolato);
    }
  
    [Fact]
    public void SeEseguoLaSommaMartedìRestituisceSommaPazza()
    {
        //esempio con Moq
        var mock = new Mock<IClock>();
        mock.Setup(clock => clock.Now()).Returns(new DateTime(2025,10,28));
        var calcolatrice = new Calcolatrice(mock.Object);

        //Arrange
        int a = 1;
        int b = 6;
        var atteso = 37;
        //Act
        var calcolato = calcolatrice.Somma(a, b);

        //Assert
        Assert.Equal(atteso, calcolato);
    }
    [Fact]
    public void SeEseguoSommaMercoledìRestituisceSommaNormale()
    {
        //esempio con Moq
        var mock = new Mock<IClock>();
        mock.Setup(clock => clock.Now()).Returns(new DateTime(2025, 10, 29));
        var calcolatrice = new Calcolatrice(mock.Object);

        //Arrange
        int a = 1;
        int b = 6;
        var atteso = 7;
        //Act
        var calcolato = calcolatrice.Somma(a, b);

        //Assert
        Assert.Equal(atteso, calcolato);
    }
}