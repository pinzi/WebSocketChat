using Hangfire;
using System;
using System.Linq.Expressions;

namespace CommLib
{
    public static class HangfireHelper
    {
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="methodCall">任务调度方法</param>
        /// <returns>任务ID</returns>
        public static string Enqueue(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }
    }
}
