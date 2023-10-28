using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.extensions.word_similarity;

namespace YordleYelper.bot.extensions; 

public static class ListExtensions {
    public static bool TryGetSimilarEntry<T>(this IEnumerable<T> iEnumerable, string searchPhrase, Func<T, string> nameGetter, out T data) {
        List<T> list = iEnumerable.ToList();
        data = default;
        List<T> matches = list.FindAll(item => string.Equals(nameGetter.Invoke(item), searchPhrase, StringComparison.CurrentCultureIgnoreCase));
        if (matches.Any()) {
            data = matches.First();
            return true;
        }
        (T, int) similarEntry = WordSimilarityChecker.FindMostSimilarEntry(searchPhrase, list, nameGetter);
        data = similarEntry.Item1;
        return similarEntry.Item2 <= 3;
    }
}