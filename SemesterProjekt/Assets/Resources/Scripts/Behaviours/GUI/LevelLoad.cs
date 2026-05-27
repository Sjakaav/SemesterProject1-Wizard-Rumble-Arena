using GUIManagement;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    public Animator transition;

    private void Start()
    {
        GUIManager.Instance.setAnimator(transition);
    }
}
