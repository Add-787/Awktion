namespace Awktion.Domain.Entities.Rounds;

public class RoundBuilder
{
    private int _no;
    private Round _round;

    public RoundBuilder()
    {
        _no = 0;
        _round = new();
    }

    public void AddRoundNo()
    {
        _round.No = _no;
    }

    public void AddTimer(int mins)
    {
        _round.Mins = mins;
    }

    public void Reset()
    {
        _no++;
        _round = new Round();
    }

    public Round GetRound()
    {
        Round round = _round;
        Reset();

        return round;
    }


}
