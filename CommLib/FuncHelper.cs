using System;
using System.Linq.Expressions;

namespace CommLib
{
    /// <summary>
    /// Expression.Lambda表达式辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuncHelper<T> where T : class
    {
        private ParameterExpression param;
        private BinaryExpression filter;

        public FuncHelper()
        {
            param = Expression.Parameter(typeof(T), "c");
            Expression left = Expression.Constant(1);
            filter = Expression.Equal(left, left);
        }

        /// <summary>
        /// 获取表达式
        /// </summary>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetExpression()
        {
            return Expression.Lambda<Func<T, bool>>(filter, param);
        }
        /// <summary>
        /// 条件：相等
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Equal(string propertyName, object value)
        {
            Expression left = Expression.Property(param, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Constant(value, value.GetType());
            Expression result = Expression.Equal(left, right);
            filter = Expression.And(filter, result);
        }
        /// <summary>
        /// 条件拼接
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Contains(string propertyName, string value)
        {
            Expression left = Expression.Property(param, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Constant(value, value.GetType());
            Expression result = Expression.Call(left, typeof(string).GetMethod("Contains"), right);
            filter = Expression.And(filter, result);
        }
    }
}
