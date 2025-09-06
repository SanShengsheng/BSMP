using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MQKJ.BSMP.Utils.Tools
{
    public static class XmlHelper
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch
            {
                return null;
            }
        }

        public static T XmlToObject<T>(this string xml)
            where T : class
        {
            var des = Deserialize(typeof(T), xml);
            if (des == null)
            {
                return null;
            }

            return des as T;
        }

        /// <summary>
        /// 对象转xml
        /// </summary>
        /// <param name="o">要转的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的结果</returns>
        public static string XmlSerialize(object o, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, encoding);


                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 对象转xml
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="o"></param>
        /// <param name="encoding"></param>
        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");


            XmlSerializer serializer = new XmlSerializer(o.GetType());


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = " ";


            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string input) where T : class
        {
            if (!input.StartsWith("<?xml"))
                input = @"<?xml version=""1.0"" encoding=""utf-8""?>" + input;
            using (var memoryStream = new MemoryStream(Encoding.Default.GetBytes(input)))
            {
                return DeserializeObject<T>(memoryStream);
                //using (var reader = XmlReader.Create(memoryStream))
                //{
                //    var formatter = new XmlSerializer(typeof(T));
                //    return formatter.Deserialize(reader) as T;
                //}
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var str = reader.ReadToEnd();
                str = str.Replace("gb2312", "utf-8");
                var xmlSerial = new XmlSerializer(typeof(T));
                using (var rdr = new StringReader(str))
                {
                    return (T)xmlSerial.Deserialize(rdr);
                }
            }
        }

        /// <summary>
        /// xml处理
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetXmlValue(string strXml, string strData)
        {
            string xmlValue = string.Empty;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(strXml);
            var selectSingleNode = xmlDocument.DocumentElement.SelectSingleNode(strData);
            if (selectSingleNode != null)
            {
                xmlValue = selectSingleNode.InnerText;
            }
            return xmlValue;
        }

        /// <summary>
        /// 集合转换XML数据 (拼接成XML请求数据)
        /// </summary>
        /// <param name="strParam">参数</param>
        /// <returns></returns>
        public static string CreateXmlParam(IDictionary<string, string> strParam)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<xml>");
                foreach (KeyValuePair<string, string> k in strParam)
                {
                    if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                    {
                        sb.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                    }
                    else
                    {
                        sb.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                    }
                }
                sb.Append("</xml>");
            }
            catch (Exception)
            {
                //Utility.AddLog("PayHelper", "CreateXmlParam", ex.Message, ex);
            }

            return sb.ToString();
        }

        /// <summary>
        /// XML数据转换集合（XML数据拼接成字符串)
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string> GetFromXml(string xmlString)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlElement root = doc.DocumentElement;
                int len = root.ChildNodes.Count;
                for (int i = 0; i < len; i++)
                {
                    string name = root.ChildNodes[i].Name;
                    if (!sParams.ContainsKey(name))
                    {
                        sParams.Add(name.Trim(), root.ChildNodes[i].InnerText.Trim());
                    }
                }
            }
            catch (Exception)
            {
                //.AddLog("PayHelper", "GetFromXml", ex.Message, ex);
            }
            return sParams;
        }
    }
}
