//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Windows.Common.Services.Data
{
    public class DokuField
    {
        public string id { get; set; }

        public string text { get; set; }

        public string type { get; set; }

        public string dokuField { get; set; }

        public int order { get; set; }

        public int mandatory { get; set; }

        public string description { get; set; }

        public string key { get; set; }

        public string options { get; set; }

        public object value { get; set; }

        public static DokuField CreateNew(DokuField dkField)
        {
            var dkf = new DokuField()
            {
                id = dkField.id,
                text = dkField.text,
                type = dkField.type,
                dokuField = dkField.dokuField,
                order = dkField.order,
                mandatory = dkField.mandatory,
                description = dkField.description,
                options = dkField.options,
                value = dkField.value,
                key = dkField.key
            };

            return dkf;
        }
    }
}
