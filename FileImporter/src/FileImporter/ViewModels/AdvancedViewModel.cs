using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileImporter.Models;
using DokuFlex.Windows.Common.Services.Data;
using System.IO;

namespace FileImporter.ViewModels
{
    public class AdvancedViewModel
    {
        public AdvancedViewModel(List<DokuField> dokuFields, string fileName)
        {
            _fileName = fileName;
            _dokuFieldList = dokuFields;
            InitViewModel();
        }

        private void InitViewModel()
        {
            if (_dokuFieldList != null)
            {
                foreach (var dokuField in _dokuFieldList)
                {
                    Fields.Add(new FieldInfo
                    {
                        DokuField = dokuField.key,
                    });
                }
            }

            Fields.Add(new FieldInfo { DokuField = "Filename", IsFilePath = true});
        }

        private string[] _fieldNames;
        private string _fileName;
        private List<DokuField> _dokuFieldList;
        private char _delimiter;
        public char Delimiter
        {
            get => _delimiter;
            set
            {
                if (_delimiter == value)
                    return;

                _delimiter = value;

            }
        }
        public bool FirstRowIsHeader { get; set; } = true;
        public FieldInfoCollection Fields { get; private set; } = new FieldInfoCollection();

        public void FillFieldNames()
        {
            _fieldNames = GetFieldNames();
        }

        public string[] GetFieldNames()
        {
            if (FirstRowIsHeader)
            {
                using (var reader = new StreamReader(File.OpenRead(_fileName), Encoding.GetEncoding("iso-8859-1"), false))
                {
                    if (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        return line.Split(Delimiter);
                    }
                }
            }

            return new string[] { };
        }

        public void ClearSourceFieldValues()
        {
            foreach (var fieldInfo in Fields)
            {
                fieldInfo.SourceField = string.Empty;
            }
        }

        private string[] GetFieldValues(string textLine)
        {
            return textLine.Split(_delimiter);
        }

        private int GetFileNameColIndex()
        {
            var fileNameColName = Fields.FirstOrDefault(f => f.DokuField.Equals("Filename"));
            var fieldNames = GetFieldNames();

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (fileNameColName.SourceField.Equals(fieldNames[i]))
                    return i;
            }

            return 0;
        }

        public Dictionary<string, List<DokuField>> GetUploadList()
        {
            var list = new Dictionary<string, List<DokuField>>();
            var isFirstRow = true;
            var spColIndex = GetFileNameColIndex();

            using (var reader = new StreamReader(File.OpenRead(_fileName), Encoding.GetEncoding("iso-8859-1"), false))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (isFirstRow && FirstRowIsHeader)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    var dokuFields = new List<DokuField>();
                    var fieldValues = GetFieldValues(line);

                    for (int i = 0; i < fieldValues.Length; i++)
                    {
                        var fieldName = _fieldNames[i];
                        var dokuField = CreateDokuFieldFromFieldName(fieldName);

                        if (dokuField != null)
                        {
                            dokuField.value = fieldValues[i];
                            dokuFields.Add(dokuField);
                        }
                    }

                    list.Add(fieldValues[spColIndex], dokuFields);

                }
            }

            return list;
        }

        private DokuField CreateDokuFieldFromFieldName(string fieldName)
        {
            var field = Fields.FirstOrDefault(f => f.SourceField.Equals(fieldName));

            if (field == null || field.IsFilePath)
                return null;

            var dkField = _dokuFieldList.FirstOrDefault(f => f.key.Equals(field.DokuField));
            var newDkField = new DokuField
            {
                dokuField = dkField.dokuField,
                id = dkField.id,
                key = dkField.key,
                mandatory = dkField.mandatory,
                text = dkField.text,
                type = dkField.type,
                options = dkField.options
            };

            return newDkField;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
