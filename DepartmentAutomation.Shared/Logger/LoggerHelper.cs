using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DepartmentAutomation.Shared.Logger
{
    public static class LoggerHelper
    {
        public static void LogInformationWithProjectTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            string customMessage)
        {
            logger.LogInformation($"[{className} | {methodName}] {customMessage}.");
        }

        public static void LogInformationWithProjectTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            IEnumerable<string> customMessages)
        {
            foreach (var message in customMessages)
            {
                logger.LogInformation($"[{className} | {methodName}] {message}.");
            }
        }

        public static void LogWarningWithProjectTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            string customMessage)
        {
            logger.LogWarning($"[{className} | {methodName}] Warning: {customMessage}.");
        }

        public static void LogErrorWithProjectTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            Exception exception)
        {
            logger.LogError($"[{className} | {methodName}] ex.Message == {exception.Message}", exception);
        }

        public static void LogErrorWithProjectTemplate<T>(
            this ILogger<T> logger,
            string className,
            string methodName,
            string customMessage,
            Exception exception)
        {
            logger.LogError($"[{className} | {methodName}] {customMessage}; ex.Message == {exception.Message}",
                exception);
        }
    }
}