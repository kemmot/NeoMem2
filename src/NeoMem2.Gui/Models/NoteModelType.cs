using System.ComponentModel;

namespace NeoMem2.Gui.Models
{
    public enum NoteModelType
    {
        [Description("Alphabetical Flat Notes")]
        AlphabeticalFlatNotes,

        [Description("Class Notes")]
        ClassNotes,

        [Description("Property Value Notes")]
        PropertyValueNotes,

        [Description("Flat Property Value Notes")]
        FlatPropertyValueNotes,

        [Description("Structured Notes")]
        StructuredNotes,

        [Description("Scored Flat Notes")]
        ScoredFlatNotes,
    }
}
