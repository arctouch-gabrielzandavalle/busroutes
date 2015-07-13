using System;

namespace busroutes
{
	public class Route
	{
		public Int32 Id { get; set;}
		public String LongName { get; set;}
		public String shortName;
		public DateTime lastModifiedDate;
		public Int32 agencyId;

		public Route (Int32 Id, String LongName){
			this.Id = Id;
			this.LongName = LongName;
		}

		bool equals(Route obj){
			return obj != null && this.Id.Equals (obj.Id);
		}

		public Route ()
		{
			
		}
	}
}

