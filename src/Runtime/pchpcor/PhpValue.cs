﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pchp.Core
{
    /// <summary>
    /// Represents a non-aliased PHP value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PhpValue // <T>
    {
        #region Fields

        /// <summary>
        /// Value type.
        /// </summary>
        [FieldOffset(0)]
        PhpTypeCode _type;

        [FieldOffset(4)]
        object _obj;

        [FieldOffset(8)]
        long _long;

        [FieldOffset(8)]
        double _double;

        [FieldOffset(8)]
        bool _bool;

        #endregion

        #region Properties

        public PhpTypeCode TypeCode => _type;

        public long Long
        {
            get
            {
                Debug.Assert(TypeCode == PhpTypeCode.Long);
                return _long;
            }
        }

        public double Double
        {
            get
            {
                Debug.Assert(TypeCode == PhpTypeCode.Double);
                return _double;
            }
        }

        /// <summary>
        /// Gets value indicating whether the value is a <c>NULL</c>.
        /// </summary>
        public bool IsNull => object.ReferenceEquals(_obj, null) && TypeCode == PhpTypeCode.Object;

        #endregion

        #region Construction

        public static PhpValue Create(PhpNumber number)
        {
            return (number.IsLong)
                 ? Create(number.Long)
                 : Create(number.Double);
        }

        public static PhpValue Create(long value)
        {
            return new PhpValue() { _type = PhpTypeCode.Long, _long = value };
        }

        public static PhpValue Create(double value)
        {
            return new PhpValue() { _type = PhpTypeCode.Double, _double = value };
        }

        #endregion
    }
}