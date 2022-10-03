using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_EnergyControll : MonoBehaviour
{
    private static new_EnergyControll new_energyControll;

    public Text Now;
    public Text Max;

    protected ushort Max_mana;
    protected ushort Now_mana;
    private void Awake()
    {
        new_energyControll = this;
        Max_mana = new_GameManager.instance.UserChar_Info.energy;
        Now_mana = new_GameManager.instance.UserChar_Info.energy;
    }

    public static void SettingMana()
    {
        new_energyControll.Now_mana = new_GameManager.instance.UserChar_Info.energy;
        new_energyControll.Max.text = new_energyControll.Max_mana.ToString();
        new_energyControll.Now.text = new_energyControll.Now_mana.ToString();
    }

    public static ushort GetMana()
    {
        return new_energyControll.Now_mana;
    }

    public static void SetMana(ushort _num)
    {
        new_energyControll.Now_mana = _num;
        new_energyControll.Now.text = new_energyControll.Now_mana.ToString();
    }
}
