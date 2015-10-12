using Melomans.Core.Models;
using System;

namespace Melomans.Core.Network
{
	/// <summary>Интерфейс сканера участников</summary>
	public interface IMelomansScanner : IDisposable
	{

		/// <summary>Запустить сканирование участников</summary>
		void Scan();

		/// <summary>Подписаться на событе наденых участников</summary>
		/// <param name="meloman">Найденый участник</param>
		/// <returns></returns>
		IDisposable FindMeloman(Action<Meloman> meloman);
	}
}
