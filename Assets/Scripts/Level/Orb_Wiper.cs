public class Orb_Wiper : Orb
{
    protected override void PlayEffect()
    {
        WaveManager.I.SpawnEnemy(4);
    }
}