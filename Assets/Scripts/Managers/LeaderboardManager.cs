using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using Dan.Models;

public class LeaderboardManager : MonoBehaviour
{
    private string m_publicKey = LeaderboardKey.PUBLIC_KEY;
    
    [SerializeField] private TMP_InputField m_playeNameInputField;
    [SerializeField] private Transform m_leaderboardUITransform;
    [SerializeField] private GameObject m_leaderboardEntryPrefab;

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(m_publicKey, ((msg) =>
        {
            for (int i = 0; i < msg.Length; i++)
            {
                Dan.Models.Entry entry = msg[i];
                LeaderboardEntry newEntry = Instantiate(m_leaderboardEntryPrefab, m_leaderboardUITransform)
                                            .GetComponent<LeaderboardEntry>();

                newEntry.InitializeEntry(entry.Rank, entry.Username, entry.Score);
            }
            Debug.Log(msg);
        }));

        LeaderboardCreator.GetLeaderboard(m_publicKey, OnLeaderboardLoaded);
    }

    public void SetEntry(string _username, int _score)
    {
        LeaderboardCreator.UploadNewEntry(m_publicKey, _username, _score, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }

    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (Transform t in m_leaderboardUITransform)
            Destroy(t.gameObject);

        foreach (var t in entries)
        {
            LeaderboardEntry newEntry = Instantiate(m_leaderboardEntryPrefab, m_leaderboardUITransform)
                                           .GetComponent<LeaderboardEntry>();

            newEntry.InitializeEntry(t.Rank, t.Username, t.Score);

        }
    }
}
