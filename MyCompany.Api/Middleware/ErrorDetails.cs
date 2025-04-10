﻿namespace MyCompany.Api.Middleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public override string ToString() => System.Text.Json.JsonSerializer.Serialize(this);
    }
}