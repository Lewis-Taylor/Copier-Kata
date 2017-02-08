using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataCopier;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace KataCopierTest
{
    [TestFixture]
    public class CopyTest
    {
        [Test]
        public void CopysFromSourceToDestination()
        {
            var expectedChar = '0';
            var source = new Source(expectedChar);
            var destination = new Destination();

            var copier = new Copier(source, destination);
            copier.Copy();

            Assert.IsTrue(destination.WasSetCharCalledWithCharParameter(expectedChar));
        }

        [Test]
        public void CopysFromSourceToDestinationWithNewLine()
        {
            var expectedChar = '\n';
            var source = new Source(expectedChar);
            var destination = new Destination();

            var copier = new Copier(source, destination);
            copier.Copy();

            Assert.IsFalse(destination.WasSetCharCalledWithCharParameter(expectedChar));
        }

        [Test]
        public void CopysFromSourceToDestinationUsingMockingFramework()
        {
            var expectedChar = '0';
            var source = new Mock<ISource>();
            source.Setup(x => x.GetChar()).Returns(expectedChar);

            var destination = new Mock<IDestination>();
            
            var copier = new Copier(source.Object, destination.Object);
            copier.Copy();

            destination.Verify(x => x.SetChar(expectedChar));
        }

        [Test]
        public void CopysFromSourceToDestinationWasNotCalledWithNewLineUsingMockingFramework()
        {
            var expectedChar = '\n';
            var source = new Mock<ISource>();
            source.Setup(x => x.GetChar()).Returns(expectedChar);

            var destination = new Mock<IDestination>();

            var copier = new Copier(source.Object, destination.Object);
            copier.Copy();

            destination.Verify(x => x.SetChar(expectedChar), Times.Never);
        }

        [Test] public void CopysMultipleCharsFromSourceToDestinationWasNotCalledWithNewLineUsingMockingFramework()
        {
            var expectedChar = new List<char>();
            var source = Substitute.For<ISource>();
            source.GetChar().Returns('a', 'b', 'c', '\n');

            var destination = Substitute.For<IDestination>();

            var copier = new Copier(source, destination);
            copier.Copy();

            destination.Received(3).SetChar(Arg.Any<char>());
        }
    }
}