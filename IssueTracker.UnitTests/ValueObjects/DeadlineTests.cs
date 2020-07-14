using FluentAssertions;
using IssueTracker.Domain.Language.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IssueTracker.UnitTests.ValueObjects
{
    public class DeadlineTests
    {
        [Fact]
        public void ShouldNotSetDeadline()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now.AddDays(-1);

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotSetDeadlineAtTheSameDay()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now;

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeFalse();
        }
        [Fact]
        public void ShouldSetDeadline()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now.AddDays(+1);

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeTrue();
        }
    }
}
