import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { GenreService } from '../core/services/genre.service';
import { MovieCard } from '../shared/models/moviecard';
import { Genre } from '../shared/models/genre';
import { Movie } from '../shared/models/movie';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  mypageTitle = "Movie Shop SPA";
  movieCards!: MovieCard[];
  genres!: Genre[];
  movies!: Movie;
  id:number = 1;

  constructor(private movieService: MovieService, private genreService: GenreService) { }

  ngOnInit(): void {
    // ngOnInit is one of the most important life cycle hooks method in angular
    // It is recommended to use this method to call the API and initilize any data properties
    // Will be called automatically by your angular component after calling constructor
    // only when u subscribe to the observable you get the data
    // Observable<MovieCard[]>
    this.movieService.getTopRevenueMovies().subscribe(
      m => {
        this.movieCards = m;
        console.log('inside the ngOnInit method of Home Component');
        console.table(this.movieCards);
      }
    );
  //     this.movieService.getTopRatedMovies().subscribe(
  //       m => {
  //         this.movieCards = m;
  //         console.log('inside the ngOnInit method of Home Component');
  //         console.table(this.movieCards);
  //       }
  //   );
  //   this.genreService.getAllGenres().subscribe(
  //     g => {
  //       this.genres = g;
  //       console.log('inside the ngOnInit method of Home Component');
  //       console.table(this.genres);
  //     }
  // );
  // this.movieService.getMovieDetails(3).subscribe(
  //   m => {
  //     this.movies = m;
  //     console.log('inside the ngOnIt method of Home Component');
  //     console.table(this.movies);
  //   }
  // );
  // }

}}