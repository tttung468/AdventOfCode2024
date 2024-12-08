using Common;

// 75,47,61,53,29
// 75 | 29, 53, 47, 61, 13    
// 47 | 53, 13, 61, 29        
// 61 | 13, 53, 29            
// 53 | 29, 13                
// 29 | 13                    
// 97 | 13, 61, 47, 29, 53, 75
// 13 |                       

// Part 2
// 10778 too high
// 6004

// 75,97,47,61,53   becomes  97,75,47,61,53
// 13,29,61         becomes  61,29,13.
// 97,13,75,29,47   becomes  97,75,47,29,13.

// 13,29,61
// 13 | 29  FirstPage  > MiddlePage
// 29 | 61  MiddlePage > LastPage
// 61 | 13  LastPage   > FirstPage

Dictionary<int, List<int>> pageOrderingRules = [];
List<List<int>> pageNumbersOfUpdates = [];

ReadInputFile(pageOrderingRules, pageNumbersOfUpdates);

// print rules
var sortedRules = pageOrderingRules
  .OrderBy(selector => selector.Value.Count)
  .ToList();
foreach (var rule in sortedRules) {
  Console.Write($"{rule.Key} \t- orders count: {rule.Value.Count}\t");
  foreach (var item in rule.Value) {
    Console.Write($"{item} ");
  }
  Console.WriteLine();
}

var countPart01 = Part01_CountMiddlePageNumberOfCorrectlyOrderedUpdates(pageOrderingRules, pageNumbersOfUpdates);
Console.WriteLine($"\nPart 01: {countPart01}");

var countPart02 = Part02_CountMiddlePageNumberOfIncorrectlyOrderedUpdates_AfterReorder(pageOrderingRules, pageNumbersOfUpdates);
Console.WriteLine($"\nPart 02: {countPart02}");

static int Part01_CountMiddlePageNumberOfCorrectlyOrderedUpdates(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pageNumbersOfUpdates) {
  var (correctlyOrderedUpdates, incorrectlyOrderedUpdates) = FindCorrectlyOrderedUpdates(pageOrderingRules, pageNumbersOfUpdates);
  return CountMiddlePageNumber(correctlyOrderedUpdates);
}

static int Part02_CountMiddlePageNumberOfIncorrectlyOrderedUpdates_AfterReorder(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pageNumbersOfUpdates) {
  var (correctlyOrderedUpdates, incorrectlyOrderedUpdates) = FindCorrectlyOrderedUpdates(pageOrderingRules, pageNumbersOfUpdates);

  var reorderedUpdates = Part02_FindIncorrectlyOrderedUpdatesCanReorder(incorrectlyOrderedUpdates, pageOrderingRules);

  return CountMiddlePageNumber(reorderedUpdates);
}

static List<List<int>> Part02_FindIncorrectlyOrderedUpdatesCanReorder(List<List<int>> incorrectlyOrderedUpdates, Dictionary<int, List<int>> pageOrderingRules) {
  List<List<int>> reorderedUpdates = [];

  foreach (var update in incorrectlyOrderedUpdates) {
    // print incorrectly ordered updates
    Console.Write("\n\n  Input: ");
    foreach (var item in update) {
      Console.Write($"{item} ");
    }

    if (CanReorderIncorectUpdate(update, pageOrderingRules)) {
      reorderedUpdates.Add(update);

      // print incorrectly ordered updates
      Console.Write("\nReorder: ");
      foreach (var item in update) {
        Console.Write($"{item} ");
      }
    }
  }

  return reorderedUpdates;
}

static bool CanReorderIncorectUpdate(List<int> update, Dictionary<int, List<int>> pageOrderingRules) {
  for (int i = 0; i < update.Count - 1; i++) {
    var minId = i;

    for (int j = i + 1; j < update.Count; j++) {
      pageOrderingRules.TryGetValue(update[j], out List<int>? rule);
      pageOrderingRules.TryGetValue(update[minId], out List<int>? reverseRule);

      if (rule != null && rule.Contains(update[minId])) {
        if (reverseRule != null && reverseRule.Contains(update[j])) {
          return false;
        }

        minId = j;
      }
    }

    (update[minId], update[i]) = (update[i], update[minId]);
  }

  return true;
}

static (List<List<int>> correctlyOrderedUpdates, List<List<int>> incorrectlyOrderedUpdates) FindCorrectlyOrderedUpdates(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pageNumbersOfUpdates) {
  List<List<int>> correctlyOrderedUpdates = [];
  List<List<int>> incorrectlyOrderedUpdates = [];

  foreach (var update in pageNumbersOfUpdates) {
    var isCorrectlyOrdered = true;

    for (int idPage = 0; idPage < update.Count - 1; idPage++) {
      for (int idPageAfter = idPage + 1; idPageAfter < update.Count; idPageAfter++) {
        var pageAfter = update[idPageAfter];
        var pagePointerToCheck = update[idPage];

        if (pageOrderingRules.TryGetValue(pageAfter, out List<int>? rule)
          && rule.Contains(pagePointerToCheck)) {
          isCorrectlyOrdered = false;
          break;
        }
      }
    }

    if (isCorrectlyOrdered) {
      correctlyOrderedUpdates.Add(update);
    } else {
      incorrectlyOrderedUpdates.Add(update);
    }
  }

  return (correctlyOrderedUpdates, incorrectlyOrderedUpdates);
}

static int CountMiddlePageNumber(List<List<int>> updates) {
  var count = 0;

  foreach (var update in updates) {
    int idMiddlePage = update.Count / 2;
    count += update[idMiddlePage];
  }

  return count;
}

static void ReadInputFile(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pageNumbersOfUpdates) {
  var lines = InputHelper.ReadInputFile(InputFileName.Input05);
  var isFirstSection = true;

  foreach (var line in lines) {
    if (line == string.Empty) {
      isFirstSection = false;
      continue;
    }

    if (isFirstSection) {
      var pageNumbers = line.Split('|');
      var firstPage = int.Parse(pageNumbers[0]);
      var secondPage = int.Parse(pageNumbers[1]);

      if (!pageOrderingRules.TryGetValue(firstPage, out _)) {
        pageOrderingRules[firstPage] = [];
      }
      pageOrderingRules[firstPage].Add(secondPage);
    } else {
      var pageNumbers = line.Split(',');
      List<int> update = [];

      foreach (var number in pageNumbers) {
        var parsedNumber = int.Parse(number);
        update.Add(parsedNumber);
      }

      pageNumbersOfUpdates.Add(update);
    }
  }
}