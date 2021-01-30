﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using OtripleS.Web.Api.Models.CalendarEntryAttachments;
using OtripleS.Web.Api.Models.CalendarEntryAttachments.Exceptions;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.CalendarEntryAttachments
{
    public partial class CalendarEntryAttachmentServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenSqlExceptionOccursAndLogItAsync()
        {
            // given
            Guid someAttachmentId = Guid.NewGuid();
            Guid someCalendarEntryId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var expectedCalendarEntryAttachmentDependencyException = 
                new CalendarEntryAttachmentDependencyException(sqlException);

            this.storageBrokerMock.Setup(broker =>
                 broker.SelectCalendarEntryAttachmentByIdAsync(someCalendarEntryId, someAttachmentId))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<CalendarEntryAttachment> removeCalendarEntryAttachmentTask =
                this.calendarEntryAttachmentService.RemoveCalendarEntryAttachmentByIdAsync(
                    someCalendarEntryId,
                    someAttachmentId);

            // then
            await Assert.ThrowsAsync<CalendarEntryAttachmentDependencyException>(() => 
                removeCalendarEntryAttachmentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedCalendarEntryAttachmentDependencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectCalendarEntryAttachmentByIdAsync(someCalendarEntryId, someAttachmentId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteCalendarEntryAttachmentAsync(It.IsAny<CalendarEntryAttachment>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

    }
}