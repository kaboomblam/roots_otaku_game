namespace OtakuGameJam
{
    public abstract class PlayState
    {
        internal abstract void EnterState(GamePlayManager gpm);

        internal abstract void UpdateState(GamePlayManager gpm);

        internal abstract void ExitState(GamePlayManager gpm);
    }
}