﻿using System;
using System.Reflection;
using System.Text;

namespace Common
{
    /*模型的帮助类*/
    public class MyModelHelper
    {
        public static string MakeIniString(Object obj, bool removeLastSplit = true)
        {
            string temp = MakeIniStringExt(obj, removeLastSplit: removeLastSplit);
            return temp;
        }

        public static string MakeIniStringExt(Object obj, string equalOperator = "=", string lastSplit = ";", bool removeLastSplit = true)
        {
            string schema = string.Format("{0}{1}{2}{3}", "{0}", equalOperator, "{1}", lastSplit);
            StringBuilder sb = new StringBuilder();
            if (obj != null)
            {
                //获取类型信息
                Type t = obj.GetType();
                PropertyInfo[] propertyInfos = t.GetProperties();
                foreach (PropertyInfo var in propertyInfos)
                {
                    object value = var.GetValue(obj, null);
                    string temp = "";

                    //如果是string，并且为null
                    if (value == null)
                    {
                        temp = "";
                    }
                    else
                    {
                        temp = value.ToString();
                    }

                    value = temp.Replace(lastSplit, "=");
                    sb.AppendFormat(schema, var.Name, value);
                }
            }
            //去掉最后的分号
            if (removeLastSplit)
            {
                string result = sb.ToString();
                return result.Substring(0, result.Length - 1);
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}
