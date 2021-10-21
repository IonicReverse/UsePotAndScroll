using robotManager.Helpful;
using System.Linq;
using System.Threading;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace UsePotAndScroll
{
  internal class Helpers
  {
    public static void Log(string message)
    {
      Logging.Write("[UsePotAndScroll] " + message, Logging.LogType.Normal, System.Drawing.Color.AliceBlue);
    }

    public static int HasPotion()
    {
      var returnPot = Data.HealthPotionList.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
      return returnPot;
    }

    public static int HasManaPot()
    {
      var returnPot = Data.ManaPotionList.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
      return returnPot;
    }

    public static int HasScroll()
    {
      var returnScroll = Data.ScrollList.Where(x => ItemsManager.GetItemCountById((uint)x) > 0).FirstOrDefault();
      return returnScroll;
    }

    public static void UsePot()
    {
      var haspotion = HasPotion();

      if (!ObjectManager.Me.IsDead
          && !ObjectManager.Me.IsMounted
          && (!ObjectManager.Me.HaveBuff("Food") && !ObjectManager.Me.HaveBuff("Drink"))
          && (ObjectManager.Target.Health >= ObjectManager.Me.HealthPercent) 
          && UsePotAndScrollSettings.CurrentSettings.AutoUseHealthPotion
          && ObjectManager.Me.HealthPercent <= UsePotAndScrollSettings.CurrentSettings.UseHealthPotionPercent
          && haspotion != 0)
      {
        ItemsManager.UseItem((uint)haspotion);
        Log("Using Potion " + ItemsManager.GetNameById(haspotion));
        Thread.Sleep(Usefuls.Latency + 100);
      } 
    }
    public static void UseManaPot()
    {

      var hasmanapotion = HasManaPot();

      if (!ObjectManager.Me.IsDead
          && !ObjectManager.Me.IsMounted
          && (!ObjectManager.Me.HaveBuff("Food") && !ObjectManager.Me.HaveBuff("Drink"))
          && UsePotAndScrollSettings.CurrentSettings.AutoUseManaPotion
          && DrinkingClass()
          && ObjectManager.Me.ManaPercentage <= UsePotAndScrollSettings.CurrentSettings.UseManaPotionPercent
          && hasmanapotion != 0)
      {
        ItemsManager.UseItem((uint)hasmanapotion);
        Log("Using Potion " + ItemsManager.GetNameById(hasmanapotion));
        Thread.Sleep(Usefuls.Latency + 100);
      }
    }

    public static void UseScroll()
    {
      var hasScroll = HasScroll();

      if (!ObjectManager.Me.IsDead
          && !ObjectManager.Me.IsMounted
          && (!ObjectManager.Me.HaveBuff("Food") && !ObjectManager.Me.HaveBuff("Drink"))
          && UsePotAndScrollSettings.CurrentSettings.AutoUseAnyScroll
          && hasScroll != 0
          && Bag.GetBagItem().Where(i => i.Entry == hasScroll && i.GetItemInfo.ItemMinLevel <= ObjectManager.Me.Level).FirstOrDefault() != null
          && !ObjectManager.Me.HaveBuff(ItemsManager.GetNameById(hasScroll)))
      {
        Interact.InteractGameObject(ObjectManager.Me.GetBaseAddress, false, false);
        Thread.Sleep(200);
        ItemsManager.UseItem((uint)hasScroll);
        Log("Using Scroll " + ItemsManager.GetNameById(hasScroll));
        Thread.Sleep(Usefuls.Latency + 100);
      }
    }

    public static bool DrinkingClass()
    {
      if (ObjectManager.Me.WowClass == WoWClass.Shaman
       || ObjectManager.Me.WowClass == WoWClass.Warlock
       || ObjectManager.Me.WowClass == WoWClass.Paladin
       || ObjectManager.Me.WowClass == WoWClass.Mage
       || ObjectManager.Me.WowClass == WoWClass.Hunter
       || ObjectManager.Me.WowClass == WoWClass.Druid
       || ObjectManager.Me.WowClass == WoWClass.Priest)
        return true;

      return false;
    }

  }
}
