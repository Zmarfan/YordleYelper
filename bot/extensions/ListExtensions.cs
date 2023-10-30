using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data;
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

    public static bool NullOrEmpty<T>(this IEnumerable<T> iEnumerable) {
        return iEnumerable == null || !iEnumerable.Any();
    }
    
    public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> iEnumerable, Func<TSource, TKey> keySelector, SortOrder sortOrder) {
        return sortOrder == SortOrder.Ascending
            ? iEnumerable.OrderBy(keySelector)
            : iEnumerable.OrderByDescending(keySelector);
    }
}