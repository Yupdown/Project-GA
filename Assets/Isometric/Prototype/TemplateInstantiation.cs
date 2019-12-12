using System;
using System.Collections.Generic;

namespace Gnome.Template
{
    public static class CSVTemplateInstantiation
    {
        public static InstanceType[] CreateInstancesFromText<InstanceType>(string s) where InstanceType : ITemplateOverride, new()
        {
            string[][] value = CSVParser.Parse(s);

            if (!(value.Length > 0))
                return null;

            string[] fieldNames = value[0];

            List<InstanceType> list = new List<InstanceType>();

            for (int index = 1; index < value.Length; index++)
            {
                TemplateRecord record = new TemplateRecord(fieldNames, value[index]);

                InstanceType instance = new InstanceType();
                instance.Override(record);

                list.Add(instance);
            }

            return list.ToArray();
        }
    }

    public class TemplateRecord
    {
        private Dictionary<string, string> fields;

        public TemplateRecord(string[] keys, string[] values)
        {
            fields = new Dictionary<string, string>();

            for (int index = 0; index < keys.Length && index < values.Length; index++)
            {
                fields.Add(keys[index], values[index]);
            }
        }

        public bool TryGetValue<FieldType>(string key, ref FieldType destination)
        {
            try
            {
                destination = (FieldType)Convert.ChangeType(fields[key], typeof(FieldType));
                return true;
            }
            catch
            {
                UnityEngine.Debug.LogError("Key " + key + " is not found");
                return false;
            }
        }
    }

    public interface ITemplateOverride
    {
        void Override(TemplateRecord record);
    }
}