using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class SearchResponse
	{
		public SearchData data { get; set; }
	}

	public class SearchData 
	{
		public Cspsearch cspSearch { get; set; }
	}

	public class Cspsearch
	{
		public List<ReverbSearchFilter> filters { get; set; }
		public List<CanonicalSearchViewModel> csps { get; set; }
		public int total { get; set; }
		public int offset { get; set; }
		public int limit { get; set; }
		public string __typename { get; set; }
	}

	 public class ReverbSearchFilterOption
	{
		//public GoogleProtobufInt32Value Count { get; set; }
		public string Name { get; set; }
		public bool Selected { get; set; }
		public string ParamName { get; set; }
		public List<string> SetValues { get; set; }
		public List<string> UnsetValues { get; set; }
		public bool All { get; set; }
		public string OptionValue { get; set; }
		public string __typename { get; set; }
	}

	public class ReverbSearchFilter
	{
		public string Name { get; set; }
		public string Key { get; set; }
		public string AggregationName { get; set; }
		public string WidgetType { get; set; }
		public List<ReverbSearchFilterOption> Options { get; set; }
		public string __typename { get; set; }
	}

	// public class CSP
	// {
	// 	//public string _id { get; set; }
	// 	//public string Id { get; set; }
	// 	public string Title { get; set; }
	// 	// public List<string> Finishes { get; set; }
	// 	public List<string> CanonicalProductIds { get; set; }
	// 	//public CoreApimessagesImage Image { get; set; }
	// 	//public string Slug { get; set; }
	// 	//public Brand Brand { get; set; }
	// 	//public string __typename { get; set; }
	// }

	public class CoreApimessagesImage
	{
		public string Source { get; set; }
		public string __typename { get; set; }
	}

	public class Brand
	{
		public string _id { get; set; }
		public string Name { get; set; }
		public string __typename { get; set; }
	}