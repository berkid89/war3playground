using Microsoft.AspNetCore.Mvc;
using Piranha;
using System;
using war3playground.BusinessLogic.Models.Interfaces;
using war3playground.BusinessLogic.Services.Interfaces;
using war3playground.Models;

namespace war3playground.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly IPlayerService playerService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CmsController(IApi api, IPlayerService playerService)
        {
            _api = api;
            this.playerService = playerService;
        }

        /// <summary>
        /// Gets the blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        [Route("archive")]
        public IActionResult Archive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null)
        {
            Models.BlogArchive model;

            if (category.HasValue)
                model = _api.Archives.GetByCategoryId<Models.BlogArchive>(id, category.Value, page, year, month);
            else if (tag.HasValue)
                model = _api.Archives.GetByTagId<Models.BlogArchive>(id, tag.Value, page, year, month);
            else model = _api.Archives.GetById<Models.BlogArchive>(id, page, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        [Route("page")]
        public IActionResult Page(Guid id)
        {
            var model = _api.Pages.GetById<Models.StandardPage>(id);

            return View(model);
        }

        /// <summary>
        /// Gets the post with the given id.
        /// </summary>
        /// <param name="id">The unique post id</param>
        [Route("post")]
        public IActionResult Post(Guid id)
        {
            return toConcretePost(id, _api.Posts.GetById(id).TypeId);
        }

        private ViewResult toConcretePost(Guid id, string type)
        {
            IW3Post post = null;

            switch (type)
            {
                case "KingOfTheHillPost":
                    post = _api.Posts.GetById<KingOfTheHillPost>(id);
                    break;
                default:
                    post = _api.Posts.GetById<BlogPost>(id);
                    break;
            }

            post.Init(playerService);

            return View(type, post);
        }
    }
}
