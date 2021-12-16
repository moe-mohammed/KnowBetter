namespace KnowBetter_WebApp.Models
{
    public class APIDeserializer
    {
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://opensearch.org/searchsuggest2", IsNullable = false)]
        public partial class SearchSuggestion
        {

            private SearchSuggestionQuery queryField;

            private SearchSuggestionItem[] sectionField;

            private decimal versionField;

            /// <remarks/>
            public SearchSuggestionQuery Query
            {
                get
                {
                    return this.queryField;
                }
                set
                {
                    this.queryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
            public SearchSuggestionItem[] Section
            {
                get
                {
                    return this.sectionField;
                }
                set
                {
                    this.sectionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        public partial class SearchSuggestionQuery
        {

            private string spaceField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string space
            {
                get
                {
                    return this.spaceField;
                }
                set
                {
                    this.spaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        public partial class SearchSuggestionItem
        {

            private SearchSuggestionItemText textField;

            private SearchSuggestionItemUrl urlField;

            private SearchSuggestionItemImage imageField;

            /// <remarks/>

            public SearchSuggestionItemText Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>

            public SearchSuggestionItemUrl Url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            /// <remarks/>
            public SearchSuggestionItemImage Image
            {
                get
                {
                    return this.imageField;
                }
                set
                {
                    this.imageField = value;
                }
            }

            public object UrlField { get; set; }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        public partial class SearchSuggestionItemText
        {

            private string spaceField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string space
            {
                get
                {
                    return this.spaceField;
                }
                set
                {
                    this.spaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        public partial class SearchSuggestionItemUrl
        {

            private string spaceField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string space
            {
                get
                {
                    return this.spaceField;
                }
                set
                {
                    this.spaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://opensearch.org/searchsuggest2")]
        public partial class SearchSuggestionItemImage
        {

            private string sourceField;

            private byte widthField;

            private byte heightField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string source
            {
                get
                {
                    return this.sourceField;
                }
                set
                {
                    this.sourceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }




            }
        }
    }




    }

