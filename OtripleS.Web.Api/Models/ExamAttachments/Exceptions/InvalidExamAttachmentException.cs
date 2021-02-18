﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtripleS.Web.Api.Models.ExamAttachments.Exceptions
{
    public class InvalidExamAttachmentException : Exception
    {
        public InvalidExamAttachmentException(string parameterName, object parameterValue)
           : base($"Invalid Exam Attachment, " +
                 $"ParameterName: {parameterName}, " +
                 $"ParameterValue: {parameterValue}.")
        { }
    }
}
