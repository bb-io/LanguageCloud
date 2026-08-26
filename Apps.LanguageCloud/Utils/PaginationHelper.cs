using Apps.LanguageCloud.Models.Responses;

namespace Apps.LanguageCloud.Utils;

internal static class PaginationHelper
{
    private const int DefaultPageSize = 100;

    internal static async Task<List<T>> GetAllAsync<T>(
        Func<int, int, Task<ResponseWrapper<List<T>>>> getPage,
        int pageSize = DefaultPageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

        var items = new List<T>();
        var skip = 0;

        while (true)
        {
            var response = await getPage(pageSize, skip);
            var page = response.Items ?? [];

            items.AddRange(page);

            var reachedItemCount = response.ItemCount.HasValue &&
                                   items.Count >= response.ItemCount.Value;
            var reachedLastPageWithoutItemCount = !response.ItemCount.HasValue &&
                                                  page.Count < pageSize;

            if (page.Count == 0 || reachedItemCount || reachedLastPageWithoutItemCount)
            {
                break;
            }

            skip += page.Count;
        }

        return items;
    }
}
