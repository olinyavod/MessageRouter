using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessageRouter.Network;

namespace MessageRouter.Models
{
	/// <summary>Модель участника</summary>
	[DataContract]
	public class Meloman
	{
		/// <summary>Идентификатор участника (IMEI-устройства)</summary>
		[DataMember]
		public string Id { get; set; }

		/// <summary>Имя участника</summary>
		[DataMember]
		public string Title { get; set; }

		/// <summary>Участник являеся проигривателем музыки</summary>
		[DataMember]
		public bool IsPlayer { get; set; }

		/// <summary>Адрес пользователя</summary>
		[DataMember]
		public string IpAddress { get; set; }

		/// <summary>Порт участника на который нужно слать сообщения</summary>
		[DataMember]
		public int Port { get; set; }

		/// <summary>Разришения пользователя которые он дал другим пользователям</summary>
		[DataMember]
		IEnumerable<AccessGroups> InnerPermissins { get; set; }

		/// <summary>Разрешения назначеные пользователю пользователем устройства</summary>
		[IgnoreDataMember]
		public IList<AccessGroups> OuterPermissions { get; set; }

		/// <summary>Сколько времени назад отпользователя происходило действие (если это значение боьше 10 секунд то пользователь не всети)</summary>
		[IgnoreDataMember]
		public TimeSpan LastActionTime { set; get; }
	}
}
