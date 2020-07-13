﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Whp.MaisTop.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> self)
        {
            var properties = typeof(T).GetProperties();

            var dataTable = new DataTable();
            foreach (var info in properties)
                dataTable.Columns.Add(
                    (info.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault() != null ?
                    info.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault().DisplayName : 
                    info.Name),
                    Nullable.GetUnderlyingType(info.PropertyType)
                   ?? info.PropertyType);

            foreach (var entity in self)
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());

            return dataTable;
        }
    }
}
