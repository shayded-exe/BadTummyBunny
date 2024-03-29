﻿using UnityEngine;

namespace PachowStudios.BadTummyBunny
{
  public interface ICameraBaseBehavior : ICameraPositionAssertion
  {
    bool IsEnabled { get; }
    
    #if UNITY_EDITOR
    // useful for while we are in the editor to provide a UI
    // ReSharper disable once InconsistentNaming
    void onDrawGizmos(Vector3 basePosition);
    #endif
  }
}
