using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina;
    float maxStamina;

    public Slider staminaBar;
    public float dValue;

    private bool decreasing;

    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            decreasing = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            decreasing = false;
        }

        UpdateEnergy(decreasing);
    }

    private void UpdateEnergy(bool decreasing)
    {
        if (decreasing && stamina > 0)
        {
            stamina -= dValue * Time.deltaTime;
            staminaBar.value = stamina;
        }
        else if (!decreasing && stamina < maxStamina)
        {
            stamina += dValue * Time.deltaTime;
            staminaBar.value = stamina;
        }
    }
}
