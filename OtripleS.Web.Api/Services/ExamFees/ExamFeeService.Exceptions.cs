﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using OtripleS.Web.Api.Models.ExamFees;
using OtripleS.Web.Api.Models.ExamFees.Exceptions;

namespace OtripleS.Web.Api.Services.ExamFees
{
    public partial class ExamFeeService
    {
        private delegate ValueTask<ExamFee> ReturningExamFeeFunction();

        private async ValueTask<ExamFee> TryCatch(
            ReturningExamFeeFunction returningExamFeeFunction)
        {
            try
            {
                return await returningExamFeeFunction();
            }

            catch (NullExamFeeException nullExamFeeException)
            {
                throw CreateAndLogValidationException(nullExamFeeException);
            }
            catch (InvalidExamFeeException invalidExamFeeInputException)
            {
                throw CreateAndLogValidationException(invalidExamFeeInputException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsExamFeeException =
                    new AlreadyExistsExamFeeException(duplicateKeyException);

                throw CreateAndLogValidationException(alreadyExistsExamFeeException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidExamFeeReferenceException =
                    new InvalidExamFeeReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogValidationException(invalidExamFeeReferenceException);
            }
        }

        private ExamFeeValidationException CreateAndLogValidationException(Exception exception)
        {
            var ExamFeeValidationException = new ExamFeeValidationException(exception);
            this.loggingBroker.LogError(ExamFeeValidationException);

            return ExamFeeValidationException;
        }
    }
}