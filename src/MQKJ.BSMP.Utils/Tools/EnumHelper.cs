using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MQKJ.BSMP.Utils.Tools
{
    public static class EnumHelper
    {
        /// <summary>
        /// get all information of enum,include value,name and description
        /// </summary>
        /// <param name="enumName">the type of enumName</param>
        /// <returns></returns>
        public static List<EnumModel> GetAllItems(this Type enumName)
        {
            List<EnumModel> list = new List<EnumModel>();
            // get enum fileds
            FieldInfo[] fields = enumName.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }
                // get enum value
                int value = (int)enumName.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                string text = field.Name;
                string description = string.Empty;
                object[] array = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (array.Length > 0)
                {
                    description = ((DescriptionAttribute)array[0]).Description;
                }
                else
                {
                    description = ""; //none description,set empty
                }
                //add to list
                var obj = new EnumModel { Value = value, Text = text, Description = description };
                list.Add(obj);
            }
            return list;
        }
        public class EnumModel
        {
            public int Value { get; set; }

            public string Text { get; set; }

            public string Description { get; set; }
        }
    }
}
