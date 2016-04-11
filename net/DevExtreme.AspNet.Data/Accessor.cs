﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevExtreme.AspNet.Data {

#warning TODO use shared accessor for grouping and aggregation

    class Accessor<T> : ExpressionCompiler {
        IDictionary<string, Func<T, object>> _accessors = new Dictionary<string, Func<T, object>>();

        public object Read(T obj, string selector) {
            if(String.IsNullOrEmpty(selector))
                return null;

            if(!_accessors.ContainsKey(selector)) {
                var param = CreateItemParam(typeof(T));

                _accessors[selector] = Expression.Lambda<Func<T, object>>(
                    Expression.Convert(CompileAccessorExpression(param, selector), typeof(Object)),
                    param
                ).Compile();
            }

            return _accessors[selector](obj);
        }

    }

}
