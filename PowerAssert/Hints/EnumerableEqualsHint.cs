﻿using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PowerAssert.Infrastructure;

namespace PowerAssert.Hints
{
    /// <summary>
    /// This hint triggers when you've compared two enumerables with <see cref="object.Equals(object)"/> but
    /// theyy would have compared equal if you had used <see cref="Enumerable.SequenceEqual{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Collections.Generic.IEnumerable{TSource})"/>.
    /// </summary>
    class EnumerableEqualsHint : IHint
    {
        static readonly MethodInfo ObjectInstanceEqualsMethodInfo = typeof (object).GetRuntimeMethods().Single(x => x.Name == "Equals" && !x.IsStatic);

        public bool TryGetHint(ExpressionParser parser, Expression expression, out string hint)
        {
            var methE = expression as MethodCallExpression;
            if (methE != null)
            {
                if (methE.Method == ObjectInstanceEqualsMethodInfo)
                {
                    var obj = parser.DynamicInvoke(methE.Object) as IEnumerable;
                    var arg = parser.DynamicInvoke(methE.Arguments.First()) as IEnumerable;
                    if (obj != null && arg != null)
                    {
                        if (obj.Cast<object>().SequenceEqual(arg.Cast<object>()))
                        {
                            hint = ", but would have been True with .SequenceEqual()";
                            return true;
                        }
                    }
                }
            }

            hint = null;
            return false;
        }
    }
}