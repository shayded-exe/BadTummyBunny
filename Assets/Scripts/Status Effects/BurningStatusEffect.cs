﻿using PachowStudios.Assertions;
using UnityEngine;

namespace PachowStudios.BadTummyBunny
{
  public class BurningStatusEffect : BaseStatusEffect
  {
    private float DamageTimer { get; set; }
    private float DurationTimer { get; set; }
    private float Duration { get; set; }

    private BurningStatusEffectSettings Config { get; }
    private IStatusEffectView View { get; }

    public BurningStatusEffect(BurningStatusEffectSettings config, IStatusEffectView view)
      : base(config)
    {
      View = view;
      Config = config;
    }

    public override void Attach(IStatusEffectable affectectedCharacter)
    {
      base.Attach(affectectedCharacter);
      View.Attach(affectectedCharacter);
      Duration = Random.Range(Config.MinDuration, Config.MaxDuration);
    }

    public override void Detach()
    {
      base.Detach();
      View.Detach();
    }

    public override void Tick()
    {
      AffectedCharacter.Should().NotBeNull("because a detatched status effect shouldn't be ticked");

      base.Tick();

      DamageTimer += Time.deltaTime;

      if (DamageTimer >= Config.TimePerDamage)
      {
        AffectedCharacter.Health.TakeDamage(Config.Damage);
        DamageTimer = 0f;
      }

      DurationTimer += Time.deltaTime;

      if (DurationTimer >= Duration)
        Detach();
    }
  }
}
