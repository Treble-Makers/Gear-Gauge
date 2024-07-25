using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GearGauge.Controllers;


// [Route("api/[controller]")]
// [ApiController]
public class ProductSearchController : Controller
    {
		private readonly IConfiguration _configuration;
		public ProductSearchController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
    // [Authorize]
    [HttpPost]
	[Route("SearchResults")]
    public async Task<IActionResult> PostSearch(ProductSearchViewModel productSearchViewModel)
	{
        using (var client = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            var baseAddress = "https://rql.reverb.com";
            var api = "/graphql";
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(contentType);

            SearchRequestObject queryObject = new SearchRequestObject()
            {
                operationName = "Core_SellFlow_Search",
                variables = new SearchRequestVariables()
                {
                    offset = 0,
                    sellCardLimit = 2,
                    q = productSearchViewModel.SearchTerm,
                    excludedCategoryUuids = new string[]
                    {
                        "7681b711-435c-4923-bdc3-65076d15d78c",
                        "98a45e2d-2cc2-4b17-b695-a5d198c8f6d3",
                        "4ca6d5e9-f00f-468d-bcae-8c7497537281",
                        "22af0079-d5e7-48d1-9e5c-108105a2156c"
                    },
                    fullTextQueryOperand = "AND",
                    sort = "RECENT_ORDERS_COUNT_USED_DESC",
                    fuzzy = true,
                    listingsThatShipTo = "XX",
                    hasExpressSaleBid = false,
                    includePriceRecommendations = false
                },
                query =
                    "query Core_SellFlow_Search($q: String, $decades: [String], $finishes: [String], $brandNames: [String], $category_uuids: [String], $sellCardLimit: Int, $excludedCategoryUuids: [String], $boostByClicks: Boolean, $fullTextQueryOperand: reverb_search_FullTextQueryOperand, $sort: reverb_search_CSPSearchRequest_Sort, $fuzzy: Boolean, $offset: Int, $listingsThatShipTo: String, $hasExpressSaleBid: Boolean!, $includePriceRecommendations: Boolean!) {\n  cspSearch(\n    input: {fullTextQuery: $q, decades: $decades, finishes: $finishes, brandNames: $brandNames, categoryUuids: $category_uuids, withAggregations: [CATEGORY_UUIDS, FINISHES, DECADES, BRAND_NAMES], excludedCategoryUuids: $excludedCategoryUuids, limit: $sellCardLimit, offset: $offset, boostByClicks: $boostByClicks, fullTextQueryOperand: $fullTextQueryOperand, sort: $sort, fuzzy: $fuzzy, listingsThatShipTo: $listingsThatShipTo, hasExpressSaleBid: $hasExpressSaleBid}\n  ) {\n    filters {\n      ...FlatFilter\n      __typename\n    }\n    csps {\n      _id\n      ...SellCardData\n      ...PriceRecommendationData @include(if: $includePriceRecommendations)\n      ...ExpressSaleItemBidData @include(if: $hasExpressSaleBid)\n      __typename\n    }\n    total\n    offset\n    limit\n    __typename\n  }\n}\n\nfragment FlatFilter on reverb_search_Filter {\n  name\n  key\n  aggregationName\n  widgetType\n  options {\n    count {\n      value\n      __typename\n    }\n    name\n    selected\n    paramName\n    setValues\n    unsetValues\n    all\n    optionValue\n    __typename\n  }\n  __typename\n}\n\nfragment PriceRecommendationData on CSP {\n  _id\n  priceRecommendations(\n    input: {conditionUuids: [\"f7a3f48c-972a-44c6-b01a-0cd27488d3f6\", \"ac5b9c1e-dc78-466d-b0b3-7cf712967a48\"]}\n  ) {\n    conditionUuid\n    priceLow {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n\nfragment SellCardData on CSP {\n  _id\n  id\n  title\n  finishes\n  canonicalProductIds\n  image(input: {transform: \"card_square\"}) {\n    source\n    __typename\n  }\n  slug\n  brand {\n    _id\n    name\n    __typename\n  }\n  __typename\n}\n\nfragment ExpressSaleItemBidData on CSP {\n  _id\n  expressSalePriceEstimate(\n    input: {conditionUuid: \"ae4d9114-1bd7-4ec5-a4ba-6653af5ac84d\"}\n  ) {\n    priceLow {\n      amountCents\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      currency\n      __typename\n    }\n    __typename\n  }\n  expressSaleItemBid {\n    cspUuid\n    bidId\n    carrier\n    shopName\n    estimatedPayout {\n      display\n      amount\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n"
            };

            var jsonData = JsonConvert.SerializeObject(queryObject);
            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(api, contentData);

            SearchResponse responseObject = null;
            if (response.IsSuccessStatusCode)
            {
                responseObject = JsonConvert.DeserializeObject<SearchResponse>(
                    await response.Content.ReadAsStringAsync()
                );
                var canonicalSearchViewModel = new List<CanonicalSearchViewModel>();
                Console.WriteLine("Search submitted successfully!");

                foreach (var results in responseObject.data.cspSearch.csps)
                {
                    var priceRecommendation = await PostGetPriceGuide(results.CanonicalProductIds.FirstOrDefault());
                    results.PriceMiddle = ((priceRecommendation?.PriceMiddle?.AmountCents ?? 0)/100).ToString();
                    results.PriceMiddleThirtyDaysAgo = ((priceRecommendation?.PriceMiddleThirtyDaysAgo?.AmountCents ?? 0)/100).ToString();
                
                    if (canonicalSearchViewModel != null)
                    // {
                    //     var firstProductId = canonicalSearchViewModel.CanonicalProductIds[0];
                    //     if (firstProductId != null)
                        {
                            var youtubeApiClient = new YouTubeApiController();
                            var videos = await youtubeApiClient.GetProductVideo(results.Title);

                            if (videos != null)
                            {
                                results.Videos = videos;
                            }
                        }
                }
                //dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());



                canonicalSearchViewModel = responseObject.data.cspSearch.csps;
				// if (canonicalSearchViewModel.Any())
                // {
                //     var firstProductId = canonicalSearchViewModel.FirstOrDefault()?.CanonicalProductIds[0];
                //     if (firstProductId != null)
                //     {
                //         var videos = await YouTubeApiController.GetProductVideo(canonicalSearchViewModel);

                //         if (videos.IsSuccessStatusCode)
                //         {
                //             var videoDetailsResponse = videos as OkObjectResult;
                //             var videoDetails = videoDetailsResponse?.Value as VideoDetailsResponse;
                //             ViewBag.Videos = videoDetails?.Videos;
                //         }
                //     }
                Console.WriteLine(canonicalSearchViewModel);
                return View("SearchResults", canonicalSearchViewModel);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                // return View("Error", "Shared");
            }
            // return responseObject.data.cspSearch.csps;
            return View("Index", "Home");
        }
    }

    [HttpPost]
	[Route("PriceResults")]
    public async Task<IActionResult> PostSearchFinal(ProductSearchViewModel productSearchViewModel)
	{
        using (var client = new HttpClient())
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            var baseAddress = "https://rql.reverb.com";
            var api = "/graphql";
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(contentType);

            SearchRequestObject queryObject = new SearchRequestObject()
            {
                operationName = "Core_SellFlow_Search",
                variables = new SearchRequestVariables()
                {
                    offset = 0,
                    sellCardLimit = 9,
                    q = productSearchViewModel.SearchTerm,
                    excludedCategoryUuids = new string[]
                    {
                        "7681b711-435c-4923-bdc3-65076d15d78c",
                        "98a45e2d-2cc2-4b17-b695-a5d198c8f6d3",
                        "4ca6d5e9-f00f-468d-bcae-8c7497537281",
                        "22af0079-d5e7-48d1-9e5c-108105a2156c"
                    },
                    fullTextQueryOperand = "AND",
                    sort = "RECENT_ORDERS_COUNT_USED_DESC",
                    fuzzy = true,
                    listingsThatShipTo = "XX",
                    hasExpressSaleBid = false,
                    includePriceRecommendations = false
                },
                query =
                    "query Core_SellFlow_Search($q: String, $decades: [String], $finishes: [String], $brandNames: [String], $category_uuids: [String], $sellCardLimit: Int, $excludedCategoryUuids: [String], $boostByClicks: Boolean, $fullTextQueryOperand: reverb_search_FullTextQueryOperand, $sort: reverb_search_CSPSearchRequest_Sort, $fuzzy: Boolean, $offset: Int, $listingsThatShipTo: String, $hasExpressSaleBid: Boolean!, $includePriceRecommendations: Boolean!) {\n  cspSearch(\n    input: {fullTextQuery: $q, decades: $decades, finishes: $finishes, brandNames: $brandNames, categoryUuids: $category_uuids, withAggregations: [CATEGORY_UUIDS, FINISHES, DECADES, BRAND_NAMES], excludedCategoryUuids: $excludedCategoryUuids, limit: $sellCardLimit, offset: $offset, boostByClicks: $boostByClicks, fullTextQueryOperand: $fullTextQueryOperand, sort: $sort, fuzzy: $fuzzy, listingsThatShipTo: $listingsThatShipTo, hasExpressSaleBid: $hasExpressSaleBid}\n  ) {\n    filters {\n      ...FlatFilter\n      __typename\n    }\n    csps {\n      _id\n      ...SellCardData\n      ...PriceRecommendationData @include(if: $includePriceRecommendations)\n      ...ExpressSaleItemBidData @include(if: $hasExpressSaleBid)\n      __typename\n    }\n    total\n    offset\n    limit\n    __typename\n  }\n}\n\nfragment FlatFilter on reverb_search_Filter {\n  name\n  key\n  aggregationName\n  widgetType\n  options {\n    count {\n      value\n      __typename\n    }\n    name\n    selected\n    paramName\n    setValues\n    unsetValues\n    all\n    optionValue\n    __typename\n  }\n  __typename\n}\n\nfragment PriceRecommendationData on CSP {\n  _id\n  priceRecommendations(\n    input: {conditionUuids: [\"f7a3f48c-972a-44c6-b01a-0cd27488d3f6\", \"ac5b9c1e-dc78-466d-b0b3-7cf712967a48\"]}\n  ) {\n    conditionUuid\n    priceLow {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n\nfragment SellCardData on CSP {\n  _id\n  id\n  title\n  finishes\n  canonicalProductIds\n  image(input: {transform: \"card_square\"}) {\n    source\n    __typename\n  }\n  slug\n  brand {\n    _id\n    name\n    __typename\n  }\n  __typename\n}\n\nfragment ExpressSaleItemBidData on CSP {\n  _id\n  expressSalePriceEstimate(\n    input: {conditionUuid: \"ae4d9114-1bd7-4ec5-a4ba-6653af5ac84d\"}\n  ) {\n    priceLow {\n      amountCents\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      currency\n      __typename\n    }\n    __typename\n  }\n  expressSaleItemBid {\n    cspUuid\n    bidId\n    carrier\n    shopName\n    estimatedPayout {\n      display\n      amount\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n"
            };

            var jsonData = JsonConvert.SerializeObject(queryObject);
            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(api, contentData);

            SearchResponse responseObject = null;
            if (response.IsSuccessStatusCode)
            {
                responseObject = JsonConvert.DeserializeObject<SearchResponse>(
                    await response.Content.ReadAsStringAsync()
                );
                var canonicalSearchViewModel = new List<CanonicalSearchViewModel>();
                Console.WriteLine("Search submitted successfully!");

                foreach (var results in responseObject.data.cspSearch.csps)
                {
                    var priceRecommendation = await PostGetPriceGuide(results.CanonicalProductIds.FirstOrDefault());
                    results.PriceMiddle = (priceRecommendation.PriceMiddle.AmountCents/100).ToString();
                    results.PriceMiddleThirtyDaysAgo = (priceRecommendation.PriceMiddleThirtyDaysAgo.AmountCents/100).ToString();
                    if (canonicalSearchViewModel != null)
                    // {
                    //     var firstProductId = canonicalSearchViewModel.CanonicalProductIds[0];
                    //     if (firstProductId != null)
                        {
                            var youtubeApiClient = new YouTubeApiController();
                            var videos = await youtubeApiClient.GetProductVideo(results.Title);

                            if (videos != null)
                            {
                                results.Videos = videos;
                            }
                        }
                }
                //dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                canonicalSearchViewModel = responseObject.data.cspSearch.csps;
				// if (canonicalSearchViewModel.Any())
                // {
                //     var firstProductId = canonicalSearchViewModel.FirstOrDefault()?.CanonicalProductIds[0];
                //     if (firstProductId != null)
                //     {
                //         var videos = await YouTubeApiController.GetProductVideo(canonicalSearchViewModel);

                //         if (videos.IsSuccessStatusCode)
                //         {
                //             var videoDetailsResponse = videos as OkObjectResult;
                //             var videoDetails = videoDetailsResponse?.Value as VideoDetailsResponse;
                //             ViewBag.Videos = videoDetails?.Videos;
                //         }
                //     }
                Console.WriteLine(canonicalSearchViewModel);
                return View("PriceResults", canonicalSearchViewModel);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                // return View("Error", "Shared");
            }
            // return responseObject.data.cspSearch.csps;
            return View("Index", "Home");
        }
    }

    // [HttpPost]
	// // [Route("PriceResults")]
    // public async Task<IActionResult> PostTotalSearch(ProductSearchViewModel productSearchViewModel)
    // {
    //     using (var client = new HttpClient())
    //     {
    //         var contentType = new MediaTypeWithQualityHeaderValue("application/json");
    //         var baseAddress = "https://rql.reverb.com";
    //         var api = "/graphql";
    //         client.BaseAddress = new Uri(baseAddress);
    //         client.DefaultRequestHeaders.Accept.Add(contentType);

    //         SearchRequestObject queryObject = new SearchRequestObject()
    //         {
    //             operationName = "Core_SellFlow_Search",
    //             variables = new SearchRequestVariables()
    //             {
    //                 offset = 0,
    //                 sellCardLimit = 1,
    //                 q = productSearchViewModel.SearchTerm,
    //                 excludedCategoryUuids = new string[]
    //                 {
    //                     "7681b711-435c-4923-bdc3-65076d15d78c",
    //                     "98a45e2d-2cc2-4b17-b695-a5d198c8f6d3",
    //                     "4ca6d5e9-f00f-468d-bcae-8c7497537281",
    //                     "22af0079-d5e7-48d1-9e5c-108105a2156c"
    //                 },
    //                 fullTextQueryOperand = "AND",
    //                 sort = "RECENT_ORDERS_COUNT_USED_DESC",
    //                 fuzzy = true,
    //                 listingsThatShipTo = "XX",
    //                 hasExpressSaleBid = false,
    //                 includePriceRecommendations = false
    //             },
    //             query =
    //                 "query Core_SellFlow_Search($q: String, $decades: [String], $finishes: [String], $brandNames: [String], $category_uuids: [String], $sellCardLimit: Int, $excludedCategoryUuids: [String], $boostByClicks: Boolean, $fullTextQueryOperand: reverb_search_FullTextQueryOperand, $sort: reverb_search_CSPSearchRequest_Sort, $fuzzy: Boolean, $offset: Int, $listingsThatShipTo: String, $hasExpressSaleBid: Boolean!, $includePriceRecommendations: Boolean!) {\n  cspSearch(\n    input: {fullTextQuery: $q, decades: $decades, finishes: $finishes, brandNames: $brandNames, categoryUuids: $category_uuids, withAggregations: [CATEGORY_UUIDS, FINISHES, DECADES, BRAND_NAMES], excludedCategoryUuids: $excludedCategoryUuids, limit: $sellCardLimit, offset: $offset, boostByClicks: $boostByClicks, fullTextQueryOperand: $fullTextQueryOperand, sort: $sort, fuzzy: $fuzzy, listingsThatShipTo: $listingsThatShipTo, hasExpressSaleBid: $hasExpressSaleBid}\n  ) {\n    filters {\n      ...FlatFilter\n      __typename\n    }\n    csps {\n      _id\n      ...SellCardData\n      ...PriceRecommendationData @include(if: $includePriceRecommendations)\n      ...ExpressSaleItemBidData @include(if: $hasExpressSaleBid)\n      __typename\n    }\n    total\n    offset\n    limit\n    __typename\n  }\n}\n\nfragment FlatFilter on reverb_search_Filter {\n  name\n  key\n  aggregationName\n  widgetType\n  options {\n    count {\n      value\n      __typename\n    }\n    name\n    selected\n    paramName\n    setValues\n    unsetValues\n    all\n    optionValue\n    __typename\n  }\n  __typename\n}\n\nfragment PriceRecommendationData on CSP {\n  _id\n  priceRecommendations(\n    input: {conditionUuids: [\"f7a3f48c-972a-44c6-b01a-0cd27488d3f6\", \"ac5b9c1e-dc78-466d-b0b3-7cf712967a48\"]}\n  ) {\n    conditionUuid\n    priceLow {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      amount\n      currency\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n\nfragment SellCardData on CSP {\n  _id\n  id\n  title\n  finishes\n  canonicalProductIds\n  image(input: {transform: \"card_square\"}) {\n    source\n    __typename\n  }\n  slug\n  brand {\n    _id\n    name\n    __typename\n  }\n  __typename\n}\n\nfragment ExpressSaleItemBidData on CSP {\n  _id\n  expressSalePriceEstimate(\n    input: {conditionUuid: \"ae4d9114-1bd7-4ec5-a4ba-6653af5ac84d\"}\n  ) {\n    priceLow {\n      amountCents\n      currency\n      __typename\n    }\n    priceHigh {\n      amountCents\n      currency\n      __typename\n    }\n    __typename\n  }\n  expressSaleItemBid {\n    cspUuid\n    bidId\n    carrier\n    shopName\n    estimatedPayout {\n      display\n      amount\n      __typename\n    }\n    __typename\n  }\n  __typename\n}\n"
    //         };

    //         var jsonData = JsonConvert.SerializeObject(queryObject);
    //         var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

    //         var response = await client.PostAsync(api, contentData);
    //         var test = await response.Content.ReadAsStringAsync();
    //         Console.WriteLine(test);
    //         SearchResponse responseObject = null;
    //         if (response.IsSuccessStatusCode)
    //         {
    //             responseObject = JsonConvert.DeserializeObject<SearchResponse>(
    //                 await response.Content.ReadAsStringAsync());
                
    //             // var canonicalSearchViewModel = new List<CanonicalSearchViewModel>();
    //             var canonicalSearchViewModel = responseObject.data.cspSearch.csps.Single();

	// 			if (canonicalSearchViewModel != null)
    //             {
    //                 var firstProductId = canonicalSearchViewModel.CanonicalProductIds[0];
    //                 if (firstProductId != null)
    //                 {
	// 					// var youtubeApiClient = new YouTubeApiController();
    //                     var videos = await YouTubeApiController.GetProductVideo(canonicalSearchViewModel);

    //                     if (videos != null)
    //                     {
    //                         var videoDetailsResponse = videos as OkObjectResult;
    //                         var videoDetails = videoDetailsResponse?.Value as VideoDetailsResponse;
    //                         ViewBag.Videos = canonicalSearchViewModel.Videos;
    //                     }
    //                 }
    //                 Console.WriteLine("Search submitted successfully!");
    //                 //dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
    //                 return View("PriceResults", canonicalSearchViewModel);
    //             }
    //             else
    //             {
    //                 return View($"Error: {response.StatusCode}");
    //                 // return View("Error", "Shared");
    //             }
    //             // return responseObject.data.cspSearch.csps;
    //         }
    //             return View("Index", "Home");
    //     }
    // }

            // [HttpPost]
        	// [Route("PriceResults")]
        	// [Route("PriceResults")]
        //Display canonicalsearchviewmodel, when one is clicked then you send this method
        	 public async Task<PriceRecommendation> PostGetPriceGuide(string canonicalProductId)
            {
        		using (var client = new HttpClient())
        		{
        			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        			var baseAddress = "https://rql.reverb.com";
        			var api = "/graphql";
        			client.BaseAddress = new Uri(baseAddress);
        			client.DefaultRequestHeaders.Accept.Add(contentType);

        			PriceGuideRequestObject? queryObject = new PriceGuideRequestObject()
        			{
        				operationName = "DataServices_PriceGuideToolEstimatesContainer",
        				variables = new PriceGuideRequestVariables()
        				{
        					priceRecommendationQueries = new List<PriceRecommendationQuery>()
        					{
        						new PriceRecommendationQuery()
        						{
        							canonicalProductId = canonicalProductId,
        							conditionUuid = "df268ad1-c462-4ba6-b6db-e007e23922ea",
        							countryCode = "US"
        						}
        					}
        				},
        				query = "query DataServices_PriceGuideToolEstimatesContainer($priceRecommendationQueries: [Input_reverb_pricing_PriceRecommendationQuery]) {\n  priceRecommendations(\n    input: {priceRecommendationQueries: $priceRecommendationQueries}\n  ) {\n    priceRecommendations {\n      priceLow {\n        amountCents\n        currency\n        __typename\n      }\n      priceMiddle {\n        amountCents\n        currency\n        __typename\n      }\n      priceHigh {\n        amountCents\n        currency\n        __typename\n      }\n      priceMiddleThirtyDaysAgo {\n        amountCents\n        currency\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"
        			};

        			var jsonData = JsonConvert.SerializeObject(queryObject);
        			var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

        			var response = await client.PostAsync(api, contentData);
                    var test = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(test);
        			PriceGuideResponse responseObject = null;
        			if (response.IsSuccessStatusCode)
        			{
        				// responseObject = JsonConvert.DeserializeObject<PriceGuideResponse>(await response.Content.ReadAsStringAsync());
        				// //dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        				// Console.WriteLine("Search submitted successfully!");

        				responseObject = JsonConvert.DeserializeObject<PriceGuideResponse>(await response.Content.ReadAsStringAsync());
        				var priceRecommendationViewModel = new List<PriceRecommendation>();
        				Console.WriteLine("Search submitted successfully!");
                        
        				//dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        				return responseObject.Data.PriceRecommendations.PriceRecommendations.FirstOrDefault();
        				// priceRecommendationViewModel = responseObject.Data.PriceRecommendationsResponse.PriceRecommendation.PriceMiddle;
        				// return View(priceRecommendationViewModel);
        				// return RedirectToAction("GetYouTubeVideos", new {title = canonicalSearchViewModel.Title});
        			}
        			else
        			{
        				Console.WriteLine($"Error: {response.StatusCode}");
                        return null;
        			}

        			// return responseObject.Data.PriceRecommendations.PriceRecommendations;
        			// return View("Index", "Home");
        		}
            }
    }


