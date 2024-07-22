using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.ViewModels;

public class Money
	{
		public int Id { get; set; }
		public long? AmountCents { get; set; }
		public long? Amount { get; set;} //take out if not needed, just trying to fix build issues with this
		public string? Currency { get; set; }
		public string? __typename { get; set; }
	}

	public class PriceRecommendation
	{
		public int Id { get; set; }
		public Money? PriceLow { get; set; }
		public Money? PriceMiddle { get; set; }
		public Money? PriceHigh { get; set; }
		public Money? PriceMiddleThirtyDaysAgo { get; set; }
		public string? __typename { get; set; }
	}

	public class PriceRecommendationsResponse
	{
		public int Id { get; set; }
		public List<PriceRecommendation>? PriceRecommendations { get; set; }
		public string? __typename { get; set; }
	}

	public class PriceGuideResponse
	{
		public int Id { get; set; }
		public PriceGuideData? Data { get; set; }

	}

	public class PriceGuideData
	{
		public int Id { get; set; }
		public PriceRecommendationsResponse? PriceRecommendations { get; set; }

	}


