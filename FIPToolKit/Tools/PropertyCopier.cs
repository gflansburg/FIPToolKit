using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Tools
{
    public class PropertyCopier<TParent, TChild> where TParent : class where TChild : class
    {
        public static void Copy(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                try
                {
                    MethodInfo setMethod = parentProperty.GetSetMethod();
                    if (setMethod != null)
                    {
                        if (!parentProperty.GetCustomAttributes(false).Any(a => a is XmlIgnoreAttribute))
                        {
                            foreach (var childProperty in childProperties)
                            {
                                if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                                {
                                    childProperty.SetValue(child, parentProperty.GetValue(parent));
                                    break;
                                }
                            }
                        }
                    }
                }
                catch(Exception)
                {

                }
            }
        }
    }
}
