using System;
using System.Collections.Generic;
using System.Linq;

namespace YordleYelper.bot.data_fetcher.word_similarity; 

public static class WordSimilarityChecker {
    public static (T, int) FindMostSimilarEntry<T>(string text, IEnumerable<T> entries, Func<T, string> stringProvider) {
        return entries
            .Select(entry => (entry, CheckWorldSimilarity(text, stringProvider.Invoke(entry))))
            .OrderBy(entry => entry.Item2)
            .First();
    }
    
    private static int CheckWorldSimilarity(string text1, string text2) {
        int n = text1.Length;
        int m = text2.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0) {
            return m;
        }

        if (m == 0) {
            return n;
        }

        for (int i = 0; i <= n; d[i, 0] = i++) {
        }

        for (int j = 0; j <= m; d[0, j] = j++) {
        }

        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= m; j++) {
                int cost = text2[j - 1] == text1[i - 1] ? 0 : 1;
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
            }
        }
        
        return d[n, m];
    }
}