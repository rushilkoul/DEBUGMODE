using System;

[Flags]
public enum PipeDir
{
  None = 0,
  North = 1,   // +Z
  East = 2,   // +X
  South = 4,   // -Z
  West = 8    // -X
}
