using EventHub.EventManagement.Application.Models.LinkModels;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EventHub.EventManagement.Application.Models
{
   public class Entity : DynamicObject, IDictionary<string, object>, IXmlSerializable
   {
      private readonly string _root = "Entity";
      private readonly IDictionary<string, object> _expando;

      public Entity()
      {
         _expando = new ExpandoObject()!;
      }

      public object this[string key]
      {
         get => _expando[key];
         set => _expando[key] = value;
      }

      public ICollection<string> Keys => _expando.Keys;

      public ICollection<object> Values => _expando.Values;

      public int Count => _expando.Count;

      public bool IsReadOnly => _expando.IsReadOnly;

      public void Add(string key, object value) => _expando.Add(key, value);

      public void Add(KeyValuePair<string, object> item) => _expando.Add(item);

      public void Clear() => _expando.Clear();

      public bool Contains(KeyValuePair<string, object> item) => _expando.Contains(item);

      public bool ContainsKey(string key) => _expando.ContainsKey(key);

      public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
      {
         _expando.CopyTo(array, arrayIndex);
      }

      public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
      {
         return _expando.GetEnumerator();
      }

      public XmlSchema? GetSchema()
      {
         throw new NotImplementedException();
      }

      public void ReadXml(XmlReader reader)
      {
         reader.ReadStartElement(_root);

         while (!reader.Name.Equals(_root))
         {
            string typeContent;
            Type underlyingType;
            var name = reader.Name;

            reader.MoveToAttribute("type");
            typeContent = reader.ReadContentAsString();
            underlyingType = Type.GetType(typeContent)!;
            reader.MoveToContent();
            _expando[name] = reader.ReadElementContentAs(underlyingType, null!);

         }
      }

      public bool Remove(string key)
      {
         return _expando.Remove(key);
      }

      public bool Remove(KeyValuePair<string, object> item)
      {
         return _expando.Remove(item);
      }

      public override bool TryGetMember(GetMemberBinder binder, out object? result)
      {
         if (_expando.TryGetValue(binder.Name, out object? value))
         {
            result = value;
            return true;
         }
         return base.TryGetMember(binder, out result);
      }

      public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
      {
         return _expando.TryGetValue(key, out value);
      }

      public override bool TrySetMember(SetMemberBinder binder, object? value)
      {
         _expando[binder.Name] = value!;
         return true;
      }

      public void WriteXml(XmlWriter writer)
      {
         foreach (var key in _expando.Keys)
         {
            var value = _expando[key];
            WriteLinksToXml(key, value, writer);
         }
      }

      private void WriteLinksToXml(string key, object value, XmlWriter writer)
      {
         writer.WriteStartElement(key);
         if (value.GetType() == (typeof(List<Link>)))
         {
            foreach (var val in (List<Link>)value)
            {
               writer.WriteStartElement(nameof(Link));
               WriteLinksToXml(nameof(val.Href), val.Href!, writer);
               WriteLinksToXml(nameof(val.Rel), val.Rel!, writer);
               WriteLinksToXml(nameof(val.Method), val.Method!, writer);
               writer.WriteEndElement();
            }
         }
         else
            writer.WriteString(value.ToString());

         writer.WriteEndElement();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return _expando.GetEnumerator();
      }
   }
}
