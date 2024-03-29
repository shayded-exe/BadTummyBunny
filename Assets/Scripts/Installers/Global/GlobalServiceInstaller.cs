﻿using UnityEngine;
using Zenject;

namespace PachowStudios.BadTummyBunny
{
  [AddComponentMenu("Bad Tummy Bunny/Installers/Global/Service Installer")]
  public class GlobalServiceInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindAllInterfacesToSingle<Bootystrapper>();
      Container.BindPriority<Bootystrapper>(-100);

      InstallGeneralServiceBindings();
      InstallPlayerServiceBindings();
    }

    private void InstallGeneralServiceBindings()
    {
      Container.Bind<IEventAggregator>(BindingIds.Global).ToSingle<EventAggregator>();
      Container.Bind<IEventAggregator>().ToLookup<IEventAggregator>(BindingIds.Global);

      Container.BindSingleWithInterfaces<SceneService>();
      Container.BindSingleWithInterfaces<SaveService>();
    }

    private void InstallPlayerServiceBindings()
    {
      Container.Bind<IScoreKeeper>().ToSingle<PlayerScoreService>();
      Container.BindSingle<PlayerStatsService>();
    }
  }
}