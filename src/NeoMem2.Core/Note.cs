// <copyright file="Note.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    using NeoMem2.Core.Scripting;
    using NeoMem2.Utils;
    using NeoMem2.Utils.Collections.Generic;
    using NeoMem2.Utils.ComponentModel;
    using NeoMem2.Utils.ComponentModel.Generic;

    using NLog;

    /// <summary>
    /// A single note of information being tracked.
    /// </summary>
    public class Note : TreeNode<Note>, ICloneable<Note>, INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the <see cref="Attachments"/> property.
        /// </summary>
        public const string PropertyNameAttachments = "Attachments";

        /// <summary>
        /// The name of the <see cref="Class"/> property.
        /// </summary>
        public const string PropertyNameClass = "Class";

        /// <summary>
        /// The name of the <see cref="CreatedDate"/> property.
        /// </summary>
        public const string PropertyNameCreatedDate = "CreatedDate";

        /// <summary>
        /// The name of the <see cref="DeletedDate"/> property.
        /// </summary>
        public const string PropertyNameDeletedDate = "DeletedDate";

        /// <summary>
        /// The name of the <see cref="FormattedText"/> property.
        /// </summary>
        public const string PropertyNameFormattedText = "FormattedText";

        /// <summary>
        /// The name of the <see cref="Id"/> property.
        /// </summary>
        public const string PropertyNameId = "Id";

        /// <summary>
        /// The name of the <see cref="IsPinned"/> property.
        /// </summary>
        public const string PropertyNameIsPinned = "IsPinned";

        /// <summary>
        /// The name of the <see cref="LastAccessedDate"/> property.
        /// </summary>
        public const string PropertyNameLastAccessedDate = "LastAccessedDate";

        /// <summary>
        /// The name of the <see cref="LastModifiedDate"/> property.
        /// </summary>
        public const string PropertyNameLastModifiedDate = "LastModifiedDate";

        /// <summary>
        /// The name of the <see cref="Name"/> property.
        /// </summary>
        public const string PropertyNameName = "Name";

        /// <summary>
        /// The name of the <see cref="Namespace"/> property.
        /// </summary>
        public const string PropertyNameNamespace = "Namespace";

        /// <summary>
        /// The name of the <see cref="Score"/> property.
        /// </summary>
        public const string PropertyNameScore = "Score";

        /// <summary>
        /// The name of the <see cref="Tags"/> property.
        /// </summary>
        public const string PropertyNameTags = "Tags";

        /// <summary>
        /// The name of the <see cref="Text"/> property.
        /// </summary>
        public const string PropertyNameText = "Text";

        /// <summary>
        /// The name of the <see cref="TextFormat"/> property.
        /// </summary>
        public const string PropertyNameTextFormat = "TextFormat";

        /// <summary>
        /// The string to use as a tag delimiter.
        /// </summary>
        public const string TagDelimiter = ";";

        /// <summary>
        /// Backing field for the <see cref="Attachments"/> property.
        /// </summary>
        private readonly ObservableCollection<Attachment> attachments = new ObservableCollection<Attachment>();

        /// <summary>
        /// Backing field for the <see cref="LinkedNotes"/> property.
        /// </summary>
        private readonly ObservableCollection<NoteLink> linkedNotes = new ObservableCollection<NoteLink>();

        /// <summary>
        /// Backing field for the <see cref="Properties"/> property.
        /// </summary>
        private readonly List<Property> properties = new List<Property>();

        /// <summary>
        /// Tracks property changes.
        /// </summary>
        private readonly TrackedPropertyHolder propertyHolder = new TrackedPropertyHolder();

        /// <summary>
        /// Backing field for the <see cref="Tags"/> property.
        /// </summary>
        private readonly ObservableCollection<NoteTag> tags = new ObservableCollection<NoteTag>();

        /// <summary>
        /// Backing field for the <see cref="Tags"/> property.
        /// </summary>
        private readonly TrackedProperty<string> className = new TrackedProperty<string>(PropertyNameClass, string.Empty);

        /// <summary>
        /// Backing field for the <see cref="CreatedDate"/> property.
        /// </summary>
        private readonly TrackedProperty<DateTime> createdDate = new TrackedProperty<DateTime>(PropertyNameCreatedDate, DateTime.MinValue);

        /// <summary>
        /// Backing field for the <see cref="DeletedDate"/> property.
        /// </summary>
        private readonly TrackedProperty<DateTime?> deletedDate = new TrackedProperty<DateTime?>(PropertyNameDeletedDate, null);

        /// <summary>
        /// Backing field for the <see cref="FormattedText"/> property.
        /// </summary>
        private readonly TrackedProperty<string> formattedText = new TrackedProperty<string>(PropertyNameFormattedText, string.Empty);

        /// <summary>
        /// Backing field for the <see cref="Id"/> property.
        /// </summary>
        private readonly TrackedProperty<long> id = new TrackedProperty<long>(PropertyNameId);

        /// <summary>
        /// Backing field for the <see cref="IsPinned"/> property.
        /// </summary>
        private readonly TrackedProperty<bool> isPinned = new TrackedProperty<bool>(PropertyNameIsPinned);

        /// <summary>
        /// Backing field for the <see cref="LastAccessedDate"/> property.
        /// </summary>
        private readonly TrackedProperty<DateTime> lastAccessedDate = new TrackedProperty<DateTime>(PropertyNameLastAccessedDate, DateTime.MinValue);

        /// <summary>
        /// Backing field for the <see cref="LastModifiedDate"/> property.
        /// </summary>
        private readonly TrackedProperty<DateTime> lastModifiedDate = new TrackedProperty<DateTime>(PropertyNameLastModifiedDate, DateTime.MinValue);

        /// <summary>
        /// Backing field for the <see cref="Name"/> property.
        /// </summary>
        private readonly TrackedProperty<string> name = new TrackedProperty<string>(PropertyNameName, string.Empty);

        /// <summary>
        /// Backing field for the <see cref="Namespace"/> property.
        /// </summary>
        private readonly TrackedProperty<string> namespaceName = new TrackedProperty<string>(PropertyNameNamespace, NoteNamespace.Note);

        /// <summary>
        /// Backing field for the <see cref="Text"/> property.
        /// </summary>
        private readonly TrackedProperty<string> text = new TrackedProperty<string>(PropertyNameText, string.Empty);

        /// <summary>
        /// Backing field for the <see cref="TextFormat"/> property.
        /// </summary>
        private readonly TrackedProperty<TextFormat> textFormat = new TrackedProperty<TextFormat>(PropertyNameTextFormat, TextFormat.Txt);

        /// <summary>
        /// Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        public Note()
        {
            this.propertyHolder.PropertyChanged += this.PropertyHolderPropertyChanged;
            this.propertyHolder.Register(this.className);
            this.propertyHolder.Register(this.formattedText);
            this.propertyHolder.Register(this.id);
            this.propertyHolder.Register(this.isPinned);
            this.propertyHolder.Register(this.name);
            this.propertyHolder.Register(this.namespaceName);
            this.propertyHolder.Register(this.text);
            this.propertyHolder.Register(this.textFormat);
            this.propertyHolder.Register(this.linkedNotes);

            this.attachments.CollectionChanged += this.AttachmentsCollectionChanged;
            this.tags.CollectionChanged += this.TagsCollectionChanged;
        }

        /// <summary>
        /// Raised after a tracked property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets this note's attachments.
        /// </summary>
        public ObservableCollection<Attachment> Attachments
        {
            get { return this.attachments; }
        }

        /// <summary>
        /// Gets or sets the unique identifier of this note's class.
        /// </summary>
        public long ClassValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this note has children.
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// Gets the notes linked to this note.
        /// </summary>
        public ObservableCollection<NoteLink> LinkedNotes
        {
            get { return this.linkedNotes; }
        }

        /// <summary>
        /// Gets or sets the name of this note's parent.
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this note's parent.
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// Gets or sets this note's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes of this note.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets this note's tags.
        /// </summary>
        public ObservableCollection<NoteTag> Tags
        {
            get { return this.tags; }
        }

        /// <summary>
        /// Gets all of this note's properties.
        /// </summary>
        public IEnumerable<Property> AllProperties
        {
            get { return this.properties; }
        }

        /// <summary>
        /// Gets or sets this note's class name.
        /// </summary>
        public string Class
        {
            get { return this.className; } 
            set { this.className.Value = value; }
        }

        /// <summary>
        /// Gets or sets the created date of this note.
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; } 
            set { this.createdDate.Value = value; }
        }

        /// <summary>
        /// Gets or sets the deleted date of this note.
        /// </summary>
        public DateTime? DeletedDate
        {
            get { return this.deletedDate; } 
            set { this.deletedDate.Value = value; }
        }

        /// <summary>
        /// Gets or sets the formatted text for this note.
        /// </summary>
        public string FormattedText
        {
            get { return this.formattedText; } 
            set { this.formattedText.Value = value; }
        }

        /// <summary>
        /// Gets or sets the unique identifier of this note.
        /// </summary>
        public long Id
        {
            get { return this.id; } 
            set { this.id.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this note has been deleted.
        /// </summary>
        public bool IsDeleted { get { return DeletedDate.HasValue; } }

        /// <summary>
        /// Gets or sets a value indicating whether this note has been pinned.
        /// </summary>
        public bool IsPinned
        { 
            get { return this.isPinned; } 
            set { this.isPinned.Value = value; } 
        }

        /// <summary>
        /// Gets or sets the last accessed date of this note.
        /// </summary>
        public DateTime LastAccessedDate
        { 
            get { return this.lastAccessedDate; } 
            set { this.lastAccessedDate.Value = value; }
        }

        /// <summary>
        /// Gets or sets the last modified date of this note.
        /// </summary>
        public DateTime LastModifiedDate
        {
            get { return this.lastModifiedDate; } 
            set { this.lastModifiedDate.Value = value; }
        }

        /// <summary>
        /// Gets or sets the name of this note.
        /// </summary>
        public string Name
        {
            get { return this.name; } 
            set { this.name.Value = value; }
        }

        /// <summary>
        /// Gets or sets the namespace of this note.
        /// </summary>
        public string Namespace
        {
            get { return this.namespaceName; } 
            set { this.namespaceName.Value = value; }
        }

        /// <summary>
        /// Gets this note's properties.
        /// </summary>
        public IEnumerable<Property> Properties
        { 
            get
            {
                return this.properties.Where(property => !property.IsDeleted).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the score from the last search.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets a string of all of this note's tags concatenated together.
        /// </summary>
        public string TagText
        {
            get
            {
                string tagText = string.Empty;
                foreach (NoteTag noteTag in this.Tags)
                {
                    if (tagText.Length > 0)
                    {
                        tagText += ";";
                    }

                    tagText += noteTag.Tag.Name;
                }

                return tagText;
            }
        }

        /// <summary>
        /// Gets or sets the text of this note.
        /// </summary>
        public string Text
        {
            get { return this.text; } 
            set { this.text.Value = value; }
        }

        /// <summary>
        /// Gets or sets the format of this note's text.
        /// </summary>
        public TextFormat TextFormat
        {
            get { return this.textFormat; } 
            set { this.textFormat.Value = value; }
        }

        /// <summary>
        /// Links a note to this one.
        /// </summary>
        /// <param name="note">The note to link.</param>
        public void AddLinkedNote(Note note)
        {
            bool found = false;
            foreach (NoteLink linkedNote in this.linkedNotes)
            {
                if ((linkedNote.Note1 == this && linkedNote.Note2 == note)
                    || (linkedNote.Note1 == note && linkedNote.Note2 == this))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                var noteLink = new NoteLink(this, note);
                this.AddLinkedNote(noteLink);
                note.AddLinkedNote(noteLink);
            }
        }

        /// <summary>
        /// Adds a linked note.
        /// </summary>
        /// <param name="noteLink">The link to add.</param>
        public void AddLinkedNote(NoteLink noteLink)
        {
           this.linkedNotes.Add(noteLink);
            noteLink.PropertyChanged += this.PropertyPropertyChanged;
        }

        /// <summary>
        /// Adds a property.
        /// </summary>
        /// <param name="property">The property to add.</param>
        public void AddProperty(Property property)
        {
            this.properties.Add(property);
            property.Note = this;
            property.PropertyChanged += this.PropertyPropertyChanged;
            this.OnPropertyChanged(new PropertyChangedEventArgs(property.Name));
        }

        /// <summary>
        /// Applies a template to this note.
        /// </summary>
        /// <param name="template">The template to apply.</param>
        /// <param name="host">The host in which to run any template scripts.</param>
        public void ApplyTemplate(Note template, IScriptHost host, bool includeText)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            if (template.Namespace != NoteNamespace.NoteTemplate)
            {
                string message = string.Format(
                    "Supplied noteLink is not a template: {0}",
                    template);
                throw new ArgumentException(message, "template");
            }

            using (NestedDiagnosticsContext.Push(string.Format("Note: {0}, apply template: {1}", this.Id, template.Name)))
            {
                ApplyTemplateChanges(template, this, host, includeText);
            }
        }

        /// <summary>
        /// Detaches this note from its original storage so that it appears like a new note to be imported into another store.
        /// </summary>
        public void Detach()
        {
            if (this.Id != 0)
            {
                this.Id = 0;

                foreach (var property in this.AllProperties)
                {
                    property.Detach();
                }

                for (int noteLinkIndex = this.LinkedNotes.Count - 1; noteLinkIndex >= 0; noteLinkIndex--)
                {
                    this.LinkedNotes[noteLinkIndex].Detach();
                }

                for (int attachmentIndex = this.Attachments.Count - 1; attachmentIndex >= 0; attachmentIndex--)
                {
                    this.Attachments[attachmentIndex].Detach();
                }

                for (int tagIndex = this.Tags.Count - 1; tagIndex >= 0; tagIndex--)
                {
                    this.Tags[tagIndex].Detach();
                }
            }
        }

        /// <summary>
        /// Gets a property by name.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The requested property.</returns>
        /// <exception cref="Exception">Thrown if the requested property does not exist.</exception>
        public Property GetProperty(string propertyName)
        {
            Property property;
            if (!TryGetPropertyByName(propertyName, out property))
            {
                throw new Exception("Property not found: " + propertyName);
            }

            return property;
        }

        /// <summary>
        /// Removes a linked note.
        /// </summary>
        /// <param name="noteLink">The link to remove.</param>
        public void RemoveLinkedNote(NoteLink noteLink)
        {
            this.linkedNotes.Remove(noteLink);
            noteLink.PropertyChanged -= this.PropertyPropertyChanged;
        }

        /// <summary>
        /// Removes a property.
        /// </summary>
        /// <param name="propertyName">The name of the property to remove.</param>
        public void RemoveProperty(string propertyName)
        {
            bool found = false;
            foreach (var property in this.Properties)
            {
                if (property.Name == propertyName)
                {
                    this.RemoveProperty(property);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                string message = string.Format(
                    "Property cannnot be found: '{0}'",
                    propertyName);
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Removes a property.
        /// </summary>
        /// <param name="property">The property to remove.</param>
        public void RemoveProperty(Property property)
        {
            property.IsDeleted = true;
            property.PropertyChanged -= this.PropertyPropertyChanged;
        }

        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set.</param>
        public void SetProperty(string name, object value)
        {
            Property property;
            if (!this.TryGetPropertyByName(name, out property))
            {
                property = new Property(name, value, isSystemProperty: false);
                this.AddProperty(property);
            }
            else
            {
                property.Value = value.ToString();
            }
        }

        /// <summary>
        /// Attempts to retrieve a property by name.
        /// </summary>
        /// <param name="name">The name of the property to retrieve.</param>
        /// <param name="foundProperty">The requested property.</param>
        /// <returns>True if the property was found; false otherwise.</returns>
        public bool TryGetPropertyByName(string name, out Property foundProperty)
        {
            foundProperty = this.Properties.FirstOrDefault(property => string.Compare(property.Name, name, true, CultureInfo.InvariantCulture) == 0);
            return foundProperty != null;
        }

        /// <summary>
        /// Clones this object to produce a new Note.
        /// </summary>
        /// <param name="deepCopy">True to perform a deep copy.</param>
        /// <returns>The note clone.</returns>
        public Note Clone(bool deepCopy)
        {
            var note = new Note();
            ApplyTemplateChanges(this, note, null, includeText:true);
            note.CreatedDate = this.CreatedDate;
            note.Id = this.Id;
            note.Namespace = this.Namespace;
            note.LastAccessedDate = this.LastAccessedDate;
            note.LastModifiedDate = this.LastModifiedDate;
            note.Name = this.Name;
            note.Parent = this.Parent;
            note.ParentId = this.ParentId;
            note.ParentName = this.ParentName;
            note.Size = this.Size;
            return note;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        /// <summary>
        /// Applies template changes.
        /// </summary>
        /// <param name="template">The template to apply.</param>
        /// <param name="destination">The note to apply the template to.</param>
        /// <param name="host">The host to use to run any template scripts.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        private static void ApplyTemplateChanges(Note template, Note destination, IScriptHost host, bool includeText)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            destination.Class = template.Name;
            destination.ClassValue = template.ClassValue;
            destination.Description += template.Description;

            foreach (NoteTag noteTag in template.Tags)
            {
                destination.Tags.Add(noteTag);
            }

            if (includeText)
            {
                destination.FormattedText += Environment.NewLine + template.FormattedText;

                int positionOfFirstNewLine = template.Text.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);
                if (positionOfFirstNewLine > 0)
                {
                    string templateTextMinusName = template.Text.Substring(positionOfFirstNewLine);
                    if (templateTextMinusName.Length > 0)
                    {
                        destination.Text += templateTextMinusName;
                    }
                }
            }

            if (template.TextFormat != TextFormat.Txt && destination.TextFormat == TextFormat.Txt)
            {
                destination.TextFormat = template.TextFormat;
            }

            var scriptProperties = new List<Property>();
            foreach (var sourceProperty in template.Properties)
            {
                Property destinationProperty;
                if (!destination.TryGetPropertyByName(sourceProperty.Name, out destinationProperty))
                {
                    destinationProperty = new Property(sourceProperty.Name, sourceProperty.Value);
                    destination.AddProperty(destinationProperty);
                }

                destinationProperty.ClrDataType = sourceProperty.ClrDataType;
                destinationProperty.IsSystemProperty = sourceProperty.IsSystemProperty;

                if (string.IsNullOrEmpty(destinationProperty.Value))
                {
                    string script = sourceProperty.GetScript();
                    if (host != null && !string.IsNullOrEmpty(script))
                    {
                        scriptProperties.Add(sourceProperty);
                    }
                    else
                    {
                        destinationProperty.Value = sourceProperty.Value;
                    }
                }

                if (string.IsNullOrEmpty(destinationProperty.ValueString))
                {
                    destinationProperty.ValueString = sourceProperty.ValueString;
                }
            }

            foreach (var sourceProperty in scriptProperties)
            {
                Property destinationProperty;
                if (!destination.TryGetPropertyByName(sourceProperty.Name, out destinationProperty))
                {
                    destinationProperty = new Property(sourceProperty.Name, sourceProperty.Value);
                    destination.AddProperty(destinationProperty);
                }

                string scriptText = sourceProperty.GetScript();
                if (!string.IsNullOrEmpty(scriptText))
                {
                    var script = new ScriptArguments { ScriptText = scriptText };
                    var scriptResult = host.Execute(script, new Dictionary<string, object> { { "note", destination } });
                    destinationProperty.Value = scriptResult.Output;
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="ObservableCollection{Attachment}.CollectionChanged"/> event of the <see cref="Attachments"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AttachmentsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Attachment newAttachment in e.NewItems)
                {
                    newAttachment.PropertyChanged += this.AttachmentPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Attachment oldAttachment in e.OldItems)
                {
                    oldAttachment.PropertyChanged -= this.AttachmentPropertyChanged;
                }
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameAttachments));
        }

        /// <summary>
        /// Handles the <see cref="Attachment.PropertyChanged"/> event of the <see cref="Attachment"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void AttachmentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameAttachments));
        }

        /// <summary>
        /// Handles the <see cref="ObservableCollection{Tag}.CollectionChanged"/> event of the <see cref="Tags"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TagsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (NoteTag newTag in e.NewItems)
                {
                    newTag.PropertyChanged += this.TagPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (NoteTag oldTag in e.OldItems)
                {
                    oldTag.PropertyChanged -= this.TagPropertyChanged;
                }
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameTags));
        }

        /// <summary>
        /// Handles the <see cref="Tag.PropertyChanged"/> event of the <see cref="Tag"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TagPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameTags));
        }

        /// <summary>
        /// Handles the <see cref="Property.PropertyChanged"/> event of the <see cref="Property"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PropertyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);
        }

        /// <summary>
        /// Handles the <see cref="TrackedPropertyHolder.PropertyChanged"/> event of the <see cref="propertyHolder"/> field.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PropertyHolderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);

            //if (e.PropertyName == PropertyNameText)
            //{
            //    using (StringReader reader = new StringReader(this.text))
            //    {
            //        string newName = reader.ReadLine();
            //        if (!string.IsNullOrEmpty(newName))
            //        {
            //            this.Name = newName;
            //        }
            //    }
            //}
        }
    }
}
