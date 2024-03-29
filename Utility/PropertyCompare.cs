﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Utility
{
    public class PropertyCompare<T> : IEqualityComparer<T>
    {
        #region Reflection
        //private PropertyInfo _PropertyInfo;

        //public PropertyCompare(string propertyName)
        //{
        //    _PropertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        //    if (_PropertyInfo == null)
        //    {
        //        throw new ArgumentException(string.Format("{0} is not a property of type {1}.", propertyName, typeof(T)));
        //    }
        //}

        //public bool Equals(T x, T y)
        //{
        //    object xValue = _PropertyInfo.GetValue(x, null);
        //    object yValue = _PropertyInfo.GetValue(y, null);

        //    if (xValue == null)
        //        return yValue == null;
        //    return xValue.Equals(yValue);
        //}

        //public int GetHashCode(T obj)
        //{
        //    object propertyValue = _PropertyInfo.GetValue(obj, null);

        //    if (propertyValue == null)
        //        return 0;
        //    else
        //        return propertyValue.GetHashCode();
        //}
        #endregion

        #region Expression Tree

        private Func<T, object> getPropertyValueFunc = null;

        public PropertyCompare(string propertyName)
        {
            PropertyInfo _PropertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            if (_PropertyInfo == null)
            {
                throw new ArgumentException(string.Format("{0} is not a property of type {1}.", propertyName, typeof(T)));
            }

            ParameterExpression expPara = Expression.Parameter(typeof(T), "obj");
            MemberExpression me = Expression.Property(expPara, _PropertyInfo);
            getPropertyValueFunc = Expression.Lambda<Func<T, object>>(me, expPara).Compile();
        }

        public bool Equals(T x, T y)
        {
            object xValue = getPropertyValueFunc(x);
            object yValue = getPropertyValueFunc(y);

            if (xValue == null)
                return yValue == null;
            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            object propertyValue = getPropertyValueFunc(obj);

            if (propertyValue == null)
                return 0;
            else
                return propertyValue.GetHashCode();
        }
        #endregion
    }
}
