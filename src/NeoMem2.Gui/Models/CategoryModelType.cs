using System.ComponentModel;

namespace NeoMem2.Gui.Models
{
    public enum CategoryModelType
    {
        Classes,

        [Description("Initials")]
        Initials,

        [Description("Last Modified")]
        LastModified,

        Multi,

        [Description("Property Values (Flat)")]
        PropertyValuesFlat,

        [Description("Property Values (Structured)")]
        PropertyValuesStructured,

        [Description("Tags (Flat)")]
        TagsFlat,

        [Description("Tags (Structured)")]
        TagsStructured
    }
}
