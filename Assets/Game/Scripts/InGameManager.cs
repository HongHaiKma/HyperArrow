using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
    }
}
