namespace Awktion.Domain.Entities.Rounds;

public class RoundBuilder
{
    private int _no;
    private Round _round;

    public RoundBuilder()
    {
        _no = 1;
        _round = new Round(_no);
    }

    public void AddTimer(int mins)
    {
        _round.Mins = mins;
    }

    public void Reset()
    {
        _no++;
        _round = new Round(_no);
    }

    public Round GetRound()
    {
        Round round = _round;
        Reset();

        return round;
    }


}
