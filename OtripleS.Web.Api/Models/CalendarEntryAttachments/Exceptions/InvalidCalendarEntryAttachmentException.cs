﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;

namespace OtripleS.Web.Api.Models.CalendarEntryAttachments.Exceptions
{
    public class InvalidCalendarEntryAttachmentException : Exception
    {
        public InvalidCalendarEntryAttachmentException(string parameterName, object parameterValue)
            : base(message: $"Invalid calendar entry attachment, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }
    }
}
