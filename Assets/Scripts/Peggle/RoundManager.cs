using Peggle;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private PeggleManager peggleManager;

    void Start()
    {
        peggleManager.StartNewGame(this);   
    }
}
