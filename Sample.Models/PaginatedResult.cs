using System.Collections.Generic;

namespace Sample.Models {
    public class PaginatedResult<TEntity> where TEntity : class {
        public PaginatedResult(IReadOnlyList<TEntity> data, long count, long pageIndex, long pageSize) {
            Data = data;
            Count = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public IReadOnlyList<TEntity> Data { get; }
        public long Count { get; }
        public long PageIndex { get; }
        public long PageSize { get; }
    }
}
