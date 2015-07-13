using System;

namespace busroutes
{
	public class Stop
	{

		public Int32 Id { get; set;}
		public String Name { get; set;}

		public Stop (Int32 Id, String Name){
			this.Id = Id;
			this.Name = Name;
		}

		public Stop ()
		{
			
		}
	}
}

