using MQKJ.BSMP.Utils.CustomAttribute;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MQKJ.BSMP.Utils.Extensions
{
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties();//.ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties, table);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, DataTable dt) where T : new()
        {
            T item = new T();
            var colName = "";
            object value = new object();
            try
            {
                // 获得此模型的公共属性      
                foreach (PropertyInfo pi in properties)
                {
                    var tempName = pi.Name;  // 检查DataTable是否包含此列    
                    colName = tempName;
                    if (!dt.Columns.Contains(pi.Name) || row[pi.Name] == null || row[pi.Name] == DBNull.Value)
                    {
                        continue;  //DataTable列中不存在集合属性或者字段内容为空则，跳出循环，进行下个循环   
                    }
                    else
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;
                        value = row[tempName];
                        if (value != DBNull.Value && (string)value != "")
                        {
                            var obj = Convert.ChangeType(row[pi.Name], pi.PropertyType);//类型强转，将table字段类型转为集合字段类型
                            pi.SetValue(item, obj, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("列name->" + colName);
                Console.WriteLine("列值->" + value);
                throw ex;
            }
            return item;
        }

        /// <summary>
        /// 获取列名对应的下标
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="cloumnName"></param>
        /// <returns></returns>
        public static int GetColumnByName (ExcelWorksheet ws,string cloumnName)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            var s = ws.Cells["1:1"].First(c => c.Value.ToString() == cloumnName).Start.Column;
            return ws.Cells["1:1"].First(c => c.Value.ToString() == cloumnName).Start.Column;
        }

        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {
            //System.Attribute.GetCustomAttributes
            Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(ExcelColumnAttribute);
            var columns = typeof(T)
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(columnOnly))
                .Select(p => new
                {
                    Property = p,
                    Column = p.GetCustomAttributes<ExcelColumnAttribute>().First().ColumnName
                }).ToList();

            var rows = worksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);

            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var tnew = new T();
                    columns.ForEach(col =>
                    {
                        var val = worksheet.Cells[row, GetColumnByName(worksheet, col.Column)];
                        if (val.Value == null)
                        {
                            col.Property.SetValue(tnew, null);
                            return;
                        }
                        //
                        if (col.Property.PropertyType == typeof(int))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(int?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int?>());
                            return;
                        }
                        //
                        if (col.Property.PropertyType == typeof(double))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(double?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double?>());
                            return;
                        }
                        //
                        if (col.Property.PropertyType == typeof(DateTime?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime?>());
                            return;
                        }
                        //
                        if (col.Property.PropertyType == typeof(DateTime))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime>());
                            return;
                        }
                        //
                        if (col.Property.PropertyType == typeof(bool))
                        {
                            col.Property.SetValue(tnew, val.GetValue<bool>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(string))
                        {
                            col.Property.SetValue(tnew, val.GetValue<string>());
                            return;
                        }
                    });

                    return tnew;
                });
            return collection;
        }
    }
}
