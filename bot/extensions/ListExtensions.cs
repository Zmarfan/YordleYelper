using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.word_similarity;

namespace YordleYelper.bot.extensions; 

public static class ListExtensions {
    public static bool TryGetSimilarEntry<T>(this List<T> list, string searchPhrase, Func<T, string> nameGetter, out T data) {
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