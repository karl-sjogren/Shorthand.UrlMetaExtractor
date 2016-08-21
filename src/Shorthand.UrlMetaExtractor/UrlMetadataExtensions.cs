using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Shorthand.UrlMetaExtractor {
    internal static class UrlMetadataExtensions {
        internal static void SetProperty<T>(this UrlMetadata meta, Expression<Func<UrlMetadata, T>> expression, T value) {
            var memberSelectorExpression = expression.Body as MemberExpression;
            if(memberSelectorExpression != null) {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if(property == null)
                    throw new InvalidOperationException("Failed to get property, which shouldn't really happen with an expression.");

                var currentValue = (T)property.GetValue(meta);
                if(currentValue == null)
                    property.SetValue(meta, value, null);
            }
        }

        internal static void PushProperty<T, T2>(this UrlMetadata meta, Expression<Func<UrlMetadata, T>> expression, T2 value) where T: IList<T2> {
            var memberSelectorExpression = expression.Body as MemberExpression;
            if(memberSelectorExpression != null) {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if(property == null)
                    throw new InvalidOperationException("Failed to get property, which shouldn't really happen with an expression.");

                var currentValue = (T)property.GetValue(meta);
                if(currentValue == null)
                    throw new InvalidOperationException("Failed to push property value, property isn't a list.");

                var list = currentValue as List<T2>;
                list.Add(value);

                property.SetValue(meta, list, null);
            }
        }
    }
}