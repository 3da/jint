﻿using System.Linq;
using System.Threading.Tasks;

namespace Jint.Runtime.Interop
{
    public static class AsyncHelpers
    {
        // The idea of this helper was to avoid async constructs in javascript. So this one just auto awaits on any task type.
        // I realize it might be better to just return the Task and handle the await in JS to give more control to the developer (cancellation and timeout)
        // However, it would also be useful in many cases to just have this done automatically by the helper, to keep the JS code clean - so maybe it could be an optional feature w/timeout.
        // I'm leaving it here, as this is what I use at the moment

        public async static Task<object> AwaitWhenAsyncResult(this object callResult)
        {
            if (!(callResult is Task task)) return callResult;

            await task;

            // Return the result, unless it's a VoidTaskResult (guard needed to avoid dynamic exception, when accessing .Result on a VoidTaskResult)
            return IsVoidTaskResult(task)
                ? null
                : (object)((dynamic)task)?.Result;
        }

        private static bool IsVoidTaskResult(Task task)
        {
            // VoidTaskResult is an internal Microsoft class used as Task<VoidTaskResult> which correlates to the standard non generic Task
            var taskType = task.GetType();
            if (taskType == typeof(Task))
                return true;
            return taskType.IsGenericType
                && taskType.GenericTypeArguments[0].Name == "VoidTaskResult";
        }
    }
}