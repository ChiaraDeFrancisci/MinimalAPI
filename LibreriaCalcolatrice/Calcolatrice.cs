namespace LibreriaCalcolatrice;

public class Calcolatrice
{
    public Calcolatrice(IClock clock)
    {
        this.clock = clock;
    }

    internal IClock clock { get; private set; }

    public int Somma(int a,int b)
    {
        DateTime oggi = clock.Now();
        var giorno = oggi.DayOfWeek;
        if (giorno == DayOfWeek.Tuesday)
            return (a * a) + (b * b);
        else
            return a + b;
    }

}
