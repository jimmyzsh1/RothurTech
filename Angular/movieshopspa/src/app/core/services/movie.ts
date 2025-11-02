import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MovieCardModel } from '../../shared/models/MovieCardModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Movie {
  constructor(private http: HttpClient) { }
  //https://localhost:7152/api/Movies/top-grossing

  getTopGrossingMovies() : Observable<MovieCardModel[]> {
    return this.http.get<MovieCardModel[]>('https://localhost:7152/api/Movies/top-grossing');
  }

  getMovieDetails(id: number) { // 在C#里会写 int id
  }

  getMoviesByGenre(genreId: number) {
  }
}
