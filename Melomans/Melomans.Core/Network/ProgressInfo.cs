using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	/// <summary>Информация о процессе</summary>
	/// <typeparam name="TMessage">Тип сообщения</typeparam>
	public class ProgressInfo<TMessage>
	{
		public ProgressInfo(TMessage message, ulong total, ulong current)
		{
			Message = message;
			Total = total;
			Current = current;
		}

		/// <summary> Сообщение </summary>
		public TMessage Message { get; private set; }

		/// <summary>Сколько всего</summary>
		public ulong Total { get; private set; }

		/// <summary>Текущеее значение</summary>
		public ulong Current { get; private set; }
	}
}
