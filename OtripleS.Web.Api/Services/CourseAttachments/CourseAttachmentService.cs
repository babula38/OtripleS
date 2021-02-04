﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.CourseAttachments;

namespace OtripleS.Web.Api.Services.CourseAttachments
{
    public partial class CourseAttachmentService : ICourseAttachmentService
    {

        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public CourseAttachmentService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<CourseAttachment> RetrieveCourseAttachmentByIdAsync(Guid courseId, Guid attachmentId) =>
        TryCatch(async () =>
        {
            ValidateCourseAttachmentIds(courseId, attachmentId);

            CourseAttachment storageCourseAttachment =
                await this.storageBroker.SelectCourseAttachmentByIdAsync(courseId, attachmentId);

            ValidateStorageCourseAttachment(storageCourseAttachment, courseId, attachmentId);

            return storageCourseAttachment;
        });
    }
}