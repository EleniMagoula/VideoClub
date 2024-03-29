﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Core.Constants;
using VideoClub.Core.Entities;
using VideoClub.Core.Enums;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Models;
using VideoClub.Infrastructure.Models.Dtos;
using VideoClub.Infrastructure.Services;
using VideoClub.Web.Areas.Movies.Models;

namespace VideoClub.Web.Areas.Movies.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieDb;
        private readonly IMoviePagingService _moviePagingDb;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;

        public MovieController(IMovieService movieDb, IMoviePagingService moviePagingDb, IMapper mapper, ILoggingService logger)
        {
            _movieDb = movieDb;
            _moviePagingDb = moviePagingDb;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult> Index(string movieGenre = "", string filterTitle = "", int page = 1, int size = 3)
        {
            var paginationDto = new PaginationDto(page, size);
            var paginationModel = await _moviePagingDb.GetPaginatedMovies(paginationDto, movieGenre, filterTitle);

            var paginationViewModel = new PaginationModel<MovieViewModel>
            {
                PageNum = paginationModel.PageNum,
                Items = new List<MovieViewModel>(),
                PageSize = paginationModel.PageSize,
                TotalItems = paginationModel.TotalItems,
                TotalPagesCount = paginationModel.TotalPagesCount
            };

            foreach (var paginationItem in paginationModel.Items)
            {
                paginationViewModel.Items.Add(new MovieViewModel
                {
                    Id = paginationItem.Id,
                    Title = paginationItem.Title,
                    Description = paginationItem.Description,
                    Genre = paginationItem.Genre,
                    DVDs = paginationItem.DVDs,
                    TestItems = new List<SelectListItem>() { new SelectListItem() { Text = "some text", Value = "some value" } }
                });
            }

            var genresList = GetMovieGenres();
            ViewBag.Genres = genresList;

            return View(paginationViewModel);
        }


        public ActionResult List(MovieViewModel model)
        {
            model.Title = "test title";
            model.Description = "test description";
            model.Genre = MovieGenre.Αστυνομική;

            return View(model);
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult RenderCreate()
        {
            var genres = GetMovieGenres();

            var model = new MovieBindingModel() { Genres = new SelectList(genres) };
            return PartialView("~/Areas/Movies/Views/Movie/_CreateForm.cshtml", model);
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            var genresList = GetMovieGenres();

            var movieForm = new MovieBindingModel
            {
                Genres = new SelectList(genresList),
                TestItems = new List<SelectListItem>() 
                { 
                    new SelectListItem() { Text = "some text", Value = "some value" }, 
                    new SelectListItem() { Text = "some other text", Value = "some other value" } 
                }
            };

            return View(movieForm);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> Create(MovieBindingModel movieForm)
        {
            var genresList = GetMovieGenres();

            if (!ModelState.IsValid)
            {
                movieForm.Genres = new SelectList(genresList);

                return View(movieForm);
            }

            //var movie = new Movie(movieForm.Title, movieForm.Description, movieForm.Genre/*, movieForm.AvailableDVDs*/);
            var movie = _mapper.Map<Movie>(movieForm);

            await _movieDb.Add(movie, movieForm.AvailableDVDs);

            _logger.Writer.Information("Admin {adminName} created a new movie {movieTitle}", User.Identity.Name, movie.Title);

            return RedirectToAction("Index", "Movie");
        }

        #region private methods
        private List<string> GetMovieGenres()
        {
            var genresList = new List<string>();
            var genresQuery = from MovieGenre genre in Enum.GetValues(typeof(MovieGenre))
                              select new
                              {
                                  Name = genre.ToString()
                              };
            genresList.AddRange(genresQuery.Select(g => g.Name).Distinct());
            return genresList;
        }
        #endregion
    }
}