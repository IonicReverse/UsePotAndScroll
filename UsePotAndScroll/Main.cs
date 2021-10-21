using System;
using System.ComponentModel;
using System.Threading;
using wManager.Plugin;
using UsePotAndScroll;
using wManager.Wow.Helpers;

public class Main : IPlugin
{
  public static bool isRunning;
  private BackgroundWorker pulseThread = new BackgroundWorker();

  public void Start()
  {
    if (!pulseThread.IsBusy) {
      pulseThread.DoWork += Pulse;
      pulseThread.RunWorkerAsync();
    }
  }

  public void Pulse(object sender, DoWorkEventArgs args)
  {
    try
    {
      while (isRunning)
      {
        if (Conditions.InGameAndConnectedAndAliveAndProductStarted)
        {
          Helpers.UsePot();
          Helpers.UseManaPot();
          Helpers.UseScroll();
          Thread.Sleep(1000);
        }
      }
    }
    catch (Exception ex)
    {
      Helpers.Log("Something wrong.. " + ex);
    }
  }

  public void Dispose()
  {
    isRunning = false;
    Helpers.Log("Stopped");
  }

  public void Initialize()
  {
    try
    {
      UsePotAndScrollSettings.Load();
      isRunning = true;
      Start();
    }
    catch(Exception ex)
    {
      Helpers.Log("Something wrong : " + ex);
    }
  }

  public void Settings()
  {
    UsePotAndScrollSettings.Load();
    UsePotAndScrollSettings.CurrentSettings.ToForm();
    UsePotAndScrollSettings.CurrentSettings.Save();
  }
}

