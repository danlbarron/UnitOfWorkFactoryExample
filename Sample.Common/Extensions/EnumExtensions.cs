using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Common.Extensions {
    public static class IEnumerableExtensions {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            foreach (var item in source) {
                action(item);
            }
        }
        
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action) {
            var index = 0;
            
            foreach (var item in source) {
                action(item, index++);
            }
        }
        
        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action) {
            foreach (var item in source) {
                await action(item);
            }
        }
        
        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, int, Task> action) {
            var index = 0;
            
            foreach (var item in source) {
                await action(item, index++);
            }
        }
        
        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out IEnumerable<T> rest) {
            var sourceArray = source as T[] ?? source.ToArray();
            first = sourceArray.FirstOrDefault();
            rest = sourceArray.Skip(1);
        }

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out IEnumerable<T> rest)
            => (first, (second, rest)) = source;

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out IEnumerable<T> rest)
            => (first, second, (third, rest)) = source;

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out T fourth, out IEnumerable<T> rest)
            => (first, second, third, (fourth, rest)) = source;

        public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out T fourth, out T fifth, out IEnumerable<T> rest)
            => (first, second, third, fourth, (fifth, rest)) = source;
    }
}