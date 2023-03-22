﻿namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request eviado tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "Recurso no encontrado",
                500 => "Errores en el servidor",
                _ => string.Empty,
            };
        }
    }
}