
using QU.WordFinderAPI.Cache;
using QU.WordFinderAPI.Domain.Models;
using QU.WordFinderAPI.Interfaces;

namespace QU.WordFinderAPI.Services.Cache
{
    /// <summary>
    /// Provides caching functionality for storing and retrieving word search results based on matrix input.
    /// </summary>
    public class WordFinderCache : IWordFinderCache
    {
        private const string JoinMatrixDelimiter = "_"; // Change the delimiter as needed
        private readonly ICacheHelper _cacheHelper;

        public WordFinderCache(ICacheHelper cacheProvider)
        {
            _cacheHelper = cacheProvider;
        }

        public IEnumerable<string>? Get(WordFinder wordFinder)
        {
            return _cacheHelper.Get<IEnumerable<string>?>(GetMatrixCacheKey(wordFinder.Matrix, wordFinder.WordStream));
        }

        public Task Save(WordFinder wordFinder, IEnumerable<string> foundWords)
        {
            _cacheHelper.Set(GetMatrixCacheKey(wordFinder.Matrix, wordFinder.WordStream), foundWords, null);
            return Task.CompletedTask;
        }

        private string GetMatrixCacheKey(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            string matrixKey = string.Join(JoinMatrixDelimiter, matrix);
            string wordStreamKey = string.Join(JoinMatrixDelimiter, wordStream);
            return $"{matrixKey}_{wordStreamKey}";
        }
    }
}
