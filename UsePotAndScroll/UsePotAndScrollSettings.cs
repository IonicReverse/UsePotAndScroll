using System;
using wManager.Wow.ObjectManager;
using wManager.Wow.Helpers;
using robotManager.Helpful;
using System.IO;
using System.ComponentModel;
using System.Configuration;

[Serializable]
public class UsePotAndScrollSettings : Settings
{

  [Setting]
  [DefaultValue(true)]
  [Category("Settings")]
  [DisplayName("Use Health Potion")]
  [Description("Use Health Potion Based on Health Percent")]
  public bool AutoUseHealthPotion { get; set; }

  [Setting]
  [DefaultValue(false)]
  [Category("Settings")]
  [DisplayName("Use Mana Potion")]
  [Description("Use Mana Potion Based on Mana Percent")]
  public bool AutoUseManaPotion { get; set; }

  [Setting]
  [DefaultValue(true)]
  [Category("Settings")]
  [DisplayName("Use Any Scroll")]
  [Description("Use Any Scroll In Bag")]
  public bool AutoUseAnyScroll { get; set; }

  [Setting]
  [DefaultValue(25)]
  [Category("Settings")]
  [DisplayName("Use Health Potion Percent")]
  [Description("Health Percentage")]
  public int UseHealthPotionPercent { get; set; }

  [Setting]
  [DefaultValue(25)]
  [Category("Settings")]
  [DisplayName("Use Mana Potion Percent")]
  [Description("Mana Percentage")]
  public int UseManaPotionPercent { get; set; }

  public UsePotAndScrollSettings()
  {
    AutoUseHealthPotion = true;
    AutoUseManaPotion = false;
    AutoUseAnyScroll = true;
    UseHealthPotionPercent = 30;
    UseManaPotionPercent = 20;
  }

  public static UsePotAndScrollSettings CurrentSettings { get; set; }

  public bool Save()
  {
    try
    {
      return Save(AdviserFilePathAndName("UsePotAndScroll", ObjectManager.Me.Name + "." + Usefuls.RealmName));
    }
    catch (Exception e)
    {
      Logging.WriteError("[UsePotAndScrollSettings] > Save() : " + e);
      return false;
    }
  }

  public static bool Load()
  {
    try
    {
      if (File.Exists(AdviserFilePathAndName("UsePotAndScroll", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
      {
        CurrentSettings = Load<UsePotAndScrollSettings>(AdviserFilePathAndName("UsePotAndScroll", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        return true;
      }
      CurrentSettings = new UsePotAndScrollSettings();
    }
    catch (Exception e)
    {
      Logging.WriteError("[UsePotAndScrollSettings] > Load() : " + e);
    }
    return false;
  }

}
