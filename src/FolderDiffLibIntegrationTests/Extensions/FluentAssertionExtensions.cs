using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace TestUtils.Extensions
{
    public static class FluentAssertionExtensions
    {
        public static AndConstraint<StringCollectionAssertions> Contains(this IEnumerable<string> actual, IEnumerable<string> expected,
            IEqualityComparer<string> comparer, string because = "", params object[] reasonArgs)
        {
            var node = new StringCollectionAssertions(actual);

            if (expected == null)
            {
                throw new NullReferenceException("Cannot verify containment against a <null> collection");
            }

            if (!expected.Any())
            {
                throw new ArgumentException("Cannot verify containment against an empty collection");
            }

            if (ReferenceEquals(node.Subject, null))
            {
                Execute.Assertion
                    .BecauseOf(because, reasonArgs)
                    .FailWith("Expected {context:collection} to contain {0}{reason}, but found <null>.", expected);
            }

            var missingItems = expected.Except(node.Subject, comparer);
            if (missingItems.Any())
            {
                if (expected.Count() > 1)
                {
                    Execute.Assertion
                        .BecauseOf(because, reasonArgs)
                        .FailWith("Expected {context:collection} {0} to contain {1}{reason}, but could not find {2}.", node.Subject,
                            expected, missingItems);
                }
                else
                {
                    Execute.Assertion
                        .BecauseOf(because, reasonArgs)
                        .FailWith("Expected {context:collection} {0} to contain {1}{reason}.", node.Subject,
                            expected.Cast<object>().First());
                }
            }

            return new AndConstraint<StringCollectionAssertions>(node);
        }
    }
}