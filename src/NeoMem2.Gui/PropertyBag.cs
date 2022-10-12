using System;
using System.Collections.Generic;
using System.ComponentModel;

using NeoMem2.Core;

namespace NeoMem2.Gui
{
    public class PropertyBag : ICustomTypeDescriptor
    {
        private readonly Dictionary<string, PropertyDescriptor> m_Properties = new Dictionary<string, PropertyDescriptor>();

        public void AddProperty(Type type, string name, object value)
        {
            m_Properties[name] = new CustomPropertyDescriptor(type, name, value);
        }

        public void AddProperty(CustomPropertyDescriptor property)
        {
            m_Properties[property.Name] = property;
        }

        public void AddProperty(NotePropertyDescriptor property)
        {
            m_Properties[property.Name] = property;
        }


        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return new AttributeCollection();
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return null;
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)this).GetEvents();
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return new EventDescriptorCollection(new EventDescriptor[0]);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)this).GetProperties();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptor[] properties = new PropertyDescriptor[m_Properties.Count];
            List<string> keys = new List<string>(m_Properties.Keys);
            for (int propertyIndex = 0; propertyIndex < properties.Length; propertyIndex++)
            {
                properties[propertyIndex] = m_Properties[keys[propertyIndex]];
            }
            return new PropertyDescriptorCollection(properties);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }

    public class NotePropertyDescriptor : PropertyDescriptor
    {
        private readonly List<string> m_AvailableValues;
        private readonly string m_Category;
        private readonly Property m_Property;

        public NotePropertyDescriptor(Property property, string category, List<string> availableValues = null)
            : base(property.Name, null)
        {
            m_Property = property;
            m_Category = category;
            m_AvailableValues = availableValues;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override string Category { get { return m_Category; } }

        public override Type ComponentType
        {
            get { return GetType(); }
        }

        public override object GetValue(object component)
        {
            return m_Property.Value;
        }

        public override bool IsReadOnly
        {
            get { return m_Property.IsSystemProperty; }
        }

        public override Type PropertyType
        {
            get
            {
                Type propertyType;
                try
                {
                    propertyType = Type.GetType(m_Property.ClrDataType);
                }
                catch (Exception)
                {
                    propertyType = typeof(string);
                }
                return propertyType;
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            m_Property.Value = value.ToString();
            m_Property.ValueString = value.ToString();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override TypeConverter Converter
        {
            get { return m_AvailableValues != null ? new PropertyConverter(m_AvailableValues) : base.Converter; }
        }
    }

    public class PropertyConverter : StringConverter
    {
        private readonly List<string> m_Values;

        public PropertyConverter(List<string> values)
        {
            m_Values = values;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(m_Values);
        }
    }

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private readonly object m_DefaultValue;
        private readonly Type m_Type;
        private object m_Value;

        public CustomPropertyDescriptor(Type type, string name, object value)
            : this(type, name, value, null)
        {
        }

        public CustomPropertyDescriptor(Type type, string name, object value, object defaultValue)
            : base(name, null)
        {
            m_Type = type;
            m_Value = value;
            m_DefaultValue = defaultValue;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return GetType(); }
        }

        public override object GetValue(object component)
        {
            return m_Value;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return m_Type; }
        }

        public override void ResetValue(object component)
        {
            m_Value = m_DefaultValue;
        }

        public override void SetValue(object component, object value)
        {
            m_Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
