#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using OpenAccessModel;

namespace OpenAccessModel	
{
	public partial class CustomerDemographic
	{
		private string _customerTypeID;
		public virtual string CustomerTypeID
		{
			get
			{
				return this._customerTypeID;
			}
			set
			{
				this._customerTypeID = value;
			}
		}
		
		private string _customerDesc;
		public virtual string CustomerDesc
		{
			get
			{
				return this._customerDesc;
			}
			set
			{
				this._customerDesc = value;
			}
		}
		
		private IList<Customer> _customers = new List<Customer>();
		public virtual IList<Customer> Customers
		{
			get
			{
				return this._customers;
			}
		}
		
	}
}
#pragma warning restore 1591
