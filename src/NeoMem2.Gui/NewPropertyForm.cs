using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Gui.Models;

namespace NeoMem2.Gui
{
    public partial class NewPropertyForm : Form
    {
        private Dictionary<string, string> m_PropertyTypes;


        public NewPropertyForm()
        {
            InitializeComponent();
        }


        public string PropertyClrDataType { get { return CboClrDataType.Text; } }
        public string PropertyName { get { return CboName.Text; } }
        public string PropertyValue { get { return CboValue.Text; } }
        public Model Model { get; set; }


        public void SetPropertyTypes(Dictionary<string, string> propertyTypes)
        {
            m_PropertyTypes = propertyTypes;
            foreach (string name in propertyTypes.Keys)
            {
                CboName.Items.Add(name);

                string propertyType = propertyTypes[name];
                if (!CboClrDataType.Items.Contains(propertyType))
                {
                    CboClrDataType.Items.Add(propertyType);
                }
            }
        }

        private void CboNameSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPropertyName = (string)CboName.SelectedItem;

            string clrDataType;
            if (m_PropertyTypes.TryGetValue(selectedPropertyName, out clrDataType))
            {
                CboClrDataType.SelectedItem = clrDataType;
            }

            CboValue.Items.Clear();
            foreach (string existingItem in Model.GetExistingPropertyValues(selectedPropertyName))
            {
                CboValue.Items.Add(existingItem);
            }
        }
    }
}
