using FluentAssertions;
using IssueTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IssueTracker.UnitTests.Comments
{
    public class CommentTests
    {
        [Fact]
        public void ShouldCreateComment()
        {
            var currentTime = DateTime.Now;
            var comment = Comment.Create("Some description", 5, 123, currentTime);

            comment.IsSuccess.Should().BeTrue();
        }
    }
}
