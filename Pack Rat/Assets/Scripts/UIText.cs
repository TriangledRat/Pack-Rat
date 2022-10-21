using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    public bool climbing, holding, climbRange, itemRange;
    public Text uiText;
    private PlayerController playerController;
    private ItemChecker checker;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        checker = GetComponent<ItemChecker>();  
    }

    // Update is called once per frame
    void Update()
    {

        if (climbing)
        {
            uiText.text = "W = up, S = down, Jump to let go";
        }

        if (holding)
        {
            uiText.text = "RightMouse = launch, release LeftMouse to let go";
        }

        if (climbRange)
        {
            uiText.text = "Press E to climb";
        }

        if (itemRange)
        {
            uiText.text = "Hold LeftMouse to carry";
        }
    }
}
