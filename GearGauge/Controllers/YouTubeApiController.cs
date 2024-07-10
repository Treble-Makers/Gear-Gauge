using System.Net.Http.Headers;
using System.Text;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace GearGauge.Controllers;

//For some reason, enclosing squig bracket won't work

// [Route("api/[controller]")]
// [ApiController]
public class YouTubeApiController : Controller
{
    private readonly IConfiguration _configuration;

    public YouTubeApiController()
    {
    }

    public YouTubeApiController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public static async Task<IActionResult> GetProductVideo(
        CanonicalSearchViewModel canonicalSearchViewModel
    )
    {
        // var apiConnectionKey = _configuration["apiConnectionKey"]; // Access from user secrets
        var apiConnectionKey = "AIzaSyAnZyjSKYIcEuXGh95lc5r1CCJEyQvOz_g";
        var youtubeService = new YouTubeService(
            new BaseClientService.Initializer
            {
                ApplicationName = "GearGauge-YouTube API",
                ApiKey = apiConnectionKey
            }
        );

        using (var client = new HttpClient())
        {
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        var searchRequest = youtubeService.Search.List("snippet");
        searchRequest.Q = canonicalSearchViewModel.Title.ToString();
        searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
        searchRequest.MaxResults = 1;

        var searchResponse = await searchRequest.ExecuteAsync();

        // var videos = searchResponse
        //     .Items.Select(item => new CanonicalSearchViewModel
        //     {
        //         Title = item.Snippet.Title,
        //         Link = $"https//www.youtube.com/watch?v={item.Id.VideoId}",
        //         Thumbnail = item.Snippet.Thumbnails.Medium.Url,
        //         PublishedAt = item.Snippet.PublishedAt
        //     })
        //     .OrderByDescending(video => video.PublishedAt)
        //     .ToList();

        var videoList = searchResponse
            .Items.Select(item => new VideoDetails
            {
                Title = item.Snippet.Title,
                Link = $"https//www.youtube.com/watch?v={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAt
            })
            .OrderByDescending(video => video.PublishedAt)
            .ToList();
        var response = new VideoDetailsResponse
        {
            Videos = videoList
        };
        var controller = new YouTubeApiController();
        return controller.View("PriceResults", response);
        }



        // ViewBag.Videos = response.Videos;

        // // return Ok(new {videos = videoDetails});
        // return Ok(response);
    }
}
