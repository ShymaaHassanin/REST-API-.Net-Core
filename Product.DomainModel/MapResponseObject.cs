﻿using AutoWrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Model
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
        public string Message { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_Details)]
        public string StackTrace { get; set; }
    }
}
