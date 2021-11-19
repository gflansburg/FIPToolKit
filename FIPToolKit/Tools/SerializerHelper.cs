using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FIPToolKit.Tools
{
    /// <summary>
    /// XML serialization helper class
    /// </summary>
    /// <remarks>XML serialization helper class.</remarks>
    public class SerializerHelper
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>Default constructor</remarks>
        public SerializerHelper()
        {
        }

        /// <summary>
        /// Deserialize a json encoded string into an object.
        /// </summary>
        /// <param name="jsondata">XML encoded string</param>
        /// <param name="objectType">Type of the object</param>
        /// <returns>A <see cref="System.Object"/></returns>
        /// <remarks>Use this method to deserailize a json encoded string into an object of the specified type.</remarks>
        public static object FromJson(string jsondata, Type objectType)
        {
            JsonSerializerSettings jset = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsondata, objectType, jset);
        }

        /// <summary>
        /// Serialize an object into a json encoded string
        /// </summary>
        /// <param name="obj">A <see cref="System.Object"/></param>
        /// <returns>A json encoded string</returns>
        /// <remarks>Use this method to serialize an object into a json encoded string.</remarks>
        public static string ToJson(object obj)
        {
            if (obj != null)
            {
                JsonSerializerSettings jset = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                };
                return JsonConvert.SerializeObject(obj, jset);
            }
            return string.Empty;
        }

        /// <summary>
        /// Deserialize a XML encoded string into an object.
        /// </summary>
        /// <param name="xmldata">XML encoded string</param>
        /// <param name="objectType">Type of the object</param>
        /// <returns>A <see cref="System.Object"/></returns>
        /// <remarks>Use this method to deserailize a XML encoded string into an object of the specified type.</remarks>
        public static object FromXml(string xmldata, Type objectType)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objectType);
            StringReader loginDataString = new StringReader(xmldata);
            return serializer.Deserialize(loginDataString);
        }

        /// <summary>
        /// Serialize an object into a XML encoded string
        /// </summary>
        /// <param name="obj">A <see cref="System.Object"/></param>
        /// <returns>A XML encoded string</returns>
        /// <remarks>Use this method to serialize an object into a XML encoded string.</remarks>
        public static string ToXml(object obj)
        {
            if (obj != null)
            {
                string xmlData = null;
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                // create a MemoryStream here, we are just working
                // exclusively in memory
                using (System.IO.Stream stream = new System.IO.MemoryStream())
                {
                    // The XmlTextWriter takes a stream and encoding
                    // as one of its constructors
                    using (System.Xml.XmlTextWriter xtWriter = new System.Xml.XmlTextWriter(stream, Encoding.UTF8))
                    {
                        serializer.Serialize(xtWriter, obj);
                        xtWriter.Flush();
                        // go back to the beginning of the Stream to read its contents
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        // read back the contents of the stream and supply the encoding
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8))
                        {
                            xmlData = reader.ReadToEnd();
                        }
                    }
                }
                return PrettyXml(xmlData);
            }
            return string.Empty;
        }

        /// <summary>
        /// Remove XML header from a XML encoded string
        /// </summary>
        /// <param name="xmlstring">Original XML string</param>
        /// <returns>A new XML encoded string with out headers</returns>
        /// <remarks>Use this method to remove XML headers.</remarks>
        public static string RemoveHeader(string xmlstring)
        {
            string newXml = xmlstring;
            int start = xmlstring.IndexOf("<?xml");
            int end = xmlstring.IndexOf(">", start + 5);
            if(start != -1)
            {
                newXml = xmlstring.Substring(end + 1);
            }
            return newXml;
        }

        public static string PrettyXml(string xml)
        {
            string result = String.Empty;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode))
                {
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        // Load the XmlDocument with the XML.
                        document.LoadXml(xml);
                        writer.Formatting = System.Xml.Formatting.Indented;
                        // Write the XML into a formatting XmlTextWriter
                        document.WriteContentTo(writer);
                        writer.Flush();
                        mStream.Flush();
                        // Have to rewind the MemoryStream in order to read
                        // its contents.
                        mStream.Position = 0;
                        // Read MemoryStream contents into a StreamReader.
                        using (StreamReader sReader = new StreamReader(mStream))
                        {
                            // Extract the text from the StreamReader.
                            string formattedXml = sReader.ReadToEnd();
                            result = formattedXml;
                        }
                    }
                    catch (XmlException)
                    {
                        // Handle the exception
                    }
                    writer.Close();
                }
                mStream.Close();
            }
            return result;
        }
    }

    public class CustomEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type != null && type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullable = IsNullableType(objectType);
            var enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;
            var names = Enum.GetNames(enumType ?? throw new InvalidOperationException());

            if (reader.TokenType != JsonToken.String) return null;
            var enumText = reader.Value.ToString();

            if (string.IsNullOrEmpty(enumText)) return null;
            var match = names.FirstOrDefault(e => string.Equals(e, enumText, StringComparison.OrdinalIgnoreCase));

            return match != null ? Enum.Parse(enumType, match) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override bool CanWrite => true;

        private static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}