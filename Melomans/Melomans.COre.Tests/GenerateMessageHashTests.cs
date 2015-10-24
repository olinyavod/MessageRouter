using System;
using System.Collections.Generic;
using Melomans.Core.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Melomans.Core.Tests
{
	[TestClass]
	public class GenerateMessageHashTests
	{
		[TestMethod]
		public void MustGenereteDiffrentHashForDiffierentString()
		{
			var messageService = new MessageService();
			var hashes = new List<long>();
			for (int i = 0; i < 10000; i++)
			{
				hashes.Add(messageService.CreateMessageHash(new MessageDefinition
				{
					MessageId = Guid.NewGuid().ToString() + i
				}));
			}
			int c = 0;
			while (hashes.Count > 0)
			{
				c++;
				var l = hashes[0];
				hashes.RemoveAt(0);
				Assert.IsFalse(hashes.Contains(l), string.Format("Count: {0} for {1}", c, l));
			}
		}

		[TestMethod]
		public void HashMustEqualForEqualString()
		{
			var messageService = new MessageService();
			var hashes = new List<long>();
			var value = Guid.NewGuid().ToString();
            for (int i = 0; i < 100000; i++)
			{
				hashes.Add(messageService.CreateMessageHash(new MessageDefinition
				{
					MessageId = value
				}));
			}
			var head = hashes[0];
			for (int ci = 1; ci < hashes.Count; ci++)
			{
				Assert.AreEqual(head, hashes[ci]);
			}


		}
	}
}
