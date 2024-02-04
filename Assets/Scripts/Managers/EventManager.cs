using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnGamePaused;
    public event Action OnGameUnPaused;
    public event Action<int> OnScoreUpdate;

    public void OnGamePaused_Invoke()
    {
        OnGamePaused?.Invoke();
    }

    public void OnGameUnPaused_Invoke()
    {
        OnGameUnPaused?.Invoke();
    }

    public void OnScoreUpdate_Invoke(int _score)
    {
        OnScoreUpdate?.Invoke(_score);
    }
}
