﻿namespace PachowStudios.BadTummyBunny
{
  public interface IStar
  {
    string Id { get; }
    string Name { get; }
    StarRequirement Requirement { get; }

    bool IsCompleted { get; set; }
  }
}