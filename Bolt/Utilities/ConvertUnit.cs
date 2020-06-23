﻿using Bolt;
using Ludiq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Lasm.BoltExtensions
{
    [UnitTitle("Convert")]
    [TypeIcon(typeof(object))]
    [UnitOrder(99)]
    public sealed class ConvertUnit : Unit
    {
        [Inspectable]
        public ConversionType conversion;

        [Inspectable]
        public Type type = typeof(object);

        [PortLabelHidden]
        [DoNotSerialize]
        public ValueInput value;

        [PortLabelHidden]
        [DoNotSerialize]
        public ValueOutput result;

        protected override void Definition()
        {
            value = ValueInput<object>("value");
            result = ValueOutput(type, "result", (flow) => 
            {
                switch (conversion)
                {
                    case ConversionType.Any:
                        return flow.GetValue<object>(value).ConvertTo(type);
                    case ConversionType.ToArrayOfObject:
                        return flow.GetValue<IEnumerable>(value).Cast<object>().ToArray<object>();
                    case ConversionType.ToListOfObject:
                        return flow.GetValue<IEnumerable>(value).Cast<object>().ToList<object>();
                }

                return null;
            });
        }
    }
}