﻿using Zenject;

namespace PachowStudios.BadTummyBunny
{
  public sealed class CollectCoinsStar : BaseStarController,
    IHandles<PlayerCoinCollectedMessage>
  {
    [Inject] private CollectCoinsStarSettings Config { get; set; }

    private int collectedCoins;

    private int CollectedCoins
    {
      get { return this.collectedCoins; }
      set
      {
        this.collectedCoins = value;
        
        if (this.collectedCoins >= Config.RequiredCoins)
          Complete();
      }
    }

    [PostInject]
    private void PostInject()
      => EventAggregator.Subscribe(this);

    protected override void OnCompleted()
      => EventAggregator.Unsubscribe(this);

    public void Handle(PlayerCoinCollectedMessage message)
      => CollectedCoins += message.Value;
  }
}