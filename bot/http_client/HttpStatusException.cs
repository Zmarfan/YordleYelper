using System;
using System.Collections;
using System.Net;

namespace YordleYelper.bot.http_client; 

public class HttpStatusException : Exception {
    public readonly HttpStatusCode statusCode;
    private readonly Exception exception;
    
    public HttpStatusException(HttpStatusCode statusCode, Exception exception) {
        this.statusCode = statusCode;
        this.exception = exception;
    }

    public override string Message => exception.Message;
    public override IDictionary Data => exception.Data;
    public override string StackTrace => exception.StackTrace;
    public override string HelpLink => exception.HelpLink;
    public override string Source => exception.Source;
}