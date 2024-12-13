using Common;
using System.Text;

namespace Day08;

public static class Helper {
  public const char DotChar = '.';
  public const int SpaceInDisk = -1;

  public static (string denseInput, List<int> decodedInput) ReadAndDecodeInputFromFile() {
    var map = InputHelper.ReadInputFile(InputFileName.Input09);
    var denseInput = map[0];
    var sb = new StringBuilder();
    var decodedInput = new List<int>();
    var isSpace = false;

    for (int i = 0; i < denseInput.Length; i++) {
      int value;

      if (!isSpace) {
        value = i / 2;
        isSpace = true;
      } else {
        // value = -1 represents to DotChar
        value = SpaceInDisk;
        isSpace = false;
      }

      var loopCount = int.Parse(denseInput[i].ToString());
      for (int j = 0; j < loopCount; j++) {
        decodedInput.Add(value);
      }
    }

    return (denseInput, decodedInput);
  }
}