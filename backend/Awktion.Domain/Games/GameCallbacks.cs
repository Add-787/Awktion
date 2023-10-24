namespace Awktion.Domain.Games;

/// <summary>
/// Callback functions to be implemented in respective projects.
/// </summary>
public interface GameCallbacks
{
    public void RoundStarted();
    public void BiddingStarted(object sender,EventArgs e);
    public void PlayerSold();
}
