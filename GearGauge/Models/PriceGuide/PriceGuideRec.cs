using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

	public class PriceRecommendationQuery
	{
		public string? canonicalProductId { get; set; }
		public string? conditionUuid { get; set; }
		public string? countryCode { get; set; }
	}

	//public class Money
	//{
	//	public long AmountCents { get; set; }
	//	public string Currency { get; set; }
	//	public string __typename { get; set; }
	//}

	//public class PriceRecommendation
	//{
	//	public Money PriceLow { get; set; }
	//	public Money PriceMiddle { get; set; }
	//	public Money PriceHigh { get; set; }
	//	public Money PriceMiddleThirtyDaysAgo { get; set; }
	//	public string __typename { get; set; }
	//}

	public class PriceGuideRequestObject
	{
		public string? operationName { get; set; }
		public PriceGuideRequestVariables? variables { get; set; }
		public string? query { get; set; }
	}

	public class PriceGuideRequestVariables
	{
		public List<PriceRecommendationQuery> priceRecommendationQueries { get; set; }
	}