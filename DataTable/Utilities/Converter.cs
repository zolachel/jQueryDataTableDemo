using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DataTable.Utilities
{
    public class Converter
    {
        public static string ConvertDateTimeToString(DateTime? date)
        {
            if (date.HasValue) {
                return date.Value.ToString("dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            } else {
                return string.Empty;
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null) {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }
    }
}